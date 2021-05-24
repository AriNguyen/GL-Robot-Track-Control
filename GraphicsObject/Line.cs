using System;
using OpenTK.Graphics.OpenGL;

namespace GLTrackControl.GraphicsObject
{
    public class Line : GraphicsObject
    {
        public Line(int startX, int startY, int startZ) : base(startX, startY, startZ)
        {}

        public void Display(int endX, int endY, int endZ)
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(position.X, position.Y, position.Z);
            GL.Vertex3(endX, endY, endZ);
            GL.End();
        }
    }
}
