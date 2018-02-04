using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Threading;
using System.Net.Http;
using Kodi.JsonRpc.GlobalTypes;
using Kodi.JsonRpc.GlobalTypes.Video;
using Kodi.JsonRpc.GlobalTypes.Video.Responses;
using Kodi.JsonRpc.GlobalTypes.List.Item;
using Newtonsoft.Json.Linq;
using Kodi.JsonRpc.GlobalTypes.List;

namespace Testing
{


    // http://kodi.wiki/view/JSON-RPC_API/v6



//    public class Player
//    {

//        public void OpenFile(string filePath)
//        {

//        }

//        public class OpenOptions
//        {
//            public ResumeOption Resume { get; set; }
//            public object Repeat { get; set; }
//            /*
//            "cycle" will cycle through the available repeat functionalities
//"off" turns repeating off
//"one" only repeats the current item
//"all" repeats all items
//            */
//            public bool? Shuffled { get; set; } // true, false

//        }

//        public struct ResumeOption
//        {
//            public bool Resume { get; set; }
//            public decimal Percentage { get; set; }
//            public TimeSpan Time { get; set; }
//            public override string ToString()
//            {
//                if (this.Resume)
//                {
//                    if (this.Time != TimeSpan.Zero)
//                        return this.Time.ToString();
//                    else if (this.Percentage != decimal.Zero)
//                        return this.Percentage.ToString();
//                }
//                return "false";
//            }
//        }
//    }




    class Program
    {
        static void Main(string[] args)
        {

            string json = "{ \"jsonrpc\": \"2.0\", \"method\": \"JSONRPC.Introspect\", \"id\": 1 }";
            //string json = "{ \"jsonrpc\": \"2.0\", \"method\": \"JSONRPC.Introspect\", \"params\": { \"filter\": { \"id\": \"AudioLibrary.GetAlbums\", \"type\": \"method\" } }, \"id\": 1 }";

            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>()
                    {
                       { "request", json }
                    };

                var content = new FormUrlEncodedContent(values);

                var postTask = client.PostAsync("http://192.168.1.148/jsonrpc", content);
                postTask.Wait();
                var resultTask = postTask.Result.Content.ReadAsStringAsync();
                resultTask.Wait();
                var result = resultTask.Result;
                Console.WriteLine(result);

            }


            //Kodi.JsonRpc.API api = new Kodi.JsonRpc.API();
            //api.Start(new Kodi.JsonRpc.ApiConfig() { IpAddress = "192.168.1.148", Port = 9090 });

            //var ep = api.VideoLibrary.GetEpisodes(new Limits() { Start = 0, End = 20 }, Enum.GetValues(typeof(EpisodeProperty)).Cast<EpisodeProperty>().ToList());
            //ep.Wait();
            //var episodes = ep.Result;

            //var s = api.VideoLibrary.GetSeasons(1, Enum.GetValues(typeof(SeasonProperty)).Cast<SeasonProperty>().ToList());
            //s.Wait();
            //var seasons = s.Result;

            //var tv = api.VideoLibrary.GetTVShows(Enum.GetValues(typeof(TvShowProperty)).Cast<TvShowProperty>().ToList());
            //tv.Wait();
            //var shows = tv.Result;

            //api.Dispose();



            //bool sleepWait = true;

            //string cmx = "{\"id\":\"1\",\"jsonrpc\":\"2.0\",\"method\":\"VideoLibrary.GetMovies\",\"params\":{\"limits\":{\"End\":2147483647,\"Start\":0},\"properties\":[\"art\",\"rating\",\"thumbnail\",\"playcount\",\"file\"]}}";
            //string cmd = "{\"jsonrpc\": \"2.0\", \"method\": \"VideoLibrary.GetMovies\", \"params\": { \"limits\": { \"start\" : 0, \"end\": 99999 }, \"properties\" : [\"art\", \"rating\", \"thumbnail\", \"playcount\", \"file\"] }, \"id\": \"libMovies\"}";
            //string cmd = "{ \"jsonrpc\": \"2.0\", \"method\" : \"Player.Open\", \"params\": {\"item\":{\"file\":\"smb://D4SERVE/video/tv/True Blood/Season 01 (1080p)/S01E01 - Strange Love.mkv\"}}, \"id\": 1 }";


