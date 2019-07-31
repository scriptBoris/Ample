using Ample.Core;
using Ample.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(Ample.UWP.Platform.CrossPlatform))]
namespace Ample.UWP.Platform
{
    public class CrossPlatform : ICrossPlatform
    {
        public async Task<List<Track>> AddTracks()
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add(".mp3");

            var folder = await folderPicker.PickSingleFolderAsync();
            if (folder == null)
                return null;

            var res = new List<Track>();
            var files = await folder.GetFilesAsync();
            foreach (var file in files)
            {
                res.Add(new Track
                {
                    FileName = file.Name,
                    AuthorName = "unknown",
                    TrackName = "unknown",
                    AbsolutePath = file.Path,
                });
            }

            // Application now has read/write access to all contents in the picked folder
            // (including other sub-folder contents)
            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
            return res;
        }
    }
}
