using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring3 : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer boxTriger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boxTriger.enabled == true)
        {
            gameObject.SetActive(false);
        }
    }
}
