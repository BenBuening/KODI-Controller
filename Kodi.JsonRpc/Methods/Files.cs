using Kodi.JsonRpc.GlobalTypes.Files.Responses;
using Kodi.JsonRpc.GlobalTypes.List;
using Kodi.JsonRpc.GlobalTypes.List.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodi.JsonRpc.Methods
{
    public class Files : MethodLibraryBase
    {

        internal Files(API api) : base(api) { }

        // todo:
        public Task<object> Download(string path)
        {
            throw new NotImplementedException("Not available via sockets");
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("path", path);

            //return RunAsync<object>("Files.Download", parameters);
        }

        // todo:
        public Task<GetDirectoryResponse> GetDirectory(string directory, List<string> properties, Sort sort, string media = "files")
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("directory", directory);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("media", media.ToString().ToLower());

            return RunAsync<GetDirectoryResponse>("Files.GetDirectory", parameters);
        }

        // todo:
        public Task<File> GetFileDetails(string file, List<string> properties, string media = "files")
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("file", file);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("media", media.ToString().ToLower());

            return RunAsync<File>("Files.GetFileDetails", parameters);
        }

        public Task<GetSourcesResponse> GetSources(string media, List<string> properties, Sort sort)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("media", media.ToString().ToLower());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("sort", sort);

            return RunAsync<GetSourcesResponse>("Files.GetSources", parameters);
        }

        public Task<PrepareDownloadResponse> PrepareDownload(string path)
        {
            throw new NotImplementedException("Not available via sockets");
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.ConditionalAdd("path", path);

            //return RunAsync<PrepareDownloadResponse>("Files.PrepareDownload", parameters);
        }

    }
}
