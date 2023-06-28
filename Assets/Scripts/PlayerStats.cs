using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBarScript : MonoBehaviour
{
    public float playerInitialHealth = 100;
    public float playerInitialStamina = 100;
    private float playerHealth;
    private float playerStamina;
    public GameObject healthBar;
    public GameObject staminaBar;

    private RectTransform healthTransform;
    private RectTransform staminaTransform;
    private float healthStartingWidth;
    private float staminaStartingWidth;

    void Start()
    {
        healthTransform = healthBar.GetComponent<RectTransform>();
        healthStartingWidth = healthTransform.rect.width;

        staminaTransform = staminaBar.GetComponent<RectTransform>();
        staminaStartingWidth = staminaTransform.rect.width;

        playerHealth = playerInitialHealth;
        playerStamina = playerInitialStamina;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void changePlayerHealth(float amount)
    {
        playerHealth += amount;
        healthTransform.sizeDelta = new Vector2((healthStartingWidth / playerInitialHealth) * playerHealth, healthTransform.sizeDelta.y);
        healthTransform.position = new Vector2(- healthTransform.position.x + (((healthStartingWidth / playerInitialHealth) * playerHealth) / 2), healthTransform.position.y);
    }

    public void changePlayerStamina(float amount)
    {
        playerStamina += amount;
        staminaTransform.sizeDelta = new Vector2((staminaStartingWidth / playerInitialStamina) * playerStamina, staminaTransform.sizeDelta.y);
        staminaTransform.position = new Vector2(staminaTransform.position.x - ((staminaStartingWidth / playerInitialStamina) * playerStamina / 2), staminaTransform.position.y);
    }
}
