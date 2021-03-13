using UnityEngine;
using UnityEngine.Events;

/*
 * A general class for the Game.
 * - spawns player
 * - keeps count of kills
 * - handles Quitting and some events
 */

public class Game : MonoBehaviour
{
    // TODO - find more suitable place for these
    public static UnityEvent EnemyKilledEvent = new UnityEvent();
    public static UnityEvent PlayerKilledEvent = new UnityEvent();

    private static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    [Header("Mandatory references")]
    [SerializeField]
    private GameObject playerPrototype;

    [SerializeField]
    private GameObject playerSpawnPointGameObject;

    [Header("HUD")]
    [SerializeField]
    private GameObject hudGameObject;

    private HUD hud;
    private int totalKillCount; 

    private void Awake()
    {
        if (!playerPrototype || !playerSpawnPointGameObject)
        {
            Debug.LogError("Some mandatory GameObjects are not defined. Quitting...");
            Quit();
        }

        Instantiate(playerPrototype, playerSpawnPointGameObject.transform.position, Quaternion.identity);

        EnemyKilledEvent.AddListener(OnEnemyKilled);
        PlayerKilledEvent.AddListener(OnPlayerKilled);

        hud = hudGameObject.GetComponent<HUD>();
    }

    private void OnDestroy()
    {
        EnemyKilledEvent.RemoveListener(OnEnemyKilled);
        PlayerKilledEvent.RemoveListener(OnPlayerKilled);
    }

    private void OnEnemyKilled() 
    {
        if (!hud) 
        {
            Debug.LogError("HUD misconfiger.");
            return;
        }

        totalKillCount++;
        hud.SetKillCount(totalKillCount);
    }

    private void OnPlayerKilled()
    {
        Debug.Log("YOU DIED - Score: " + totalKillCount.ToString());
        Quit();
    }
}