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

namespace JumpAndRun
{
    class Game
    {
        //private GameWorld world;
        static SFML.Graphics.RenderWindow InitWindow()
        {
            SFML.Graphics.RenderWindow window = new SFML.Graphics.RenderWindow(VideoMode.FullscreenModes[0], "Jump'n'Run", Styles.Fullscreen);
            window.SetVerticalSyncEnabled(true);
            window.SetFramerateLimit(61);
            return window;
        }

        static void Main(string[] args)
        {
            SFML.Graphics.RenderWindow window = InitWindow();
            GameWorld world = new GameWorld(window);
            while (window.IsOpen)
            {
                window.Clear();
                world.Update();
                window.Draw(world);
                window.Display();
            }
        }
    }
}