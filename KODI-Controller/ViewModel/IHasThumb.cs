namespace KODI_Controller.ViewModel
{
    interface IHasThumb
    {
        string GetRawThumbUrl();
        string ThumbnailPath { get; set; }
    }
}
