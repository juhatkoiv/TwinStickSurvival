
public interface IDamageable
{
    int HitPoints { get; }
    int CurrentHitPoints { get; set; }
    bool IsDead();
    void HandleDeath();
}
