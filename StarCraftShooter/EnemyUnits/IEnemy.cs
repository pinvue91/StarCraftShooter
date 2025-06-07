using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCraftShooter
{
    public interface IEnemy
    {
        int Health { get; set; }
        int LeftPosition { get; set; }
        int TopPosition { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        List<int[,]> CurrentPositions { get; set; }
        SpawnLanesEnum SpawnLane { get; set; }
        bool IsAlive { get; set; }


        void Draw();

        void Move();

        void Shoot();

        void Redraw();
    }
}
