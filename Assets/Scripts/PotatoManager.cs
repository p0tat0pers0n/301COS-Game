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
        potatoes = 0;
    }

    public void increasePotatoes()
    {
        potatoes += Random.Range(16, 40);
        potatoCounter.GetComponent<Text>().text = potatoes.ToString();
    }
}
