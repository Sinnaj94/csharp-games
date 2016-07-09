using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using SFML.Graphics;

namespace JumpAndRun
{
    class GameWorld : SFML.Graphics.Drawable
    {
        private World world;
        DebugDraw debug;
        private Player player;
        InputHandler input;
        List<Command> currentCommands;
        TileMapBuilder tmb;
        Map map;
        bool[,] testmap;
        public GameWorld(RenderWindow window)
        {
            world = new World(new Vector2(0, 1));
            player = new Player(BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(30), ConvertUnits.ToSimUnits(60), 10));
            player.Body.Position = new Vector2(ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(0));
            player.Body.BodyType = BodyType.Dynamic;
            player.Body.LinearVelocity = new Vector2(0, 0);
            map = new Map(100, 100, 32);
            tmb = new TileMapBuilder(world, map);
            debug = new DebugDraw(world, window);
            input = new InputHandler();
            pathfindigtest();
        }

        public void pathfindigtest()
        {
            Point start = new Point(1, 1);
            Point end = new Point(1, 20);
            SearchParameters sp = new SearchParameters(start, end, map);
            //Start with a clear map (don't add any obstacles)
           // InitializeMap();
            PathFinder pathFinder = new PathFinder(sp);
            List<Point> path = pathFinder.FindPath();

            foreach(Point p in path)
            {
                Console.WriteLine("X: " + p.X + "Y: " + p.Y);
            }
         //   ShowRoute("The algorithm should find a direct path without obstacles:", path);
        }

        private void InitializeMap()
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            this.testmap = new bool[7, 5];
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 7; x++)
                    testmap[x, y] = true;

            var startLocation = new Point(1, 2);
            var endLocation = new Point(5, 2);
            //  this.searchParameters = new SearchParameters(startLocation, endLocation, map);
        }


        public View setCameraToPlayer(RenderTarget target)
        {
            SFML.System.Vector2f defaultSize = target.DefaultView.Size;
            return new View(new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(player.Body.Position.X), ConvertUnits.ToDisplayUnits(player.Body.Position.Y)), defaultSize);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.SetView(setCameraToPlayer(target));
            debug.DrawDebugData();
            debug.DrawWorldTiles();
            tmb.Draw(target, states);
            player.Draw(target, states);
            map.Draw(target, states);
        }

        private void HandleInputCommands()
        {
            currentCommands = input.HandleInput();
            foreach(Command c in currentCommands)
            {
                c.Execute(player);
            }
            input.ResetInput();
        }

        public void Update()
        {

            HandleInputCommands();
            world.Step(.01639344262f);

        }
    }
}
