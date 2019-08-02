using Ample.Core;
using Ample.Models;
using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(Ample.UWP.Platform.CrossPlatform))]
namespace Ample.UWP.Platform
{
    public class CrossPlatform : ICrossPlatform
    {
        private static ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

        public void Echo(string msg)
        {

        }

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
                    AbsolutePath = file.Path,
                    FileExtension = Path.GetExtension(file.Path),
                });
            }

            // Application now has read/write access to all contents in the picked folder
            // (including other sub-folder contents)
            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
            return res;
        }

        public async void Play(string pathFile, object alternative = null)
        {
            var file = await StorageFile.GetFileFromPathAsync(pathFile);

            player.Load(await file.OpenStreamForReadAsync());
            player.Play();
        }

        public void Pause()
        {
            player.Pause();
        }

        public void Resume()
        {
            player.Play();
        }
    }
}
