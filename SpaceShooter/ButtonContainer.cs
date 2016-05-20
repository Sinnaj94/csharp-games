using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SpaceShooter.GameObjects;
namespace SpaceShooter
{
    class ButtonContainer : Drawable
    {
        List<Button> container;

        public ButtonContainer()
        {
            container = new List<Button>();
        }

        public void AddButton(Button b)
        {
            container.Add(b);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Button b in Container)
            {
                b.Draw(target, states);
            }
        }

        internal List<Button> Container
        {
            get
            {
                return container;
            }

            set
            {
                container = value;
            }
        }
    }
}
