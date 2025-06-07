using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCraftShooter
{
    //Eventually want to delete this dict and the Lane Enums and utilize ILane implementations
    public static class SpawnLanesDictionary
    {
        public static Dictionary<SpawnLanesEnum, int[,]> Lanes { get; } = new Dictionary<SpawnLanesEnum, int[,]>()
        {
            { SpawnLanesEnum.lane1, new int[,]{ { 10 },{ 5 } } },
            { SpawnLanesEnum.lane2, new int[,]{ { 40 },{ 5 } } },
            { SpawnLanesEnum.lane3, new int[,]{ { 70 },{ 5 } } },
            { SpawnLanesEnum.lane4, new int[,]{ { 100 },{ 5 } } },
            { SpawnLanesEnum.lane5, new int[,]{ { 130 },{ 5 } } }
        };
    }
}