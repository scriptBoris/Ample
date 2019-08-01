using Ample.Core;
using Ample.ViewModels.Tabs;
using Ample.Views;
using Ample.Views.Tabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xam.Plugin.TabView;
using Xamarin.Forms;

namespace Ample.ViewModels
{
    public class MainVm : BaseViewModel
    {
        public override Page View { get; protected set; } = new MainView();
        public MainView RealView => (MainView)View;
        public MainVm()
        {
            var current = new CurrentTabModel(this);
            var local = new LocalTabModel(this);
            var cloud = new CloudTabModel(this);

            RealView.CodeBehindRetards.AddTab(current.Tab);
            RealView.CodeBehindRetards.AddTab(local.Tab);
            RealView.CodeBehindRetards.AddTab(cloud.Tab);

            // init app map
            AppMap.MainVm = this;
            AppMap.CloudlistTab = cloud;
            AppMap.PlayerTab = current;
            AppMap.PlaylistTab = local;
        }
    }
}
