using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShopPractice
{
    public partial class LandingPg : ContentPage
    {
        #region Private Settings

        private const uint ANIMATIONDURATIONLOGINSLIDEIN = 750;

        #endregion
        #region Private Properties

        private bool cntnrPosnsSetUp = false;   //Prevents SetupCntnrPosn() from getting called every time there's a layout update

        #endregion
        #region UI Business

        public LandingPg()
        {
            InitializeComponent();

            //Give the gallery view the names of the images to display
            galVw.ImageNames = new List<string> { "landing_logo.png" };
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (!cntnrPosnsSetUp)
            {
                SetupCntnrPosns();
            }
        }

        #endregion
        #region Event Handlers

        void SlideLogInCntnrIn(Object sender, EventArgs e)
        {
            Animation animation;    //Animation to shorten top image

			//Set up the animation to shorten the top image
			gridSection0_0Height.Height = new GridLength(Application.Current.MainPage.Height / 2);  //Switch from "*"(xaml) to a hard number
            animation = new Animation(
                (obj) => gridSection0_0Height.Height = new GridLength(LimitDouble(obj, 0, Double.MaxValue)),
                gridSection0_0Height.Height.Value,
                Application.Current.MainPage.Bounds.Height * 0.4
            );
            //Fire the animation
            animation.Commit(baseCntnr, "shortenGridSection0_0", 1, ANIMATIONDURATIONLOGINSLIDEIN, Easing.CubicInOut);

            //Fire and forget
            Task.Run(() => { 
				//Slide the gallery view out and fade out
                galVw.TranslateTo(-galVw.Bounds.Width, 0, ANIMATIONDURATIONLOGINSLIDEIN, Easing.CubicInOut);
                galVw.FadeTo(0, ANIMATIONDURATIONLOGINSLIDEIN);

                //Slide the log in container in and fade in
                loginCntnr.Opacity = 0;
				loginCntnr.TranslateTo(0, 0, ANIMATIONDURATIONLOGINSLIDEIN, Easing.CubicInOut);
                loginCntnr.FadeTo(1, ANIMATIONDURATIONLOGINSLIDEIN);
            });
        }

        #endregion
        #region Private Methods

        void SetupCntnrPosns()
        {
            //Have the login container start offscreen to the right
            loginCntnr.TranslationX = loginCntnr.Bounds.Width;

            cntnrPosnsSetUp = true;
        }

		double LimitDouble(double value, double minValue, double maxValue)
		{
            if (value < minValue)
            {
                return minValue;
            }
			else if (value > maxValue)
			{
				return maxValue;
			}
            else
            {
				return value;
            }
        }

        #endregion
    }
}
