using System.Diagnostics;
using System.Drawing;
using UIKit;

namespace ShopPractice.iOS.CustomRenderedUI
{
    public class GalleryVwIOS : UIView
    {
        public GalleryVwIOS(RectangleF frame) : base(frame)
        {
        }

        public GalleryVwIOS()
        {
            BackgroundColor = UIColor.Yellow;
			var swiper = new UISwipeGestureRecognizer((obj) =>
			{
				Debug.WriteLine("HOLY COW A SWIPE");
			});
			swiper.Direction = UISwipeGestureRecognizerDirection.Right;
			AddGestureRecognizer(swiper);
        }
    }
}
