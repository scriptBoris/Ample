using Ample.Core;
using Ample.Models;
using Ample.Models.Enums;
using Ample.Views.Tabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xam.Plugin.TabView;

namespace Ample.ViewModels.Tabs
{
    public class CloudTabModel : BaseTabModel
    {
        public CloudTabStatus Status { get; set; } = CloudTabStatus.Idle;
        public ObservableCollection<Track> CloudList { get; set; } = new ObservableCollection<Track>();
        public ObservableCollection<string> Clients { get; set; } = new ObservableCollection<string>();


        public SimpleCommand CommandSelectConnect { get; set; }
        public LockCommand CommandStartServer { get; set; }

        public LockCommand CommandConnect { get; set; }
        public LockCommand CommandAutoFind { get; set; }
        public LockCommand CommandSync { get; set; }

        public LockCommand CommandStopServer { get; set; }

        public override TabItem Tab { get; set; } = new TabItem("Cloud", new CloudTab());
        [Obsolete("Only dev XAML")]
        public CloudTabModel() { }
        public CloudTabModel(BaseViewModel parent) : base(parent)
        {
            CommandSelectConnect = new SimpleCommand(ActionSelectConnect);
            CommandStartServer = new LockCommand(parent, ActionStartServer);
        }

        private void ActionStartServer()
        {
            Status = CloudTabStatus.ServerMode;
            AppServices.Server.Start();
        }

        private void ActionSelectConnect()
        {
            Status = CloudTabStatus.ConnectDialog;
        }
    }
}
