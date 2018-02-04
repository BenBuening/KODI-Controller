using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.List.Filter
{
    public class Rule
    {
        public object Value { get; set; }
        public string Operator { get; set; }

        /// <summary>
        /// movies or episodes or artists or albums, etc
        /// </summary>
        public List<string> Field { get; set; }
    }
}
