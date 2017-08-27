using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    public int health;
    GameObject player;
    THINGS thing;
    Actor actor;
    MeshRenderer mr;
    Mesh mesh;
    GameObject sprObj;
    public bool debug;
    public string overrideState = ""; // TODO: remove? only for debugging
    AudioSource audioSource;
    StateController stateController;
    Dictionary<string, AudioClip> usedSounds = new Dictionary<string, AudioClip>();

    //TODO: SOUNDS
    public void OnCreate(List<PICTURES> sprites, THINGS thing, Dictionary<string, AudioClip> sounds)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        this.thing = thing;
        actor = GetComponent<Actor>();
        audioSource = gameObject.AddComponent<AudioSource>();
        CapsuleCollider collider = gameObject.AddComponent<CapsuleCollider>();

        health = actor.Health; //sets the health of the actor
        transform.rotation = Quaternion.Euler(0, thing.angle, 0);
        gameObject.name = actor.Name;
        collider.radius = actor.Radius;
        collider.height = actor.Height;
        collider.center = new Vector3(0, actor.Height / 2f, 0);

        sprObj = new GameObject("sprite");
        sprObj.transform.parent = transform;
        sprObj.transform.position = transform.position;
        this.mesh = createPlane();
        MeshFilter mf = sprObj.AddComponent<MeshFilter>();
        mr = sprObj.AddComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Custom/DoomShader"));
        mf.mesh = this.mesh;

        /*foreach (AudioClip clip in sounds.Values)//trim down the list of sounds for efficiency?
        {
            if(SoundInfo.soundInfo[actor.SeeSound] == clip.name)
            {
                usedSounds.Add(actor.SeeSound, clip);
            }
            else if (SoundInfo.soundInfo[actor.AttackSound] == clip.name)
            {
                usedSounds.Add(actor.AttackSound, clip);
            }
            else if (SoundInfo.soundInfo[actor.PainSound] == clip.name)
            {
                usedSounds.Add(actor.PainSound, clip);
            }
            else if (SoundInfo.soundInfo[actor.DeathSound] == clip.name)
            {
                usedSounds.Add(actor.DeathSound, clip);
            }
            else if (SoundInfo.soundInfo[actor.ActiveSound] == clip.name)
            {
                usedSounds.Add(actor.ActiveSound, clip);
            }
        }*/

        stateController = new StateController(actor, sprites, audioSource);
        stateController.UpdateMaterial(mr.material, 1);

    }


    // Update is called once per frame
    void Update ()
    {
        //the sprite will always face the player
        Quaternion sprLookRot = Quaternion.LookRotation(transform.position - player.transform.position, Vector3.up);
        Vector3 rot = sprLookRot.eulerAngles;
        rot = new Vector3(0, rot.y, 0);
        sprObj.transform.rotation = Quaternion.Euler(rot);

        // Use the state controller to set our texture according to angle from player
        Quaternion lookRot = Quaternion.LookRotation(transform.position - player.transform.position, Vector3.up);
        int angleTexIndex = pickSide(lookRot.eulerAngles.y);
        stateController.OverrideState(ref overrideState);
        stateController.Update();
        PICTURES texture = stateController.UpdateMaterial(mr.material, angleTexIndex);
        if (stateController.stopped)
        {
            Destroy(gameObject);
        }
        else
        {
            float tWidth = mr.material.mainTexture.width;
            float tHeight = mr.material.mainTexture.height;
            sprObj.transform.localScale = new Vector3(tWidth / 2f, tHeight, 1);
            if (texture != null)
            {
                Vector3 xOffset = sprObj.transform.rotation * Vector3.forward * (texture.LeftOffset - texture.Width / 2f);
                Vector3 yOffset = new Vector3(0, Math.Max(texture.TopOffset - texture.Height, 0), 0); // TODO: investigate why I have to use Math.Max here
                sprObj.transform.localPosition = xOffset + yOffset;
            }
        }
    }

    private int pickSide(float ang)
    {
        // DOOM has 8 frames for 8 directions
        float sideFrames = 8f;

        // offset angle by half of the angle between frames
        // this is so we change frames at angle 22.5 instead of angle 0
        float ang2 = ang - (360f / sideFrames) / 2f;

        // offset angle by -90 degrees, to make north actually north
        ang2 += 90;

        // wrap the angle by 360 so we don't have any negative degrees
        ang2 = (360f * 2f - thing.angle - ang2) % 360;

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
        tmpVerts.Add(new Vector3(-1, 0, 0));
        tmpVerts.Add(new Vector3(-1, 1, 0));
        tmpVerts.Add(new Vector3(1, 1, 0));
        tmpVerts.Add(new Vector3(1, 0, 0));


        tmpUv.Add(new Vector2(0, 0));
        tmpUv.Add(new Vector2(0, 1));
        tmpUv.Add(new Vector2(1, 1));
        tmpUv.Add(new Vector2(1, 0));

        // set mesh data
        mesh.uv = tmpUv.ToArray();
        mesh.vertices = tmpVerts.ToArray();

        mesh.triangles = new int[6] { 0, 1, 2, 2, 3, 0 };
        mesh.normals = new Vector3[4] { transform.forward, transform.forward, transform.forward, transform.forward };
        mesh.RecalculateBounds();
        return mesh;
    }
}
