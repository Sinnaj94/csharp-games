using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class TileMapBuilder
    {
        public TileMapBuilder()
        {
            TiledSharp.TmxMap test = new TiledSharp.TmxMap(@"Resources\Tield_Datei.tmx");
            var myTileset = test.Tilesets["tileset"];

        }
    }
}
