using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using StarCraftShooter.Lanes;

namespace StarCraftShooter
{
    public class Scout : IEnemy
    {
        public int Health { get; set; } = 40;
        public int LeftPosition { get; set; }
        public int TopPosition { get; set; }
        public int Width { get; set; } = 9;
        public int Height { get; set; } = 4;

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

        public Scout(int leftPosition, int topPosition)
        {
            LeftPosition = leftPosition;
            TopPosition = topPosition;

            Draw();
            Shoot();
            Move();
        }

        public Scout(SpawnLanesEnum spawnLanesEnum)
        {
            int[,] positions;
            if (!SpawnLanesDictionary.Lanes.TryGetValue(spawnLanesEnum, out positions))
            {
                SpawnLanesDictionary.Lanes.TryGetValue(SpawnLanesEnum.lane1, out positions);
            }

            LeftPosition = positions[0, 0];
            TopPosition = positions[1, 0];

            Draw();
            Shoot();
            Move();
        }

        public Scout(ILane spawnLanesEnum)
        {
            LeftPosition = spawnLanesEnum.LeftPositionStart;
            TopPosition = spawnLanesEnum.TopPositionStart;

            Draw();
            Shoot();
            Move();
        }

        public void Draw()
        {
            while (Program.cursorInUse == true)
            {

            }

            Program.cursorInUse = true;

            Console.SetCursorPosition(LeftPosition, TopPosition);
            Console.Write("   \\V/");
            Console.SetCursorPosition(LeftPosition, TopPosition + 1);
            Console.Write("___|||___");
            Console.SetCursorPosition(LeftPosition, TopPosition + 2);
            Console.Write("\\_[(_)]_/");
            Console.SetCursorPosition(LeftPosition, TopPosition + 3);
            Console.Write(" T \\_/ T");

            Program.cursorInUse = false;

            InstatiatePosition();
        }

        public async void Move()
        {
            await Task.Run(() =>
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
            });

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
            //for (int i = 0; i < currentPositions.Count; i++)
            //{
            //    currentPositions.Remove(currentPositions[i]);
            //}
            CurrentPositions = new List<int[,]>();
        }

        public async void Shoot()
        {
            await Task.Run(() =>
                {
                    while (IsAlive)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        Random random = new Random();

                        stopwatch.Start();
                        if (stopwatch.ElapsedMilliseconds % 100 == 0 && random.Next(3) == 1)
                        {
                            Missile leftMissle = new Missile(Direction.down, 2, 10, LeftPosition + 1, TopPosition + 5, 1, 1);
                            Missile rightMissle = new Missile(Direction.down, 2, 10, LeftPosition + 7, TopPosition + 5, 1, 1);

                            Missiles.Add(leftMissle);
                            Missiles.Add(rightMissle);

                            while (Program.cursorInUse == true)
                            {

                            }

                            Program.cursorInUse = true;

                            leftMissle.DrawScoutMissile();
                            rightMissle.DrawScoutMissile();

                            Program.cursorInUse = false;

                            //use async instead of threads
                            Thread leftMissile = new Thread(() => leftMissle.MoveEnemyMissile());
                            leftMissile.Start();

                            Thread rightMissile = new Thread(() => rightMissle.MoveEnemyMissile());
                            rightMissile.Start();
                        }

                        Thread.Sleep(1000);
                    }
                });
        }

        public void Redraw()
        {
            Console.SetCursorPosition(LeftPosition, TopPosition);
            Console.Write("   \\V/");
            Console.SetCursorPosition(LeftPosition, TopPosition + 1);
            Console.Write("___|||___");
            Console.SetCursorPosition(LeftPosition, TopPosition + 2);
            Console.Write("\\_[(_)]_/");
            Console.SetCursorPosition(LeftPosition, TopPosition + 3);
            Console.Write(" T \\_/ T");
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
            Console.SetCursorPosition(LeftPosition, TopPosition + 3);
            Console.Write("           ");

            Program.cursorInUse = false;
        }
    }
}
