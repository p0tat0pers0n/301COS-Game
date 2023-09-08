using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LightingManager lightingManager;
    public PotatoManager potatoManager;
    public GameObject interactText;
    public GameObject UIFade;
    public GameObject winUI;
    public GameObject loseUI;

    [SerializeField] private int winState;
    private bool withinRange;
    // Start is called before the first frame update
    void Start()
    {
        winState = 0;
        withinRange = false;
    }

    private void OnMouseOver()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance <= 5)
            {
                withinRange = true;
                if (potatoManager.potatoes < 1000) { interactText.GetComponent<Text>().text = "I need more"; }
                if (potatoManager.potatoes >= 1000) { interactText.GetComponent<Text>().text = "Save your family"; }
            }
        }
    }

    private void OnMouseExit()
    {
        interactText.GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (lightingManager.numberOfDays >= 10)
        {
            // Lose Condition
            winState = 1;
            UIFade.GetComponent<Image>().color = new Color32(200, 0, 0, 255); // Sets to red
            loseUI.SetActive(true);

        }

        if (Input.GetMouseButtonDown(0) && withinRange)
        {
            if (potatoManager.potatoes >= 1000)
            {
                // Win condition
                winState = 2;
                UIFade.GetComponent<Image>().color = new Color32(0, 200, 0, 255); // Sets to green
                winUI.SetActive(true);
            }
        }
    }
}
