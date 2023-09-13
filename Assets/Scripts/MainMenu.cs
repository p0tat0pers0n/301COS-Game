using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Changes the scene from main menu to the game or quits the game depending on the button press
    /// </summary>
    public void playGame()
    {
        // Loads the next scene in the build queue (The game)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        // Quits the game
        Application.Quit();
    }
}