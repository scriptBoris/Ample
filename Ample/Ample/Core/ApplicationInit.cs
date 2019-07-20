using Ample.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Ample.Core
{
    internal static class ApplicationInit
    {
        internal static void EntryPointAfter()
        {
            var nav = new NavigationPage(new MainVm().View);
            App.Current.MainPage = nav;
        }
    }
}
