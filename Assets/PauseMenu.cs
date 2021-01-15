using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool pause = false;
    public GameObject PauseMenuCanvas;

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

    public void MainMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        FindObjectOfType<Player>().GetComponent<Player>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuCanvas.SetActive(false);
        //Time.timeScale = 1;
        pause = false;
    }

    void Pause()
    {
        FindObjectOfType<Player>().GetComponent<Player>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PauseMenuCanvas.SetActive(true);
        //Time.timeScale = 0;
        pause = true;
    }
}
