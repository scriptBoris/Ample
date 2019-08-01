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
using System.IO;
using Android.Provider;
using Android.Net;

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
        private Android.Net.Uri mCurrentDirectoryUri;

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
                    //string path = GetPathFromUri.GetActualPathFromFile(uri, this);
                    //var res = new List<Track>();

                    ////
                    ////var folder = Android.OS.Environment.ExternalStorageDirectory + Java.IO.File.Separator + "yourfoldername";
                    ////if (!Directory.Exists(folder))
                    ////    Directory.CreateDirectory(folder);

                    ////var filesList = Directory.GetFiles(folder);
                    ////foreach (var file in filesList)
                    ////{
                    ////    var filename = Path.GetFileName(file);
                    ////}
                    ////

                    //var files = Directory.EnumerateFiles(path);
                    //foreach (var file in files)
                    //{
                    //    string fileName = Path.GetFileName(file);
                    //    string extension = Path.GetExtension(file);
                    //    res.Add(new Track
                    //    {
                    //        AbsolutePath = file,
                    //        FileName = fileName,
                    //        FileExtension = extension,
                    //    });
                    //}

                    //PickFolderTaskCompletionSource.SetResult(res);
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
            var docUri = DocumentsContract.BuildDocumentUriUsingTree(uri, DocumentsContract.GetTreeDocumentId(uri));
            var childrenUri = DocumentsContract.BuildChildDocumentsUriUsingTree(uri, DocumentsContract.GetTreeDocumentId(uri));

            var docCursor = contentResolver.Query(docUri, new[] {
                DocumentsContract.Document.ColumnDisplayName,
                DocumentsContract.Document.ColumnMimeType
            }, null, null, null);
            try
            {
                while (docCursor.MoveToNext())
                {
                    string name = docCursor.GetString(0);
                    mCurrentDirectoryUri = uri;
                }
            }
            catch (Exception)
            {
                return null;
            }

            var childCursor = contentResolver.Query(childrenUri, new[] {
                DocumentsContract.Document.ColumnDisplayName,
                DocumentsContract.Document.ColumnMimeType,
                DocumentsContract.Document.ColumnDocumentId,
            }, null, null, null);

            try
            {
                var findTracks = new List<Track>();
                while (childCursor.MoveToNext())
                {
                    string file = childCursor.GetString(0);
                    string mimeType = childCursor.GetString(1);
                    string id = childCursor.GetString(2);

                    if (mimeType.Contains("audio"))
                    {
                        var track = new Track
                        {
                            FileName = file,
                            AbsolutePath = GetRealPathFromURI(id)
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

        private string GetRealPathFromURI(string docId)
        {
            var cursor = ContentResolver.Query(MediaStore.Audio.Media.ExternalContentUri, null, MediaStore.Audio.Media.DATA + " like ? ", new[] { docId }, null);
            cursor.MoveToFirst();
            string path = cursor.GetString(cursor.GetColumnIndex(Android.Provider.MediaStore.Audio.AudioColumns.Data));
            cursor.Close();

            return path;
        }
    }
}