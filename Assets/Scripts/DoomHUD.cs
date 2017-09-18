using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoomHUD : MonoBehaviour {

    public wadReader reader;
    public Image StatBar;


    //Various Images and Text displayed on the stat bar
    public Image KeyBlue;
    public Image KeyYellow;
    public Image KeyRed;
    public Text BullMax;
    public Text ShelMax;
    public Text RcktMax;
    public Text CellMax;
    public Text BullAmnt;
    public Text ShelAmnt;
    public Text RcktAmnt;
    public Text CellAmnt;
    public Text Armor;
    public Image Arms;
    public Text Arms2;
    public Text Arms3;
    public Text Arms4;
    public Text Arms5;
    public Text Arms6;
    public Text Arms7;
    public Text Health;
    public Text Ammo;
    public Image Face;
    public Image Weapon;


    //Values
    public int bullMax = 0;
    public int shelMax = 0;
    public int rcktMax = 0;
    public int cellMax = 0;
    public int bull = 0;
    public int shel = 0;
    public int rckt = 0;
    public int cell = 0;

    public int armor = 0;
    public int ammo = 0;

    public int health = 100;
    int oldHealth = 0;
    int healthInd = 0;

    Dictionary<string, Sprite> faces = new Dictionary<string, Sprite>();
    string newfaceName = "STFST00";


    private void Awake()
    {

        StartCoroutine(LateAwake());
    }

    IEnumerator LateAwake()
    {//make sure the wad has finished being read before we start looking for assets
        yield return new WaitForEndOfFrame();

        StatBar.sprite = reader.newWad.UIGraphics["STBAR"];
        StatBar.SetNativeSize();
        StatBar.preserveAspect = true;

        Arms.sprite = reader.newWad.UIGraphics["STARMS"];

        //Default font for having no weapons for the hotkey
        Arms2.font = reader.newWad.fonts["STGNUM"];
        Arms3.font = reader.newWad.fonts["STGNUM"];
        Arms4.font = reader.newWad.fonts["STGNUM"];
        Arms5.font = reader.newWad.fonts["STGNUM"];
        Arms6.font = reader.newWad.fonts["STGNUM"];
        Arms7.font = reader.newWad.fonts["STGNUM"];

        BullMax.font = reader.newWad.fonts["STYSNUM"];
        BullAmnt.font = reader.newWad.fonts["STYSNUM"];
        ShelMax.font = reader.newWad.fonts["STYSNUM"];
        ShelAmnt.font = reader.newWad.fonts["STYSNUM"];
        RcktMax.font = reader.newWad.fonts["STYSNUM"];
        RcktAmnt.font = reader.newWad.fonts["STYSNUM"];
        CellMax.font = reader.newWad.fonts["STYSNUM"];
        CellAmnt.font = reader.newWad.fonts["STYSNUM"];

        Health.font = reader.newWad.fonts["STTNUM"];
        Ammo.font = reader.newWad.fonts["STTNUM"];
        Armor.font = reader.newWad.fonts["STTNUM"];

        foreach (string str in reader.newWad.UIGraphics.Keys)
        {
            if(str.StartsWith("STF") && !str.Contains("STFB0") && !str.Contains("STFB1") && !str.Contains("STFB2") && !str.Contains("STFB3"))
            {
                faces.Add(str, reader.newWad.UIGraphics[str]);
            }
        }


        Face.sprite = faces["STFST00"];
        StartCoroutine(ChangeFace());
    }

    private void Update()
    {
        //StatBar.rectTransform.sizeDelta = new Vector2(Screen.width + 15, (Screen.width + 15) / 10);
        Health.text = health + "%";
        Ammo.text = ammo + "";
        Armor.text = armor + "%";

        BullAmnt.text = bull.ToString();
        BullMax.text = bullMax.ToString();
        ShelAmnt.text = shel.ToString();
        ShelMax.text = shelMax.ToString();
        CellAmnt.text = cell.ToString();
        CellMax.text = cellMax.ToString();
        RcktAmnt.text = rckt.ToString();
        RcktMax.text = rcktMax.ToString();


        if (health > 0 && Face.sprite)
        {
            if (health > 80)
                healthInd = 0;
            else if (health < 80 && health > 60)
                healthInd = 1;
            else if (health < 60 && health > 40)
                healthInd = 2;
            else if (health < 40 && health > 20)
                healthInd = 3;
            else if (health < 20 && health > 0)
                healthInd = 4;

            newfaceName = "STFST" + healthInd + Random.Range(0, 3);

            //If its the same as the last one, try to get a new face (variety)
            if (newfaceName == Face.sprite.name)
            {
                newfaceName = "STFST" + healthInd + Random.Range(0, 3);
            }

            if (health < oldHealth)
            {
                Face.sprite = faces["STFOUCH" + healthInd];
            }
        }
    }

    IEnumerator ChangeFace()
    {
        oldHealth = health;

        if (health <= 0)
            Face.sprite = faces["STFDEAD0"];

        if (health > 0)
        {
            if (newfaceName != Face.sprite.name)
                Face.sprite = faces[newfaceName];
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(ChangeFace());
    }
}
