using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomPlayer : MonoBehaviour
{

    public int Speed = 20; //adjusted for use with unity
    public int Health = 100;
    public int Radius = 16;
    public int Height = 40; //adjusted from 56 for use with Unity
    public int Mass = 100;
    public int PainChance = 255;
    public string DisplayName = "Marine";
    public string CrouchSprite = "PLYC";
    public int direction = 0;

    Transform cam;
    Rigidbody rb;
    CharacterController cc;

    public float sensitivity = 1f;
    public float smoothing = 1f;

    float mouseInputX;
    float mouseInputY;

    Vector3 moveDir;
    public float friction = 1f;
    public float jumpHeight = 20f;
    public float downForce = 20f;

    float timer = 0.0f;
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount = 0.2f;
    public float midpoint = 2.0f;

    Vector3 curMoveDir;

    float vertical;
    float horizontal;

    public bool god = false;
    public bool noclip = false;

    void Start()
    {
        cam = transform.GetChild(0);//camera transform
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        //action = Input.GetButtonDown("Action");

        if (cc.isGrounded)//cant already be jumping
        {
            moveDir = new Vector3(horizontal, 0, vertical);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= Speed;

            curMoveDir = Vector3.Lerp(curMoveDir, moveDir, friction * Time.deltaTime);

            if (horizontal != 0f || vertical != 0f)
                curMoveDir = moveDir;

        }

        if (!noclip)
        {
            if (cc.isGrounded)
            {
                if (Input.GetButtonDown("Jump") && !Physics.Raycast(transform.position + new Vector3(0, Height, 0), Vector3.up, Height))//dont jump if we are in the air, or our head is blocked
                    curMoveDir.y = jumpHeight;
            }
            curMoveDir.y -= downForce * Time.deltaTime;
        }
        else
        {
            moveDir = new Vector3(horizontal, 0, vertical);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= Speed;

            curMoveDir = Vector3.Lerp(curMoveDir, moveDir, friction * Time.deltaTime);

            if (horizontal != 0f || vertical != 0f)
                curMoveDir = moveDir;

        }

        cc.Move(curMoveDir * Time.deltaTime);

        //Mouse look
        mouseInputX += Mathf.Lerp(mouseInputX, Input.GetAxis("Mouse X") * sensitivity, smoothing);
        mouseInputY += Mathf.Lerp(mouseInputY, -Input.GetAxis("Mouse Y") * sensitivity, smoothing);

        mouseInputY = Mathf.Clamp(mouseInputY, -90, 90);

        //Movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

    }

    void FixedUpdate()
    {
        cam.localRotation = Quaternion.AngleAxis(mouseInputY, Vector3.right);//look up and down with the camera
        transform.rotation = Quaternion.AngleAxis(mouseInputX, Vector3.up);//look left and right with the player

        //taken from http://wiki.unity3d.com/index.php?title=Headbobber and modified for use with Unity Doom
        //Headbobbing
        float waveslice = 0.0f;
        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer = timer + bobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }
        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            cam.localPosition = new Vector3(cam.localPosition.x, midpoint + translateChange + Height - 2, cam.localPosition.x);
        }
        else
        {
            cam.localPosition = new Vector3(cam.localPosition.x, midpoint + Height - 2, cam.localPosition.x);
        }
    }

    public void TakeDamage(int damage)
    {
        if(!god)
            Health -= damage;
    }
}
