using System;
namespace GLTrackControl.Graphics
{
    public class BaseObject
    {
        protected int x, y, z; // location
        protected Colors color;

        public BaseObject(int x, int y, int z)
        {
            SetLocation(x, y, z);
        }
        public void SetLocation(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public int[] GetLocation()
        {   
            return new int[]{ x, y, z };
        }
        public void Color(double r, double g, double b)
        {
            color = new Colors(r, g, b);
         
        }       
    }
}
