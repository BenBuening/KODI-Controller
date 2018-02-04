namespace Kodi.JsonRpc.GlobalTypes.List
{
    public class Sort
    {
        public string Order { get; set; } // "ascending"
        public bool IgnoreArticle { get; set; }
        public string Method { get; set; } // "none"
    }

    // "sort": {"order": "ascending", "method": "title", "ignorearticle": true}

}
