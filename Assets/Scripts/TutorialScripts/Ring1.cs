using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// First tutorial ring behaviour.
/// </summary>
public class Ring1 : MonoBehaviour
{

    [SerializeField]
    private GameObject ring2;

    /// <summary>
    /// Runs when player enters the collider.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ring2.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
