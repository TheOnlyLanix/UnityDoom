using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingController : MonoBehaviour {

    public int health;
    GameObject player;
    Actor actor;
    MeshRenderer mr;
    GameObject sprObj;
    public List<Texture2D> thingSprites = new List<Texture2D>();
    public bool debug;

    string currentState = "Spawn";

    //TODO: SOUNDS
    public void OnCreate(List<Texture2D> sprites, THINGS thing)
    {
        actor = GetComponent<Actor>();
        player = GameObject.Find("FPSController");
        
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
        mr.material.mainTexture = thingSprites[0];

    }

    // Update is called once per frame
    void Update ()
    {
        //the sprite will always face the player
        Quaternion sprLookRot = Quaternion.LookRotation(transform.position - player.transform.position, Vector3.up);
        Vector3 rot = sprLookRot.eulerAngles;
        rot = new Vector3(0, rot.y + 225, 0);
        sprObj.transform.rotation = Quaternion.Euler(rot);

        //This is for the object rotation, will help us know which sprite to use
        Quaternion lookRot = Quaternion.LookRotation(transform.position - player.transform.position, Vector3.up);
        float angToPlr = lookRot.eulerAngles.y;

        sidePicker(angToPlr);

        //Here we do something for each state
        foreach (State state in actor.actorStates)
        {
            if(state.type == currentState)
            {
                
            }
        }


    }

    int sidePicker(float ang)
    {
        int side = 0;
        if (ang < 202.5 && ang > 157.5)//front image
        {
            side = 1;
        }
        else if (ang < 247.5 && ang > 202.5)//frontleft image
        {
            side = 2;
        }
        else if (ang < 292.5 && ang > 247.5)//left image
        {
            side = 3;
        }
        else if (ang < 337.5 && ang > 292.5)//rearleft image
        {
            side = 4;
        }
        else if (ang > 337.5 || ang < 22.5)//rear image
        {
            side = 5;
        }
        else if (ang < 67.5 && ang > 22.5)//rear right image
        {
            side = 6;
        }
        else if (ang < 112.5 && ang > 67.5)//right image
        {
            side = 7;
        }
        else if (ang < 157.5 && ang > 112.5)//front right image
        {
            side = 8;
        }
        return side;
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
