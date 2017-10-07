using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ShopPractice.CustomRenderedUI
{
    public class GalleryVw : View
    {
        public static readonly BindableProperty ImageNamesProperty = BindableProperty.Create(
            propertyName: "ImageNames",
            returnType: typeof(List<String>),
            declaringType: typeof(GalleryVw),
            defaultValue: new List<String>()
        );

        public List<String> ImageNames {
            get { return (List<String>)GetValue(ImageNamesProperty); }
            set { SetValue(ImageNamesProperty, value); }
        }
    }
}
