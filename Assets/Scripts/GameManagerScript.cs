using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TimeController timeController;
    public PotatoManager potatoManager;
    public GameObject interactText;


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
                if (potatoManager.potatoes < 1000) { interactText.GetComponent<Text>().text = "I need more potatoes than that kid"; }
                if (potatoManager.potatoes >= 1000) { interactText.GetComponent<Text>().text = "Save your family"; }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (timeController.numberOfDays >= 10)
        {
            // Lose Condition
            winState = 1;
        }

        if (Input.GetMouseButtonDown(0) && withinRange)
        {
            if (potatoManager.potatoes >= 1000)
            {
                // Win condition
                winState = 2;

            }
        }
    }
}
