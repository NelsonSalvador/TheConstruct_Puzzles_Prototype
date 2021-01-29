using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    public int loadLevel = 0;
    private CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (loadLevel == 1)
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

        if (loadLevel == -1)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                cc.enabled = false;
                transform.position = new Vector3 (-18,0.9f, 0);
                cc.enabled = true;
            }
        }

        if (loadLevel == 2)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                cc.enabled = false;
                transform.position = new Vector3 (-18,0.9f, 0);
                cc.enabled = true;
            }
        }
    }
}
