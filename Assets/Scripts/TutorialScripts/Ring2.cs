using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Second Tutorial ring behaviour.
/// </summary>
public class Ring2 : MonoBehaviour
{
    [SerializeField]
    private GameObject nextRing;
    [SerializeField]
    private GameObject triggerObject;

    // Update is called once per frame
    void Update()
    {
        if (triggerObject.activeSelf == false)
        {
            nextRing.SetActive(true);
            gameObject.SetActive(false);

        }
    }
}
