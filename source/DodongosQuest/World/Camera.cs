using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DodongosQuest
{
    public class Camera
    {
        private static int MOVE_SPEED = 8;
        private static float REALLY_BIG_NUMBER = 999999;
        private static Vector2 topLeftCornerWorldPosition = new Vector2(0, 0);
        private static Vector2 worldSize = new Vector2(REALLY_BIG_NUMBER, REALLY_BIG_NUMBER);

        public static Vector2 GetWorldPosition(Vector2 screenPosition)
        {
            Vector2 worldPosition = new Vector2(screenPosition.X + topLeftCornerWorldPosition.X, screenPosition.Y + topLeftCornerWorldPosition.Y);
            return worldPosition;
        }

        public static Vector2 GetScreenPosition(Vector2 worldPosition)
        {
            Vector2 screenPosition = new Vector2(worldPosition.X - topLeftCornerWorldPosition.X, worldPosition.Y - topLeftCornerWorldPosition.Y);

            return screenPosition;
        }

        public static void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            if (state.X >= 1024 - 1 && topLeftCornerWorldPosition.X <= worldSize.X - 1024 + MOVE_SPEED)
                topLeftCornerWorldPosition.X += MOVE_SPEED;
            else if (state.X <= 0 && topLeftCornerWorldPosition.X >= 0)
                topLeftCornerWorldPosition.X -= MOVE_SPEED;

            if (state.Y >= 768 - 1 && topLeftCornerWorldPosition.Y <= worldSize.Y - 768 + MOVE_SPEED)
                topLeftCornerWorldPosition.Y += MOVE_SPEED;
            else if (state.Y <= 0 && topLeftCornerWorldPosition.Y >= 0)
                topLeftCornerWorldPosition.Y -= MOVE_SPEED;
        }

        public static void SetWorldSize(float x, float y)
        {
            worldSize.X = x;
            worldSize.Y = y;
        }

        public static void CenterCameraOnWorldPosition(float x, float y)
        {
            int topLeftX = (int)(x - (1024 / 2));
            int topLeftY = (int)(y - (768 / 2));
            if (topLeftX < 0)
                topLeftX = 0;
            if (topLeftY < 0)
                topLeftY = 0;

            topLeftCornerWorldPosition.X = topLeftX;
            topLeftCornerWorldPosition.Y = topLeftY;
        }

        public static void CenterCameraOnScreenPosition(float x, float y)
        {
            Vector2 worldPosition = GetWorldPosition(new Vector2(x, y));

            CenterCameraOnWorldPosition(worldPosition.X, worldPosition.Y);
        }
    }
}
