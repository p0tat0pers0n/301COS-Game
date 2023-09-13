using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Manages the win lose condition and the proximity interact with Mr Beast
    /// </summary>
    public LightingManager lightingManager;
    public PotatoManager potatoManager;
    public GameObject interactText;
    public GameObject UIFade;
    public GameObject winUI;
    public GameObject loseUI;

    private bool withinRange;
    // Start is called before the first frame update
    void Start()
    {
        withinRange = false;
    }

    private void OnMouseOver()
    {
        // Checks if the player is within 5 metres of the Mr Beast asset
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance <= 5)
            {
                withinRange = true;
                if (potatoManager.potatoes < 1000) { interactText.GetComponent<Text>().text = "I need more"; } // Sets the Mr Beast text to I need more
                if (potatoManager.potatoes >= 1000) { interactText.GetComponent<Text>().text = "Save your family"; } // Sets the Mr Beast text to Save your family
            }
        }
    }

    private void OnMouseExit()
    {
        // When the mouse leaves the interactable the proximity interact text is set to nothing
        interactText.GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (lightingManager.numberOfDays >= 10)
        {
            // Lose Condition
            UIFade.GetComponent<Image>().color = new Color32(200, 0, 0, 255); // Sets to red
            loseUI.SetActive(true);

        }

        if (Input.GetMouseButtonDown(0) && withinRange)
        {
            if (potatoManager.potatoes >= 1000)
            {
                // Win condition
                UIFade.GetComponent<Image>().color = new Color32(0, 200, 0, 255); // Sets to green
                winUI.SetActive(true);
            }
        }
    }
}
