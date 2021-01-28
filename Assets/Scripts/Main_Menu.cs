using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Controls the main menu Buttons.
/// </summary>
public class Main_Menu : MonoBehaviour
{
    /// <summary>
    /// Loads the first scene ...
    /// </summary>
    public void PlayGrowthPuzzles()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayLabyrintPuzzles()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void PlayPortalsPuzzles()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
