using StarCraftShooter.EnemyUnits;
using StarCraftShooter.Factories;
using StarCraftShooter.Lanes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCraftShooter.Levels
{
    public class Level1 : ILevel
    {
        public IEnemyUnitsManager EnemyUnitsManager { get; set; }
        public IEnemyFactory EnemyFactory { get; set; }
        public Decimal EnemyStatsMultiplier { get; set; }
        public List<ILane> Lanes { get; set; } = GetLanesForLevel();
        public bool IsComplete { get; set; } = false;
        public bool Lane1IsOccupied { get; set; }
        public bool Lane2IsOccupied { get; set; }
        public bool Lane3IsOccupied { get; set; }
        public bool Lane4IsOccupied { get; set; }
        public bool Lane5IsOccupied { get; set; }
        public List<EnemyFactoryRequest> EnemyFactoryRequests { get; set; }

        public Level1(IEnemyUnitsManager enemyUnitsManagerParam, IEnemyFactory enemyFactoryParam)
        {
            EnemyUnitsManager = enemyUnitsManagerParam;
            EnemyFactory = enemyFactoryParam;
            EnemyFactoryRequests = GenerateEnemyFactoryRequests();
            AddEnemiesToEnemyUnitsManager();
        }

        //write method to add whatever enemy units you want to Manager
        public async void AddEnemiesToEnemyUnitsManager()
        {
            //call the Enemyfactory to generate enemies list to send to the Enemies Manager
            EnemyFactory.GenerateEnemies(EnemyFactoryRequests);
        }

        public async void SpawnUnits()
        {
            while (!IsComplete)
            {
                //check if lanes are occupied, if no, spawn first row of units based on Enemy Unit's designated spawn lane
                
            }
        }

        private EnemyFactoryRequest GetNextEnemyRequestForLane<TLane>(ILane lane)
        {
            if (lane is Lane1)
            {
                return EnemyFactoryRequests.First(e => e.SpawnLane is Lane1);
            }
                
            return EnemyFactoryRequests.First(e => e.SpawnLane is Lane1); //how do you account for custom lanes?
        }

        private List<EnemyFactoryRequest> GenerateEnemyFactoryRequests()
        {
            return new List<EnemyFactoryRequest>
            {
                new EnemyFactoryRequest(EnemyRequestEnum.Scout,Lanes[0]),
                new EnemyFactoryRequest(EnemyRequestEnum.Scout,Lanes[1]),
                new EnemyFactoryRequest(EnemyRequestEnum.Scout,Lanes[2]),
                new EnemyFactoryRequest(EnemyRequestEnum.Scout,Lanes[3]),
                new EnemyFactoryRequest(EnemyRequestEnum.Scout,Lanes[4])
            };
        }

        private static List<ILane> GetLanesForLevel()
        {
            return new List<ILane>
            {
                new Lane1(),
                new Lane2(),
                new Lane3(),
                new Lane4(),
                new Lane5()
            };
        }
    }
}
