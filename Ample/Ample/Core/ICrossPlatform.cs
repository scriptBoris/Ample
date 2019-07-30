using Ample.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ample.Core
{
    public interface ICrossPlatform
    {
        Task<List<Track>> AddTracks();
    }
}
