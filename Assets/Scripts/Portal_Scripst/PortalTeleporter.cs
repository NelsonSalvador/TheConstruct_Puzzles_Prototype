using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform teleportPoint;

    public Transform renderPlane;

    public bool Growth = true;

    private bool playerIsOverlapping = false;

    // Update is called once per frame
    void Update()
    {

        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;

            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                CharacterController cc = player.GetComponent<CharacterController>();
                cc.enabled = false;

                float rotationDiff = -Quaternion.Angle(transform.rotation, teleportPoint.rotation);
                Debug.Log(rotationDiff);
                //rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;

                Vector3 portalOffsets = new Vector3(transform.position.x - renderPlane.position.x, 0, 0);


                if (Growth)
                {
                    if (player.GetComponent<PlayerInteract>().playerSize == 0)
                    {
                        player.transform.localScale = player.transform.localScale + new Vector3(0, player.localScale.y, 0);
                        player.position = teleportPoint.position + positionOffset + portalOffsets + new Vector3(0, player.GetComponent<CharacterController>().height / 2, 0);
                        player.GetComponent<PlayerInteract>().playerSize += 1;

                        for (int i = 0; i < player.GetComponent<PlayerInteract>()._inventorySize.Count; ++i)
                        {
                            if(player.GetComponent<PlayerInteract>()._inventorySize[i] < 1)
                            {
                                player.GetComponent<PlayerInteract>()._inventorySize[i] += 1;
                            }
                        }
                    }
                    else if (player.GetComponent<PlayerInteract>().playerSize == -1)
                    {
                        player.transform.localScale = player.transform.localScale + new Vector3(0, player.localScale.y, 0);
                        player.position = teleportPoint.position + positionOffset + portalOffsets + new Vector3(0, player.GetComponent<CharacterController>().height / 4, 0);
                        player.GetComponent<PlayerInteract>().playerSize += 1;

                        for (int i = 0; i < player.GetComponent<PlayerInteract>()._inventorySize.Count; ++i)
                        {
                            if(player.GetComponent<PlayerInteract>()._inventorySize[i] < 1)
                            {
                                player.GetComponent<PlayerInteract>()._inventorySize[i] += 1;
                            }
                        }
                    }
                    else
                    {
                        player.position = teleportPoint.position + positionOffset + portalOffsets;

                        for (int i = 0; i < player.GetComponent<PlayerInteract>()._inventorySize.Count; ++i)
                        {
                            if(player.GetComponent<PlayerInteract>()._inventorySize[i] < 1)
                            {
                                player.GetComponent<PlayerInteract>()._inventorySize[i] += 1;
                            }
                        }
                    }
                    
                }
                else
                {
                    
                    if (player.GetComponent<PlayerInteract>().playerSize == 0)
                    {
                        player.transform.localScale = player.transform.localScale - new Vector3(0, player.localScale.y / 2, 0);
                        player.position = teleportPoint.position + positionOffset + portalOffsets - new Vector3(0, player.GetComponent<CharacterController>().height / 4, 0);
                        player.GetComponent<PlayerInteract>().playerSize -= 1;

                        for (int i = 0; i < player.GetComponent<PlayerInteract>()._inventorySize.Count; ++i)
                        {
                            if(player.GetComponent<PlayerInteract>()._inventorySize[i] > -1)
                            {
                                player.GetComponent<PlayerInteract>()._inventorySize[i] -= 1;
                            }
                        }
                    }
                    else if (player.GetComponent<PlayerInteract>().playerSize == 1)
                    {
                        player.transform.localScale = player.transform.localScale - new Vector3(0, player.localScale.y / 2, 0);
                        player.position = teleportPoint.position + positionOffset + portalOffsets - new Vector3(0, player.GetComponent<CharacterController>().height / 2, 0);
                        player.GetComponent<PlayerInteract>().playerSize -= 1;

                        for (int i = 0; i < player.GetComponent<PlayerInteract>()._inventorySize.Count; ++i)
                        {
                            if(player.GetComponent<PlayerInteract>()._inventorySize[i] > -1)
                            {
                                player.GetComponent<PlayerInteract>()._inventorySize[i] -= 1;
                            }
                        }
                    }
                    else
                    {
                        player.position = teleportPoint.position + positionOffset + portalOffsets;

                        for (int i = 0; i < player.GetComponent<PlayerInteract>()._inventorySize.Count; ++i)
                        {
                            if(player.GetComponent<PlayerInteract>()._inventorySize[i] > -1)
                            {
                                player.GetComponent<PlayerInteract>()._inventorySize[i] -= 1;
                            }
                        }
                    }
                    
                }

                cc.enabled = true;
                playerIsOverlapping = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }
}