using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Third tutorial ring behaviour.
/// </summary>
public class Ring3 : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer boxTriger;

    // Update is called once per frame
    void Update()
    {
        if (boxTriger.enabled == true)
        {
            gameObject.SetActive(false);
        }
    }
}
