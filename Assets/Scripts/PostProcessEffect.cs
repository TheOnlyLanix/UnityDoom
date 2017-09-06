using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessEffect : MonoBehaviour
{

    public float bonusTime = 0f;

    public Shader shader;

    private Material m_Material;

    bool visible = true;

    protected Material material
    {
        get
        {
            if (m_Material == null)
            {
                m_Material = new Material(shader);
                m_Material.hideFlags = HideFlags.HideAndDontSave;
            }
            return m_Material;
        }
    }

    protected void OnDisable()
    {
        if (m_Material)
        {
            DestroyImmediate(m_Material);
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (visible)
            Graphics.Blit(source, destination, material);
        else//used for flashing effect
            Graphics.Blit(source, destination, Vector2.one, Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        bonusTime -= Time.deltaTime;

        visible = true;//show the nightvision effect

        if (bonusTime < 5f)
        {
            //time is running out, flash the effect
            if ((Mathf.Round(bonusTime * 2) % 2) == 1)
            {
                visible = true;
            }
            else
                visible = false;
        }

        //no more bonus
        if (bonusTime <= 0)
            Destroy(this);
    }

}
