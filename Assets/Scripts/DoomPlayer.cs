using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomPlayer : MonoBehaviour
{

    public int speed = 20; //adjusted for use with unity
    public int radius = 16;
    public int height = 40; //adjusted from 56 for use with Unity
    public int mass = 100;
    public int painChance = 255;
    public string displayName = "Marine";
    public string crouchSprite = "PLYC";
    public int direction = 0;

    //Health and armor
    public int health = 100;
    public int armor = 0;
    public int armorResist = 0;
    public int healthMax = 200;
    public int armorMax = 200;

    Transform cam;
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

    //Initilize inventory with default values
    public Inventory inv = new Inventory();

    public bool god = false;
    public bool noclip = false;

    static List<Type> PickupType = new List<Type>
    {
        typeof(SuperShotgun),
        typeof(Megasphere),
        typeof(BlueCard),
        typeof(YellowCard),
        typeof(Backpack),
        typeof(RedCard),
        typeof(CellPack),
        typeof(RedSkull),
        typeof(YellowSkull),
        typeof(RedSkull),
        typeof(Shotgun),
        typeof(Chaingun),
        typeof(RocketLauncher),
        typeof(PlasmaRifle),
        typeof(Chainsaw),
        typeof(BFG9000),
        typeof(Clip),
        typeof(Shell),
        typeof(RocketAmmo),
        typeof(Stimpack),
        typeof(Medikit),
        typeof(Soulsphere),
        typeof(HealthBonus),
        typeof(ArmorBonus),
        typeof(GreenArmor),
        typeof(BlueArmor),
        typeof(InvulnerabilitySphere),
        typeof(Berserk),
        typeof(BlurSphere),
        typeof(RadSuit),
        typeof(Allmap),
        typeof(Infrared),
        typeof(RocketBox),
        typeof(Cell),
        typeof(ClipBox),
        typeof(ShellBox),
        typeof(Pistol)

    };

    public float bonusTime = 0f;//duration of bonus
    public bool lightAmp = false;
    public bool blurSphere = false;
    public bool radSuit = false;
    public bool invulnerability = false;
    public bool berserk = false;

    public DoomHUD hud;

    Vector3 lastPos;

    void Start()
    {
        cam = transform.GetChild(0);//camera transform
        cc = GetComponent<CharacterController>();
        lastPos = transform.position;
    }

    void Update()
    {
        hud.ammo = inv.bull;
        hud.armor = armor;
        hud.health = health;
        hud.bull = inv.bull;
        hud.bullMax = inv.bullMax;
        hud.shel = inv.shell;
        hud.shelMax = inv.shellMax;
        hud.cell = inv.cell;
        hud.cellMax = inv.cellMax;
        hud.rckt = inv.rckt;
        hud.rcktMax = inv.rcktMax;

        //action = Input.GetButtonDown("Action");
        if (cc.isGrounded)//cant already be jumping
        {
            moveDir = new Vector3(horizontal, 0, vertical);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;

            curMoveDir = Vector3.Lerp(curMoveDir, moveDir, friction * Time.deltaTime);

            if (horizontal != 0f || vertical != 0f)
                curMoveDir = moveDir;

        }

        if (!noclip)
        {
            if (cc.isGrounded)
            {
                if (Input.GetButtonDown("Jump") && !Physics.Raycast(transform.position + new Vector3(0, height, 0), Vector3.up, height))//dont jump if we are in the air, or our head is blocked
                    curMoveDir.y = jumpHeight;
            }
            curMoveDir.y -= downForce * Time.deltaTime;
        }
        else
        {
            moveDir = new Vector3(horizontal, 0, vertical);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;

            curMoveDir = Vector3.Lerp(curMoveDir, moveDir, friction * Time.deltaTime);

            if (horizontal != 0f || vertical != 0f)
                curMoveDir = moveDir;

        }
        lastPos = transform.position;
        cc.Move(curMoveDir * Time.deltaTime);

        //Mouse look
        mouseInputX += Mathf.Lerp(mouseInputX, Input.GetAxis("Mouse X") * sensitivity, smoothing);
        mouseInputY += Mathf.Lerp(mouseInputY, -Input.GetAxis("Mouse Y") * sensitivity, smoothing);

        mouseInputY = Mathf.Clamp(mouseInputY, -90, 90);

        //Movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

    }

    private void LateUpdate()
    {
       // if(Physics.Raycast(transform.position + new Vector3(0, height/2, 0), transform.forward, 30, (1 << 10)))
        {
        //    transform.position = lastPos;
        }
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
            cam.localPosition = new Vector3(cam.localPosition.x, midpoint + translateChange + height - 2, cam.localPosition.x);
        }
        else
        {
            cam.localPosition = new Vector3(cam.localPosition.x, midpoint + height - 2, cam.localPosition.x);
        }
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.layer == 9)
        {

        }
    }

    public void TakeDamage(int damage)
    {
        if(!god)
            health -= damage;
    }
}

public class Inventory
{
    //Ammo 
    public int bull = 50;
    public int shell = 0;
    public int cell = 0;
    public int rckt = 0;

    public int bullMax = 200;
    public int shellMax = 50;
    public int cellMax = 300;
    public int rcktMax = 50;

    //Keys
    public bool blueCard = false;
    public bool yellowCard = false;
    public bool redCard = false;
    public bool blueSkull = false;
    public bool yellowSkull = false;
    public bool redSkull = false;

    //Weapons
    public bool fist = true;
    public bool pistol = true;
    public bool chainsaw = false;
    public bool chaingun = false;
    public bool shotgun = false;
    public bool superShotgun = false;
    public bool rocketLauncher = false;
    public bool plasmaRifle = false;
    public bool BFG9000 = false;

    //powerups
    public bool backpack = false;
    public bool map = false;

}
