using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;

namespace xnaHelper.Primitives
{
    public class GeometryDrawer
    {
        public List<Vector2> Vertices;

        public Color LineColor;

        public GeometryDrawer(List<Vector2> vertices, Color color)
        {
            Vertices = vertices;
            LineColor = color;
        }

        public void Draw()
        {
            if (Vertices.Count > 0)
            {
                Vector2 previousPoint = Vertices[0];

                GraphicsHelper.primitiveBatch.Begin(PrimitiveType.LineList);
                foreach (Vector2 vector in Vertices)
                {
                    GraphicsHelper.primitiveBatch.AddVertex(previousPoint, LineColor);
                    GraphicsHelper.primitiveBatch.AddVertex(vector, LineColor);

                    previousPoint = vector;
                }
                GraphicsHelper.primitiveBatch.AddVertex(previousPoint, LineColor);
                GraphicsHelper.primitiveBatch.AddVertex(Vertices[0], LineColor);

                GraphicsHelper.primitiveBatch.End();
            }
        }

    }
}
