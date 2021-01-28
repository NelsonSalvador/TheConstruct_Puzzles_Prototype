using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the pause menu buttons.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public static bool pause = false;
    public GameObject PauseMenuCanvas;
    public GameObject SettingsMenu;
    public GameObject Inventory;

    private Canvas canvas;

    // Start is called before the first frame update
    private void Start()
    {
        canvas = Inventory.GetComponent<Canvas>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// Loads the main menu.
    /// </summary>
    public void MainMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Resumes the game.
    /// </summary>
    public void Resume()
    {
        FindObjectOfType<Player>().GetComponent<Player>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canvas.enabled = true;
        pause = false;
        PauseMenuCanvas.SetActive(false);
        SettingsMenu.SetActive(false);
        
        //Time.timeScale = 1;
        
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    void Pause()
    {
        FindObjectOfType<Player>().GetComponent<Player>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PauseMenuCanvas.SetActive(true);
        canvas.enabled = false;
        //Time.timeScale = 0;
        pause = true;
    }
}
