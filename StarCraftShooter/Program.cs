using System;
using System.Threading;
using System.Windows.Input;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using StarCraftShooter.EnemyUnits;
using StarCraftShooter.GameObjects;

namespace StarCraftShooter
{
    class Program
    {
        //Any code/function that uses the cursor aftr game starts must refer to this var before write to console
        //code using curosr must set this var to true first, then set to false when done w/cursor
        public static bool cursorInUse = false;
        public static List<IEnemy> enemies = new List<IEnemy>();

        //code for maximizing the console screen
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;

        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);

            Console.CursorVisible = false;

            EnemyUnitsManager enemyUnitsManager = new EnemyUnitsManager();
            EnemyFactory enemyFactory = new EnemyFactory(enemyUnitsManager);
            MissileManager missileManager = new MissileManager();
            Game game = new Game(enemyUnitsManager, enemyFactory, missileManager); //this wont do anything yet 

            //all this stuff below should be moved into Game class
            Player player = new Player(50, 50,true, enemyUnitsManager, missileManager);
            player.PlayerCanMove = true;

            player.Draw();

            Stopwatch timerForMissile = new Stopwatch(); //timer 
            Stopwatch timerForPlayer = new Stopwatch(); //timer 
            int spaceBarCounter = 0; //spacebarcounter
            int movementCounter = 0;

            while (player.PlayerCanMove)
            {
                Console.CursorVisible = false;

                if (Console.KeyAvailable)
                {

                    ConsoleKeyInfo cki = Console.ReadKey(true);

                    timerForMissile.Start();
                    timerForPlayer.Start();

                    //Maybe make a method out of this...? since these two if statements are similar
                    //or maybe encapsulate this w/Player class in 1 method to abstract
                    if (cki.Key == ConsoleKey.Spacebar)
                    {
                        spaceBarCounter++;
                        if (timerForMissile.ElapsedMilliseconds <= 700 && spaceBarCounter <= 1) //only shoot if these conditions met, controls rate of fire
                        {
                            Thread thread2 = new Thread(() => player.Shoot(cki));
                            thread2.Start();
                        }
                    }

                    if (cki.Key == ConsoleKey.LeftArrow || cki.Key == ConsoleKey.RightArrow || cki.Key == ConsoleKey.UpArrow || cki.Key == ConsoleKey.DownArrow || cki.Key == ConsoleKey.A || cki.Key == ConsoleKey.W || cki.Key == ConsoleKey.D || cki.Key == ConsoleKey.S)
                    {
                        movementCounter++;
                        if (timerForPlayer.ElapsedMilliseconds <= 30 && movementCounter <= 1) //only move if these conditions met, controls speed of player
                        {
                            Thread thread1 = new Thread(() => player.Move(cki));
                            thread1.Start();
                        }
                    }
                }

                if (timerForMissile.ElapsedMilliseconds > 700)
                {
                    timerForMissile.Reset();
                    spaceBarCounter = 0;
                }

                if (timerForPlayer.ElapsedMilliseconds > 30)
                {
                    timerForPlayer.Reset();
                    movementCounter = 0;
                }
            }
        }

        public static void OriginalGameSetUp()
        {
            Player player = new Player();
            player.PlayerCanMove = true;

            player.Draw();

            Scout scout1 = new Scout(10, 5);
            Scout scout2 = new Scout(50, 5);
            Carrier carrier = new Carrier(70, 5);

            enemies.Add(scout1);
            enemies.Add(scout2);
            enemies.Add(carrier);

            Stopwatch timerForMissile = new Stopwatch(); //timer 
            Stopwatch timerForPlayer = new Stopwatch(); //timer 
            int spaceBarCounter = 0; //spacebarcounter
            int movementCounter = 0;

            while (player.PlayerCanMove)
            {
                Console.CursorVisible = false;

                if (Console.KeyAvailable)
                {

                    ConsoleKeyInfo cki = Console.ReadKey(true);

                    timerForMissile.Start();
                    timerForPlayer.Start();

                    //Maybe make a method out of this...? since these two if statements are similar
                    //or maybe encapsulate this w/Player class in 1 method to abstract
                    if (cki.Key == ConsoleKey.Spacebar)
                    {
                        spaceBarCounter++;
                        if (timerForMissile.ElapsedMilliseconds <= 700 && spaceBarCounter <= 1) //only shoot if these conditions met, controls rate of fire
                        {
                            Thread thread2 = new Thread(() => player.Shoot(cki));
                            thread2.Start();
                        }
                    }

                    if (cki.Key == ConsoleKey.LeftArrow || cki.Key == ConsoleKey.RightArrow || cki.Key == ConsoleKey.UpArrow || cki.Key == ConsoleKey.DownArrow || cki.Key == ConsoleKey.A || cki.Key == ConsoleKey.W || cki.Key == ConsoleKey.D || cki.Key == ConsoleKey.S)
                    {
                        movementCounter++;
                        if (timerForPlayer.ElapsedMilliseconds <= 30 && movementCounter <= 1) //only move if these conditions met, controls speed of player
                        {
                            Thread thread1 = new Thread(() => player.Move(cki));
                            thread1.Start();
                        }
                    }
                }

                if (timerForMissile.ElapsedMilliseconds > 700)
                {
                    timerForMissile.Reset();
                    spaceBarCounter = 0;
                }

                if (timerForPlayer.ElapsedMilliseconds > 30)
                {
                    timerForPlayer.Reset();
                    movementCounter = 0;
                }
            }
        }

        //FOR TESTING IF CURSOR IS IN USE --DELETE LATER
        public static void Wait5Seconds()
        {
            cursorInUse = true;
            Stopwatch fiveSecTimer = new Stopwatch();
            fiveSecTimer.Start();

            while (fiveSecTimer.ElapsedMilliseconds < 5000)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(fiveSecTimer.ElapsedMilliseconds.ToString());
            }
            cursorInUse = false;

            fiveSecTimer.Stop();
        }

    }

}
