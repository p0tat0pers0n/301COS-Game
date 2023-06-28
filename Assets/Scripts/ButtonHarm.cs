using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Example : MonoBehaviour, IPointerClickHandler
{
    public GameObject player;
    public StatusBarScript sn;
    void Start ()
    {
        Debug.Log("Start runs");
        StatusBarScript sn = player.GetComponent<StatusBarScript>();
    }
    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("Clicked");
        //Use this to tell when the user left-clicks on the Button
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            sn.changePlayerHealth(-10);
        }
    }
}
