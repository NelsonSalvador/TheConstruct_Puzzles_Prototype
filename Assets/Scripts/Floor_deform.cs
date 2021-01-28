using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animation for floor in labyrints, scales and moves the floor in relation to the player 
/// distance.
/// </summary>
public class Floor_deform : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;


    // Update is called once per frame
    void Update()
    {
        DeformFloor();
    }

    /// <summary>
    /// Scales and moves the floor in relation to the player.
    /// </summary>
    private void DeformFloor()
    {
        // Calculates the distance betwen the player and the floor.
        float distance;
        distance = Vector3.Distance(Player.transform.position, transform.position);

        // Scales and move the floor accordingly.
        if (distance > 5.0f && distance < 15.0f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, ((distance + 1.3f) / 3), transform.localPosition.z);
            transform.localScale = new Vector3((15 - distance) * 0.025f, (15 - distance) * 0.025f, (15 - distance) * 0.025f);

        }
        else if (distance > 15.0f)
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("ENtrou");
        }
    }
}
