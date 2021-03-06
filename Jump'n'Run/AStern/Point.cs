﻿namespace JumpAndRun
{
    public struct Point
    {
        int x;
        int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public float XSim
        {
            get
            {
                return FarseerPhysics.ConvertUnits.ToSimUnits(x * 32 + 16);
            }
        }

        public float YSim
        {
            get
            {
                return FarseerPhysics.ConvertUnits.ToSimUnits(y * 32 + 16);
            }
        }

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }
    }
}