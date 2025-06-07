using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StarCraftShooter
{
    public class Carrier : IEnemy
    {
        public int Health { get; set; } = 200;
        public int LeftPosition { get; set; }
        public int TopPosition { get; set; }
        public int Width { get; set; } = 15;
        public int Height { get; set; } = 9;

        public static List<Missile> Missiles = new List<Missile>();

        public List<int[,]> CurrentPositions { get; set; } = new List<int[,]>();
        public SpawnLanesEnum SpawnLane { get; set; }
        public bool IsAlive
        {
            get
            {
                if (Health <= 0)
                {
                    DeletePosition();
                    Remove();
                    return false;
                }
                return true;
            }
            set
            {
            }
        }
        public int MaxInterceptorCount { get; set; } = 8;

        public Carrier(int leftPosition, int topPosition)
        {
            LeftPosition = leftPosition;
            TopPosition = topPosition;
            Draw();
            SpawnInterceptor();
        }

        public void Draw()
        {
            while (Program.cursorInUse == true)
            {

            }

            Program.cursorInUse = true;

            Console.SetCursorPosition(LeftPosition, TopPosition);
            Console.Write("     \\\\V//");
            Console.SetCursorPosition(LeftPosition, TopPosition + 1);
            Console.Write("    ([]|[])");
            Console.SetCursorPosition(LeftPosition, TopPosition + 2);
            Console.Write("   (( ||| ))");
            Console.SetCursorPosition(LeftPosition, TopPosition + 3);
            Console.Write("\\\\((=     =))//");
            Console.SetCursorPosition(LeftPosition, TopPosition + 4);
            Console.Write(" ((=       =))");
            Console.SetCursorPosition(LeftPosition, TopPosition + 5);
            Console.Write("  (( {   } ))");
            Console.SetCursorPosition(LeftPosition, TopPosition + 6);
            Console.Write("   ((     ))");
            Console.SetCursorPosition(LeftPosition, TopPosition + 7);
            Console.Write("    ((   ))");
            Console.SetCursorPosition(LeftPosition, TopPosition + 8);
            Console.Write("      ( )");

            Program.cursorInUse = false;

            InstatiatePosition();
        }

        public void Move()
        {
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //while (TopPosition < Console.WindowHeight && Alive)
            //{
            //    if (stopwatch.ElapsedMilliseconds % 1500 == 0)
            //    {
            //        int targetTopPosition = TopPosition + 1;
            //        Console.MoveBufferArea(LeftPosition, TopPosition, Width, Height, LeftPosition, targetTopPosition);
            //        TopPosition = targetTopPosition;
            //        UpdatePosition();
            //    }
            //}
            //DeletePosition();
            //Remove();
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
            //Missile leftMissle = new Missile(Direction.down, 2, 10, LeftPosition + 1, TopPosition + 5, 1, 1);
            //Missile rightMissle = new Missile(Direction.down, 2, 10, LeftPosition + 7, TopPosition + 5, 1, 1);

            //Missiles.Add(leftMissle);
            //Missiles.Add(rightMissle);

            //while (Program.cursorInUse == true)
            //{

            //}

            //Program.cursorInUse = true;

            //leftMissle.DrawScoutMissile();
            //rightMissle.DrawScoutMissile();

            //Program.cursorInUse = false;

            //Thread leftMissile = new Thread(() => leftMissle.MoveEnemyMissile());
            //leftMissile.Start();

            //Thread rightMissile = new Thread(() => rightMissle.MoveEnemyMissile());
            //rightMissile.Start();
        }

        public void Redraw()
        {
            Console.SetCursorPosition(LeftPosition, TopPosition);
            Console.Write("     \\\\V//");
            Console.SetCursorPosition(LeftPosition, TopPosition + 1);
            Console.Write("    ([]|[])");
            Console.SetCursorPosition(LeftPosition, TopPosition + 2);
            Console.Write("   (( ||| ))");
            Console.SetCursorPosition(LeftPosition, TopPosition + 3);
            Console.Write("\\\\((=     =))//");
            Console.SetCursorPosition(LeftPosition, TopPosition + 4);
            Console.Write(" ((=       =))");
            Console.SetCursorPosition(LeftPosition, TopPosition + 5);
            Console.Write("  (( {   } ))");
            Console.SetCursorPosition(LeftPosition, TopPosition + 6);
            Console.Write("   ((     ))");
            Console.SetCursorPosition(LeftPosition, TopPosition + 7);
            Console.Write("    ((   ))");
            Console.SetCursorPosition(LeftPosition, TopPosition + 8);
            Console.Write("      ( )");
        }

        public void Remove()
        {
            while (Program.cursorInUse == true)
            {

            }

            Program.cursorInUse = true;

            Console.SetCursorPosition(LeftPosition, TopPosition);
            Console.Write("                ");
            Console.SetCursorPosition(LeftPosition, TopPosition + 1);
            Console.Write("                ");
            Console.SetCursorPosition(LeftPosition, TopPosition + 2);
            Console.Write("                ");
            Console.SetCursorPosition(LeftPosition, TopPosition + 3);
            Console.Write("                ");
            Console.SetCursorPosition(LeftPosition, TopPosition + 4);
            Console.Write("                ");
            Console.SetCursorPosition(LeftPosition, TopPosition + 5);
            Console.Write("                ");
            Console.SetCursorPosition(LeftPosition, TopPosition + 6);
            Console.Write("                ");
            Console.SetCursorPosition(LeftPosition, TopPosition + 7);
            Console.Write("                ");
            Console.SetCursorPosition(LeftPosition, TopPosition + 8);
            Console.Write("                ");

            Program.cursorInUse = false;
        }

        private async Task SpawnInterceptor()
        {
            await Task.Run(() =>
            {
                Stopwatch stopwatch = new Stopwatch();

                int interceptorCount = 0;
                bool spawnLeft = true;

                while (IsAlive && interceptorCount < MaxInterceptorCount)
                {
                    stopwatch.Start();

                    if (stopwatch.ElapsedMilliseconds == 5000 && spawnLeft)
                    {
                        Interceptor interceptor = new Interceptor(this.LeftPosition, TopPosition + 10);
                        interceptorCount++;
                        stopwatch.Reset();
                        spawnLeft = false;
                    }

                    if (stopwatch.ElapsedMilliseconds == 5000 && !spawnLeft)
                    {
                        Interceptor interceptor = new Interceptor(this.LeftPosition + 11, TopPosition + 10);
                        interceptorCount++;
                        stopwatch.Reset();
                        spawnLeft = true;
                    }

                }

                stopwatch.Stop();
            });
        }
    }
}
