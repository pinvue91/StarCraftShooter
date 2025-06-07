using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StarCraftShooter.GameObjects
{
    public class InterceptorMissile : IEnemyMissile
    {
        public int Id { get; set; }
        public Direction Direction { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public int LeftPosition { get; set; }
        public int TopPosition { get; set; } //add conditional- if toppos = console height, remove from list stop from moving
        public int Width { get; set; }
        public int Height { get; set; }

        //list all player missiles to access missile position properties
        public static List<Missile> playerMissiles = new List<Missile>(); //moved to MissileManager

        //list all enemy missiles to access missile position properties
        public static List<Missile> enemyMissiles = new List<Missile>(); //moved to MissileManager

        public InterceptorMissile()
        {

        }

        public InterceptorMissile(Direction direction, int speed, int damage, int leftPosition, int topPosition, int width, int height)
        {
            Direction = direction;
            Speed = speed;
            Damage = damage;
            LeftPosition = leftPosition;
            TopPosition = topPosition;
            Width = width;
            Height = height;
        }

        public void Draw()
        {
            Console.SetCursorPosition(LeftPosition, TopPosition);
            Console.Write("\"");
        }

        public void Move()
        {
            int targetLeftPosition = LeftPosition;
            int targetTopPosition = Console.WindowHeight;

            bool hitPlayer = HasHitPlayer();

            while (TopPosition != targetTopPosition && !hitPlayer)
            {
                if (TopPosition == Console.WindowHeight - 1)
                {
                    Console.MoveBufferArea(LeftPosition, TopPosition, 1, 1, targetLeftPosition, TopPosition + 1);
                }
                else
                {
                    Console.MoveBufferArea(LeftPosition, TopPosition, 1, 1, targetLeftPosition, TopPosition + 2);
                }
                TopPosition++;
                hitPlayer = HasHitPlayer();

                //UpdateEnemyMissilePositions();

                Thread.Sleep(50); //controls speed of missile, 60 is fastest speed with least player glitch - 50< will glitch player into disapearing
            }
        }

        //eval if missile hit player
        public bool HasHitPlayer()
        {
            for (int i = 0; i < Player.currentPositions.Count; i++)
            {
                if (TopPosition == Player.currentPositions[i].GetUpperBound(1) - 1 && LeftPosition == Player.currentPositions[i].GetUpperBound(0) + 1)
                {
                    Player.Health -= 10;

                    while (Program.cursorInUse)
                    {

                    }
                    Program.cursorInUse = true;
                    Console.SetCursorPosition(LeftPosition, TopPosition);
                    Console.Write(" ");
                    Console.SetCursorPosition(LeftPosition, TopPosition + 1);
                    Console.Write(" ");
                    Player.Redraw();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine($"{Player.Health}");
                    Program.cursorInUse = false;
                    return true;
                }
            }
            return false;
        }
    }
}
