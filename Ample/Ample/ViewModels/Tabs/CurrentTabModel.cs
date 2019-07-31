using Ample.Core;
using Ample.Models;
using Ample.Models.Enums;
using Ample.Views.Tabs;
using System;
using System.Collections.Generic;
using System.Text;
using Xam.Plugin.TabView;
using Xamarin.Forms;

namespace Ample.ViewModels.Tabs
{
    public class CurrentTabModel : BaseTabModel
    {
        public Track PlayingTrack => AppServices.Player.PlayingTrack;
        public PlayerStatus PlayerStatus { get; set; } = PlayerStatus.Started;

        public LockCommand CommandPlayPause { get; set; }

        public override TabItem Tab { get; set; } = new TabItem("Current", new CurrentTab());
        [Obsolete("Only XAML intellisense")]
        public CurrentTabModel() { }
        public CurrentTabModel(BaseViewModel parrent) : base(parrent)
        {
            CommandPlayPause = new LockCommand(parrent, ActionPlayPause);
        }

        private void ActionPlayPause()
        {
            if (PlayerStatus == PlayerStatus.Started)
            {
                AppServices.Player.Play();
            }
            else if (PlayerStatus == PlayerStatus.Pause)
            {
                AppServices.Player.Resume();
            }
            else if (PlayerStatus == PlayerStatus.Playing)
            {
                AppServices.Player.Pause();
            }
        }
    }
}
