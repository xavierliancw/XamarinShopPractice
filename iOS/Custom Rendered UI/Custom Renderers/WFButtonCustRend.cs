using System;
using System.Diagnostics;
using ShopPractice.CustomRenderedUI;
using ShopPractice.iOS.CustomRenderedUI.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WFButton), typeof(WFButtonCustRend))]
namespace ShopPractice.iOS.CustomRenderedUI.CustomRenderers
{
    public class WFButtonCustRend : ViewRenderer<WFButton, UIView>
    {
        WFButtonIOS butt;
        WFButton self;

        protected override void OnElementChanged(ElementChangedEventArgs<WFButton> e)
        {
            base.OnElementChanged(e);

            //If control is null, instantiate it
            if (Control == null)
            {
                butt = new WFButtonIOS();
                SetNativeControl(butt);
            }
            //If the renderer is still rendering any old stuff, clean it up
            if (e.OldElement != null)
            {
                DisposePropagateClick();
            }
            //Configure stuff
            if (e.NewElement != null)
            {
                self = e.NewElement;

                //Forward properties
                ApplyProperties();
            }
            if (butt != null)
            {
				butt.AddTarget(
					PropagateClick,
					UIControlEvent.TouchUpInside
				);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

			LayoutSubviews();
            ApplyProperties();
        }

        void ApplyProperties()
        {
			butt.SetPrimaryText(self.WFText);
			butt.SetTextColor(self.WFTextColor.ToUIColor());
            butt.PositionPrimaryLabel(self.TextXPercentagePlacement,
                                      self.TextYPercentagePlacement);

            if (!String.IsNullOrEmpty(self.WFFontName))
			{
				butt.ChangeFont(self.WFFontName, (float)self.WFFontSize);
			}
			butt.SetBorderColor(self.WFBorderColor.ToUIColor());
			butt.SetBorderWidth(self.WFBorderWidth);
			butt.SetPressedStateColor(self.PressedStateColor.ToUIColor());
			if (!String.IsNullOrEmpty(self.SecondaryText))
			{
				butt.SetSecondaryLabel(self.SecondaryText, null,
                                       self.SecondaryTextXPercentagePlacement,
                                       self.SecondaryTextYPercentagePlacement);
			}
            LayoutSubviews();
        }

        void PropagateClick(object sender, EventArgs e)
        {
			((IButtonController)Element)?.SendClicked();
        }

        void DisposePropagateClick()
        {
            if (butt != null)
            {
                butt.RemoveTarget(PropagateClick, UIControlEvent.TouchUpInside);
            }
        }

		protected override void Dispose(bool disposing)
		{
			if (Control != null)
            {
                DisposePropagateClick();
            }
			base.Dispose(disposing);
		}
    }
}
