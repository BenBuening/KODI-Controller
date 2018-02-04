using KODI_Controller.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KODI_Controller
{
    public partial class MainWindow
    {

        private void SetUpVideoLibraryHandlers()
        {
            this.ViewModel.MoviesCommand = new RelayCommand(QueryMovies);
            this.ViewModel.TvShowsCommand = new RelayCommand(QueryTVShows);
        }


        private async void QueryMovies()
        {
            if (!this.ViewModel.IsLoadingMovies)
            {
                this.ViewModel.IsLoadingMovies = true;
                try
                {
                    if (_movies == null)
                    {
                        //var movieResults = this.API.GetFromFile<Kodi.JsonRpc.GlobalTypes.Video.Responses.GetMoviesResponse>(@"c:\_temp\movies.json");
                        var props = this.API.VideoLibrary.CommonMovieSummaryProperties.ToList();
                        props.Add(Kodi.JsonRpc.GlobalTypes.Video.MovieProperty.year);
                        var movieResults = await this.API.VideoLibrary.GetMovies(props);//new Limits() { Start = 0, End = 8 }
                        movieResults.Movies.Sort((x, y) => string.Compare(x.Title, y.Title));
                        //movieResults.Movies = movieResults.Movies.Take(7).ToList();

                        _movies = movieResults.Movies.Select(x => new MovieItem(x)).ToList();

                        var temp = Task.Factory.StartNew(new Action<object>(FindThumbs), new List<IHasThumb>(_movies));
                    }

                    this.ViewModel.LibraryItems = new ItemsCollection(_movies);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
                finally
                {
                    this.ViewModel.IsLoadingMovies = false;
                }
            }

            // todo: enable buttons and area... wait spinner/message?
        }

        private async void QueryTVShows()
        {
            if (!this.ViewModel.IsLoadingTvShows)
            {
                this.ViewModel.IsLoadingTvShows = true;
                try
                {
                    if (_tvShows == null)
                    {
                        var results = await this.API.VideoLibrary.GetTVShows();
                        results.TvShows.Sort((x, y) => string.Compare(x.Title, y.Title));

                        _tvShows = results.TvShows.Select(x => new TvShowItem(x) { ToggleExpanded = new RelayCommand<TvShowItem>(ExpandTvShow) }).ToList();

                        //var temp = Task.Factory.StartNew(new Action<object>(FindThumbs), new List<IHasThumb>(_tvShows));
                    }

                    this.ViewModel.LibraryItems = new ItemsCollection(_tvShows);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
                finally
                {
                    this.ViewModel.IsLoadingTvShows = false;
                }
            }

            // todo: enable buttons and area... wait spinner/message?
        }

        private async void ExpandTvShow(TvShowItem item)
        {
            if (item.IsExpanded)
            {
                // todo: if any children are expanded, collapse those first or remove all and keep track of expanded state?
                foreach (var child in item.Children)
                    if (child.IsExpanded) ExpandTvSeason(child);

                int itemIndex = this.ViewModel.LibraryItems.IndexOf(item);
                this.ViewModel.LibraryItems.RemoveRange(itemIndex + 1, item.Children.Count);
                item.IsExpanded = false;
            }
            else
            {
                await LoadTvShow(item);

                int itemIndex = this.ViewModel.LibraryItems.IndexOf(item);
                this.ViewModel.LibraryItems.InsertRange(itemIndex + 1, item.Children);
                item.IsExpanded = true;
            }
        }

        private async void ExpandTvSeason(TvSeasonItem item)
        {
            if (item.IsExpanded)
            {
                int itemIndex = this.ViewModel.LibraryItems.IndexOf(item);
                this.ViewModel.LibraryItems.RemoveRange(itemIndex + 1, item.Children.Count);
                item.IsExpanded = false;
            }
            else
            {
                await LoadTvSeason(item);

                int itemIndex = this.ViewModel.LibraryItems.IndexOf(item);
                this.ViewModel.LibraryItems.InsertRange(itemIndex + 1, item.Children);
                item.IsExpanded = true;
            }
        }

        private async Task LoadTvSeason(TvSeasonItem item)
        {
            List<TvEpisodeItem> children = item.Children;
            if (children == null)
            {
                item.IsLoading = true;

                var episodes = await this.API.VideoLibrary.GetEpisodes(item.TvSeason.TvShowId, item.TvSeason.SeasonNumber);
                episodes.Episodes.Sort((x, y) => x.EpisodeNumber - y.EpisodeNumber);
                item.Children = episodes.Episodes.Select(x => new TvEpisodeItem(x, item)).ToList();
                item.IsLoading = false;
            }
        }

        private async Task LoadTvShow(TvShowItem item)
        {
            List<TvSeasonItem> children = item.Children;
            if (children == null)
            {
                item.IsLoading = true;

                var seasons = await this.API.VideoLibrary.GetSeasons(item.TvShow.TvShowId);
                seasons.Seasons.Sort((x, y) => x.SeasonNumber - y.SeasonNumber);
                item.Children = seasons.Seasons.Select(x => new TvSeasonItem(x, item) { ToggleExpanded = new RelayCommand<TvSeasonItem>(ExpandTvSeason) }).ToList();
                item.IsLoading = false;
            }
        }

    }
}
