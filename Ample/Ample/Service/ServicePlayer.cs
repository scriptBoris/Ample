using Ample.Core;
using Ample.Models;
using Ample.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Ample.Service
{
    public class ServicePlayer
    {
        public Track SelectedTrack { get; private set; }
        public Track PlayingTrack { get; private set; }

        public void SelectTrack(Track selectedTrack)
        {
            SelectedTrack = selectedTrack;
        }

        public void Play()
        {
            PlayingTrack = SelectedTrack;

            DependencyService.Get<ICrossPlatform>().Play(PlayingTrack.AbsolutePath);
            AppMap.PlayerTab.PlayerStatus = PlayerStatus.Playing;
            AppMap.PlayerTab.OnPropertychanged(nameof(AppMap.PlayerTab.PlayingTrack));
        }

        public void Play(Track track)
        {
            PlayingTrack = track;

            DependencyService.Get<ICrossPlatform>().Play(PlayingTrack.AbsolutePath);
            AppMap.PlayerTab.PlayerStatus = PlayerStatus.Playing;
            AppMap.PlayerTab.OnPropertychanged(nameof(AppMap.PlayerTab.PlayingTrack));
        }

        public void Pause()
        {
            DependencyService.Get<ICrossPlatform>().Pause();
            AppMap.PlayerTab.PlayerStatus = PlayerStatus.Pause;

        }

        public void Resume()
        {
            DependencyService.Get<ICrossPlatform>().Resume();
            AppMap.PlayerTab.PlayerStatus = PlayerStatus.Playing;
        }
    }
}
