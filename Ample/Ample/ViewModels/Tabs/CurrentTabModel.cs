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
        public LockCommand CommandNext { get; set; }
        public LockCommand CommandBack { get; set; }

        public override TabItem Tab { get; set; } = new TabItem("Current", new CurrentTab());
        [Obsolete("Only XAML intellisense")]
        public CurrentTabModel() { }
        public CurrentTabModel(BaseViewModel parent) : base(parent)
        {
            CommandPlayPause = new LockCommand(parent, ActionPlayPause);
            CommandNext = new LockCommand(parent, ActionNext);
            CommandBack = new LockCommand(parent, ActionBack);
        }

        private void ActionNext()
        {
            AppServices.Player.PlayNext();
        }

        private void ActionBack()
        {
            AppServices.Player.PlayBack();
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
