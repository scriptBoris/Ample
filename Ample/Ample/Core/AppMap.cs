using Ample.ViewModels;
using Ample.ViewModels.Tabs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ample.Core
{
    public static class AppMap
    {
        public static MainVm MainVm { get; set; }
        public static CurrentTabModel PlayerTab { get; set; }
        public static LocalTabModel PlaylistTab { get; set; }
        public static CloudTabModel CloudlistTab { get; set; }
    }
}
