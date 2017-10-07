using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShopPractice
{
    public partial class LandingPg : ContentPage
    {
        #region Private Settings

        private const uint ANIMATIONDURATIONLOGINSLIDEIN = 1000;

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

                //Customize all of the custom rendered UI elements' UI elements
                startBt.SecondaryTextXPercentagePlacement = 0.75f;
                startBt.SecondaryText = "▶";
            }
        }

        #endregion
        #region Event Handlers

        void SlideLogInCntnrIn(Object sender, EventArgs e)
        {
            Animation animation;    //Animation to shorten top image

            //First, lock and unlock values to facilitate intended expanding animation behavior
			gridSection0_0Height.Height = new GridLength(Application.Current.MainPage.Height / 2);  //Switch from "*"(xaml) to a hard number
            heightLock_r1c0.Height = new GridLength(Application.Current.MainPage.Height / 2);
            heightLock_r0c0.Height = new GridLength(1, GridUnitType.Star);
            logInHeightLock.Height = new GridLength(Application.Current.MainPage.Bounds.Height * 0.6);

            //Set up the expanding animation
            animation = new Animation(
                (obj) => gridSection0_0Height.Height = new GridLength(LimitDouble(obj, 0, Double.MaxValue)),
                gridSection0_0Height.Height.Value,
                Application.Current.MainPage.Bounds.Height * 0.4
            );
            //Fire the animation
            animation.Commit(baseCntnr, "shortenGridSection0_0", 1, ANIMATIONDURATIONLOGINSLIDEIN, Easing.CubicOut);

            //Fire and forget the sliding animations
            Task.Run(() =>
            {
                //Slide the gallery view out and fade out
                galCntnr.TranslateTo(-galCntnr.Bounds.Width, 0, ANIMATIONDURATIONLOGINSLIDEIN, Easing.CubicInOut);
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
