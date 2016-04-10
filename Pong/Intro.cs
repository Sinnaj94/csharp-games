using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Pong
{
    class Intro : Drawable
    {

        Text introText;
        private int screenMid = 683;

        public Intro()
        {
            introText = new Text("Super Pong", ManageText.Instance.CrackmanFront, 400);
        }

        public int updateIntro()
        {
            if(introText.CharacterSize > 20)
            {
                introText.Position = new Vector2f(screenMid - introText.GetLocalBounds().Width / 2, 100);
                introText.Color = ManageText.Instance.Yellow;
                introText.CharacterSize -= 2;
                return 5;
            } else
            {
                return 0;
            }
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            try
            {
                introText.Draw(target, states);
            }
            catch (NotImplementedException)
            {
                Console.Out.Write("Intro draw failed");
            }
        }
    }
}
