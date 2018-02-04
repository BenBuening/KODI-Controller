using Kodi.JsonRpc.GlobalTypes.List.Item;
using KODI_Controller.ViewModel;
using System;
using System.Windows.Data;

namespace KODI_Controller
{
    class CurrentlyPlayingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            PlayingItem playingItem = values?[0] as PlayingItem;
            All playlistData = values?[1] as All;

            return playingItem != null && playlistData != null && playingItem.Id == playlistData.Id && playingItem.Type == playlistData.Type;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
