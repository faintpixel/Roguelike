using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;

namespace DodongosQuest.Creatures.Remains
{
    public class RemainsFactory
    {
        public static IRemains CreateBones(Vector2 worldIndex, World world)
        {
            return new Remains(worldIndex, world, ContentHelper.Content.Load<Texture2D>(@"Remains\Bones"));
        }

        public static IRemains CreateBatWing(Vector2 worldIndex, World world)
        {
            return new Remains(worldIndex, world, ContentHelper.Content.Load<Texture2D>(@"Remains\BatWing"));
        }

        public static IRemains CreateBlood(Vector2 worldIndex, World world)
        {
            return new Remains(worldIndex, world, ContentHelper.Content.Load<Texture2D>(@"Remains\Blood"));
        }

        public static IRemains CreateGreenGoo(Vector2 worldIndex, World world)
        {
            return new Remains(worldIndex, world, ContentHelper.Content.Load<Texture2D>(@"Remains\GreenGoo"));
        }

        public static IRemains CreateDust(Vector2 worldIndex, World world)
        {
            return new Remains(worldIndex, world, ContentHelper.Content.Load<Texture2D>(@"Remains\Dust"));
        }

    }
}
