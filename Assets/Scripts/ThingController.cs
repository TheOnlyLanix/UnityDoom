using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingController : MonoBehaviour
{

    public int health;
    GameObject player;
    THINGS thing;
    Actor actor;
    MeshRenderer mr;
    Mesh mesh;
    GameObject sprObj;
    public bool debug;
    public string overrideState = ""; // TODO: remove? only for debugging
    public AudioSource audioSource;
    StateController stateController;
    Dictionary<string, AudioClip> usedSounds = new Dictionary<string, AudioClip>();
    Transform target;
    CharacterController cc;
    bool walking = false;
    Vector3 walkDir;
    float walkTime = 0f;
    float attackTimer = 0f;
    //TODO: SOUNDS
    public void OnCreate(Dictionary<string, PICTURES> sprites, THINGS thing, Dictionary<string, AudioClip> sounds)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        this.thing = thing;
        actor = GetComponent<Actor>();
        health = actor.Health; //sets the health of the actor
        transform.rotation = Quaternion.Euler(0, 360f - (thing.angle - 90), 0);
        gameObject.name = actor.Name;
        target = player.transform;//this can change if the thing gets hurt by something other than the player
        cc = GetComponent<CharacterController>();

        //place the actor on the ceiling if it has the SPAWNCEILING flag set
        if (actor.SPAWNCEILING)
        {
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(transform.position.x, -1000, transform.position.z), Vector3.up, out hit))
            {
                transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
        }

        if (actor.SOLID && !cc)//if the actor isnt solid, it shouldnt have a collider
        {
            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            collider.size = new Vector3(actor.Radius * 2, actor.Height, actor.Radius * 2);
            collider.center = new Vector3(0, actor.Height / 2f, 0);
        }

        //if its a monster essentially
        if (cc)
        {
            cc.radius = actor.Radius;
            cc.height = actor.Height;
            cc.center = new Vector3(0, actor.Height / 2, 0);
            cc.stepOffset = 24;
        }

        sprObj = new GameObject("sprite");
        sprObj.transform.parent = transform;
        sprObj.transform.position = transform.position;
        this.mesh = createPlane();
        MeshFilter mf = sprObj.AddComponent<MeshFilter>();
        mr = sprObj.AddComponent<MeshRenderer>();

        mr.material = new Material(Shader.Find("Custom/DoomShaderTransparent"));
        mf.mesh = this.mesh;

        Light lightObj = new GameObject("lightObj").AddComponent<Light>();
        lightObj.transform.parent = transform;
        lightObj.gameObject.transform.localPosition = Vector3.zero;

        AddSounds(sounds);

        stateController = new StateController(actor, sprites, audioSource, sprObj, lightObj);
        stateController.UpdateMaterial(mr.material, 1);

    }

    public Transform GetTarget()
    {
        return target;
    }

    // Update is called once per frame
    void Update()
    {
        //the sprite will always face the player
        Quaternion sprLookRot = Quaternion.LookRotation(transform.position - player.transform.position, Vector3.up);
        Vector3 rot = sprLookRot.eulerAngles;
        rot = new Vector3(0, rot.y, 0);
        sprObj.transform.rotation = Quaternion.Euler(rot);

        // Use the state controller to set our texture according to angle from player
        Quaternion lookRot = Quaternion.LookRotation(transform.position - player.transform.position, Vector3.up);
        int angleTexIndex = pickSide(lookRot.eulerAngles.y - transform.rotation.eulerAngles.y);

        stateController.OverrideState(ref overrideState);
        stateController.Update();

        PICTURES texture = stateController.UpdateMaterial(mr.material, angleTexIndex);

        if (stateController.stopped)
            Destroy(gameObject);
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

        if (attackTimer >= 0)
            attackTimer -= Time.deltaTime;

    }

    void AddSounds(Dictionary<string, AudioClip> sounds)
    {
        if (actor.Sounds.Count == 0)//only create the audio source if the thing has sounds defined
            return;

        audioSource = gameObject.AddComponent<AudioSource>();

        foreach (AudioClip clip in sounds.Values)//trim down the list of sounds for efficiency?
        {
            foreach (string s in actor.Sounds.Keys)
            {
                if (SoundInfo.soundInfo[actor.Sounds[s]].ToUpper().Contains(clip.name))
                    usedSounds.Add(s, clip);
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
        //ang2 += 90;
        ang2 += 180;

        // wrap the angle by 360 so we don't have any negative degrees
        //ang2 = (360f * 2f - thing.angle - ang2) % 360;
        ang2 = (360f * 2f - ang2) % 360;

        // figure out the actual side
        return 1 + (int)(ang2 * sideFrames / 360f);
    }

    public void A_Look()
    {
        //Looks for the target
        //need LOS and front 180 degrees of thing to see the target
        //Default target is player

        //TODO: finish this?
        if (!Physics.Linecast(transform.position + new Vector3(0, actor.Height / 2, 0), target.position + new Vector3(0, actor.Height / 2, 0), ~(3 << 8)) &&
            Quaternion.Angle(transform.rotation, sprObj.transform.rotation) > 90f)
        {
            stateController.state = actor.actorStates["See"];//if we 'see' the target, change the state
            PlaySound("SeeSound");
        }
    }

    public void A_Chase()
    {
        if (!cc)
            return;

        //Goto(melee), or create LOS(ranged), with the target
        //Need target, dont need LOS.

        //if there is LOS
        if (!Physics.Linecast(transform.position + new Vector3(0, actor.Height / 2, 0), target.position + new Vector3(0, actor.Height / 2, 0), ~(3 << 8)) &&
            actor.actorStates.ContainsKey("Missile") && attackTimer <= 0)
        {
            //melee if the monster can
            if (actor.actorStates.ContainsKey("Melee") && Vector3.Distance(transform.position, target.transform.position) < 30)
            {
                //change states - Melee
                string st = "Melee";
                stateController.OverrideState(ref st);
                attackTimer = 2f;
            }
            else if (actor.actorStates.ContainsKey("Missile"))//otherwise missile
            {
                //change states - Missile
                string st = "Missile";
                stateController.OverrideState(ref st);
                attackTimer = Mathf.Clamp(100/Vector3.Distance(transform.position, target.transform.position), 2, 8);

            }
        }
        else if (!Physics.Linecast(transform.position + new Vector3(0, actor.Height / 2, 0), target.position + new Vector3(0, actor.Height / 2, 0), ~(3 << 8)) &&
            actor.actorStates.ContainsKey("Melee") && Vector3.Distance(transform.position, target.transform.position) < 30 && attackTimer <= 0)//LOS, Melee State, and Within range of Melee Attack
        {
            //change states - Melee
            string st = "Melee";
            stateController.OverrideState(ref st);
            attackTimer = 2f;
        }
        else//if there isnt LOS, or distance(for melee)..
        {
            if (!walking)
            {
                walkTime = 1f;
                walking = true;
                WalkAround();
                transform.rotation = Quaternion.LookRotation(walkDir, transform.up);
            }
            else
            {
                walkTime -= Time.deltaTime;
                cc.SimpleMove(walkDir);
                Debug.DrawRay(transform.position + new Vector3(0, 25, 0), walkDir, Color.red);

                if (walkTime <= 0)
                    walking = false;
            }
        }

    }

    public void A_FaceTarget()
    {
        transform.rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
    }

    void WalkAround()
    {
        if (walkTime <= 0)
            return;

        //8 directions the thing can move
        List<Vector3> directions = new List<Vector3>
        {
            transform.forward, transform.right, -transform.forward, -transform.right,
            transform.forward + transform.right, transform.forward - transform.right,
            -transform.forward + transform.right, -transform.forward - transform.right
        };

        List<Vector3> likelyDirection = new List<Vector3>();
        List<Vector3> unlikelyDirection = new List<Vector3>();

        //direction is invalid if it is blocked
        for (int i = 0; i < directions.Count; i++)
        {
            if (Physics.Raycast(transform.position + new Vector3(0, 25, 0), directions[i], actor.Radius * 2))
            {
                directions.Remove(directions[i]);
                i--;
            }
        }

        //if the direction is closer to a player than the current position, add it to the likely list, otherwise, the unlikely list
        foreach(Vector3 vec in directions)
        {
            if(Vector3.Distance(transform.position + vec, target.transform.position) < Vector3.Distance(transform.position, target.transform.position))
                likelyDirection.Add(vec);
            else
                unlikelyDirection.Add(vec);
        }

        //this will pick a list, 2 of the ints will be from the likely, 1 the unlikely
        int rnd = UnityEngine.Random.Range(1, 100);

        if((rnd < 85 || unlikelyDirection.Count == 0) && likelyDirection.Count > 0)
        {
            int rnd1 = UnityEngine.Random.Range(0, likelyDirection.Count - 1);
            walkDir = likelyDirection[rnd1];
        }
        else
        {
            int rnd1 = UnityEngine.Random.Range(0, unlikelyDirection.Count - 1);
            walkDir = unlikelyDirection[rnd1];
        }

        walkDir *= actor.Speed * 5f;

    }


    void PlaySound(string sound)
    {
        audioSource.PlayOneShot(usedSounds[sound], 0.5f);
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
