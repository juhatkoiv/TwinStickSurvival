using UnityEngine;

/*
 * Behavior for a trigger box that kills.
 */
public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.HandleDeath();
        }
    }
}
