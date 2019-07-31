using Ample.Core;
using Ample.Models;
using Ample.Views.Tabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xam.Plugin.TabView;
using Xamarin.Forms;

namespace Ample.ViewModels.Tabs
{
    public class LocalTabModel : BaseTabModel
    {
        private Track latestTrack;
        private Track selectedTrack;

        public SimpleCommand CommandSelected { get; set; }
        public LockCommand CommandAddFiles { get; set; }

        public ObservableCollection<Track> Playlist { get; set; }

        public override TabItem Tab { get; set; } = new TabItem("Local", new LocalTab());
        [Obsolete("Only XAML intellisense")]
        public LocalTabModel() { }
        public LocalTabModel(BaseViewModel parrent) : base(parrent)
        {
            CommandSelected = new SimpleCommand(ActionSelectedTrack);
            CommandAddFiles = new LockCommand(parrent, ActionLoadFiles);

            //TODO Test
            Playlist = new ObservableCollection<Track>
            {
                new Track
                {
                    AbsolutePath = "D:/Media/Music/AzazinKreet - Rating.mp3",
                    AuthorName = "Azazin Kreet",
                    FileName = "AzazinKreet - Rating.mp3",
                    TrackName = "Rating",
                    Length = 3.39m,
                    PositionId = 1,
                },
                new Track
                {
                    AbsolutePath = "D:/Media/Music/AzazinKreet - Rating.mp3",
                    AuthorName = "Azazin Kreet",
                    FileName = "AzazinKreet - Rating.mp3",
                    TrackName = "Rating",
                    Length = 3.39m,
                    PositionId = 2,
                },
                new Track
                {
                    AbsolutePath = "D:/Media/Music/AzazinKreet - Rating.mp3",
                    AuthorName = "Azazin Kreet",
                    FileName = "AzazinKreet - Rating.mp3",
                    TrackName = "Rating",
                    Length = 3.39m,
                    PositionId = 3,
                },
            };
        }

        private void ActionSelectedTrack(object obj)
        {
            selectedTrack = (Track)obj;
            if (selectedTrack == latestTrack) return;

            selectedTrack.IsSelected = true;

            if (latestTrack != null)
                latestTrack.IsSelected = false;
            latestTrack = selectedTrack;
        }

        private async Task ActionLoadFiles()
        {
            var res = await DependencyService.Get<ICrossPlatform>().AddTracks();
            if (res == null)
                return;

            ParentVm.IsLoading = true;
            foreach (var item in res)
            {
                Playlist.Add(item);
                item.PositionId = Playlist.Count;
            }
            ParentVm.IsLoading = false;
        }
    }
}
