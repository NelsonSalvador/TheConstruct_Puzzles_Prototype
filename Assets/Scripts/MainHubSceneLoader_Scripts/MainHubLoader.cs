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
    private GameObject Player;
    [SerializeField]
    private GameObject SkipCinematicTextPannel;
    [SerializeField]
    private GameObject CreditsConsole;
    [SerializeField]
    private GameObject InventoryPannel;
    [SerializeField]
    private MainHubData MainHubData;
    [SerializeField]
    private GameObject Subtitles;
    [SerializeField]
    private GameObject CinematicObjects;
    [SerializeField]
    private PlayableDirector CinematicTimeLine;
    [SerializeField]
    private GameObject HologramProjector;
    [SerializeField]
    private Animator WallExplosion;
    [SerializeField]
    private GameObject Tutorial;
    [SerializeField]
    private Interactive Orb1;
    [SerializeField]
    private Interactive Orb2;
    [SerializeField]
    private Interactive Orb3;


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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (MainHubData.level == 0))
        {
            SkipCinematic();
        }
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
            Orb1.isActive = false;
            SkipCinematicTextPannel.SetActive(false);
            WallExplosion.SetBool("Rotate", true);
            Tutorial.SetActive(false);
            CinematicObjects.SetActive(false);
            HologramProjector.SetActive(false);
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
            Orb2.isActive = false;
            PortalsDoor.SetTrigger("Interact");
            Door3.SetTrigger("Interact");
            TextDoor2.color = Color.green;
            ButtonLabyrints.isActive = true;
            ButtonLabInterior.GetComponent<MeshRenderer>().material = ButtonActive;
        }

        if (MainHubData.level >= 3)
        {
            Orb3.isActive = false;
            LabyrintsDoor.SetTrigger("Interact");
            Door4.SetTrigger("Interact");
            TextDoor3.color = Color.green;
            CreditsConsole.SetActive(true);
        }
    }

    /// <summary>
    /// Skips Cinematic.
    /// </summary>
    private void SkipCinematic()
    {
        CinematicTimeLine.Stop();
        WallExplosion.SetBool("Rotate", true);
        HologramProjector.SetActive(false);
        Tutorial.SetActive(true);
        Subtitles.SetActive(false);
        Player.SetActive(true);
        CinematicObjects.SetActive(false);
        InventoryPannel.SetActive(true);
        SkipCinematicTextPannel.SetActive(false);
    }

}
