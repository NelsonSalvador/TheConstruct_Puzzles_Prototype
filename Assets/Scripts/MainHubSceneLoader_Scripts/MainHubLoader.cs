using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Contains all the objects that variate between levels and sets them 
/// accordingly.
/// </summary>
public class MainHubLoader : MonoBehaviour
{
    [SerializeField]
    private MainHubData MainHubData;
    [SerializeField]
    private GameObject Subtitles;
    [SerializeField]
    private GameObject CinematicObjects;
    [SerializeField]
    private PlayableDirector CinematicTimeLine;

    // Level 1 
    [SerializeField]
    private GameObject Door1Box;
    [SerializeField]
    private Animator Door1;
    [SerializeField]
    private Animator Door2;
    [SerializeField]
    private Animator PressurePlate1;
    [SerializeField]
    private TextMeshPro TextDoor1;
    [SerializeField]
    private Interactive ButtonPortals;
    [SerializeField]
    private GameObject ButtonInterior;
    [SerializeField]
    private Material ButtonActive;

    //Level 2
    [SerializeField]
    private Animator PortalsDoor;
    [SerializeField]
    private Animator Door3;
    [SerializeField]
    private TextMeshPro TextDoor2;
    [SerializeField]
    private Interactive ButtonLabyrints;
    [SerializeField]
    private GameObject ButtonLabInterior;

    //Level 3
    [SerializeField]
    private Animator LabyrintsDoor;
    [SerializeField]
    private Animator Door4;
    [SerializeField]
    private TextMeshPro TextDoor3;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMainHub();
    }
    /// <summary>
    /// Sets the main hub state depending on the current level.
    /// </summary>
    private void SetUpMainHub()
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
