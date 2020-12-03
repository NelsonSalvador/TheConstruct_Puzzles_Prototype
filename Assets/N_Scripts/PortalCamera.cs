using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffset = playerCamera.position - portal.position;
        transform.position = otherPortal.position + playerOffset;

        float angularDiff = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        Quaternion portalRotatDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);
        Vector3 newCamDirection = portalRotatDiff * playerCamera.forward;

        transform.rotation = Quaternion.LookRotation(newCamDirection, Vector3.up);
    }
}
