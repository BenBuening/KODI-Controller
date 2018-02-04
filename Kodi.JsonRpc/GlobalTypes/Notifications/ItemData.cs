namespace Kodi.JsonRpc.GlobalTypes.Notifications
{
    public class ItemData
    {

        public enum ItemType
        {
            unknown,
            movie,
            episode,
            musicVideo,
            song
        }

        public int Id { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public int? Episode { get; set; }
        public int? Season { get; set; }
        public string ShowTitle { get; set; }
        public string Title { get; set; }
        public int? Track { get; set; }
        public ItemType Type { get; set; }
        public int? Year { get; set; }

    }
}
