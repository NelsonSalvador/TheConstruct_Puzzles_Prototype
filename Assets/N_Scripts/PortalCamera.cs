using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    public bool Growth = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffset = playerCamera.position - portal.position;
        if (Growth)
        {
            transform.position = otherPortal.position + playerOffset + new Vector3(0, playerCamera.transform.position.y * 2, 0);
        }
        else
        {
            transform.position = otherPortal.position + playerOffset - new Vector3(0, playerCamera.transform.position.y/2, 0);
        }
        

        float angularDiff = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        Quaternion portalRotatDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);
        Vector3 newCamDirection = portalRotatDiff * playerCamera.forward;

        transform.rotation = Quaternion.LookRotation(newCamDirection, Vector3.up);
    }
}
