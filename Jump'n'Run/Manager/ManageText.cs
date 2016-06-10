using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;


namespace JumpAndRun
{
    class ManageText
    {

        private static ManageText instance;

        private Font selectedFont;
        private Font normalFont;
        private Font textFont;
        private ManageText()
        {
            NormalFont = new Font(@"Resources/Starjhol.ttf");
            SelectedFont = new Font(@"Resources/Starjedi.ttf");
            TextFont = new Font(@"Resources/computer_pixel.ttf");
        }

        public static ManageText Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManageText();
                }
                return instance;
            }
        }

        public Font SelectedFont
        {
            get
            {
                return selectedFont;
            }

            set
            {
                selectedFont = value;
            }
        }

        public Font NormalFont
        {
            get
            {
                return normalFont;
            }

            set
            {
                normalFont = value;
            }
        }

        public Font TextFont
        {
            get
            {
                return textFont;
            }

            set
            {
                textFont = value;
            }
        }
    }
}
