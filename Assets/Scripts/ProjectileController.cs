using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    float speed = 10f;
    Vector3 dir;
    Rigidbody rb;
    GameObject sprObj;
    StateController stateController;
    MeshRenderer mr;
    Transform player;

    // Set the speed and direction
    public void OnCreate (Vector3 direction, float Speed, Actor actor, Dictionary<string, PICTURES> sprites, GameObject player)
    {
        rb = gameObject.AddComponent<Rigidbody>();
        dir = direction;
        speed = Speed;
        this.player = player.transform;
        sprObj = new GameObject("sprite");
        sprObj.transform.parent = transform;
        sprObj.transform.position = transform.position;
        Mesh mesh = createPlane();
        MeshFilter mf = sprObj.AddComponent<MeshFilter>();
        mr = sprObj.AddComponent<MeshRenderer>();

        mr.material = new Material(Shader.Find("Custom/DoomShaderTransparent"));
        mf.mesh = mesh;

        stateController = new StateController(actor, sprites, null, sprObj, null);
        stateController.UpdateMaterial(mr.material, 1);
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update ()
    {

        rb.AddForce(dir * (speed*10f), ForceMode.Force);

        //the sprite will always face the player
        Quaternion sprLookRot = Quaternion.LookRotation(transform.position - player.transform.position, Vector3.up);
        Vector3 rot = sprLookRot.eulerAngles;
        rot = new Vector3(0, rot.y, 0);
        sprObj.transform.rotation = Quaternion.Euler(rot);

        // Use the state controller to set our texture according to angle from player
        Quaternion lookRot = Quaternion.LookRotation(transform.position - player.transform.position, Vector3.up);
        int angleTexIndex = pickSide(lookRot.eulerAngles.y - transform.rotation.eulerAngles.y);

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

	}

    private void OnCollisionEnter(Collision collision)
    {
        //explode?
        Destroy(gameObject);

        if(collision.gameObject.tag == "Monster")
        {
            //replace this by explosion if its a rocket
            collision.gameObject.GetComponent<ThingController>().gotHurt(20, player, true, transform.forward);
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
