using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Factories;
using FarseerPhysics;
using FarseerPhysics.Dynamics;

namespace JumpAndRun
{
    class TileMapBuilder
    {

        public TileMapBuilder(FarseerPhysics.Dynamics.World world)
        {
            SFML.Graphics.Texture tilemap = new SFML.Graphics.Texture(@"Resources\sprites\tileset.png");
            TiledSharp.TmxMap test = new TiledSharp.TmxMap(@"Resources\Tield_Datei.tmx");
            var myTileset = test.Tilesets["tileset"];

            foreach(TiledSharp.TmxLayer l in test.Layers)
            {
                foreach (TiledSharp.TmxLayerTile t in l.Tiles)
                {
                    if (t.Gid != 0)
                    {
                        Body bodyToAdd = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(32), ConvertUnits.ToSimUnits(32), 10);
                        bodyToAdd.Position = new Microsoft.Xna.Framework.Vector2(ConvertUnits.ToSimUnits(t.X * 32), ConvertUnits.ToSimUnits(t.Y * 32));
                        SFML.Graphics.Sprite bodySprite = new SFML.Graphics.Sprite(tilemap, new SFML.Graphics.IntRect((t.Gid % 30 - 1) * 32, t.Gid / 30 * 32, 32, 32));
                        bodyToAdd.BodyType = BodyType.Static;
                        bodySprite.Position = new SFML.System.Vector2f(t.X * 32 - 16, t.Y * 32 - 16);
                        bodyToAdd.UserData = bodySprite;
                        
                        if (l.Name != "collidable")
                        {
                            bodyToAdd.Enabled = false;
                        } 
                    }
                }
            }      
        }
    }
}
