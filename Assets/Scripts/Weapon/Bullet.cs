using UnityEngine;

/*
 * Bullet causes damage to enemies when triggered.
 * 
 * TODO - make separate implementation for pushing.
 */
[RequireComponent(typeof(DamageComponent))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Color bulletColor;
    public Vector3 Velocity { get; set; }

    private DamageComponent damageComponent;
    void Start()
    {
        SetObjectColor.Set(gameObject, bulletColor);
        damageComponent = GetComponent<DamageComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity * Time.deltaTime;
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            damageComponent.DealDamage(enemy);
            Rigidbody enemyRb = enemy.gameObject.GetComponent<Rigidbody>();
            Vector3 forceVector = (enemy.gameObject.transform.position - transform.position).normalized;
            enemyRb.AddForce(forceVector * 200.0f);

            DestroyBullet();
        }
    }
}
