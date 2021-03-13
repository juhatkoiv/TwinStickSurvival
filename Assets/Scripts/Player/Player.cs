using UnityEngine;
using UnityEngine.Events;

/*
 * A component class governing some Player specific logic.
 */
[RequireComponent(typeof(Gun))]
public class Player : MonoBehaviour, IDamageable
{
    public static UnityEvent<int> HitPointsChangedEvent = new UnityEvent<int>();

    [SerializeField]
    private Color playerColor;

    [SerializeField]
    private GameObject weaponGameObject;

    [SerializeField]
    private int hitPoints;
    public int HitPoints => hitPoints;

    private int currentHitPoints;
    public int CurrentHitPoints 
    { 
        get => currentHitPoints; 
        set 
        {
            if (value != currentHitPoints) 
            {
                currentHitPoints = value;
                if(HitPointsChangedEvent != null)
                    HitPointsChangedEvent.Invoke(value);
            }
        } 
    }

    private IWeapon weapon;

    void Start()
    {
        if (!weaponGameObject)
        {
            Debug.LogError("Weapon owning gameobject is not set.");
            return;
        }
        
        // TODO - if more weapons are made make a factory for weapons and ask the required weapon from the factory.
        weapon = weaponGameObject.GetComponent<Gun>();
        if (weapon == null)
        {
            Debug.LogError("Weapon Owning gameobject does not contain weapon.");
            return;
        }

        GameCamera camera = GameCamera.Get();
        camera.SetPlayerCameraOffset(this);

        SetObjectColor.Set(gameObject, playerColor);
        CurrentHitPoints = hitPoints;
    }

    void Update()
    {
        GameCamera camera = GameCamera.Get();
        camera.SyncWithPlayer(this);
    }

    public void YieldWeapon() 
    {
        if (weapon == null)
            return;

        weapon.Fire();
    }

    public bool IsDead()
    {
        return (currentHitPoints <= 0);
    }

    public void HandleDeath()
    {
        if(Game.PlayerKilledEvent != null)
            Game.PlayerKilledEvent.Invoke();
    }
}
