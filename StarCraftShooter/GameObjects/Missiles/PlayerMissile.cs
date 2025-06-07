using StarCraftShooter.EnemyUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StarCraftShooter.GameObjects
{
    public class PlayerMissile : IPlayerMissile
    {
        public int Id { get; set; }
        public Direction Direction { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public int LeftPosition { get; set; }
        public int TopPosition { get; set; } //add conditional- if toppos = console height, remove from list stop from moving
        public int Width { get; set; } = 1;
        public int Height { get; set; } = 1;
        private IEnemyUnitsManager EnemyUnitsManager { get; set; }
        private IMissileManager MissileManager { get; set; }

        public PlayerMissile(int leftPositionParam, int topPositionParam, IEnemyUnitsManager enemyUnitsManagerParam, IMissileManager missileManagerParam, Direction direction = Direction.up, int speed = 2, int damage = 10)
        {
            Direction = direction;
            Speed = speed;
            Damage = damage;
            LeftPosition = leftPositionParam;
            TopPosition = topPositionParam;
            EnemyUnitsManager = enemyUnitsManagerParam;
            MissileManager = missileManagerParam;
            Id = MissileManager.GetMaxPlayerMissileId();
            MissileManager.AddMissileToPlayerMissilesList(this);
        }

        public void Draw()
        {
            Console.SetCursorPosition(LeftPosition, TopPosition);
            Console.Write(":");
        }

        public void Move()
        {
            int targetLeftPosition = LeftPosition;
            int targetTopPosition = 0;

            bool hitEnemy = HitEnemy();

            while (TopPosition != targetTopPosition && !hitEnemy)
            {
                if (TopPosition == 1)
                {
                    Console.MoveBufferArea(LeftPosition, TopPosition, 1, 1, targetLeftPosition, TopPosition - 1);
                }
                else
                {
                    Console.MoveBufferArea(LeftPosition, TopPosition, 1, 1, targetLeftPosition, TopPosition - 2);
                }
                TopPosition--;
                hitEnemy = HitEnemy();

                Thread.Sleep(30); //controls speed of missile, 60 is fastest speed with least player glitch - 50< will glitch player into disapearing
            }

            MissileManager.RemoveMissileFromPlayerMissilesList(Id);
        }

        public bool HitEnemy()
        {
            foreach (var enemy in EnemyUnitsManager.Enemies)
            {
                for (int i = 0; i < enemy.CurrentPositions.Count; i++)
                {
                    if (TopPosition == enemy.CurrentPositions[i].GetUpperBound(1) - 1 && LeftPosition == enemy.CurrentPositions[i].GetUpperBound(0) + 1 && enemy.IsAlive)
                    {
                        enemy.Health -= 10;

                        while (Program.cursorInUse)
                        {

                        }
                        Program.cursorInUse = true;
                        Console.SetCursorPosition(LeftPosition, TopPosition);
                        Console.Write(" ");
                        Console.SetCursorPosition(LeftPosition, TopPosition - 1);
                        Console.Write(" ");

                        //Maybe the enemy redraw should be moved into a method in EnemyUnitsManager - ALSO, send your Missile ID to EnemyUnitsManager so it can 
                        if (enemy is Scout && enemy.IsAlive)
                        {
                            var scout = (Scout)enemy;
                            scout.Redraw();
                        }
                        //There is an issue with deleting Carrier - it will glitch out game after it is killed when I uncomment this code
                        //if (enemy is Carrier && enemy.IsAlive)
                        //{
                        //    var carrier = (Carrier)enemy;
                        //    carrier.Redraw();
                        //

                        //This can go away eventually - for verifying enemy health actually decreases as missiles hit
                        Console.SetCursorPosition(0, 1);
                        Console.WriteLine($"{enemy.Health}");
                        Program.cursorInUse = false;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
