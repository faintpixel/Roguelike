using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using xnaHelper.Helpers;
using DodongosQuest.ParticleSystem;
using DodongosQuest.Maps;
using DodongosQuest.Terrain;

namespace DodongosQuest.Maps.Map_Types
{

    class FromImage : IMap
    {
        public string mapFile = @"Content\TestLevel2.png";
        //public static ITerrain[,] Tiles { get; set; }

        private const int WORLD_WIDTH = 100;
        private const int WORLD_HEIGHT = 100;
        public const int TILE_SIZE = 25;
       

        public void CreateMapFromImage(World world)
        {
            Bitmap mapImage = (Bitmap)Bitmap.FromFile(mapFile);

            world.Tiles = new ITerrain[WORLD_WIDTH, WORLD_HEIGHT];

            for (int x = 0; x < WORLD_WIDTH; x++)
                for (int y = 0; y < WORLD_HEIGHT; y++)
                {
                    Vector2 worldIndex = new Vector2(x, y);

                    if (mapImage.GetPixel(x, y).Name == "ff6a4e2f")
                    {
                        world.Tiles[x, y] = TerrainFactory.CreateCaveFloor(worldIndex, world);
                    }
                    else if (mapImage.GetPixel(x, y).Name == "ff8c8c8c")
                    {
                        world.Tiles[x, y] = TerrainFactory.CreateCaveWall(worldIndex, world);
                    }
                    else if (mapImage.GetPixel(x, y).Name == "ff000000")
                    {
                        world.Tiles[x, y] = TerrainFactory.CreateNothing(worldIndex, world);
                    }
                    else if (mapImage.GetPixel(x, y).Name == "ff0000ff")
                    {
                        world.Tiles[x, y] = TerrainFactory.CreateWater(worldIndex, world);
                    }
                    else
                    {
                        Console.WriteLine("ERROR: Unknown terrain type");
                    }
                }

        }
    } 
}
