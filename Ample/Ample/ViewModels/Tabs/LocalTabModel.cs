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
        public LockCommand CommandTapItem { get; set; }
        public LockCommand CommandAddFiles { get; set; }

        public ObservableCollection<Track> Playlist { get; set; } = new ObservableCollection<Track>();

        public override TabItem Tab { get; set; } = new TabItem("Local", new LocalTab());
        [Obsolete("Only XAML intellisense")]
        public LocalTabModel() { }
        public LocalTabModel(BaseViewModel parent) : base(parent)
        {
            CommandSelected = new SimpleCommand(ActionSelectedTrack);
            CommandTapItem = new LockCommand(parent, ActionTapItem);
            CommandAddFiles = new LockCommand(parent, ActionLoadFiles);
        }

        private void ActionTapItem(object obj)
        {
            var tapTrack = (Track)obj;

            AppServices.Player.Play(tapTrack);
        }

        private void ActionSelectedTrack(object obj)
        {
            selectedTrack = (Track)obj;
            if (selectedTrack == latestTrack) return;

            selectedTrack.IsSelected = true;

            if (latestTrack != null)
                latestTrack.IsSelected = false;
            latestTrack = selectedTrack;

            AppServices.Player.SelectTrack(selectedTrack);
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
