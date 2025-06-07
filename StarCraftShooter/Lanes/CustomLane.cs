using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCraftShooter.Lanes
{
    public class CustomLane : ILane
    {
        public int LeftPositionStart { get; set; }
        public int TopPositionStart { get; set; }

        public CustomLane(int leftPositionParam, int topPositionParam)
        {
            LeftPositionStart = leftPositionParam;
            TopPositionStart = topPositionParam;
        }
    }
}
