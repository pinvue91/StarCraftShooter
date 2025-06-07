using StarCraftShooter.EnemyUnits;
using StarCraftShooter.GameObjects;
using StarCraftShooter.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCraftShooter
{
    public class Game
    {
        public bool StartGame { get; set; }
        public int PlayerScore { get; set; }
        public List<ILevel> Levels { get; set; }
        public Player Player { get; set; } //make interface of Player and use that instead
        public string PlayerName { get; set; }
        public bool CursorIsInUse { get; set; }
        public bool IsPaused { get; set; } = false;
        public IEnemyUnitsManager EnemyUnitsManager { get; set; }
        public IEnemyFactory EnemyFactory { get; set; }
        public IMissileManager MissileManager { get; set; }

        public Game(IEnemyUnitsManager enemyUnitsManagerParam, IEnemyFactory enemyFactoryParam, IMissileManager missileManagerParam)
        {
            EnemyUnitsManager = enemyUnitsManagerParam;
            EnemyFactory = enemyFactoryParam;
            MissileManager = missileManagerParam;
            Run();
        }

        public void Run()
        {
            Levels = GetLevels();
        }

        private List<ILevel> GetLevels()
        {
            return new List<ILevel>()
            {
                new Level1(EnemyUnitsManager,EnemyFactory)
            };
        }
    }
}
