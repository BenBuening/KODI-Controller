namespace Kodi.JsonRpc
{
    public class ApiError
    {
        public class DataStack
        {
            public string Message { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }

        public class ApiErrorData
        {
            
            public string Method { get; set; }
            public DataStack Stack { get; set; }
        }

        public int Code { get; set; }
        public ApiErrorData Data { get; set; }
        public string Message { get; set; }
    }
}
