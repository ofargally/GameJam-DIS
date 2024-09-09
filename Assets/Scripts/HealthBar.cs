using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public PlayerController playerController;
    public Sprite[] healthBarSprites;
    //Need to drag the player object into the inspector
    private int currentHealth;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController component not found on the assigned Player prefab.");
            return;
        }
        else
        {
            currentHealth = GetPlayerHealth();
            UpdateHealthBar();
        }

    }

    // Update is called once per frame
    void Update()
    {
        int newHealth = GetPlayerHealth();
        if (newHealth < currentHealth)
        {
            currentHealth = newHealth;
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        Debug.Log("Updating health bar");
        int spriteIndex = Mathf.Clamp(healthBarSprites.Length - currentHealth, 0, healthBarSprites.Length - 1);
        spriteRenderer.sprite = healthBarSprites[spriteIndex];
    }

    private int GetPlayerHealth()
    {
        if (playerController != null)
        {
            // Assuming PlayerController has a health property
            return playerController.health;
        }
        Debug.LogError("PlayerController is not accessible.");
        return 0;
    }
}
