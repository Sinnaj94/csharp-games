using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SpaceShooter.GameObjects;
using SpaceShooter.Factories;

namespace SpaceShooter.Menuscreens
{
    class pickShip : SFML.Graphics.Drawable
    {
        SFML.Graphics.RectangleShape bgRect;
        private Ship player;
        private Sprite ShipSpriteCopy;
        private int upgradeCost;
        Text UpgradeCostText;
        Text HelpText;
        uint characterSize = 20;
        private bool isPaused = false;

        public bool IsPaused
        {
            get
            {
                return isPaused;
            }

            set
            {
                isPaused = value;
            }
        }

        public pickShip(Ship player)
        {
            this.player = player;
            InitBg();
            InitShip();
            upgradeCost = ShipFactory.GetShipUpgradePrice(player.name);
            initText();
        }

        public void InitBg()
        {
            bgRect = new RectangleShape(new SFML.System.Vector2f(480, 270));
            bgRect.Origin = new SFML.System.Vector2f(bgRect.Size.X / 2, bgRect.Size.Y / 2);
            bgRect.Position = new SFML.System.Vector2f(1920 / 2, 1080 / 2);
            bgRect.FillColor = new Color(0, 0, 0, 128);
        }

        public void initText()
        {
            HelpText = new Text("Enter to upgrade, ESC to leave", ManageText.Instance.SelectedFont, characterSize);
            HelpText.Origin = new SFML.System.Vector2f(HelpText.GetLocalBounds().Width / 2, HelpText.GetLocalBounds().Height / 2);
            HelpText.Position = new SFML.System.Vector2f(bgRect.Position.X, bgRect.Position.Y - bgRect.Size.Y / 3);
            
            UpgradeCostText = new Text("upgrade Cost: " + upgradeCost + " Credits", ManageText.Instance.SelectedFont, characterSize);
            UpgradeCostText.Origin = new SFML.System.Vector2f(UpgradeCostText.GetLocalBounds().Width / 2, UpgradeCostText.GetLocalBounds().Height / 2);
            UpgradeCostText.Position = new SFML.System.Vector2f(bgRect.Position.X, bgRect.Position.Y + bgRect.Size.Y / 3);
        }

        public void InitShip()
        {
            ShipSpriteCopy = player.ShipSprite;
            ShipSpriteCopy.Position = bgRect.Position;
        }

        public void Update()
        {

        }

        public void UpgradeShip()
        {

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            bgRect.Draw(target, states);
            ShipSpriteCopy.Draw(target, states);
            UpgradeCostText.Draw(target, states);
            HelpText.Draw(target, states);
        }
    }
}
