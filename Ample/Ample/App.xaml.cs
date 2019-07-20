using Ample.Core;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ample
{
    public partial class App : Application
    {
        public App()
        {
            #if DEBUG
            HotReloader.Current.Run(this, new HotReloader.Configuration
            {
                ExtensionIpAddress = System.Net.IPAddress.Parse("192.168.1.36")
            });
            #endif
            InitializeComponent();

            ApplicationInit.EntryPointAfter();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
