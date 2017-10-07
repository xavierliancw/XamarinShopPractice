using System;
using System.Diagnostics;
using CoreFoundation;
using UIKit;

namespace ShopPractice.iOS.CustomRenderedUI
{
    public class WFButtonIOS : UIControl
    {
        #region Private Properties

        UILabel primaryLbl;
        UILabel secondaryLbl;
        NSLayoutConstraint primaryX;
        NSLayoutConstraint primaryY;
        NSLayoutConstraint secondaryX;
        NSLayoutConstraint secondaryY;
        UIColor pressedStateColor;
        UIColor normalBackgroundColor;
		bool fullyCancelled = true;     //Represents if a touch has finished or not

		#endregion
		#region UI Business

		public WFButtonIOS()
        {
            //Initialize just the primary text label and set it right in the center
            primaryLbl = new UILabel();
            AddSubview(primaryLbl);
            primaryLbl.ClipsToBounds = true;
            primaryLbl.TranslatesAutoresizingMaskIntoConstraints = false;
            primaryX = primaryLbl.CenterXAnchor.ConstraintEqualTo(CenterXAnchor);
            primaryX.Active = true;
            primaryY = primaryLbl.CenterYAnchor.ConstraintEqualTo(CenterYAnchor);
            primaryY.Active = true;
            primaryLbl.HeightAnchor.ConstraintEqualTo(HeightAnchor).Active = true;
            primaryLbl.WidthAnchor.ConstraintEqualTo(WidthAnchor).Active = true;

            //Add default styling
            Layer.BorderWidth = 5;
            Layer.BorderColor = UIColor.Gray.CGColor;
            BackgroundColor = UIColor.Clear;
            primaryLbl.TextAlignment = UITextAlignment.Center;
            AddTarget(OnTouchDown, UIControlEvent.TouchDown);
            AddTarget(OnTouchUp, UIControlEvent.TouchUpInside);
            AddTarget(OnDragOut, UIControlEvent.TouchDragExit);
            AddTarget(OnDragIn, UIControlEvent.TouchDragEnter);
            pressedStateColor = UIColor.LightGray;
        }

        #endregion
        #region Methods

        public void SetPrimaryText(String text)
        {
            primaryLbl.Text = text;
        }

        public void PositionPrimaryLabel(float x = 0.5f, float y = 0.5f)
        {
            PositionLbl(true, x, y);
        }

        public void SetSecondaryLabel(String text, String iconName = null, float xPosn = 0.5f, float yPosn = 0.5f)
        {
            //Initialize
            secondaryLbl = new UILabel();
            AddSubview(secondaryLbl);

            //Set properties
            if (iconName == null)
            {
                //Simply set the text most likely ▶ or ▼
                secondaryLbl.Text = text;
            }
            else
            {
                //If iconName isn't null, add attributed text with the icon baked in
                //http://jayeshkawli.ghost.io/add-image-to-uilabel-with-swift-ios/
                //Implement icon within text support later
                throw new NotImplementedException();
            }
            secondaryLbl.Font = UIFont.FromName(primaryLbl.Font.Name, primaryLbl.Font.PointSize);
            secondaryLbl.TextAlignment = UITextAlignment.Center;
            secondaryLbl.TextColor = primaryLbl.TextColor;

			//Place it
			secondaryLbl.TranslatesAutoresizingMaskIntoConstraints = false;
            PositionLbl(false, xPosn, yPosn);
        }

        public void PositionSecondaryLabel(float x = 0.5f, float y = 0.5f)
        {
            PositionLbl(false, x, y);
        }

        public void SetBorderColor(UIColor color)
        {
            Layer.BorderColor = color.CGColor;
        }

        public void SetBorderWidth(float width)
        {
            Layer.BorderWidth = width;
        }

        public void SetTextColor(UIColor color)
        {
            primaryLbl.TextColor = color;
            if (secondaryLbl != null)
            {
                secondaryLbl.TextColor = color;
            }
        }

        public void SetBackgroundColor(UIColor color)
        {
            normalBackgroundColor = color;
            BackgroundColor = color;
        }

        public void SetPressedStateColor(UIColor color)
        {
            pressedStateColor = color;
        }

        /// <summary>
        /// Changes the font on both the primary and secondary labels within the button.
        /// </summary>
        /// <param name="name">Name of the font as a string.</param>
        /// <param name="size">Point size of the font. If null, then the size isn't changed.</param>
        public void ChangeFont(String name, float? size = null)
        {
            nfloat internalSize;
            if (size != null)
            {
                internalSize = size.Value;
            }
            else
            {
                internalSize = primaryLbl.Font.PointSize;
            }

            primaryLbl.Font = UIFont.FromName(name, internalSize);
            if (secondaryLbl != null)
            {
                secondaryLbl.Font = UIFont.FromName(name, internalSize);
            }
        }

        #endregion
        #region Private Methods

        void PositionLbl(bool positioningPrimary, nfloat x, nfloat y)
        {
            UILabel lbl;
            NSLayoutConstraint newX;
            NSLayoutConstraint newY;

            //Initialize stuff
            lbl = primaryLbl;
            if (!positioningPrimary && secondaryLbl != null)
			{
				lbl = secondaryLbl;
			}
            else if (!positioningPrimary && secondaryLbl == null)
            {
                //Trying to set position of a secondary label that doesn't exist yet
                throw new Exception("WFButtonIOS.PositionLbl(): Secondary label is not initialized yet. Use SetSecondaryLabel() first.");
            }
            //Recalculate new position
            LayoutSubviews();   //Update rect values
            newX = lbl.CenterXAnchor.ConstraintEqualTo(LeftAnchor, constant: Bounds.Width * PercentTranslate(x));
            newY = lbl.CenterYAnchor.ConstraintEqualTo(TopAnchor, constant: Bounds.Height * PercentTranslate(y));

            //Wipe old values and set the new ones
            if (positioningPrimary)
            {
                primaryX.Active = false;
                primaryY.Active = false;
                primaryX = newX;
                primaryY = newY;
                primaryX.Active = true;
                primaryY.Active = true;
            }
            else if (secondaryLbl != null)
            {
                if (secondaryX != null)
                {
					secondaryX.Active = false;
                }
                if (secondaryY != null)
				{
					secondaryY.Active = false;
				}
                secondaryX = newX;
                secondaryY = newY;
                secondaryX.Active = true;
                secondaryY.Active = true;
            }
        }

        nfloat PercentTranslate(nfloat numIn)
        {
            //If it's negative, cap at 0
            if (numIn < 0)
            {
                return 0;
            }
            //If it's an actual percent, just return it (excludes 1 because why put a label at the very right edge?)
            else if (numIn >= 0 && numIn < 1)
            {
                return numIn;
            }
            //If a readable percent is given then convert it
            else if (numIn >= 1 && numIn <= 100)
            {
                return numIn / 100;
            }
            //Otherwise just cap it at 1
            else
            {
                return 1;
            }
        }

        void OnTouchDown(object sender, EventArgs e)
        {
			fullyCancelled = false;
            BackgroundColor = pressedStateColor;
        }

        void OnTouchUp(object sender, EventArgs e)
        {
			fullyCancelled = true;
            BackgroundColor = normalBackgroundColor;
        }

        void OnDragOut(object sender, EventArgs e)
        {
			fullyCancelled = true;
            BackgroundColor = normalBackgroundColor;
        }

        void OnDragIn(object sender, EventArgs e)
        {
            //Only change the color back to a pressed state if the touch is still in effect
            if (fullyCancelled)
            {
				BackgroundColor = pressedStateColor;
            }
        }

        #endregion
    }
}
