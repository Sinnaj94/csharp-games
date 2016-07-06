using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Factories;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using FarseerPhysics.Common.PolygonManipulation;
using FarseerPhysics.Common.Decomposition;


namespace JumpAndRun
{
    class TileMapBuilder
    {
        World world;
        static private void CreateShape(SFML.Graphics.Texture texture, World world)
        {
            // Get pixel data in array​
            byte[] bytes = texture.CopyToImage().Pixels;
            uint[] data = new uint[texture.Size.X * texture.Size.Y];
            for (int i = 0; i < bytes.Length; i += 4)
            {
                data[i / 4] = BitConverter.ToUInt32(bytes, i);
            }
            // Get outline polygon from pixels
            Vertices verts = PolygonTools.CreatePolygon(data, (int)texture.Size.X, false);
            Vector2 scale = ConvertUnits.ToSimUnits(new Vector2(1, 1));
            verts.Scale(scale);
            verts.Translate(- texture.Size.ToVector2f().ToSimVector() / 2);
            verts = SimplifyTools.DouglasPeuckerSimplify(verts, 0.05f);
            Body body = new Body(world);
            List<Fixture> fixtures = FixtureFactory.AttachCompoundPolygon(
                FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(verts, TriangulationAlgorithm.Flipcode, false, 0.1f),
                1, body);
        }

        // Triangulate.ConvexPartition(verts, TriangulationAlgorithm.SeidelTrapezoids


        static private void CreateCollisionWorld(World world, SFML.Graphics.Texture tilemap)
        {
            uint[] data = new uint[tilemap.Size.X * tilemap.Size.Y];
            FarseerPhysics.Common.TextureTools.TextureConverter.DetectVertices(data, (int)tilemap.Size.X, true);
            Vertices verts = PolygonTools.CreatePolygon(data, (int)tilemap.Size.X, true);
            Vector2 scale = new Vector2(0.07f, 0.07f);
            verts.Scale(ref scale);
            List<Vertices> _list;
            _list = Triangulate.ConvexPartition(verts, TriangulationAlgorithm.Bayazit);
            Body body = new Body(world);
            body.BodyType = BodyType.Static;
            List<Fixture> compund = FixtureFactory.AttachCompoundPolygon(_list, 1, body);
            //compund[0].Body.BodyType = BodyType.Dynamic;
        }

        public TileMapBuilder(FarseerPhysics.Dynamics.World world)
        {

            //CreateShape(new SFML.Graphics.Texture(@"Resources\sprites\tileset.png"), world);
 
            SFML.Graphics.Texture tilemap = new SFML.Graphics.Texture(@"Resources\Tield_Datei2.png");
            TiledSharp.TmxMap test = new TiledSharp.TmxMap(@"Resources\Tield_Datei.tmx");
            var myTileset = test.Tilesets["tileset"];
          //  CreateCollisionWorld(world, tilemap);
            foreach (TiledSharp.TmxLayer l in test.Layers)
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
