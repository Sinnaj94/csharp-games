﻿using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using SFML.Graphics;
using SFML.System;

namespace JumpAndRun
{
    class Game
    {
        //private GameWorld world;

        static SFML.Graphics.RenderWindow InitWindow()
        {
            SFML.Graphics.RenderWindow window = new SFML.Graphics.RenderWindow(VideoMode.DesktopMode, "Top Down", Styles.Fullscreen);
            window.SetVerticalSyncEnabled(true);
            window.SetFramerateLimit(61);   
            return window;
        }

        static void Main(string[] args)
        {
            SFML.Graphics.RenderWindow window = InitWindow();
            GameWorld world = new GameWorld(window);
            AbstractNavigation menu = new MainMenu();
            AbstractNavigation dialog = new Dialog("1");
            InputHandlerMenu inputMenu = new InputHandlerMenu();
            List<MenuCommand> _temp;
            world.Update();
            ManageSound.Instance.StartPlayingMusic();
            while (window.IsOpen)
            {
                window.Clear();

                //INPUT HANDLING AGAIN!
                _temp = inputMenu.HandleInput();
                foreach (MenuCommand m in _temp)
                {
                    m.Execute(menu);
                    m.Execute(dialog);
                }

                window.Draw(world);

                inputMenu.Flush();
                if (menu.Active)
                {
                    window.Draw(menu);
                } else
                {
                    if (dialog.Active)
                    {
                        dialog.Update();
                        window.Draw(dialog);
                    } else
                    {
                        world.Update();
                    }
                }
                window.Display();
            }
        }

    }
}