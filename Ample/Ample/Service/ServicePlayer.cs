using Ample.Core;
using Ample.Models;
using Ample.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (SelectedTrack != null)
            {
                PlayingTrack = SelectedTrack;
            }
            else if (AppMap.PlaylistTab.Playlist.Count > 0)
            {
                PlayingTrack = AppMap.PlaylistTab.Playlist.FirstOrDefault();
            }

            if (PlayingTrack != null)
            {
                DependencyService.Get<ICrossPlatform>().Play(PlayingTrack.AbsolutePath);
                AppMap.PlayerTab.PlayerStatus = PlayerStatus.Playing;
                AppMap.PlayerTab.OnPropertychanged(nameof(AppMap.PlayerTab.PlayingTrack));
            }
        }

        public void Play(Track track)
        {
            PlayingTrack = track;

            DependencyService.Get<ICrossPlatform>().Play(PlayingTrack.AbsolutePath);
            AppMap.PlayerTab.PlayerStatus = PlayerStatus.Playing;
            AppMap.PlayerTab.OnPropertychanged(nameof(AppMap.PlayerTab.PlayingTrack));
        }

        public void PlayNext()
        {
            if (PlayingTrack != null)
            {
                int i = AppMap.PlaylistTab.Playlist.IndexOf(PlayingTrack);
                int count = AppMap.PlaylistTab.Playlist.Count;
                if (i+2 <= count)
                {
                    var next = AppMap.PlaylistTab.Playlist[i + 1];
                    Play(next);
                }
            }
        }

        public void PlayBack()
        {
            if (PlayingTrack != null)
            {
                int i = AppMap.PlaylistTab.Playlist.IndexOf(PlayingTrack);
                int count = AppMap.PlaylistTab.Playlist.Count;
                if (i-1 >= 0)
                {
                    var back = AppMap.PlaylistTab.Playlist[i - 1];
                    Play(back);
                }
            }
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
