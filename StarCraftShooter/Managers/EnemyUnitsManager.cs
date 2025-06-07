using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCraftShooter.EnemyUnits
{
    public class EnemyUnitsManager : IEnemyUnitsManager
    {
        public List<IEnemy> Enemies { get; private set; } = new List<IEnemy>();

        //method to add units to Enemies list (should be async and thread safe)
        public async Task AddUnitsToEnemiesList(List<IEnemy> enemiesParam)
        {
            object lockObject = new object();
            lock (lockObject)
            {
                foreach (var enemy in enemiesParam)
                {
                    Enemies.Add(enemy);
                }
            }
        }

        public async Task AddUnitToEnemiesList(IEnemy enemyParam)
        {
            object lockObject = new object();
            lock (lockObject)
            {
                Enemies.Add(enemyParam);
            }
        }

        //method to remove unit(s) from list (should be async and thread safe)

        //method to clear the Enemies list (should be async and thread safe)

        //method to redraw an enemy that's been hit - maybe use reflection instead of big switch statement? 
    }
}
