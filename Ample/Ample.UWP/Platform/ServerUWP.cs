using Ample.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(Ample.UWP.Platform.ServerUWP))]
namespace Ample.UWP.Platform
{
    public class ServerUWP : IServerUWP
    {
        public void StartServer()
        {
            throw new NotImplementedException();
        }

        public void StopServer()
        {
            throw new NotImplementedException();
        }
    }
}
