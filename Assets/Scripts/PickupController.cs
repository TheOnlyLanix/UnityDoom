using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
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
    Dictionary<string, AudioClip> sounds;

    //TODO: SOUNDS
    public void OnCreate(Dictionary<string, PICTURES> sprites, THINGS thing, Dictionary<string, AudioClip> snds)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        this.thing = thing;
        actor = GetComponent<Actor>();
        sounds = snds;
        transform.rotation = Quaternion.Euler(0, thing.angle, 0);
        gameObject.name = actor.Name;
        audioSource = gameObject.AddComponent<AudioSource>();
        BoxCollider coll = gameObject.AddComponent<BoxCollider>();
        coll.isTrigger = true;
        coll.size = new Vector3(40, 20, 40);


        Light lightObj = new GameObject("lightObj").AddComponent<Light>();
        lightObj.transform.parent = transform;
        lightObj.transform.localPosition = new Vector3(0f, 5f, 0f);
        lightObj.intensity = 20f;
        lightObj.range = 20f;
        lightObj.cullingMask = (1 << 9);
        lightObj.cullingMask = ~lightObj.cullingMask;
        lightObj.enabled = false;

        sprObj = new GameObject("sprite");
        sprObj.transform.parent = transform;
        sprObj.transform.position = transform.position;
        sprObj.layer = 9;
        this.mesh = createPlane();
        MeshFilter mf = sprObj.AddComponent<MeshFilter>();
        mr = sprObj.AddComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Custom/DoomShader"));
        mf.mesh = this.mesh;

        stateController = new StateController(actor, sprites, audioSource, sprObj, lightObj);
        stateController.UpdateMaterial(mr.material, 1);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //if the player touches the pickup and picks it up
            if(actor.PickedUp(other.GetComponent<DoomPlayer>(), other.GetComponent<DoomPlayer>().inv))
            {
                audioSource.clip = sounds[actor.pickupSound];
                audioSource.Play();
                sprObj.GetComponent<MeshRenderer>().enabled = false;
                StartCoroutine(Destroy(audioSource.clip.length));
            }
                
        }
    }

    IEnumerator Destroy(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
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

        stateController.OverrideState(ref overrideState);
        stateController.Update();
        PICTURES texture = stateController.UpdateMaterial(mr.material, 0);
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
