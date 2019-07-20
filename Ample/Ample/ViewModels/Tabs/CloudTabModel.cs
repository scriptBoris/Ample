using Ample.Core;
using Ample.Views.Tabs;
using System;
using System.Collections.Generic;
using System.Text;
using Xam.Plugin.TabView;

namespace Ample.ViewModels.Tabs
{
    public class CloudTabModel : BaseTabModel
    {
        public override TabItem Tab { get; set; } = new TabItem("Cloud", new CloudTab());

        public CloudTabModel(BaseViewModel parrent) : base(parrent)
        {

        }
    }
}
