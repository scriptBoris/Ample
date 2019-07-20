using Ample.Core;
using Ample.Views.Tabs;
using System;
using System.Collections.Generic;
using System.Text;
using Xam.Plugin.TabView;

namespace Ample.ViewModels.Tabs
{
    public class CurrentTabModel : BaseTabModel
    {
        public override TabItem Tab { get; set; } = new TabItem("Current", new CurrentTab());
        [Obsolete("Only XAML intellisense")]
        public CurrentTabModel() { }
        public CurrentTabModel(BaseViewModel parrent) : base(parrent)
        {

        }
    }
}
