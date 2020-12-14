using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetUp : MonoBehaviour
{
    public Camera cameraB;
    public Material cameraMatB;
    public Camera cameraA;
    public Material cameraMatA;
    public Camera cameraC;
    public Material cameraMatC;

    // Start is called before the first frame update
    void Start()
    {
        if(cameraB.targetTexture != null)
        {
            cameraB.targetTexture.Release();
        }
        if (cameraA.targetTexture != null)
        {
            cameraA.targetTexture.Release();
        }
        if (cameraC.targetTexture != null)
        {
            cameraC.targetTexture.Release();
        }
        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB.mainTexture = cameraB.targetTexture;

        cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatA.mainTexture = cameraA.targetTexture;

        cameraC.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatC.mainTexture = cameraC.targetTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
