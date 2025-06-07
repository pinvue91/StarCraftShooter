using StarCraftShooter.EnemyUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCraftShooter.Levels
{
    public interface ILevel
    {
        IEnemyUnitsManager EnemyUnitsManager { get; set; }
        decimal EnemyStatsMultiplier { get; set; } //stretch goal
    }
}
