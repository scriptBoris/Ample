using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ample.Core;
using Ample.Models;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.FilePicker;
using Xamarin.Forms;

[assembly: Dependency(typeof(Ample.Droid.Platform.CrossPlatform))]
namespace Ample.Droid.Platform
{
    public class CrossPlatform : ICrossPlatform
    {
        public static readonly int PickFolderId = 1337;

        public void Echo(string msg)
        {
            Toast.MakeText(Android.App.Application.Context, msg, ToastLength.Long).Show();
        }

        public Task<List<Track>> AddTracks()
        {
            //var pick = await CrossFilePicker.Current.PickFile(null);

            //if (pick != null)
            //{
            //    var res = new List<Track>();
            //    pick.
            //}

            Intent intent = new Intent(Intent.ActionOpenDocumentTree);
            MainActivity.Instance.StartActivityForResult(intent, PickFolderId);

            MainActivity.Instance.PickFolderTaskCompletionSource = new TaskCompletionSource<List<Track>>();
            return MainActivity.Instance.PickFolderTaskCompletionSource.Task;
        }

        public void Pause()
        {
        }

        public void Play(string pathFile)
        {
        }

        public void Resume()
        {
        }
    }
}