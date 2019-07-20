using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ample.Core
{
    public abstract class NotifyUnit : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertychanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
