using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{

    GameObject player;
    THINGS thing;
    public Actor actor;
    Image img;
    public State state;
    float time = 0;
    int infoIndex = 0;
    int sprIndex = 0;
    public bool stopped = false;
    public bool debug;
    public string overrideState = ""; // TODO: remove? only for debugging
    public AudioSource audioSource;
    DoomPlayer dPlayer;
    wadReader reader;
    Canvas canvas;
    bool lowering = false;
    bool raising = false;
    float yHeight = 0;
    float shootAccuracy = 0f;

    public void OnCreate(wadReader read, Actor weap)
    {
        reader = read;
        player = GameObject.FindGameObjectWithTag("Player");
        dPlayer = player.GetComponent<DoomPlayer>();
        actor = dPlayer.currentWeapon;
        img = dPlayer.hud.Weapon;
        state = actor.actorStates["Ready"];
        actor = weap;
        canvas = GameObject.Find("DoomHUD").GetComponent<Canvas>();
        
    }

    void Update ()
    {
        if(actor != dPlayer.currentWeapon)
        {
            //first run 'deselect' state, once completed switch weapons, and run 'select' state
            string st = "Deselect";
            OverrideState(ref st);
        }

        if(lowering)
        {
            Vector2 vec = img.rectTransform.anchoredPosition;
            vec.y -= Time.deltaTime * 100f;
            img.rectTransform.anchoredPosition = vec;
            if (vec.y <= 0)
            {
                if (dPlayer.currentWeapon = dPlayer.inv.chainsaw)
                {
                    dPlayer.audS.loop = false;
                    dPlayer.audS.Stop();
                }

                lowering = false;
                actor = dPlayer.currentWeapon;
                string st = "Select";
                OverrideState(ref st);
            }
        }

        if (raising)
        {
            Vector2 vec = img.rectTransform.anchoredPosition;
            vec.y += Time.deltaTime *100f;
            img.rectTransform.anchoredPosition = vec;
            if (vec.y >= yHeight)
            {
                raising = false;
                string st = "Ready";
                OverrideState(ref st);

                if (dPlayer.currentWeapon == dPlayer.inv.chainsaw)
                    StartCoroutine(ChainsawIdle());
            }
        }

        OverrideState(ref overrideState);

        if(!lowering && !raising)
        {
            PICTURES spr = UpdateSprite(reader.newWad.sprites);
            SpritePicker();
        }

    }


    public void OverrideState(ref string stateName)
    {
        // allows you to override the current state from the Unity editor
        if (actor.actorStates.ContainsKey(stateName))
        {
            time = 0;
            infoIndex = 0;
            sprIndex = 0;
            stopped = false;
            state = actor.actorStates[stateName];
            stateName = "";
        }
    }

    void SpritePicker()
    {
        // advance the time
        float ticksPerSecond = 35f;
        time += Time.deltaTime;

        // if we've spent enough time in current sprIndex, advance sprIndex
        if (time >= state.info[infoIndex].time / ticksPerSecond)
        {
            if (state.info[infoIndex].time < 0) { return; } // if time is -1, never advance from here
            time -= state.info[infoIndex].time / ticksPerSecond;
            sprIndex++;
            //functions

            // if we've run out of sprIndices, advance the infoIndex
            if (sprIndex >= state.info[infoIndex].sprInd.Length)
            {
                sprIndex = 0;
                infoIndex++;
                dPlayer.shooting = false;
                // if we're at the last infoIndex, figure out what function we need to follow
                if (infoIndex >= state.info.Count - 1)
                {
                    string func = state.info[infoIndex].function;
                    if (func == "Loop")
                    {
                        // loop to start
                        infoIndex = 0;
                    }
                    else if (func == "Stop")
                    {
                        // stop animating completely
                        stopped = true;
                        infoIndex--;
                    }
                    else
                    {
                        string nextState = func;
                        int nextInfoIndex = 0;
                        if (func.Contains("+"))
                        {
                            // figure out which infoIndex our next state starts from
                            string[] funcSplit = func.Split('+');
                            nextState = funcSplit[0];
                            nextInfoIndex = int.Parse(funcSplit[1]);
                        }

                        if (actor.actorStates.ContainsKey(nextState))
                        {
                            // set our next state from the function variable
                            state = actor.actorStates[nextState];
                            infoIndex = nextInfoIndex;
                        }
                        else
                        {
                            // we shouldn't end up here, but it's good to cover our bases
                            infoIndex--;
                        }
                    }
                }

                //light?
            }
        }

        string funct = state.info[infoIndex].function;
        if ((funct != "" && funct != null))
        {
            if (funct == "A_Lower")
            {
                lowering = true;
            }
            else if (funct == "A_Raise")
            {
                yHeight = img.rectTransform.anchoredPosition.y;
                img.rectTransform.anchoredPosition = new Vector2(img.rectTransform.anchoredPosition.x, 0);
                raising = true;

                if (dPlayer.currentWeapon = dPlayer.inv.chainsaw)
                {
                    Debug.Log("Raising Chainsaw");
                    dPlayer.audS.clip = reader.newWad.sounds["DSSAWUP"];
                    dPlayer.audS.Play();
                }

            }
            else if (funct == "A_WeaponReady")
                A_WeaponReady();
            else if (funct == "A_ReFire")
                A_ReFire();
            else if (funct == "A_FirePistol")
                dPlayer.A_FirePistol();
            else if (funct == "A_Punch")
                dPlayer.A_Punch();
            else if (funct == "A_Saw")
                dPlayer.A_Saw();
        }
    }

    void A_WeaponReady()
    {
        if (Input.GetButton("Fire1"))
        {
            string st = "Fire";
            OverrideState(ref st);
            shootAccuracy = 0f;
            dPlayer.shootDir = dPlayer.cam.transform.forward;
        }
        //bobbing?
    }

    IEnumerator ChainsawIdle()
    { 
        if (!Input.GetButton("Fire1") && (dPlayer.currentWeapon = dPlayer.inv.chainsaw))
        {
            dPlayer.audS.clip = reader.newWad.sounds["DSSAWIDL"];
            dPlayer.audS.Play();
        }
        yield return new WaitForSeconds(0.3f);
        if (dPlayer.currentWeapon == dPlayer.inv.chainsaw)
            StartCoroutine(ChainsawIdle());
    }

    void A_ReFire()
    {
        if (Input.GetButton("Fire1"))
        {
            string st = "Fire";
            OverrideState(ref st);
            dPlayer.shooting = false;
            shootAccuracy += 0.025f;
            shootAccuracy = Mathf.Min(shootAccuracy, 0.2f);
            dPlayer.shootDir = new Vector3(dPlayer.cam.transform.forward.x + UnityEngine.Random.Range(-shootAccuracy, shootAccuracy), dPlayer.cam.transform.forward.y, dPlayer.cam.transform.forward.z + UnityEngine.Random.Range(-shootAccuracy, shootAccuracy));
        }
    }

    public PICTURES UpdateSprite(Dictionary<string, PICTURES> sprites)
    {
        string sprAndSide = state.info[infoIndex].sprInd[sprIndex] + "0";

        foreach (PICTURES sprite in sprites.Values)
        {
            if (!sprite.texture.name.Contains(state.info[infoIndex].spr))
                continue;

            if (sprite.texture.name.Substring(4).Contains(sprAndSide))
            {
                Sprite newSpr = Sprite.Create(sprite.texture, new Rect(0, 0, sprite.Width, sprite.Height), new Vector2(0, 0));
                sprite.texture.filterMode = FilterMode.Point;
                img.rectTransform.sizeDelta = new Vector2(sprite.Width, sprite.Height);
                float scaleX = (canvas.GetComponent<RectTransform>().sizeDelta.x / 320f);
                float scaleY = (canvas.GetComponent<RectTransform>().sizeDelta.y / 200f);
                img.rectTransform.localScale = new Vector2(scaleX, scaleY);
                float xOffset = Mathf.Abs(sprite.LeftOffset)*scaleX;
                float yOffset = (((sprite.Height*scaleY) - sprite.Height) + (200 - Mathf.Abs(sprite.TopOffset)));
                img.rectTransform.anchoredPosition = new Vector2(xOffset, yOffset);
                img.sprite = newSpr;//apply it to the HUD image

                return sprite;
            }
        }

        return null;
    }
}
