using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform teleportPoint;

    public Transform renderPlane;

    private bool playerIsOverlapping = false;

    // Update is called once per frame
    void Update()
    {

        Debug.Log(teleportPoint.position);
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;

            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if(dotProduct < 0f)
            {
                CharacterController cc = player.GetComponent<CharacterController>();
                cc.enabled = false;
                
                float rotationDiff = -Quaternion.Angle(transform.rotation, teleportPoint.rotation);
                //rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;

                Vector3 portalOffsets = new Vector3 (transform.position.x - renderPlane.position.x, 0, 0);
                player.position = teleportPoint.position + positionOffset + portalOffsets;

                cc.enabled = true;
                playerIsOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }
}
