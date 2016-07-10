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
        Enemy enemy; 
        InputHandler input;
        List<Command> currentCommands;
        TileMapBuilder tmb;
        Map map;
        bool[,] testmap;
        public GameWorld(RenderWindow window)
        {
            world = new World(new Vector2(0, 1));
            Vector2 playerSize = new Vector2(64, 64);
            player = new Player(BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(playerSize.X), ConvertUnits.ToSimUnits(playerSize.Y), 10),playerSize);
            enemy = new JumpingEnemy(BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(30), ConvertUnits.ToSimUnits(60), 10), world);
            map = new Map(100, 100, 32);
            tmb = new TileMapBuilder(world, map);
            debug = new DebugDraw(world, window);
            input = new InputHandler();
            enemy.calculatePathToTarget(new Point(25, 22), map);
        }

        public List<Point> pathfindigtest(Point start, Point end)
        {
            SearchParameters sp = new SearchParameters(start, end, map);
            PathFinder pathFinder = new PathFinder(sp);
            List<Point> path = pathFinder.FindPath();
            return path;
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
            enemy.Update();
            HandleInputCommands();
            player.Update();
            world.Step(.01639344262f);
        }
    }
}
