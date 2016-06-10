using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Factories;
using FarseerPhysics;

namespace JumpAndRun
{
    class TileMapBuilder
    {
        public TileMapBuilder(FarseerPhysics.Dynamics.World world)
        {
            TiledSharp.TmxMap test = new TiledSharp.TmxMap(@"Resources\Tield_Datei.tmx");
            var myTileset = test.Tilesets["tileset"];
            foreach(TiledSharp.TmxLayerTile t in test.Layers[0].Tiles){
                if (t.Gid != 0)
                {
                    BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(32), ConvertUnits.ToSimUnits(32), 10).Position = new Microsoft.Xna.Framework.Vector2(t.X, t.Y);
                }
                
            }
          //  test.Layers[0].Tiles.
        }
    }
}
