using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;
using DodongosQuest.Creatures;


namespace DodongosQuest.Terrain
{
    public class TerrainFactory
    {
        private static Color _obstructionColor = Color.White;
        private static Color _waterColor = Color.Blue;
        private static Color _floorColor = Color.Gray;
        private static Color _nothingColor = Color.Black;

        public static Terrain CreateCaveFloor(Vector2 worldIndex, World world)
        {
            List<MovementType> movementTypes = new List<MovementType>();
            movementTypes.Add(MovementType.Flying);
            movementTypes.Add(MovementType.Walking);
            return new Terrain("Cave Floor", ContentHelper.Content.Load<Texture2D>("CaveFloor"), false, movementTypes, _floorColor, worldIndex, world);
        }

        public static Terrain CreateCaveWall(Vector2 worldIndex, World world)
        {
            List<MovementType> movementTypes = new List<MovementType>();
            return new Terrain("Cave Wall", ContentHelper.Content.Load<Texture2D>("CaveWall"), true, movementTypes, _obstructionColor, worldIndex, world);
        }

        public static Terrain CreateWater(Vector2 worldIndex, World world)
        {
            List<MovementType> movementTypes = new List<MovementType>();
            movementTypes.Add(MovementType.Flying);
            movementTypes.Add(MovementType.Swimming);
            return new Terrain("Water", ContentHelper.Content.Load<Texture2D>("Water"), false, movementTypes, _waterColor, worldIndex, world);
        }

        public static Terrain CreateNothing(Vector2 worldIndex, World world)
        {
            List<MovementType> movementTypes = new List<MovementType>();
            return new Terrain("Nothing", ContentHelper.Content.Load<Texture2D>("Nothing"), false, movementTypes, _nothingColor, worldIndex, world);
        }
    }
}
