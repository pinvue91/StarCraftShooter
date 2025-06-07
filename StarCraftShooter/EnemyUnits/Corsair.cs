using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StarCraftShooter
{
    public class Corsair : IEnemy
    {
        public int Health { get; set; } = 20;
        public int LeftPosition { get; set; }
        public int TopPosition { get; set; }
        public int Width { get; set; } = 5;
        public int Height { get; set; } = 3;

        public static List<Missile> Missiles = new List<Missile>();

        public List<int[,]> CurrentPositions { get; set; } = new List<int[,]>();
        public SpawnLanesEnum SpawnLane { get; set; }
        public bool IsAlive
        {
            get
            {
                if (Health <= 0)
                {
                    return false;
                }
                return true;
            }
            set
            {
            }
        }

        public Corsair(int leftPosition, int topPosition)
        {
            LeftPosition = leftPosition;
            TopPosition = topPosition;
        }

        public void Draw()
        {
            while (Program.cursorInUse == true)
            {

            }

            Program.cursorInUse = true;

            Console.SetCursorPosition(LeftPosition, TopPosition);
            Console.Write("_\\V/_");
            Console.SetCursorPosition(LeftPosition, TopPosition + 1);
            Console.Write("\\[O]/");
            Console.SetCursorPosition(LeftPosition, TopPosition + 2);
            Console.Write("  v ");

            Program.cursorInUse = false;

            InstatiatePosition();
        }

        public void Move()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (TopPosition < Console.WindowHeight && IsAlive)
            {
                if (stopwatch.ElapsedMilliseconds % 1500 == 0)
                {
                    int targetTopPosition = TopPosition + 1;
                    Console.MoveBufferArea(LeftPosition, TopPosition, Width, Height, LeftPosition, targetTopPosition);
                    TopPosition = targetTopPosition;
                    UpdatePosition();
                }
            }
            DeletePosition();
            Remove();
        }

        //instantiates the currentPositions with data
        private void InstatiatePosition()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int[,] enemyPiece = new int[LeftPosition + x, TopPosition + y];
                    CurrentPositions.Add(enemyPiece);
                }
            }
        }

        //update the current positions instead of deleting and adding new positions
        private void UpdatePosition()
        {
            for (int i = 0; i < CurrentPositions.Count; i++)
            {
                //moves down 1
                int upperBoundLeft = CurrentPositions[i].GetUpperBound(0) + 1;
                int upperBoundTop = CurrentPositions[i].GetUpperBound(1) + 1 + 1;
                int[,] newPosition = new int[upperBoundLeft, upperBoundTop];
                CurrentPositions[i] = newPosition;
            }
        }

        private void DeletePosition()
        {
            CurrentPositions = new List<int[,]>();
        }

        public void Shoot()
        {
            Missile leftMissle = new Missile(Direction.down, 2, 10, LeftPosition + 1, TopPosition + 5, 1, 1);
            Missile rightMissle = new Missile(Direction.down, 2, 10, LeftPosition + 7, TopPosition + 5, 1, 1);

            Missiles.Add(leftMissle);
            Missiles.Add(rightMissle);

            while (Program.cursorInUse == true)
            {

            }

            Program.cursorInUse = true;

            leftMissle.DrawCorsairMissile();
            rightMissle.DrawCorsairMissile();

            Program.cursorInUse = false;

            Thread leftMissile = new Thread(() => leftMissle.MoveEnemyMissile());
            leftMissile.Start();

            Thread rightMissile = new Thread(() => rightMissle.MoveEnemyMissile());
            rightMissile.Start();
        }

        public void Redraw()
        {
            Console.SetCursorPosition(LeftPosition, TopPosition);
            Console.Write("_\\V/_");
            Console.SetCursorPosition(LeftPosition, TopPosition + 1);
            Console.Write("\\[O]/");
            Console.SetCursorPosition(LeftPosition, TopPosition + 2);
            Console.Write("  v ");
        }

        public void Remove()
        {
            while (Program.cursorInUse == true)
            {

            }

            Program.cursorInUse = true;

            Console.SetCursorPosition(LeftPosition, TopPosition);
            Console.Write("           ");
            Console.SetCursorPosition(LeftPosition, TopPosition + 1);
            Console.Write("           ");
            Console.SetCursorPosition(LeftPosition, TopPosition + 2);
            Console.Write("           ");

            Program.cursorInUse = false;
        }
    }
}
