namespace StarCraftShooter.GameObjects
{
    public interface IEnemyMissile
    {
        int Id { get; set; }
        int Damage { get; set; }
        Direction Direction { get; set; }
        int Height { get; set; }
        int LeftPosition { get; set; }
        int Speed { get; set; }
        int TopPosition { get; set; }
        int Width { get; set; }

        void Draw();
        bool HasHitPlayer();
        void Move();
    }
}