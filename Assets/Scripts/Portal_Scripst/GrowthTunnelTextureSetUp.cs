using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets up the correct cameras to the correct textures, with the correct widht and height
/// for the growth tunnels.
/// </summary>
public class GrowthTunnelTextureSetUp : MonoBehaviour
{
    [SerializeField]
    private Camera cameraB;
    [SerializeField]
    private Material cameraMatB;
    [SerializeField]
    private Camera cameraA;
    [SerializeField]
    private Material cameraMatA;

    // Start is called before the first frame update.
    void Start()
    {
        // Releases the camera target render textures.
        if(cameraB.targetTexture != null)
        {
            cameraB.targetTexture.Release();
        }
        if (cameraA.targetTexture != null)
        {
            cameraA.targetTexture.Release();
        }

        // Creates and assigns the texture for Camera B.
        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB.mainTexture = cameraB.targetTexture;

        // Creates and assigns the texture for Camera A.
        cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatA.mainTexture = cameraA.targetTexture;
    }
}
