using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCamera : MonoBehaviour {
    public Camera cam;
    public RenderTexture[] renderTextures;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // This is the default "passthrough" output of the camera
        Graphics.Blit(src, dest);
        // Also copy output to any additional rendertextures
        for (int i = 0; i < renderTextures.Length; i++)
        {
            Graphics.Blit(null, renderTextures[i]);
        }
    }
}
