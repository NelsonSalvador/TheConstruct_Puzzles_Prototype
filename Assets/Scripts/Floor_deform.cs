using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_deform : MonoBehaviour
{
    public GameObject Player;


    // Update is called once per frame
    void Update()
    {
        float distance;
        distance = Vector3.Distance(Player.transform.position, transform.position);
        if (distance > 5.0f)
        {
            transform.position = new Vector3(transform.position.x, (distance-5)/3 , transform.position.z);
            if(distance < 15.0f)
            {
                transform.localScale = new Vector3((15 - distance) * 0.1f, (15 - distance) * 0.1f, (15 - distance) * 0.1f);
            }
            else
            {
                transform.localScale = new Vector3(0, 0, 0);
            }
        }
        
    }
}
