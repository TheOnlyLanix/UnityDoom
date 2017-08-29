using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimator : MonoBehaviour {

    public WAD wad;


    //This is a collection of all the animated textures in doom 1 and 2.
    //the string in the dictionary is the name that should be present in the map
    //the list of strings is each frame of that animated texture
    public Dictionary<string, List<string>> AnimatedFlats = new Dictionary<string, List<string>>
    {
        //Flats
        {"NUKAGE1", new List<string>{ "NUKAGE1", "NUKAGE2", "NUKAGE3"} },
        {"FWATER1", new List<string>{ "FWATER1", "FWATER2", "FWATER3", "FWATER4" } },
        {"SWATER1", new List<string>{ "SWATER1", "SWATER2", "SWATER3", "SWATER4" } },
        {"LAVA1", new List<string>{ "LAVA1", "LAVA2", "LAVA3", "LAVA4" } },
        {"BLOOD1", new List<string>{ "BLOOD1", "BLOOD2", "BLOOD3" } },
        {"RROCK05", new List<string>{ "RROCK05", "RROCK06", "RROCK07", "RROCK08" } },
        {"SLIME01", new List<string>{ "SLIME01", "SLIME02", "SLIME03", "SLIME04" } },
        {"SLIME05", new List<string>{ "SLIME05", "SLIME06", "SLIME07", "SLIME08" } },
        {"SLIME09", new List<string>{ "SLIME09", "SLIME10", "SLIME11", "SLIME12" } }
    };

    public Dictionary<string, List<string>> AnimatedTextures = new Dictionary<string, List<string>>
    {
        //Textures (Walls)
        {"BLODGR1", new List<string>{ "BLODGR1", "BLODGR2", "BLODGR3", "BLODGR4" } },
        {"BLODRIP1", new List<string>{ "BLODRIP1", "BLODRIP2", "BLODRIP3", "BLODRIP4" } },
        {"FIREBLU1", new List<string>{ "FIREBLU1", "FIREBLU2" } },
        {"FIRELAV3", new List<string>{ "FIRELAV3", "FIRELAVA" } },
        {"FIREMAG1", new List<string>{ "FIREMAG1", "FIREMAG2", "FIREMAG3" } },
        {"FIREWALA", new List<string>{ "FIREWALA", "FIREWALB", "FIREWALL" } },
        {"GSTFONT1", new List<string>{ "GSTFONT1", "GSTFONT2", "GSTFONT3" } },
        {"ROCKRED1", new List<string>{ "ROCKRED1", "ROCKRED2", "ROCKRED3" } },
        {"SLADRIP1", new List<string>{ "SLADRIP1", "SLADRIP2", "SLADRIP3" } },
        {"BFALL1", new List<string>{ "BFALL1", "BFALL2", "BFALL3", "BFALL4" } },
        {"SFALL1", new List<string>{ "SFALL1", "SFALL2", "SFALL3", "SFALL4" } },
        {"WFALL1", new List<string>{ "WFALL1", "WFALL2", "WFALL3", "WFALL4" } },
        {"DBRAIN1", new List<string>{ "DBRAIN1", "DBRAIN2", "DBRAIN3", "DBRAIN4" } }
    };

    MeshRenderer rend;
    

    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<MeshRenderer>();
        //if somehow this gameObject doesnt have a mesh renderer, there is no need to animate
        if (rend == null)
            Destroy(this);

        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {

        foreach(Material mat in rend.materials)
        {
            foreach (string str1 in AnimatedTextures.Keys)
            {

                if (AnimatedTextures[str1].Contains(mat.mainTexture.name))
                {
                    int index = AnimatedTextures[str1].IndexOf(mat.mainTexture.name)+1;

                    if (index == AnimatedTextures[str1].Count)
                        index = 0;

                        mat.mainTexture = wad.textures[AnimatedTextures[str1][index]].mainTexture;//set texture to frame


                }
            }

            foreach (string str1 in AnimatedFlats.Keys)
            {
                if (AnimatedFlats[str1].Contains(mat.mainTexture.name))
                {
                    int index = AnimatedFlats[str1].IndexOf(mat.mainTexture.name) + 1;

                    if (index == AnimatedFlats[str1].Count)
                        index = 0;

                    mat.mainTexture = wad.flats[AnimatedFlats[str1][index]].mainTexture;//set texture to frame


                }
            }
        }

        yield return new WaitForSeconds(0.33f);
        StartCoroutine(Animate());
    }




}
