using StarCraftShooter.EnemyUnits;
using StarCraftShooter.Factories;
using System.Collections.Generic;

namespace StarCraftShooter
{
    public interface IEnemyFactory
    {
        void GenerateEnemies(List<EnemyFactoryRequest> enemyFactoryRequests);
    }
}