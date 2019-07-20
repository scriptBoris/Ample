using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xam.Plugin.TabView;

namespace Ample.Core
{
    public abstract class BaseTabModel : NotifyUnit
    {
        public BaseViewModel ParentVm { get; private set; }

        public abstract TabItem Tab { get; set; }

        [Obsolete("Only for XAML intellisense")]
        public BaseTabModel() { }

        public BaseTabModel(BaseViewModel parrent)
        {
            Tab.Content.BindingContext = this;

            ParentVm = parrent;
            ParentVm.ChildTabs.Add(this);

            PropertyChanged += BaseTabModel_PropertyChanged;
        }

        private void BaseTabModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ParentVm.OnPropertychanged(e.PropertyName);
        }
    }
}
