//Script taken from and modified//
//http://answers.unity3d.com/questions/303337/real-time-rendering-in-cubemap-in-image-effect.html


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{

    public RenderTexture m_RenderTexture = null;
    public bool m_OneFacePerFrame = false;
    public Camera m_DummyCam = null;
    Material newMat;

    void OnDisable()
    {
        if (m_RenderTexture)
        {
            DestroyImmediate(m_RenderTexture);
            m_RenderTexture = null;
        }
    }

    void Start()
    {
        // render all six faces at startup
        UpdateCubemap(63);
        newMat = new Material(Shader.Find("Skybox/Cubemap"));
        newMat.name = "Skybox Material";

    }

    public void ChangeSky()
    {
        UpdateCubemap();
    }

    void UpdateCubemap(int inFaceMask)
    {
        if (!m_RenderTexture)
        {
            m_RenderTexture = new RenderTexture(1024, 1024, 24);
            m_RenderTexture.dimension = UnityEngine.Rendering.TextureDimension.Cube;
            m_RenderTexture.useMipMap = false;
            m_RenderTexture.Create();

        }

        m_DummyCam.transform.position = transform.position;
        m_DummyCam.transform.rotation = transform.rotation;
        m_DummyCam.RenderToCubemap(m_RenderTexture, inFaceMask);

    }

    void UpdateCubemap()
    {
        if (m_OneFacePerFrame)
        {
            int aFaceToRender = Time.frameCount % 6;
            int aFaceMask = 1 << aFaceToRender;
            UpdateCubemap(aFaceMask);
        }
        else
        {
            UpdateCubemap(63); // all six faces
        }
        newMat.SetTexture("_Tex", m_RenderTexture);
        RenderSettings.skybox = newMat;
    }

}
