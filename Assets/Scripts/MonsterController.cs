﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController
{
    Actor actor;
    List<Texture2D> sprites = new List<Texture2D>();
    State state;
    float time = 0;
    int infoIndex = 0;
    int sprIndex = 0;
    public bool stopped = false;

    public StateController(Actor actor, List<Texture2D> allSprites)
    {
        state = actor.actorStates["Spawn"];
        this.actor = actor;

        // get a list of every sprite name possible in states
        HashSet<string> stateSprites = new HashSet<string>();
        foreach(State state in actor.actorStates.Values)
        {
            foreach(StateInfo stateInfo in state.info)
            {
                if(!stateSprites.Contains(stateInfo.spr))
                {
                    stateSprites.Add(stateInfo.spr);
                }
            }
        }

        // remember every sprite that matches a state sprite
        foreach(Texture2D sprite in allSprites)
        {
            if (stateSprites.Contains(sprite.name.Substring(0, 4)))
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
            if(sprIndex >= state.info[infoIndex].sprInd.Length)
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
    }

    public void UpdateMaterial(Material mat, int side)
    {
        // find out current state's sprite according to side
        string sprAndSide = state.info[infoIndex].sprInd[sprIndex] + side.ToString();
        foreach (Texture2D sprite in sprites)
        {
            if (sprite.name.Substring(4).Contains(sprAndSide))
            {
                mat.mainTexture = sprite;
                if (sprite.name.Substring(4, 2) == sprAndSide)
                    mat.SetTextureScale("_MainTex", new Vector2(1, 1));
                else
                    mat.SetTextureScale("_MainTex", new Vector2(-1, 1)); // if it was found as the second item in the list, flip it

                return;
            }
        }

        // if we found no match, find our current state's sprite with no sides
        sprAndSide = state.info[infoIndex].sprInd[sprIndex] + "0";
        foreach (Texture2D sprite in sprites)
        {
            if (sprite.name.Substring(4).Contains(sprAndSide))
            {
                mat.mainTexture = sprite;
                if (sprite.name.Substring(4, 2) == sprAndSide)
                    mat.SetTextureScale("_MainTex", new Vector2(1, 1));
                else
                    mat.SetTextureScale("_MainTex", new Vector2(-1, 1)); // if it was found as the second item in the list, flip it
                return;
            }
        }
    }
}

public class MonsterController : MonoBehaviour {

    public int health;
    GameObject player;
    THINGS thing;
    Actor actor;
    MeshRenderer mr;
    GameObject sprObj;
    public bool debug;
    public string overrideState = ""; // TODO: remove? only for debugging

    StateController stateController;
    
    //TODO: SOUNDS
    public void OnCreate(List<Texture2D> sprites, THINGS thing)
    {
        player = GameObject.Find("FPSController");
        this.thing = thing;
        actor = GetComponent<Actor>();
        
        CapsuleCollider collider = gameObject.AddComponent<CapsuleCollider>();

        health = actor.Health; //sets the health of the actor
        transform.rotation = Quaternion.Euler(0, 315-thing.angle, 0);

        collider.radius = actor.Radius;
        collider.height = actor.Height;
        collider.center = new Vector3(0, actor.Height / 2, 0);

        sprObj = new GameObject("sprite");
        sprObj.transform.parent = transform;
        sprObj.transform.position = transform.position;
        Mesh mesh = createPlane();
        MeshFilter mf = sprObj.AddComponent<MeshFilter>();
        mr = sprObj.AddComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Custom/DoomShader"));
        mf.mesh = mesh;

        stateController = new StateController(actor, sprites);
        stateController.UpdateMaterial(mr.material, 1);
    }

    // Update is called once per frame
    void Update ()
    {
        //the sprite will always face the player
        Quaternion sprLookRot = Quaternion.LookRotation(transform.position - player.transform.position, Vector3.up);
        Vector3 rot = sprLookRot.eulerAngles;
        rot = new Vector3(0, rot.y + 225, 0);
        sprObj.transform.rotation = Quaternion.Euler(rot);

        // Use the state controller to set our texture according to angle from player
        Quaternion lookRot = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
        int angleTexIndex = pickSide(lookRot.eulerAngles.y);
        stateController.OverrideState(ref overrideState);
        stateController.Update();
        stateController.UpdateMaterial(mr.material, angleTexIndex);
        if (stateController.stopped)
        {
            Destroy(gameObject);
        }
    }

    private int pickSide(float ang)
    {
        // DOOM has 8 frames for 8 directions
        float sideFrames = 8f;

        // offset angle by half of the angle between frames
        // this is so we change frames at angle 22.5 instead of angle 0
        float ang2 = ang + (360f / sideFrames) / 2f;

        // offset angle by -90 degrees, to make north actually north
        ang2 -= 90;

        // wrap the angle by 360 so we don't have any negative degrees
        ang2 = (thing.angle + ang2 + 360f) % 360;

        // figure out the actual side
        return 1 + (int)(ang2 * sideFrames / 360f);
    }

    Mesh createPlane()
    {
        List<Vector3> tmpVerts = new List<Vector3>();
        List<Vector2> tmpUv = new List<Vector2>();

        Mesh mesh = new Mesh();


        mesh.vertices = new Vector3[4];
        mesh.uv = new Vector2[4];
        
        // create the vertices
        tmpVerts.Add(new Vector3(-(actor.Radius/1.5f), 0, -(actor.Radius / 1.5f)));
        tmpVerts.Add(new Vector3(-(actor.Radius / 1.5f), actor.Height, -(actor.Radius / 1.5f)));
        tmpVerts.Add(new Vector3(actor.Radius / 1.5f, actor.Height, actor.Radius / 1.5f));
        tmpVerts.Add(new Vector3(actor.Radius / 1.5f, 0, actor.Radius / 1.5f));


        tmpUv.Add(new Vector2(0, 0));
        tmpUv.Add(new Vector2(0, 1));
        tmpUv.Add(new Vector2(1, 1));
        tmpUv.Add(new Vector2(1, 0));

        // set mesh data
        mesh.uv = tmpUv.ToArray();
        mesh.vertices = tmpVerts.ToArray();

        mesh.triangles = new int[6] { 2, 1, 0, 0, 3, 2 };
        mesh.normals = new Vector3[4] { transform.forward, transform.forward, transform.forward, transform.forward };
        mesh.RecalculateBounds();
        return mesh;
    }
}