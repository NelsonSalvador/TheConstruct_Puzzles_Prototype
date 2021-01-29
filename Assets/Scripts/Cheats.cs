using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    public int loadLevelCheats = 0;
    private CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (loadLevelCheats == 1)
        {
            if (Input.GetKeyDown("1"))
            {
                cc.enabled = false;
                transform.position = new Vector3 (-18,0.9f, 0);
                cc.enabled = true;
            }

            if (Input.GetKeyDown("2"))
            {
                cc.enabled = false;
                transform.position = new Vector3 (5, 0.9f, 0);
                cc.enabled = true;
            }
        }

        if (loadLevelCheats == -1)
        {
            if (Input.GetKeyDown("1"))
            {
                cc.enabled = false;
                transform.position = new Vector3 (-18,0.9f, 0);
                cc.enabled = true;
            }
        }

        if (loadLevelCheats == 2)
        {
            if (Input.GetKeyDown("1"))
            {
                cc.enabled = false;
                transform.position = new Vector3 (-8,3, 8);
                cc.enabled = true;
            }

            if (Input.GetKeyDown("2"))
            {
                cc.enabled = false;
                transform.position = new Vector3 (-16,20, 70);
                cc.enabled = true;
            }
            if (Input.GetKeyDown("3"))
            {
                cc.enabled = false;
                transform.position = new Vector3 (-30,10, 88);
                cc.enabled = true;
            }
        }
    }
}
