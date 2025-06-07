using System.Threading.Tasks;

namespace StarCraftShooter.GameObjects
{
    public interface IMissileManager
    {
        Task AddMissileToEnemyMissilesList(IEnemyMissile enemyMissileParam);
        Task AddMissileToPlayerMissilesList(IPlayerMissile playerMissileParam);
        Task RemoveMissileFromEnemyMissilesList(int enemyMissileIDParam);
        Task RemoveMissileFromPlayerMissilesList(int playerMissileIDParam);
        int GetMaxPlayerMissileId();
    }
}