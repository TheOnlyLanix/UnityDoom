using UnityEngine;

[ExecuteInEditMode]
public class ManualSkyboxClear : MonoBehaviour
{

    void OnPreRender()
    {
        GL.ClearWithSkybox(true, GetComponent<Camera>());
    }
}