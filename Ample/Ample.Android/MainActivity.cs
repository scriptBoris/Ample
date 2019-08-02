using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using Android.Content;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ample.Models;
using Ample.Droid.Platform;
using Ample.Droid.Support;
using Android.Provider;
using Android.Net;
using Android.Database;

namespace Ample.Droid
{
    [Activity(
        Label = "Ample",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize)
        ]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        public TaskCompletionSource<List<Track>> PickFolderTaskCompletionSource { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //====== CUSTOM ========
            // TabViewInit
            CarouselViewRenderer.Init();
            Instance = this;

            // Run PCL
            LoadApplication(new App());
        }

        /// <summary>
        /// For permissions
        /// </summary>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            if (requestCode == CrossPlatform.PickFolderId)
            {
                if (resultCode == Result.Ok && intent != null)
                {
                    var uri = intent.Data;
                    PickFolderTaskCompletionSource.SetResult(UpdateDirectoryEntries(uri));
                }
                else
                {
                    PickFolderTaskCompletionSource.SetResult(null);
                }
            }
        }

        private List<Track> UpdateDirectoryEntries(Android.Net.Uri uri)
        {
            var contentResolver = Instance.ContentResolver;
            var childrenUri = DocumentsContract.BuildChildDocumentsUriUsingTree(uri, DocumentsContract.GetTreeDocumentId(uri));

            string[] projection = {
                MediaStore.Audio.AudioColumns.Data,
                MediaStore.Audio.AudioColumns.Title,
                MediaStore.Audio.AudioColumns.Album,
                MediaStore.Audio.ArtistColumns.Artist,
                DocumentsContract.Document.ColumnDisplayName,
                DocumentsContract.Document.ColumnMimeType,
                DocumentsContract.Document.ColumnDocumentId,
            };

            var childCursor = contentResolver.Query(childrenUri, projection, MediaStore.Audio.AudioColumns.Data + " like ? ", new String[] { "%utm%" }, null);

            try
            {
                var findTracks = new List<Track>();
                while (childCursor.MoveToNext())
                {
                    string path = childCursor.GetString(0);
                    string title = childCursor.GetString(1);
                    string album = childCursor.GetString(2);
                    string artist = childCursor.GetString(3);
                    string fileName = childCursor.GetString(4);
                    string mime = childCursor.GetString(5);
                    string docId = childCursor.GetString(6);
                    var childUri = DocumentsContract.BuildChildDocumentsUriUsingTree(uri, docId);
                    //var childUri = DocumentsContract.Bui(childrenUri, docId);

                    if (mime.Contains("audio"))
                    {
                        var file = new Java.IO.File(childUri.Path);
                        var split = file.Path.Split(":");
                        path = split[1];

                        var track = new Track
                        {
                            FileName = fileName,
                            AlternativePathObject = childUri,//path + Java.IO.File.Separator + fileName,
                        };
                        findTracks.Add(track);
                    }
                }

                return findTracks;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}