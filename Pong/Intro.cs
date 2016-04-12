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
        Text introTextBack;
        Text developers;
        private int screenMid = 683;

        public Intro()
        {
            introText = new Text("Super Pong", ManageText.Instance.CrackmanFront, 400);
            introTextBack = new Text("Super Pong", ManageText.Instance.CrackmanFront, 410);
            developers = new Text("Developed_by_Niklas_Hinte_and_Jannis_Jahr_HS_Bremen", ManageText.Instance.ArcadeClassic, 30);

            introText.Color = ManageText.Instance.Yellow;
            introTextBack.Color = ManageText.Instance.Grey;

        }

        public int updateIntro()
        {
            if(introText.CharacterSize > 20)
            {
                introText.Position = new Vector2f(screenMid - introText.GetLocalBounds().Width / 2, 100);
                developers.Position = new Vector2f(screenMid - developers.GetLocalBounds().Width / 2, 700);
                introTextBack.Position = introText.Position;
                introText.CharacterSize -= 2;
                introTextBack.CharacterSize -= 2;
                return 5;
            } else
            {
                return 0;
            }
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
                introText.Draw(target, states);
                introTextBack.Draw(target, states);
                developers.Draw(target, states);
        }
    }
}
