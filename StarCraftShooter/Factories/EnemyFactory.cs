using StarCraftShooter.EnemyUnits;
using StarCraftShooter.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCraftShooter
{
    public class EnemyFactory : IEnemyFactory
    {
        private IEnemyUnitsManager _enemyUnitsManager;

        public EnemyFactory(IEnemyUnitsManager enemyUnitsManager)
        {
            _enemyUnitsManager = enemyUnitsManager;
        }

        //maybe refactor to use Generic type T? eg GetEnemies<List<T>> to avoid big switch statement
        public void GenerateEnemies(List<EnemyFactoryRequest> enemyFactoryRequests)
        {
            foreach (var request in enemyFactoryRequests)
            {
                switch (request.EnemyEnum)
                {
                    case EnemyRequestEnum.Scout:
                        if (request.SpawnLane != null)
                        {
                            _enemyUnitsManager.AddUnitToEnemiesList(new Scout(request.SpawnLane));
                        }
                        else
                        {
                            _enemyUnitsManager.AddUnitToEnemiesList(new Scout(request.LeftPosition, request.TopPosition));
                        }
                        break;
                }
            }
        }
    }
}
