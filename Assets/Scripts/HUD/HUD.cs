using UnityEngine;
using UnityEngine.UI;

/*
 * A minimal hud to display killcount and player health.
 */
public class HUD : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreGameObject;

    [SerializeField]
    private GameObject healthGameObject;

    private Text scoreText;
    
    private Text healthText;

    private void Awake()
    {
        if (!scoreGameObject || !healthGameObject)
        {
            Debug.LogError("Did you forget to assign the required prototypes?");
            return;
        }

        scoreText = scoreGameObject.GetComponent<Text>();
        healthText = healthGameObject.GetComponent<Text>();
        SetPlayerHealth(0);
        SetKillCount(0);

        Player.HitPointsChangedEvent.AddListener(OnPlayerHitpointsChanged);
    }

    private void OnDestroy()
    {
        Player.HitPointsChangedEvent.RemoveListener(OnPlayerHitpointsChanged);
    }

    public void SetPlayerHealth(int health) 
    {
        if (!healthText)
        {
            Debug.LogError("Health text not assigned.");
            return;
        }
        healthText.text = Global.HEALTH_TEXT_PREFIX + health.ToString();
    }

    public void SetKillCount(int killCount)
    {
        if (!scoreText)
        {
            Debug.LogError("Score text not assigned.");
            return;
        }
        scoreText.text = Global.SCORE_TEXT_PREFIX + killCount.ToString();
    }

    private void OnPlayerHitpointsChanged(int value)
    {
        SetPlayerHealth(value);
    }
}
