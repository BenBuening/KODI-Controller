using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Kodi.JsonRpc
{
    public class MessagePump
    {

        private const int bufferLength = 4096;

        private volatile bool _isShuttingDown = false;
        private bool _isStarted = false;

        private Thread _sendThread;
        private Thread _receiveThread;
        private Thread _jsonThread;

        private TcpClient _client;
        private NetworkStream _stream;
        private ConcurrentQueue<string> _sendQueue;
        private EventWaitHandle _jsonWaitHandle;
        private ConcurrentQueue<byte[]> _rawIncomingData;



        public event EventHandler<string> MessageReceived;
        public event EventHandler<Exception> ExceptionReceived;
        public event EventHandler RemoteConnectionTerminated;

        public MessagePump()
        {
            _sendQueue = new ConcurrentQueue<string>();
            _rawIncomingData = new ConcurrentQueue<byte[]>();
        }

        public void Start(string ipAddress, int port)
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);

            _client = new TcpClient();
            _client.Connect(endpoint);

            _stream = _client.GetStream();
            _jsonWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            _sendThread = new Thread(SendPump);
            _receiveThread = new Thread(ReceivePump);
            _jsonThread = new Thread(JsonPump);

            _jsonThread.Start();
            _receiveThread.Start();
            _sendThread.Start();

            _isStarted = true;
        }

        public void Stop()
        {
            if (_isStarted)
            {
                _isStarted = false;

                _isShuttingDown = true; //volatile
                _stream.Close();
                _client.Close();
                _jsonThread.Abort();
                _receiveThread.Abort();
                _sendThread.Abort();
                _receiveThread = null;
                _sendThread = null;
                _sendQueue = new ConcurrentQueue<string>();
            }
        }

        public void Send(string jsonMessage)
        {
            _sendQueue.Enqueue(jsonMessage);
        }



        // todo: handle ioexception in both send & receive. event letting main app know io problem?

        private void SendPump()
        {
            try
            {
                string message;
                while (true)
                    if (_sendQueue.TryDequeue(out message))
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(message);
                        _stream.Write(buffer, 0, buffer.Length);
                    }
                    else
                        Thread.Sleep(333);
            }
            catch (Exception) when (_isShuttingDown) { }
            catch (IOException ex)
            {
                Stop();
                this.ExceptionReceived?.Invoke(this, ex);
            }
            catch (Exception ex)
            {
                this.ExceptionReceived?.Invoke(this, ex);
            }
        }

        private void ReceivePump()
        {
            try
            {
                byte[] buffer = new byte[bufferLength];
                byte[] receivedBytes;
                int readLength;

                while (true)
                {
                    readLength = _stream.Read(buffer, 0, bufferLength);

                    if (readLength == 0)
                    {
                        this.RemoteConnectionTerminated?.Invoke(this, EventArgs.Empty);
                        break;
                    }
                    else
                    {
                        if (readLength == bufferLength)
                        {
                            receivedBytes = buffer;
                            buffer = new byte[bufferLength];
                        }
                        else
                        {
                            receivedBytes = new byte[readLength];
                            Array.Copy(buffer, receivedBytes, readLength);
                        }
                        _rawIncomingData.Enqueue(receivedBytes);
                        _jsonWaitHandle.Set();
                    }
                }
            }
            catch (Exception) when (_isShuttingDown) { }
            catch (IOException ex) 
            {
                Stop();
                this.ExceptionReceived?.Invoke(this, ex);
            }
            catch (Exception ex)
            {
                this.ExceptionReceived?.Invoke(this, ex);
            }
        }

        private void JsonPump()
        {
            byte[] incomingData = null;
            byte[] dataFromQueue;

            while (true)
            {
                _jsonWaitHandle.WaitOne();
                // got new data

                while (_rawIncomingData.TryDequeue(out dataFromQueue))
                {
                    if (incomingData == null)
                        incomingData = dataFromQueue;
                    else
                    {   // append dequeued bytes to existing data
                        byte[] temp = new byte[incomingData.Length + dataFromQueue.Length];
                        Array.Copy(incomingData, temp, incomingData.Length);
                        Array.Copy(dataFromQueue, 0, temp, incomingData.Length, dataFromQueue.Length);
                        incomingData = temp;
                    }

                    // find json message end, if possible
                    int jsonMessageEndIndex = FindJsonMessageEnd(incomingData);
                    if (jsonMessageEndIndex != -1)
                    {
                        // if message end found, copy just the bytes for the message
                        int messageLength = jsonMessageEndIndex + 1;
                        byte[] message = new byte[messageLength];
                        Array.Copy(incomingData, message, messageLength);

                        this.MessageReceived?.Invoke(this, Encoding.UTF8.GetString(message));

                        // check if there are leftover bytes
                        if (incomingData.Length == messageLength)
                            incomingData = null;
                        else
                        {
                            byte[] temp = incomingData;
                            incomingData = new byte[incomingData.Length - messageLength];
                            Array.Copy(temp, messageLength, incomingData, 0, incomingData.Length);
                        }
                    }
                }
            }
        }

        private static int FindJsonMessageEnd(byte[] bytes)
        {
            bool isOutsideQuotedField = true;
            int braceCount = 0;
            byte previousByte = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                byte b = bytes[i];

                if (b >> 7 == 0) // check for multi-byte characters => 1xxxxxxx = part of multi-byte character
                    switch (b)
                    {
                        case 34: // "
                            if (previousByte != 92) // \ -escape
                                isOutsideQuotedField = !isOutsideQuotedField;
                            break;

                        case 123: // { -opening brace
                            if (isOutsideQuotedField)
                                braceCount++;
                            break;

                        case 125: // } -closing brace
                            if (isOutsideQuotedField)
                                if (--braceCount == 0)
                                    return i;
                            break;
                    }

                previousByte = b;
            }

            return -1;


            /*
             * If you know that the data is UTF-8, then you just have to check the high bit:

                0xxxxxxx = single-byte ASCII character
                1xxxxxxx = part of multi-byte character
                Or, if you need to distinguish lead/trail bytes:

                10xxxxxx = 2nd, 3rd, or 4th byte of multi-byte character
                110xxxxx = 1st byte of 2-byte character
                1110xxxx = 1st byte of 3-byte character
                11110xxx = 1st byte of 4-byte character
             * 
             * */
        }

    }
}
