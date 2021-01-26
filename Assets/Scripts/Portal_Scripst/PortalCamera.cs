using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    public bool Growth = false;
    public bool normal = false;
    private PlayerInteract player;

    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<PlayerInteract>();    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffset = playerCamera.position - portal.position;
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

        


        float angularDiff = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        Quaternion portalRotatDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);
        Vector3 newCamDirection = portalRotatDiff * playerCamera.forward;

        transform.rotation = Quaternion.LookRotation(newCamDirection, Vector3.up);
    }
}
