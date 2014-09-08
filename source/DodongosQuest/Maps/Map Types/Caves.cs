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

//this type doesn't work yet

namespace DodongosQuest.Maps.Map_Types
{
    class Caves : IMap
    {
        private const int WORLD_WIDTH = 100;
        private const int WORLD_HEIGHT = 100;
        public const int TILE_SIZE = 25;

        private const int MAX_CAVE_AREA = 500;
        private const int MAX_ROOM_AREA = 50;
        private const int MAX_ROOM_WIDTH = 10; 

        private int currentvolume = 0;
        Random random = new Random();

        public void CreateCave(World world)
        {
            world.Tiles = new ITerrain[WORLD_WIDTH, WORLD_HEIGHT];
            
            int start_x = random.Next(0, 99);
            int start_y = random.Next(0, 99);


            while (currentvolume < MAX_CAVE_AREA) //stop when our map is big enough
            {
                
            }
            

        }

        //public static CarveRoom(int x, int y, int volume, World world)
        //{
        //    int width = random.Next(0, MAX_ROOM_WIDTH);
        //    //carve shit
        //    //return volume
        //    return "lol"
        //}

    }
}
