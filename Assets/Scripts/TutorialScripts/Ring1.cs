using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ring1 : MonoBehaviour
{

    [SerializeField]
    private GameObject ring2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ring2.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
