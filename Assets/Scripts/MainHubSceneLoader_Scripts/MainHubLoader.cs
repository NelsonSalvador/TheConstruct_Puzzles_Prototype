using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class MainHubLoader : MonoBehaviour
{
    public MainHubData MainHubData;
    public GameObject Subtitles;
    public GameObject CinematicObjects;
    public PlayableDirector CinematicTimeLine;

    public GameObject Door1Box;
    public Animator Door1;
    public Animator Door2;
    public Animator PressurePlate1;
    public TextMeshPro TextDoor1;

    // Start is called before the first frame update
    void Start()
    {
        if (MainHubData.level == 0)
        {
            CinematicTimeLine.Play();
        }

        if (MainHubData.level == 1)
        {
            CinematicObjects.SetActive(false);
            Subtitles.SetActive(false);
            Door1Box.SetActive(false);
            Door1.SetTrigger("Interact");
            Door2.SetTrigger("Interact");
            PressurePlate1.SetTrigger("Interact");
            TextDoor1.color = Color.green;
        }


    }

}
