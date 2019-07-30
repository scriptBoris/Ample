using Ample.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Ample.Core.Converters
{
    public class ListIsEmpty : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return true;

            if (value is ObservableCollection<Track> l1 && l1.Count > 0)
            {
                return false;
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
