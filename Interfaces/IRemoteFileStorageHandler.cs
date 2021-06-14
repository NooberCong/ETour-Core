using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRemoteFileStorageHandler
    {
        public Task<string> UploadAsync(Stream fileStream, string mimeType);

        public Task DeleteAsync(string fileUri);

        public bool IsHostedFile(string fileUrl);

        public Task<string> CopyAsync(string srcUrl);
    }
}
