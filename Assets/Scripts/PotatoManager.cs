using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotatoManager : MonoBehaviour
{
    public int potatoes;
    [SerializeField] private GameObject potatoCounter;

    void Start()
    {
        potatoes = 32;
        potatoCounter.GetComponent<Text>().text = potatoes.ToString();
    }

    public void increasePotatoes()
    {
        potatoes += Random.Range(16, 40);
        potatoCounter.GetComponent<Text>().text = potatoes.ToString();
    }

    public void decreasePotatoes(int amount)
    {
        potatoes -= amount;
        potatoCounter.GetComponent<Text>().text = potatoes.ToString();
    }

    public void checkWin()
    {
        if (potatoes >= 1000)
        {

        }
    }
}
