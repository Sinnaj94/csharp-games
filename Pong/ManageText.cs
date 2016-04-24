using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;


namespace Breakout
{
    class ManageText
    {

        private static ManageText instance;

        private Font pixelOperator;
        private Font crackmanFront;
        private Font crackmanBack;
        private Font arcadeClassic;
        private Color grey;
        private Color yellow;
        private Color green;
        private Color white;
        private Color flashingYellow;

        private ManageText() {
            crackmanFront = new Font(@"Resources/crackman front.ttf");
            crackmanBack = new Font(@"Resources/crackman back.ttf");
            arcadeClassic = new Font(@"Resources/ARCADECLASSIC.TTF");
            PixelOperator = new Font(@"Resources/PixelOperator.ttf");
            grey = new Color(255, 255, 255, 32);
            yellow = new Color(0, 100, 0, 255);
            green = new Color(0, 255, 0, 255);
            white = new Color(255, 255, 255, 255);
            flashingYellow = yellow;
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

        public Color getFlashingYellow()
        {
            flashingYellow.A -= 10;
            return flashingYellow;
        }

        public Font CrackmanFront
        {
            get
            {
                return crackmanFront;
            }

            set
            {
                crackmanFront = value;
            }
        }

        public Font CrackmanBack
        {
            get
            {
                return crackmanBack;
            }

            set
            {
                crackmanBack = value;
            }
        }

        public Color Grey
        {
            get
            {
                return grey;
            }

            set
            {
                grey = value;
            }
        }

        public Color Yellow
        {
            get
            {
                return yellow;
            }

            set
            {
                yellow = value;
            }
        }

        public Color Green
        {
            get
            {
                return green;
            }

            set
            {
                green = value;
            }
        }

        public Color White
        {
            get
            {
                return white;
            }

            set
            {
                white = value;
            }
        }

        public Font ArcadeClassic
        {
            get
            {
                return arcadeClassic;
            }

            set
            {
                arcadeClassic = value;
            }
        }

        public Font PixelOperator
        {
            get
            {
                return pixelOperator;
            }

            set
            {
                pixelOperator = value;
            }
        }
    }
}
