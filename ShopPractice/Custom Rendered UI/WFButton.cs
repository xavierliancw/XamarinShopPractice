using System;
using Xamarin.Forms;

namespace ShopPractice.CustomRenderedUI
{
    public class WFButton : Button
    {
		//Text color
		public static readonly BindableProperty WFTextColorProperty = BindableProperty.Create(
			propertyName: "WFTextColor",
			returnType: typeof(Color),
            declaringType: typeof(WFButton),
            defaultValue: Color.LightGray
		);
		public Color WFTextColor
		{
            get { return (Color)GetValue(WFTextColorProperty); }
            set { SetValue(WFTextColorProperty, value); }
		}

		//Font size
		public static readonly BindableProperty FontSizeProp = BindableProperty.Create(
			propertyName: "WFFontSize",
			returnType: typeof(Double),
            declaringType: typeof(WFButton),
            defaultValue: Font.Default.FontSize
		);
		public Double WFFontSize
		{
            get { return (Double)GetValue(FontSizeProp); }
            set { SetValue(FontSizeProp, value); }
		}

		//Font https://blog.verslu.is/xamarin/xamarin-forms-xamarin/using-custom-fonts-on-ios-and-android-with-xamarin-forms/
		public static readonly BindableProperty FontProp = BindableProperty.Create(
			propertyName: "WFFont",
			returnType: typeof(Font),
            declaringType: typeof(WFButton),
            defaultValue: Font.Default
		);
        public Font WFFont
		{
            get { return (Font)GetValue(FontProp); }
            set { SetValue(FontProp, value); }
		}

        //Font name
        public static readonly BindableProperty WFFontNameProperty = BindableProperty.Create(
            propertyName: "WFFontName",
            returnType: typeof(String),
            declaringType: typeof(WFButton),
            defaultValue: Font.Default.FontFamily
        );
        public String WFFontName
        {
            get { return (String)GetValue(WFFontNameProperty); }
            set { SetValue(WFFontNameProperty, value);}
        }

		//Border color
		public static readonly BindableProperty BorderColorProp = BindableProperty.Create(
			propertyName: "WFBorderColor",
			returnType: typeof(Color),
			declaringType: typeof(WFButton),
            defaultValue: Color.DarkGray
		);
		public Color WFBorderColor
		{
            get { return (Color)GetValue(BorderColorProp); }
            set { SetValue(BorderColorProp, value); }
		}

		//Border width
		public static readonly BindableProperty BorderWidthProp = BindableProperty.Create(
			propertyName: "WFBorderWidth",
			returnType: typeof(float),
			declaringType: typeof(WFButton),
			defaultValue: 3f
		);
		public float WFBorderWidth
		{
            get { return (float)GetValue(BorderWidthProp); }
            set { SetValue(BorderWidthProp, value); }
		}

		//Pressed state color
		public static readonly BindableProperty PressedStateColorProperty = BindableProperty.Create(
			propertyName: "PressedStateColor",
			returnType: typeof(Color),
			declaringType: typeof(WFButton),
            defaultValue: Color.White
		);
		public Color PressedStateColor
		{
            get { return (Color)GetValue(PressedStateColorProperty); }
            set { SetValue(PressedStateColorProperty, value); }
		}

		//Button text (primary text)
		public static readonly BindableProperty ButtonTextProperty = BindableProperty.Create(
			propertyName: "WFText",
			returnType: typeof(String),
			declaringType: typeof(WFButton),
			defaultValue: "Button"
		);
		public String WFText
		{
            get { return (String)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
		}

		//Secondary text
		public static readonly BindableProperty SecondaryTextProperty = BindableProperty.Create(
			propertyName: "SecondaryText",
			returnType: typeof(String),
			declaringType: typeof(WFButton),
			defaultValue: ""
		);
		public String SecondaryText
		{
            get { return (String)GetValue(SecondaryTextProperty); }
            set { SetValue(SecondaryTextProperty, value); }
		}

		//Text x percentage placement
		public static readonly BindableProperty TextXPrcntPlcmntProperty = BindableProperty.Create(
			propertyName: "TextXPercentagePlacement",
			returnType: typeof(float),
			declaringType: typeof(WFButton),
			defaultValue: 0.5f
		);
		public float TextXPercentagePlacement
		{
            get { return (float)GetValue(TextXPrcntPlcmntProperty); }
            set { SetValue(TextXPrcntPlcmntProperty, value); }
		}

		//Text y percentage placement
		public static readonly BindableProperty TextYPrcntPlcmntProperty = BindableProperty.Create(
			propertyName: "TextTPercentagePlacement",
			returnType: typeof(float),
			declaringType: typeof(WFButton),
			defaultValue: 0.5f
		);
		public float TextYPercentagePlacement
		{
            get { return (float)GetValue(TextYPrcntPlcmntProperty); }
            set { SetValue(TextYPrcntPlcmntProperty, value); }
		}

		//Secondary text x percentage placement
		public static readonly BindableProperty SecondaryTextXPrcntPlcmntProperty = BindableProperty.Create(
			propertyName: "SecondaryTextXPercentagePlacement",
			returnType: typeof(float),
			declaringType: typeof(WFButton),
			defaultValue: 0.5f
		);
		public float SecondaryTextXPercentagePlacement
		{
			get { return (float)GetValue(SecondaryTextXPrcntPlcmntProperty); }
			set { SetValue(SecondaryTextXPrcntPlcmntProperty, value); }
		}

		//Secondary text y percentage placement
		public static readonly BindableProperty SecondaryTextYPrcntPlcmntProperty = BindableProperty.Create(
			propertyName: "SecondaryTextYPercentagePlacement",
			returnType: typeof(float),
			declaringType: typeof(WFButton),
			defaultValue: 0.5f
		);
		public float SecondaryTextYPercentagePlacement
		{
			get { return (float)GetValue(SecondaryTextYPrcntPlcmntProperty); }
			set { SetValue(SecondaryTextYPrcntPlcmntProperty, value); }
		}
	}
}
