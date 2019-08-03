using System;
using System.Collections.Generic;
using System.Text;

namespace Ample.Controls
{
    public static class AppControllers
    {
        public static ClientController Client { get; set; } = new ClientController();
        public static RouteController Route { get; set; } = new RouteController();
    }
}
