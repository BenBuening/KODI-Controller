﻿using Kodi.JsonRpc.GlobalTypes.Audio.Details;
using Kodi.JsonRpc.GlobalTypes.List;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Audio.Responses
{
    public class GetRecentlyAddedAlbumsResponse
    {
        public List<Album> Albums { get; set; }
        public LimitsReturned Limits { get; set; }
    }
}
