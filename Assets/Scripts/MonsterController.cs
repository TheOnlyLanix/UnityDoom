﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    public int health;
    GameObject player;
    THINGS thing;
    Actor actor;
    MeshRenderer mr;
    GameObject sprObj;
    public List<Texture2D> thingSprites = new List<Texture2D>();
    public bool debug;

    string currentState = "Spawn";

    //this will help converting the sprite index from the state from a string to a int
    Dictionary<string, int> spriteIndex = new Dictionary<string, int>
    {
        {"A", 1}, {"B", 2}, {"C", 3}, {"D", 4}, {"E", 5}, {"F", 6}, {"G", 7}, {"H", 8}, {"I", 9}, {"J", 10}, {"K", 11}, {"L", 12}, {"M", 13},
        { "N", 14}, {"O", 15}, {"P", 16}, {"Q", 17}, {"R", 18}, {"S", 19}, {"T", 20}, {"U", 21}, {"V", 22}, {"W", 23}, {"X", 24}, {"Y", 25}, {"Z", 26}
    };

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


        foreach (Texture2D spr in sprites)
        {
            char[] sprChar = spr.name.ToCharArray();

            if(spr.name.StartsWith(actor.sprite))
            {
                thingSprites.Add(spr);
            }
        }

        if (thingSprites.Count > 0)
            mr.material.mainTexture = thingSprites[0];
        else
            Debug.LogError("Could not find sprites for " + actor.name);
    }

    // Update is called once per frame
    void Update ()
    {
        //the sprite will always face the player
        Quaternion sprLookRot = Quaternion.LookRotation(transform.position - player.transform.position, Vector3.up);
        Vector3 rot = sprLookRot.eulerAngles;
        rot = new Vector3(0, rot.y + 225, 0);
        sprObj.transform.rotation = Quaternion.Euler(rot);

        // Set our texture index according to angle from player
        if (thingSprites.Count >= 4)
        {
            Quaternion lookRot = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
            int angleTexIndex = sidePicker(lookRot.eulerAngles.y);

            if (debug)
                Debug.Log(lookRot.eulerAngles.y);

            if (angleTexIndex > 4)
            {
                angleTexIndex = 8 - angleTexIndex;
                mr.material.SetTextureScale("_MainTex", new Vector2(-1, 1));
            }
            else
            {
                mr.material.SetTextureScale("_MainTex", new Vector2(1, 1));
            }
            angleTexIndex = Math.Min(angleTexIndex, thingSprites.Count);
            mr.material.mainTexture = thingSprites[angleTexIndex]; // TODO: we have to sort thingSprites in a more intelligent way, this wont work for everything
        }
    }

    int sidePicker(float ang)
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
        return (int)(ang2 * sideFrames / 360f);
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
