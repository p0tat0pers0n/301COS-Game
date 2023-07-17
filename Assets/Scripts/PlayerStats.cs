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
    private float healthStartingPosition;
    private float staminaStartingPosition;

    void Start()
    {
        healthTransform = healthBar.GetComponent<RectTransform>();
        healthStartingWidth = healthTransform.rect.width;
        healthStartingPosition = healthTransform.position.x;

        staminaTransform = staminaBar.GetComponent<RectTransform>();
        staminaStartingWidth = staminaTransform.rect.width;
        staminaStartingPosition = staminaTransform.position.x;

        playerHealth = playerInitialHealth;
        playerStamina = playerInitialStamina;
        Debug.Log(healthStartingPosition);
        Debug.Log(((playerInitialHealth / healthStartingWidth) * playerHealth) / 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changePlayerHealth(float amount)
    {
        Debug.Log("Health Transform Position.x:" + healthTransform.position.x);
        Debug.Log("Health Size Change:" + (healthStartingWidth / playerInitialHealth) * playerHealth);
        playerHealth += amount;
        healthTransform.sizeDelta = new Vector2((healthStartingWidth / playerInitialHealth) * playerHealth, healthTransform.sizeDelta.y);
        healthTransform.position = new Vector2(healthStartingPosition - (healthStartingWidth - (healthStartingWidth / playerInitialHealth) * playerHealth)/2, healthTransform.position.y);
    }

    public void changePlayerStamina(float amount)
    {
        playerStamina += amount;
        staminaTransform.sizeDelta = new Vector2((staminaStartingWidth / playerInitialStamina) * playerStamina, staminaTransform.sizeDelta.y);
        staminaTransform.position = new Vector2(staminaTransform.position.x - ((staminaStartingWidth / playerInitialStamina) * playerStamina / 2), staminaTransform.position.y);
    }
}
