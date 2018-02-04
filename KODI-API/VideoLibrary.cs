using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KODI_API
{
    public class VideoLibrary
    {

        public void Clean()
        {
        }

        public void Export()
        {
        }

        public void GetEpisodeDetails()
        {
        }

        public void GetEpisodes()
        {
        }

        public void GetGenres()
        {
        }

        public void GetMovieDetails()
        {
        }

        public void GetMovieSetDetails()
        {
        }

        public void GetMovieSets()
        {
        }

        public void GetMovies()
        {
        }




        public class ListLimit
        {
            public int start { get; set; }
            public int end { get; set; }
        }
        public class ListSort
        {
            public string order { get; set; } // ascending, descending
            public bool ignorearticle { get; set; } // false
            public string method { get; set; } // none
        }

        /*
        {  
           "jsonrpc":"2.0",
           "method":"VideoLibrary.GetMovies",
           "params":{  
              "filter":{  
                 "field":"playcount",
                 "operator":"is",
                 "value":"0"
              },
              "limits":{  
                 "start":0,
                 "end":75
              },
              "properties":[  
                 "art",
                 "rating",
                 "thumbnail",
                 "playcount",
                 "file"
              ],
              "sort":{  
                 "order":"ascending",
                 "method":"label",
                 "ignorearticle":true
              }
           },
           "id":"libMovies"
        }

    */












        public void GetMusicVideoDetails()
        {
        }

        public void GetMusicVideos()
        {
        }

        public void GetRecentlyAddedEpisodes()
        {
        }

        public void GetRecentlyAddedMovies()
        {
        }

        public void GetRecentlyAddedMusicVideos()
        {
        }
        public void GetSeasons()
        {
        }

        public void GetTVShowDetails()
        {
        }

        public void GetTVShows()
        {
        }
        public void RemoveEpisode()
        {
        }

        public void RemoveMovie()
        {
        }

        public void RemoveMusicVideo()
        {
        }

        public void RemoveTVShow()
        {
        }

        public void Scan()
        {
        }

        public void SetEpisodeDetails()
        {
        }

        public void SetMovieDetails()
        {
        }

        public void SetMusicVideoDetails()
        {
        }

        public void SetTVShowDetails()
        {
        }

    }
}
