using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCraftShooter.GameObjects
{
    public class MissileManager : IMissileManager
    {
        //May need to move player and enemy missiles lists to own classes where both inherit from IMissileManager

        //list all player missiles to access missile position properties
        private readonly List<IPlayerMissile> playerMissiles = new List<IPlayerMissile>();

        //list all enemy missiles to access missile position properties
        private readonly List<IEnemyMissile> enemyMissiles = new List<IEnemyMissile>();

        public async Task AddMissileToPlayerMissilesList(IPlayerMissile playerMissileParam)
        {
            object lockObject = new object();
            lock (lockObject)
            {
                //Check playerMissiles list for any existing missiles - if not, assign ID = 1 to missile. if yes, assign missile max ID + 1
                playerMissiles.Add(playerMissileParam);
            }
        }

        public async Task AddMissileToEnemyMissilesList(IEnemyMissile enemyMissileParam)
        {
            object lockObject = new object();
            lock (lockObject)
            {
                //Check enemyMissiles list for any existing missiles - if not, assign ID = 1 to missile. if yes, assign missile max ID + 1
                enemyMissiles.Add(enemyMissileParam);
            }
        }

        public async Task RemoveMissileFromPlayerMissilesList(int playerMissileIDParam)
        {
            object lockObject = new object();
            lock (lockObject)
            {
                var missileToRemove = playerMissiles.Where(m => m.Id == playerMissileIDParam).FirstOrDefault();
                playerMissiles.Remove(missileToRemove);
            }
        }

        public async Task RemoveMissileFromEnemyMissilesList(int enemyMissileIDParam)
        {
            object lockObject = new object();
            lock (lockObject)
            {
                var missileToRemove = enemyMissiles.Where(m => m.Id == enemyMissileIDParam).FirstOrDefault();
                enemyMissiles.Remove(missileToRemove);
            }
        }

        public int GetMaxPlayerMissileId()
        {
            return playerMissiles.Count == 0 ? 0 : playerMissiles.Max(m => m.Id) + 1;
        }
    }
}
