using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ample.Views.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentTab : ContentView
    {
        public CurrentTab()
        {
            InitializeComponent();
        }
    }
}