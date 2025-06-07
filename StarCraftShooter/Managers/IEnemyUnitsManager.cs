using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCraftShooter.EnemyUnits
{
    public interface IEnemyUnitsManager
    {
        List<IEnemy> Enemies { get; }
        Task AddUnitsToEnemiesList(List<IEnemy> enemiesParam);
        Task AddUnitToEnemiesList(IEnemy enemyParam);
    }
}
