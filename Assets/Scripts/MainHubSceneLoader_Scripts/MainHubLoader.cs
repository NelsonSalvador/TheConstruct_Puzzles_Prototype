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

    // Level 1 
    public GameObject Door1Box;
    public Animator Door1;
    public Animator Door2;
    public Animator PressurePlate1;
    public TextMeshPro TextDoor1;
    public Interactive ButtonPortals;
    public GameObject ButtonInterior;
    public Material ButtonActive;

    //Level 2
    public Animator PortalsDoor;
    public Animator Door3;
    public TextMeshPro TextDoor2;
    public Interactive ButtonLabyrints;
    public GameObject ButtonLabInterior;

    //Level 3
    public Animator LabyrintsDoor;
    public Animator Door4;
    public TextMeshPro TextDoor3;

    // Start is called before the first frame update
    void Start()
    {
        if (MainHubData.level == 0)
        {
            CinematicTimeLine.Play();
        }

        if (MainHubData.level >= 1)
        {
            CinematicObjects.SetActive(false);
            Subtitles.SetActive(false);
            Door1Box.SetActive(false);
            Door1.SetTrigger("Interact");
            Door2.SetTrigger("Interact");
            PressurePlate1.SetTrigger("Interact");
            TextDoor1.color = Color.green;
            ButtonPortals.isActive = true;
            ButtonInterior.GetComponent<MeshRenderer>().material = ButtonActive;
        }

        if (MainHubData.level >= 2)
        {
            PortalsDoor.SetTrigger("Interact");
            Door3.SetTrigger("Interact");
            TextDoor2.color = Color.green;
            ButtonLabyrints.isActive = true;
            ButtonLabInterior.GetComponent<MeshRenderer>().material = ButtonActive;
        }

        if (MainHubData.level >= 3)
        {
            LabyrintsDoor.SetTrigger("Interact");
            Door4.SetTrigger("Interact");
            TextDoor3.color = Color.green;
            //ButtonLabyrints.isActive = true;
            //ButtonLabInterior.GetComponent<MeshRenderer>().material = ButtonActive;
        }

    }

}
