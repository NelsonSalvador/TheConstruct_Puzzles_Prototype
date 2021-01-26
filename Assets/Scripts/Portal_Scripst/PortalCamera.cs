using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets the correct positions for render Cameras.
/// </summary>
public class PortalCamera : MonoBehaviour
{
    [SerializeField]
    private Transform playerCamera;
    [SerializeField]
    private Transform portal;
    [SerializeField]
    private Transform otherPortal;

    [SerializeField]
    private bool Growth = false;
    [SerializeField]
    private bool normal = false;

    private PlayerInteract player;

    // Start is called before the first frame update
    void Start()
    {
        // Signs the variable player
        player = FindObjectOfType<Player>().GetComponent<PlayerInteract>();    
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
    }

    /// <summary>
    /// Updates the camera position and rotation.
    /// </summary>
    private void UpdateCamera()
    {
        float angularDiff;
        Quaternion portalRotatDiff;
        Vector3 newCamDirection;

        // Player offset from the portal.
        Vector3 playerOffset = playerCamera.position - portal.position;

        // Updates the Camera position.
        if (normal)
        {
            transform.position = otherPortal.position - playerOffset;
        }
        else if (Growth && (player.playerSize == -1 || player.playerSize == 0))
        {
            transform.position = otherPortal.position + playerOffset + new Vector3(0, playerCamera.transform.position.y, 0);
        }
        else if (!Growth && (player.playerSize == 1 || player.playerSize == 0))
        {
            transform.position = otherPortal.position + playerOffset - new Vector3(0, playerCamera.transform.position.y / 2, 0);
        }
        else
        {
            transform.position = otherPortal.position + playerOffset;
        }



        // Calculates the new direction for the camera.
        angularDiff = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        portalRotatDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);
        newCamDirection = portalRotatDiff * playerCamera.forward;

        // Updates the camera rotation.
        transform.rotation = Quaternion.LookRotation(newCamDirection, Vector3.up);
    }
}
