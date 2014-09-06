using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;

namespace xnaHelper.Primitives
{
    public class Circle
    {
        private float _radius;
        public float Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
                SetAllPoints();
            }
        }

        private Vector2 _center;
        public Vector2 Center
        {
            get
            {
                return _center;
            }
            set
            {
                _center = value;
                SetAllPoints();
            }
        }

        public Color CircleColor;

        List<Vector2> points;

        public Circle(Vector2 center, float radius, Color color)
        {
            Radius = radius;
            Center = center;
            CircleColor = color;
        }

        private void SetAllPoints()
        {
            if (Center != null)
            {
                points = new List<Vector2>();
                for (int i = 0; i <= 360; i++)
                {
                    points.Add(GetPointAtAngle((float)i));
                }
            }
        }

        private Vector2 GetPointAtAngle(float angle)
        {
            double x = Radius * Math.Cos(angle * Math.PI / 180);
            double y = Radius * Math.Sin(angle * Math.PI / 180);

            return new Vector2((float)x + Center.X, (float)y + Center.Y);
        }

        public void Draw()
        {
            GraphicsHelper.primitiveBatch.Begin(PrimitiveType.LineList); // was point list.. this is probably going to break

            foreach (Vector2 point in points)
            {
                GraphicsHelper.primitiveBatch.AddVertex(point, CircleColor);
            }

            GraphicsHelper.primitiveBatch.End();
        }
    }
}
