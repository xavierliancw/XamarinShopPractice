using System.Diagnostics;
using System.Drawing;
using UIKit;

namespace ShopPractice.iOS
{
    public class GalleryVwIOS : UIView
    {
        public GalleryVwIOS(RectangleF frame) : base(frame)
        {
        }

        public GalleryVwIOS()
        {
            BackgroundColor = UIColor.Magenta;
			var swiper = new UISwipeGestureRecognizer((obj) =>
			{
				Debug.WriteLine("HOLY COW A SWIPE");
			});
			swiper.Direction = UISwipeGestureRecognizerDirection.Right;
			AddGestureRecognizer(swiper);
        }
    }
}
