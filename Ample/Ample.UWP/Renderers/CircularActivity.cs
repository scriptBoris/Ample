using Ample.Core.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(IndicatorRingExtended), typeof(Ample.UWP.Renderers.MyProgressRingRenderer))]
namespace Ample.UWP.Renderers
{
    public class MyProgressRingRenderer : ViewRenderer<IndicatorRingExtended, ProgressRing>
    {
        private ProgressRing ring;

        protected override void OnElementChanged(ElementChangedEventArgs<IndicatorRingExtended> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                ring = new ProgressRing();
                ring.IsActive = true;
                ring.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ring.IsEnabled = true;
                SetNativeControl(ring);
            }
        }
    }
}
