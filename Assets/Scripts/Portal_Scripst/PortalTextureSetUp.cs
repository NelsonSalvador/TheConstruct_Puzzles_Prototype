using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets up the correct cameras to the correct textures, with the correct widht and height
/// for the portalCubes.
/// </summary>
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
        // Releases the camera target render textures.
        if (cameraB.targetTexture != null)
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

        // Creates and signs the texture for Camera B.
        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB.mainTexture = cameraB.targetTexture;

        // Creates and signs the texture for Camera A.
        cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatA.mainTexture = cameraA.targetTexture;

        // Creates and signs the texture for Camera C.
        cameraC.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatC.mainTexture = cameraC.targetTexture;
    }
}
