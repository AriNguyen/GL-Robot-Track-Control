using System;

namespace GLTrackControl.Graphics
{
    public class Sphere
    {
        private int x, y, z; // location
        private Colors color;

        public Sphere(int x, int y, int z)
        {
            Locate(x, y, z);
        }
        public void Locate(int x, int y, int z)
        {

        }
        public void Color(double red, double green, double blue)
        {
            color = new Colors(red, green, blue);
            color.Display();
        }
    }
}
