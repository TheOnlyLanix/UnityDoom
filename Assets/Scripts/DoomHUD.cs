using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoomHUD : MonoBehaviour {

    public wadReader reader;
    public Image StatBar;

    private void Awake()
    {
        StatBar.sprite = reader.newWad.UIGraphics["STBAR"];
        StatBar.SetNativeSize();
        StatBar.preserveAspect = true;
    }

    private void Update()
    {
        StatBar.rectTransform.sizeDelta = new Vector2(Screen.width + 15, (Screen.width + 15) / 10);
    }
}
