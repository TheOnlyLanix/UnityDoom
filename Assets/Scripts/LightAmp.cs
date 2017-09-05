using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAmp : MonoBehaviour {

    public float bonusTime = 0f;
    public PostProcessEffects ppe;

    public void Enable(PostProcessEffects postprocess)
    {
        ppe = postprocess;
    }


    // Update is called once per frame
    void Update ()
    {
        if(ppe != null)
        {

            bonusTime -= Time.deltaTime;

            ppe.enabled = true;//show the nightvision effect

            if (bonusTime < 5f)
            {
                //time is running out, flash the effect
                if ((Mathf.Round(bonusTime) % 2) == 1)
                {
                    ppe.enabled = true;
                }
                else
                    ppe.enabled = false;
            }

            //no more bonus
            if (bonusTime <= 0)
                Destroy(this);
        }

    }
}
