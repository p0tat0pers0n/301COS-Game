using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    private bool activeState;

    void Start()
    {
        activeState = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            activeState = !activeState;
            container.SetActive(activeState);
            if (activeState)
            {
                Cursor.lockState = CursorLockMode.Confined; // Lock the cursor to the window
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the window
            }
        }
    }

    public void quitGame()
    {
        // Quits the game when the quit button is pressed
        Application.Quit();
    }

    public void resumeGame()
    {
        // Sets the window to be inactive
        activeState = !activeState;
        container.SetActive(activeState);
        if (activeState)
        {
            Cursor.lockState = CursorLockMode.Confined; // Lock the cursor to the window
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the window
        }
    }
}
