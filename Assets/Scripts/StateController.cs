using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateController
{
    Actor actor;
    List<PICTURES> sprites = new List<PICTURES>();
    State state;
    float time = 0;
    int infoIndex = 0;
    int sprIndex = 0;
    public bool stopped = false;
    AudioSource audioSource;
    GameObject go;
    Vector2[] StartUV = new Vector2[4];
    Vector2[] FlipUV = new Vector2[4];

    Light brightLight;

    public StateController(Actor actor, Dictionary<string, PICTURES> allSprites, AudioSource audioSource, GameObject obj, Light light)
    {
        state = actor.actorStates["Spawn"];
        this.actor = actor;
        go = obj;
        brightLight = light;
        StartUV = go.GetComponent<MeshFilter>().mesh.uv;//Original UVS
        FlipUV = go.GetComponent<MeshFilter>().mesh.uv;//Original UVS again?
        System.Array.Reverse(FlipUV);//Just kidding, flipped them

        // get a list of every sprite name possible in states
        HashSet<string> stateSprites = new HashSet<string>();
        foreach (State state in actor.actorStates.Values)
        {
            foreach (StateInfo stateInfo in state.info)
            {
                if (!stateSprites.Contains(stateInfo.spr))
                {
                    stateSprites.Add(stateInfo.spr);
                }
            }
        }

        // remember every sprite that matches a state sprite
        foreach (PICTURES sprite in allSprites.Values)
        {
            if (sprite.texture.name.Length < 4)
                continue;

            if (stateSprites.Contains(sprite.texture.name.Substring(0, 4)))
            {
                sprites.Add(sprite);
            }
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

    public void Update()
    {
        if (stopped) { return; }

        // advance the time
        float ticksPerSecond = 35f;
        time += Time.deltaTime;

        // if we've spent enough time in current sprIndex, advance sprIndex
        if (time >= state.info[infoIndex].time / ticksPerSecond)
        {
            if (state.info[infoIndex].time < 0) { return; } // if time is -1, never advance from here
            time -= state.info[infoIndex].time / ticksPerSecond;
            sprIndex++;

            // if we've run out of sprIndices, advance the infoIndex
            if (sprIndex >= state.info[infoIndex].sprInd.Length)
            {
                sprIndex = 0;
                infoIndex++;

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
            }
        }

        //functions?
        string funct = state.info[infoIndex].function;
        if ((funct != "" && funct != null))
        {
            //actor.Invoke(funct, 0);
        }

        if (state.info[infoIndex].bright)
            brightLight.enabled = true;
        else
            brightLight.enabled = false;

    }

    public PICTURES UpdateMaterial(Material mat, int side)
    {
        Mesh mesh = go.GetComponent<MeshFilter>().mesh;
        Vector2[] uvs = mesh.uv;

        // find out current state's sprite according to side
        string sprAndSide = state.info[infoIndex].sprInd[sprIndex] + side.ToString();

        foreach (PICTURES sprite in sprites)
        {
            if (sprite.texture.name.Substring(4).Contains(sprAndSide))
            {
                mat.mainTexture = sprite.texture;
                if (sprite.texture.name.Substring(4, 2) == sprAndSide)
                    uvs = StartUV;//Set the UV's to the original, unflipped UV's
                else
                    uvs = FlipUV;//Set the UV's to the flipped UV's

                mesh.uv = uvs;//Apply the uv change
                return sprite;
            }
        }

        // if we found no match, find our current state's sprite with no sides
        sprAndSide = state.info[infoIndex].sprInd[sprIndex] + "0";
        foreach (PICTURES sprite in sprites)
        {
            if (sprite.texture.name.Substring(4).Contains(sprAndSide))
            {
                mat.mainTexture = sprite.texture;
                if (sprite.texture.name.Substring(4, 2) == sprAndSide)
                    uvs = StartUV;//Set the UV's to the original, unflipped UV's
                else
                    uvs = FlipUV;//Set the UV's to the flipped UV's

                mesh.uv = uvs;//Apply the uv change
                return sprite;
            }
        }

        return null;
    }
}