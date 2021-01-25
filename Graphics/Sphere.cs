using System;
using OpenTK.Graphics.OpenGL;

namespace GLTrackControl.Graphics
{
    public class Sphere : BaseObject
    {
        public Sphere(int x, int y, int z) : base(x, y, z)
        {}
        
        public void Draw(double r, int lats, int longs)
        {
            int i, j;
            double M_PI = 3.14;
            for (i = 0; i <= lats; i++)
            {
                double lat0 = M_PI * (-0.5 + (double)(i - 1) / lats);
                double z0 = Math.Sin(lat0);
                double zr0 = Math.Cos(lat0);

                double lat1 = M_PI * (-0.5 + (double)i / lats);
                double z1 = Math.Sin(lat1);
                double zr1 = Math.Cos(lat1);

                GL.Begin(PrimitiveType.QuadStrip);
                color.Display();
                for (j = 0; j <= longs; j++)
                {
                    double lng = 2 * M_PI * (double)(j - 1) / longs;
                    double x1 = Math.Cos(lng);
                    double y1 = Math.Sin(lng);

                    GL.Normal3(x * zr0, y * zr0, z0);
                    GL.Vertex3(r * x1 * zr0 + x, r * y1 * zr0 + y, r * z0 + z);
                    GL.Normal3(x * zr1, y * zr1, z1);
                    GL.Vertex3(r * x1 * zr1 + x, r * y1 * zr1 + y, r * z1 + z);
                }
                GL.End();
            }
        }
    }
}
