using Ample.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ample.Core
{
    public interface ICrossPlatform
    {
        void Echo(string msg);

        void Play(string pathFile);

        void Resume();

        void Pause();

        Task<List<Track>> AddTracks();
    }
}
