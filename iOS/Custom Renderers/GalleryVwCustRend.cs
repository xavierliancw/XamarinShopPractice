using System;
using System.Diagnostics;
using CoreGraphics;
using ShopPractice.CustomUIElements;
using ShopPractice.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(GalleryVw), typeof(GalleryVwCustRend))]
namespace ShopPractice.iOS.CustomRenderers
{
    public class GalleryVwCustRend : Xamarin.Forms.Platform.iOS.ViewRenderer<GalleryVw, UIView>
    {
        GalleryVwIOS galleryVw;
        protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<GalleryVw> e)
        {
            base.OnElementChanged(e);

            //If control is null, instantiate it
			if (Control == null)
            {
                galleryVw = new GalleryVwIOS();
                SetNativeControl(galleryVw);
            }
            //If the renderer is still rendering any old stuff, clean it up
            if (e.OldElement != null)
            {
            }
            //Configure stuff
            if (e.NewElement != null)
            {
                Debug.WriteLine(e.NewElement.ImageNames.Count);
			}
        }
    }
}
