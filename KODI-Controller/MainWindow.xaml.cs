using Kodi.JsonRpc.GlobalTypes.List;
using Kodi.JsonRpc.GlobalTypes.Player;
using Kodi.JsonRpc.GlobalTypes.Video;
using Kodi.JsonRpc.GlobalTypes.Video.Details;
using KODI_Controller.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KODI_Controller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<string> _messages { get; set; } = new ObservableCollection<string>();
        private List<MovieItem> _movies;
        private List<TvShowItem> _tvShows;
        private int _videoPlayerId;
        private int _videoPlaylistId;


        private ClientConfiguration Config { get; set; }
        private Kodi.JsonRpc.API API { get; set; }
        private MainWindowViewModel ViewModel { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            _videoPlayerId = 1;

            this.API = new Kodi.JsonRpc.API();
            this.ViewModel = new MainWindowViewModel();

            SetupEventAndCommandHandlers();

            this.ViewModel.IsAppReady = false;
            this.DataContext = this.ViewModel;

            this.API.Start(new Kodi.JsonRpc.ApiConfig() { IpAddress = Config.LastConnected.IpAddress, Port = Config.LastConnected.Port });
        }

        private void SetupEventAndCommandHandlers()
        {
            SetupApiSystemHandlers();
            SetUpVideoLibraryHandlers();
            SetupPlaylistHandlers();
            SetUpPlaybackHandlers();
            SetUpConfigHandlers();

            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.API.Dispose();
        }

        private void DebugMessages_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow win = new MessageWindow(this._messages);
            win.Owner = this;
            win.Show();
        }





        // todo: hash thumbnail urls & files & cache in database? combine as single file & cache in memory?



        //var temp = await this.API.Files.PrepareDownload(videoItems[0].ThumbnailPath);
        //Console.WriteLine(temp.Details);

        //using (var client = new HttpClient())
        //{
        //    //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, string.Format("http://{0}/jsonrpc", _config.LastConnected.IpAddress));
        //    request.Content = new StringContent("{\"id\":\"2\",\"jsonrpc\":\"2.0\",\"method\":\"Files.Download\", \"parameters\": { \"path\": \"%%%\" }}".Replace("%%%", videoItems[0].TvShow.Thumbnail),
        //                                        Encoding.UTF8,
        //                                        "application/json");//CONTENT-TYPE header
        //    await client.SendAsync(request)
        //      .ContinueWith(async responseTask =>
        //      {
        //          Console.WriteLine("Response: {0}", responseTask.Result);
        //          Console.WriteLine(responseTask.Result.Content);
        //          var g = responseTask.Result.Content;
        //          var bytes = await g.ReadAsStringAsync();
        //          Console.WriteLine(bytes);
        //      });
        //}


        //var temp = await this.API.Files.Download(results.TvShows[0].Thumbnail);
        //Console.WriteLine(temp.ToString());

        //using (System.Net.WebClient cl = new System.Net.WebClient())
        //{
        //    var url = string.Format("http://{0}:8080/vfs/{1}", _config.LastConnected.IpAddress, results.TvShows[0].Thumbnail);
        //    var bytes = cl.DownloadData(url);
        //    bytes = ResizeImage(bytes, thumbWidth, thumbHeight);
        //    Console.WriteLine(bytes.Length);
        //    //System.IO.File.WriteAllBytes(filePath, bytes);
        //}

        private void FindThumbs(object data)
        {
            int thumbWidth = 54;
            int thumbHeight = 72;
            string imageProtocol = "image://";
            string thumbsPath = System.IO.Path.Combine(Config.ImageCachePath, "Thumbs");
            System.IO.Directory.CreateDirectory(thumbsPath);

            try
            {
                foreach (var item in (List<IHasThumb>)data)
                    try
                    {
                        string url = item.GetRawThumbUrl();
                        if (!string.IsNullOrEmpty(url))
                        {
                            if (url.StartsWith(imageProtocol))
                                url = System.Net.WebUtility.UrlDecode(url.Substring(imageProtocol.Length));

                            Uri uri;
                            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
                            {
                                string fileName = System.IO.Path.GetFileName(CleanPath(uri.AbsolutePath));
                                string filePath = System.IO.Path.Combine(thumbsPath, fileName);

                                if (!System.IO.File.Exists(filePath))
                                {
                                    using (System.Net.WebClient cl = new System.Net.WebClient())
                                    {
                                        var bytes = cl.DownloadData(uri);
                                        bytes = ResizeImage(bytes, thumbWidth, thumbHeight);
                                        System.IO.File.WriteAllBytes(filePath, bytes);
                                    }
                                }
                        
                                this.Dispatcher.Invoke(new Action<IHasThumb, string>(SetThumbPath), item, filePath);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SetThumbPath(IHasThumb item, string thumbPath)
        {
            item.ThumbnailPath = thumbPath;
        }

        private byte[] ResizeImage(byte[] imagebytes, int targetWidth, int targetHeight)
        {
            var image = new System.Drawing.Bitmap(new System.IO.MemoryStream(imagebytes));
            var resizedImage = new System.Drawing.Bitmap(targetWidth, targetHeight);
            using (var gr = System.Drawing.Graphics.FromImage(resizedImage))
            {
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gr.DrawImage(image, new System.Drawing.Rectangle(0, 0, targetWidth, targetHeight));
            }
            var output = new System.IO.MemoryStream(100000);
            resizedImage.Save(output, System.Drawing.Imaging.ImageFormat.Bmp);
            var arr = output.ToArray();
            return arr;
        }

        private string CleanPath(string uriAbsolutePath)
        {
            if (uriAbsolutePath.EndsWith("/"))
                uriAbsolutePath = uriAbsolutePath.Substring(0, uriAbsolutePath.Length - 1);
            return uriAbsolutePath;
        }





        /*
            temp
        */
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await this.API.Player.Open(_videoPlaylistId, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await this.API.Playlist.Clear(_videoPlaylistId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void HandleException(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        private async void Library_Refresh(object sender, RoutedEventArgs e)
        {
            try
            {
                await this.API.VideoLibrary.Scan(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void Library_Clean(object sender, RoutedEventArgs e)
        {
            try
            {
                await this.API.VideoLibrary.Clean();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