            //List<MovieProperty> properties = new List<MovieProperty>() { MovieProperty.Art, MovieProperty.Rating, MovieProperty.Thumbnail, MovieProperty.PlayCount, MovieProperty.File };
            //Kodi.JsonRpc.GlobalTypes.List.Limits limits = new Kodi.JsonRpc.GlobalTypes.List.Limits() { Start = 0, End = int.MaxValue };
            //Action<Kodi.JsonRpc.GlobalTypes.Video.Responses.GetMoviesResponse> callback = delegate (Kodi.JsonRpc.GlobalTypes.Video.Responses.GetMoviesResponse response)
            //{
            //    Console.WriteLine(response.Movies.Count);

            //    var t = response.Movies.Where(x => x.Label.IndexOf("kick-ass", StringComparison.OrdinalIgnoreCase) > -1).First();
            //    api.Player.Open(new Kodi.JsonRpc.Methods.Player.OpenItem() { MovieId = t.Movieid }, null);

            //    //sleepWait = false;
            //};

            //api.VideoLibrary.GetMovies(callback, properties, null, null, null);
            //api.Player.Stop(1);

            //GetMoviesResponse r = await api.VideoLibrary.GetMovies(properties, null, null, null);



            //string cmd = "{\"jsonrpc\": \"2.0\", \"method\": \"VideoLibrary.GetMovies\", \"params\": { \"limits\": { \"start\" : 0, \"end\": 10 }, \"properties\" : [\"art\", \"rating\", \"thumbnail\", \"playcount\", \"file\"] }, \"id\": \"libMovies\"}";
            ////string cmd = "{\"jsonrpc\": \"2.0\", \"method\": \"VideoLibrary.GetMovies\", \"params\": { \"filter\": {\"field\": \"playcount\", \"operator\": \" is\", \"value\": \"0\"}, \"limits\": { \"start\" : 0, \"end\": 75 }, \"properties\" : [\"art\", \"rating\", \"thumbnail\", \"playcount\", \"file\"], \"sort\": { \"order\": \"ascending\", \"method\": \"label\", \"ignorearticle\": true } }, \"id\": \"libMovies\"}";
            ////string cmd = "{ \"jsonrpc\": \"2.0\", \"method\" : \"Player.Open\", \"params\": {\"item\":{\"file\":\"smb://D4SERVE/video/tv/True Blood/Season 01 (1080p)/S01E01 - Strange Love.mkv\"}}, \"id\": 1 }";
            ////string cmd = "{ \"jsonrpc\": \"2.0\", \"method\" : \"Player.GetItem\", \"params\": { \"properties\": [\"title\", \"album\", \"artist\", \"duration\", \"thumbnail\", \"file\", \"fanart\", \"streamdetails\"], \"playerid\": 1 }, \"id\": \"video\"}, \"id\": 1 }";
            ////string cmd = "{ \"jsonrpc\": \"2.0\", \"method\" : \"Player.GetActivePlayers\", \"params\": [], \"id\": 1 }";




            //while (sleepWait)
            //{
            //    Thread.Sleep(2000);
            //    //Console.WriteLine("~sleep~");
            //}

            //api.Dispose();
            //messenger.Stop();

            //Console.ReadKey();
        }


        //var r = WebRequest.Create("http://192.168.1.148/jsonrpc?request=" + cmd);
        //r.Method = "GET";
        //r.ContentType = "application/json";
        //var rx = r.GetResponse();
        //var st = rx.GetResponseStream();
        //var rd = new StreamReader(st);
        //string gg = rd.ReadToEnd();
        //rd.Close();
        //st.Close();
        //rx.Close();


    }
}