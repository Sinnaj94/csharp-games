using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SpaceShooter.GameObjects;
using SFML.System;
using FarseerPhysics;
namespace SpaceShooter
{
    class DrawShipAttributes : IRenderable
    {
        RectangleShape lifeBackground;
        RectangleShape lifeForeground;
        Vector2f lifeSize;
        Vector2f lifeLeft;
        Ship s;
        public DrawShipAttributes(Ship s)
        {
            this.s = s;
            lifeSize = new Vector2f(100, 10);
            lifeLeft = new Vector2f(100*s.Life, 10);

            lifeBackground = new RectangleShape(lifeSize);
            lifeBackground.FillColor = Color.White;
            lifeForeground = new RectangleShape(lifeLeft);
            lifeForeground.FillColor = Color.Green;
        }

        private void updatePosition(Vector2f newPosition){
            Vector2f converted = new Vector2f(ConvertUnits.ToDisplayUnits(newPosition.X), ConvertUnits.ToDisplayUnits(newPosition.Y));
            lifeBackground.Position = converted;
            lifeForeground.Position = converted;
        }

        private void updateLifeLeft()
        {
            lifeForeground.Scale = new Vector2f(lifeForeground.Scale.X * s.Life, lifeForeground.Scale.Y);
        }

        public void Update()
        {
            updatePosition(new Vector2f(s.body.Position.X-ConvertUnits.ToSimUnits(50),s.body.Position.Y- ConvertUnits.ToSimUnits(100)));
            updateLifeLeft();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            lifeBackground.Draw(target, states);
            lifeForeground.Draw(target, states);

        }
    }

    class HUD : IRenderable
    {
        Ship ship;
        Text shipText;
        Text healthText;
        Font font;
        Texture hudTexture;
        Sprite hudSprite;
        public HUD(Ship ship)
        {
            font = ManageText.Instance.SelectedFont;
            this.ship = ship;
            shipText = new Text("player 1", font);
            healthText = new Text("100/100 HP", font);
            healthText.Position = new Vector2f(0, 40);
            healthText.Color = Color.Green;
            shipText.Color = Color.White;
            shipText.Position = new Vector2f(0, 0);
            hudTexture = new Texture(@"Resources/hud.png");
            hudSprite = new Sprite(hudTexture);
            hudSprite.TextureRect = new IntRect(0, 0, 1920, 1080);
        }

        public void Update()
        {
            
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            healthText.Draw(target, states);
            shipText.Draw(target, states);
            //hudSprite.Draw(target, states);
        }
    }
}
