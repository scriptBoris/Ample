using Ample.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ample.Core
{
    public static class AppServices
    {
        public static ServicePlayer Player { get; set; } = new ServicePlayer();
        public static ServiceServer Server { get; set; } = new ServiceServer();
    }
}
