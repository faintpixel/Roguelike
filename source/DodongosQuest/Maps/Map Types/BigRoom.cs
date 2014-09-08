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
    class BigRoom : IMap
    {
       

        public void CreateBigRoom(World world)
        {
            world.Tiles = new ITerrain[World.WORLD_WIDTH, World.WORLD_HEIGHT];

            for (int x = 0; x < World.WORLD_WIDTH; x++)
            {
                for (int y = 0; y < World.WORLD_HEIGHT; y++)
                {
                    Vector2 worldIndex = new Vector2(x, y);

                    
                       world.Tiles[x, y] = TerrainFactory.CreateCaveFloor(worldIndex, world);

                   
                }
            }
        }
    }
}
