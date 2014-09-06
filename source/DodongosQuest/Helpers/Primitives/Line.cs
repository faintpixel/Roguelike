using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;

namespace xnaHelper.Primitives
{
    public class Line
    {
        public Vector2 P1;
        public Vector2 P2;
        public Color LineColor;

        public Line(Vector2 p1, Vector2 p2, Color color)
        {
            P1 = p1;
            P2 = p2;
            LineColor = color;
        }

        public void Draw()
        {
            GraphicsHelper.primitiveBatch.Begin(PrimitiveType.LineList);
            GraphicsHelper.primitiveBatch.AddVertex(P1, LineColor);
            GraphicsHelper.primitiveBatch.AddVertex(P2, LineColor);
            GraphicsHelper.primitiveBatch.End();
        }

        public float Length()
        {
            //square root of ((x2-x1)^2+(y2-y1)^2)
            return (float)Math.Sqrt(Math.Pow((P2.X - P1.X), 2) + Math.Pow((P2.Y - P1.Y), 2));
        }
    }
}
