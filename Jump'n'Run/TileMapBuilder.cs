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
using SFML.Graphics;
using SFML.System;

namespace JumpAndRun
{
    class TileMapBuilder : SFML.Graphics.Drawable
    {
        List<SFML.Graphics.Drawable> TileSpriteList;
        Map map;
        TiledSharp.TmxMap test;
        int currentlevel = 1;


        static private void CreateShape(SFML.Graphics.Texture texture, World world)
        {
            // Make collision Geo from bitmap
            // Get pixel data in array​
            byte[] bytes = texture.CopyToImage().Pixels;
            uint[] data = new uint[texture.Size.X * texture.Size.Y];
            for (int i = 0; i < bytes.Length; i += 4)
            {
                data[i / 4] = BitConverter.ToUInt32(bytes, i);
            }

            Byte myByte = 1;
            
            List<Vertices> _list = PolygonTools.CreatePolygon(data, (int)texture.Size.X, 0.05f, myByte, true, true);
            Vertices verts = new Vertices();
            Vector2 scale = ConvertUnits.ToSimUnits(new Vector2(1, 1));

            foreach (Vertices v in _list)
            {
                v.Scale(scale);
                //    v.Translate(ConvertUnits.ToSimUnits(new Vector2(-16, -16)));
                Body body = new Body(world);
                body.UserData = "wall";
                List<Fixture> fixtures = FixtureFactory.AttachCompoundPolygon(
                    FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(SimplifyTools.DouglasPeuckerSimplify(v, 0.05f), TriangulationAlgorithm.Bayazit, false, 0.05f),
                    1, body);
            }
        }

        public Level getNextLevel()
        {
            LevelBuilder l = new LevelBuilder(@"Resources\json\level.json");
            List<Level> level = l.Level;
            return level[currentlevel++];
        }

        public void initLevel(ref World world, ref EnemyContainer eContrainer, ref Player player, ref CollectableContainer collectables)
        {
            world = new World(new Vector2(0, 0));
            eContrainer = new EnemyContainer(world);
            collectables = new CollectableContainer(world);
            Level level = getNextLevel();
            TileSpriteList = new List<Drawable>();
            CreateShape(new SFML.Graphics.Texture(level.AlphaTexture), world);
            SFML.Graphics.Texture tilemap = new SFML.Graphics.Texture(level.TilemapImage);
            test = new TiledSharp.TmxMap(level.TMX);
            var myTileset = test.Tilesets[level.Tilesetname];
            Map newMap = new Map(test.Width, test.Height, test.TileWidth);
            this.Map = newMap;
            parseTileLayer(tilemap);
            parseObjectLayer(world, eContrainer, ref player, collectables);
        }

        public void parseObjectLayer(World world, EnemyContainer eContrainer, ref Player player, CollectableContainer collectables)
        {
            foreach (TiledSharp.TmxObjectGroup g in test.ObjectGroups)
            {
                foreach (TiledSharp.TmxObject o in g.Objects)
                {
                    int id = o.Tile.Gid;
                    switch (id)
                    {
                        // Spawnpoint
                        case 458:
                            player = new Player(BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(10), 1), world);
                            player.body.Position = new Vector2(ConvertUnits.ToSimUnits(o.X), ConvertUnits.ToSimUnits(o.Y));
                            eContrainer.Player = player;
                            collectables.Player = player;
                            break;

                        // ENEMY
                        case 485:
                            eContrainer.Add(new Vector2(ConvertUnits.ToSimUnits(o.X), ConvertUnits.ToSimUnits(o.Y)), (float)((Math.PI / 180) * o.Rotation - (float)Math.PI / 2));
                            break;

                        // BulletCollectable
                        case 512:
                            collectables.Add(new Vector2(ConvertUnits.ToSimUnits(o.X), ConvertUnits.ToSimUnits(o.Y)), (float)((Math.PI / 180) * o.Rotation - (float)Math.PI / 2));
                           // collectables.Add(new Vector2(0,0), 0);
                            break;
                        
                      
                        default:
                            Console.WriteLine("unknown gid");
                            break;
                    }

                }
            }
        }

        public void parseTileLayer(Texture tilemap)
        {
            foreach (TiledSharp.TmxLayer l in test.Layers)
            {
                foreach (TiledSharp.TmxLayerTile t in l.Tiles)
                {
                    SFML.Graphics.Sprite bodySprite = null;

                    if (t.Gid != 0)
                    {
                        bodySprite = new SFML.Graphics.Sprite(tilemap, new SFML.Graphics.IntRect((t.Gid % 27 - 1) * 37, t.Gid / 27 * 37, 32, 32));
                        bodySprite.Position = new SFML.System.Vector2f(t.X * 32, t.Y * 32);
                        Sprite nightTexture = bodySprite;
                        nightTexture.Color = new Color(0, 0, 0, 80);
                        nightTexture.Color = new Color(128, 128, 128, 200);
                        TileSpriteList.Add(bodySprite);
                        TileSpriteList.Add(nightTexture);
                    }
                    if (l.Name == "walls")
                    {
                        if (t.Gid != 0)
                        {
                            Map.AddTile(t.X, t.Y, true);
                        }
                        else
                        {
                            Map.AddTile(t.X, t.Y, false);
                        }
                    }
                }
            }
        }

        public List<Vector2> GetObjectPositions()
        {
            List<Vector2> _return = new List<Vector2>();
            if(test.ObjectGroups.Count > 0)
            {
                TiledSharp.TmxObjectGroup liste = test.ObjectGroups[0];
                foreach (TiledSharp.TmxObject o in liste.Objects)
                {
                    _return.Add(new Vector2(ConvertUnits.ToSimUnits(o.X), ConvertUnits.ToSimUnits(o.Y)));

                }
            }
            return _return;
        }

        public Map Map
        {
            get
            {
                return map;
            }

            set
            {
                map = value;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (SFML.Graphics.Drawable d in TileSpriteList)
            {
                d.Draw(target, states);
            }
        }
    }
}
