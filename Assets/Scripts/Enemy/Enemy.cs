using UnityEngine;

/*
 * Enemy causes damage to player and pushes the player, when collided.
 * 
 * TODO - change the implementation so that it's independent of player.
 * TODO - make separate implementation for pushing.
 */

[RequireComponent(typeof(DamageComponent))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Color enemyColor;

    [SerializeField]
    private float pushForceMagnitude;

    [SerializeField]
    private int hitPoints;
    public int HitPoints => hitPoints;

    private int currentHitPoints;
    public int CurrentHitPoints { get => currentHitPoints; set { currentHitPoints = value; } }

    private DamageComponent damageComponent;

    void Start()
    {
        currentHitPoints = hitPoints;
        SetObjectColor.Set(gameObject, enemyColor);
        damageComponent = GetComponent<DamageComponent>();
    }

    public void HandleDeath()
    {
        if(Game.EnemyKilledEvent != null)
            Game.EnemyKilledEvent.Invoke();

        currentHitPoints = hitPoints;
        SetObjectColor.Set(gameObject, enemyColor);
        gameObject.SetActive(false);
    }

    public bool IsDead()
    {
        return (currentHitPoints <= 0);
    }

    // TODO - move pushing to separate component / class
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player) 
        {
            Rigidbody playerRb = player.gameObject.GetComponent<Rigidbody>();
            playerRb.AddForce(transform.forward * pushForceMagnitude);
            damageComponent.DealDamage(player);
        }
    }
}
