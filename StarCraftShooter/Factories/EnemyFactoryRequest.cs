using StarCraftShooter.Lanes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCraftShooter.Factories
{
    public class EnemyFactoryRequest
    {
        public EnemyRequestEnum EnemyEnum { get; }
        public SpawnLanesEnum? SpawnLaneEnum { get; set; }
        public ILane SpawnLane { get; set; }
        public int LeftPosition { get; set; }
        public int TopPosition { get; set; }

        public EnemyFactoryRequest(EnemyRequestEnum EnemyEnumParam, int LeftPositionParam, int TopPositionParam)
        {
            EnemyEnum = EnemyEnumParam;
            LeftPosition = LeftPositionParam;
            TopPosition = TopPositionParam;
        }

        public EnemyFactoryRequest(EnemyRequestEnum EnemyEnumParam, SpawnLanesEnum spawnLanesEnumParam)
        {
            EnemyEnum = EnemyEnumParam;
            SpawnLaneEnum = spawnLanesEnumParam;
        }

        public EnemyFactoryRequest(EnemyRequestEnum EnemyEnumParam, ILane spawnLanesEnumParam)
        {
            EnemyEnum = EnemyEnumParam;
            SpawnLane = spawnLanesEnumParam;
        }

        //to make it easier to generate a single type of enemy across all lanes 
        public EnemyFactoryRequest(bool IsMultipleRequest,int NumberOfRows, EnemyRequestEnum EnemyEnumParam)
        {
            EnemyEnum = EnemyEnumParam;
            //add other properties to this class
        }

        //3-2 pattern request - 3 units of a, 2 units of b in alternating pattern

        //4-1 pattern request - 4 units of a, 1 unit of b where b in middle lane
    }
}
