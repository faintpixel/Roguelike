using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;

namespace xnaHelper.Primitives
{
    public class Box
    {
        public Vector2 TopLeft;
        private Vector2 TopRight;
        private Vector2 BottomLeft;
        private Vector2 BottomRight;
        public int Width;
        public int Height;
        public Color LineColor;

        public Box(Vector2 topLeft, int width, int height, Color color)
        {
            TopLeft = topLeft;
            Width = width;
            Height = height;

            CalculatePoints();
            
            LineColor = color;
        }

        private void CalculatePoints()
        {
            TopRight = new Vector2(TopLeft.X + Width, TopLeft.Y);
            BottomLeft = new Vector2(TopLeft.X, TopLeft.Y + Height);
            BottomRight = new Vector2(TopLeft.X + Width, TopLeft.Y + Height);
        }

        public void Draw()
        {
            GraphicsHelper.primitiveBatch.Begin(PrimitiveType.LineList);
            GraphicsHelper.primitiveBatch.AddVertex(TopLeft, LineColor);
            GraphicsHelper.primitiveBatch.AddVertex(TopRight, LineColor);
            GraphicsHelper.primitiveBatch.AddVertex(TopRight, LineColor);
            GraphicsHelper.primitiveBatch.AddVertex(BottomRight, LineColor);
            GraphicsHelper.primitiveBatch.AddVertex(BottomRight, LineColor);
            GraphicsHelper.primitiveBatch.AddVertex(BottomLeft, LineColor);
            GraphicsHelper.primitiveBatch.AddVertex(BottomLeft, LineColor);
            GraphicsHelper.primitiveBatch.AddVertex(TopLeft, LineColor);
            GraphicsHelper.primitiveBatch.End();
        }

    }
}
