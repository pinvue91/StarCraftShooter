using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using StarCraftShooter.EnemyUnits;
using StarCraftShooter.GameObjects;

namespace StarCraftShooter
{
    public class Player
    {
        public static int Health { get; set; } = 100;
        public static int LeftPosition { get; set; } = 50;
        public static int TopPosition { get; set; } = 50;
        public int Width { get; set; } = 10;
        public int Height { get; set; } = 5;
        public bool PlayerCanMove { get; set; }
        public static List<int[,]> currentPositions = new List<int[,]>();
        public IMissileManager MissileManager { get; set; }
        private IEnemyUnitsManager EnemyUnitsManager { get; set; }

        public Player()
        {

        }

        public Player(int leftPosition, int topPosition, bool playerCanMove, IEnemyUnitsManager enemyUnitsManagerParam, IMissileManager missileManagerParam)
        {
            LeftPosition = leftPosition;
            TopPosition = topPosition;
            PlayerCanMove = playerCanMove;
            EnemyUnitsManager = enemyUnitsManagerParam;
            MissileManager = missileManagerParam;
        }

        public void Draw()
        {
            //while the cursor is being used - wait until it's not before proceeding
            while (Program.cursorInUse == true)
            {

            }

            Program.cursorInUse = true;
            Console.SetCursorPosition(LeftPosition, TopPosition);
            Console.Write("    /\\");
            Console.SetCursorPosition(LeftPosition, TopPosition + 1);
            Console.Write(" T  ||  T");
            Console.SetCursorPosition(LeftPosition, TopPosition + 2);
            Console.Write("//==[]==\\\\");
            Console.SetCursorPosition(LeftPosition, TopPosition + 3);
            Console.Write("   /||\\");
            Console.SetCursorPosition(LeftPosition, TopPosition + 4);
            Console.Write("   VVVV");
            Program.cursorInUse = false;
            SetInitialPlayerPosition();
        }

        public void Move(ConsoleKeyInfo cki)
        {
            int targetLeftPosition = LeftPosition;
            int targetTopPosition = TopPosition;

            Console.SetCursorPosition(0, 0);

            if (cki.Key == ConsoleKey.LeftArrow || cki.Key == ConsoleKey.A)
            {
                targetLeftPosition = LeftPosition - 2;
                Console.MoveBufferArea(LeftPosition, TopPosition, Width, Height, targetLeftPosition, targetTopPosition);
                LeftPosition = targetLeftPosition;
            }

            if (cki.Key == ConsoleKey.RightArrow || cki.Key == ConsoleKey.D)
            {
                targetLeftPosition = LeftPosition + 2;
                Console.MoveBufferArea(LeftPosition, TopPosition, Width, Height, targetLeftPosition, targetTopPosition);
                LeftPosition = targetLeftPosition;
            }

            if (cki.Key == ConsoleKey.UpArrow || cki.Key == ConsoleKey.W)
            {
                targetTopPosition = TopPosition - 2;
                Console.MoveBufferArea(LeftPosition, TopPosition, Width, Height, targetLeftPosition, targetTopPosition);
                TopPosition = targetTopPosition;
            }

            if (cki.Key == ConsoleKey.DownArrow || cki.Key == ConsoleKey.S)
            {
                targetTopPosition = TopPosition + 2;
                Console.MoveBufferArea(LeftPosition, TopPosition, Width, Height, targetLeftPosition, targetTopPosition);
                TopPosition = targetTopPosition;
            }
            Redraw();
            UpdatePlayerPosition(cki);
        }

        public void Shoot(ConsoleKeyInfo cki)
        {
            if (cki.Key == ConsoleKey.Spacebar)
            {
                PlayerMissile leftMissle = new PlayerMissile(LeftPosition + 1, TopPosition, EnemyUnitsManager, MissileManager);
                PlayerMissile rightMissle = new PlayerMissile(LeftPosition + 8, TopPosition, EnemyUnitsManager, MissileManager);

                while (Program.cursorInUse == true)
                {

                }

                Program.cursorInUse = true;

                leftMissle.Draw();
                rightMissle.Draw();

                Program.cursorInUse = false;

                Thread leftMissile = new Thread(() => leftMissle.Move());
                leftMissile.Start();

                Thread rightMissile = new Thread(() => rightMissle.Move());
                rightMissile.Start();
            }
        }

        //instantiates the currentPositions with data
        private void SetInitialPlayerPosition()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int[,] playerPiece = new int[LeftPosition + x, TopPosition + y];
                    currentPositions.Add(playerPiece);
                }
            }
        }

        //update the current positions instead of deleting and adding new positions
        private void UpdatePlayerPosition(ConsoleKeyInfo cki)
        {
            for (int i = 0; i < currentPositions.Count; i++)
            {
                if (cki.Key == ConsoleKey.LeftArrow || cki.Key == ConsoleKey.A)
                {
                    int upperBoundLeft = currentPositions[i].GetUpperBound(0) + 1 - 2;
                    int upperBoundTop = currentPositions[i].GetUpperBound(1) + 1;
                    int[,] newPosition = new int[upperBoundLeft, upperBoundTop];
                    currentPositions[i] = newPosition;
                }
                if (cki.Key == ConsoleKey.RightArrow || cki.Key == ConsoleKey.D)
                {
                    int upperBoundLeft = currentPositions[i].GetUpperBound(0) + 1 + 2;
                    int upperBoundTop = currentPositions[i].GetUpperBound(1) + 1;
                    int[,] newPosition = new int[upperBoundLeft, upperBoundTop];
                    currentPositions[i] = newPosition;
                }
                if (cki.Key == ConsoleKey.UpArrow || cki.Key == ConsoleKey.W)
                {
                    int upperBoundLeft = currentPositions[i].GetUpperBound(0) + 1;
                    int upperBoundTop = currentPositions[i].GetUpperBound(1) + 1 - 2;
                    int[,] newPosition = new int[upperBoundLeft, upperBoundTop];
                    currentPositions[i] = newPosition;
                }
                if (cki.Key == ConsoleKey.DownArrow || cki.Key == ConsoleKey.D)
                {
                    int upperBoundLeft = currentPositions[i].GetUpperBound(0) + 1;
                    int upperBoundTop = currentPositions[i].GetUpperBound(1) + 1 + 2;
                    int[,] newPosition = new int[upperBoundLeft, upperBoundTop];
                    currentPositions[i] = newPosition;
                }
            }
        }

        //clears player screen area and redraws player when needed
        public static void Redraw()
        {
            Console.SetCursorPosition(LeftPosition, TopPosition);
            Console.Write("           ");
            Console.SetCursorPosition(LeftPosition, TopPosition + 1);
            Console.Write("           ");
            Console.SetCursorPosition(LeftPosition, TopPosition + 2);
            Console.Write("           ");
            Console.SetCursorPosition(LeftPosition, TopPosition + 3);
            Console.Write("           ");
            Console.SetCursorPosition(LeftPosition, TopPosition + 4);
            Console.Write("           ");
            Console.SetCursorPosition(LeftPosition, TopPosition);
            Console.Write("    /\\");
            Console.SetCursorPosition(LeftPosition, TopPosition + 1);
            Console.Write(" T  ||  T");
            Console.SetCursorPosition(LeftPosition, TopPosition + 2);
            Console.Write("//==[]==\\\\");
            Console.SetCursorPosition(LeftPosition, TopPosition + 3);
            Console.Write("   /||\\");
            Console.SetCursorPosition(LeftPosition, TopPosition + 4);
            Console.Write("   VVVV");
        }
    }
}
