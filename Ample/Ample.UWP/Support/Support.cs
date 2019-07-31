using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Ample.UWP.Support
{
    public static class Support
    {
        public static async Task<StorageFile> GetFileAsync(StorageFolder folder, string filename)
        {
            StorageFile file = null;
            if (folder != null)
            {
                file = await folder.GetFileAsync(filename);
            }
            return file;
        }
    }
}
