using UnityEngine;

/*
 * Handles damage and changes object color when damaged
 */
public class DamageComponent : MonoBehaviour
{
    public void DealDamage(IDamageable damageable) 
    {
        if (damageable == null) 
        {
            Debug.LogError("Damageable argument null.");
            return;
        }

        damageable.CurrentHitPoints--;
        if (damageable.IsDead())
            damageable.HandleDeath();

        var component = damageable as MonoBehaviour;
        if (!component)
            return;

        ChangeColorOnDamage(component);
    }

    private void ChangeColorOnDamage(MonoBehaviour component)
    {
        if (component == null)
            return; // silent on purpose

        var meshRenderer = component.gameObject.GetComponent<MeshRenderer>();
        if (!meshRenderer)
        {
            Debug.LogError("MeshRenderer missing.");
            return;
        }

        Color newColor = meshRenderer.material.color;
        newColor.r -= 0.05f;
        newColor.b -= 0.2f;
        newColor.g += 0.05f;

        SetObjectColor.Set(component.gameObject, newColor);
    }
}
