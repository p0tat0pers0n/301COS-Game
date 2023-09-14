using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBarScript : MonoBehaviour
{
    /// <summary>
    /// This script reduces and manages the player's health and stamina
    /// </summary>
    public float playerInitialHealth = 100;
    public float playerInitialStamina = 100;
    public float playerStamina;
    public GameObject healthBar;
    public GameObject staminaBar;
    public DeathScript deathScript; // Allows the player to be marked as dead when their health is <= 0

    private float playerHealth;
    private RectTransform healthTransform;
    private RectTransform staminaTransform;
    private float healthStartingWidth;
    private float staminaStartingWidth;

    void Start()
    {
        // Sets default values for calculations later 
        healthTransform = healthBar.GetComponent<RectTransform>();
        healthStartingWidth = healthTransform.rect.width;

        staminaTransform = staminaBar.GetComponent<RectTransform>();
        staminaStartingWidth = staminaTransform.rect.width;

        playerHealth = playerInitialHealth;
        playerStamina = playerInitialStamina;
    }

    public void changePlayerHealth(float amount)
    {
        // Adds the amount of player heath passed to the function
        playerHealth += amount;
        // Changes the size of the health bar to represent the amount of health that the player has
        healthTransform.sizeDelta = new Vector2((healthStartingWidth / playerInitialHealth) * playerHealth, healthTransform.sizeDelta.y);
        if (playerHealth <= 0)
        {
            // If the player has 0 or less health they die and are sent back to the respawn point
            deathScript.playerDeath();
        }
    }

    public void changePlayerStamina(float amount)
    {
        // Adds the amount of stamina passed to the function
        playerStamina += amount;
        // Changes the size of the stamina bar to represent the amount of stamina that the player has
        staminaTransform.sizeDelta = new Vector2((staminaStartingWidth / playerInitialStamina) * playerStamina, staminaTransform.sizeDelta.y);
    }
}
