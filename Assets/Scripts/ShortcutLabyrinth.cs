using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShortcutLabyrinth : MonoBehaviour
{
    public Transform player;

    public Transform teleportPoint1;
    public Transform teleportPoint2;
    public Transform teleportPoint3;
    public Transform teleportPoint4;

    private CharacterController cc;


    // Start is called before the first frame update
    void Start()
    {
        cc = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cc.enabled = false;
            player.transform.position = teleportPoint1.position;
            cc.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cc.enabled = false;
		    player.transform.position = teleportPoint2.position;
            cc.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
		    cc.enabled = false;
            player.transform.position = teleportPoint3.position;
            cc.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
		    cc.enabled = false;
            player.transform.position = teleportPoint4.position;
            cc.enabled = true;
        }
    }
}
