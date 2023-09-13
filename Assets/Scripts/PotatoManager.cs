using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotatoManager : MonoBehaviour
{
    /// <summary>
    /// This script allows the increase and decrease of potatoes 
    /// </summary>
    public int potatoes;
    [SerializeField] private GameObject potatoCounter;

    void Start()
    {
        // Sets starting potatoes and updates the UI counter
        potatoes = 32;
        potatoCounter.GetComponent<Text>().text = potatoes.ToString();
    }

    public void increasePotatoes()
    {
        // Increases the potatoes by a random number between 16 and 40 (after harvesting the plot) and updates the UI counter
        potatoes += Random.Range(16, 40);
        potatoCounter.GetComponent<Text>().text = potatoes.ToString();
    }

    public void decreasePotatoes(int amount)
    {
        // Decreases the potatoes (usually when planting) and updates the UI counter
        potatoes -= amount;
        potatoCounter.GetComponent<Text>().text = potatoes.ToString();
    }
}
