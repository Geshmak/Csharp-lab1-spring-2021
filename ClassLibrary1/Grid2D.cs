

using System;

namespace ClassLibrary1
{
    [Serializable]
    public struct Grid2D
    {
        public float StepX { get; set; }
        public float StepY { get; set; }
        public int NumberX { get; set; }
        public int NumberY { get; set; }
        public Grid2D(float sx, float sy, int nx, int ny)
        {
            StepX = sx;
            StepY = sy;
            NumberX = nx;
            NumberY = ny;
        }
        public override string ToString()
        {
            return "size: " + NumberX.ToString() + " " + NumberY.ToString() +
                "step x,y: " + StepX.ToString() + "," + StepY.ToString();
        }
        public string ToString(string format)
        {
            return "size: " + NumberX.ToString() + " " + NumberY.ToString() +
                "step x,y: " + StepX.ToString(format) + "," + StepY.ToString(format);
        }


    }
}
