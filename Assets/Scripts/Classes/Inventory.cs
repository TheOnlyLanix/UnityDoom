using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    public int Amount = 0;
    public int DefMaxAmount = 25;
    public int MaxAmount = 0;
    public int InterHubAmount = 0;
    public Texture2D Icon;
    public Texture2D AltIcon;
    public string PickupMessage = "";
    public AudioClip PickupSound;
    public bool Pickupflash = false; //in zdoom this is an actor, shouldnt need to over-complicate it for Unity
    public AudioClip UseSound;
    public int RespawnTics = 0;

    //Flags
    public bool QUIET = false;
    public bool AUTOACTIVATE = false;
    public bool UNDROPPABLE = false;
    public bool INVBAR = false;
    public bool HUBPOWER = false;
    public bool PERSISTENTPOWER = false;
    public bool ALWAYSPICKUP = false;
    public bool FANCYPICKUPSOUND = false;
    public bool NOATTENPICKUPSOUND = false;
    public bool BIGPOWERUP = false;
    public bool NEVERRESPAWN = false;
    public bool KEEPDEPLETEDuiet = false;
    public bool IGNORESKILL = false;
    public bool ADDITIVETIME = false;
    public bool UNTOSSABLE = false;
    public bool RESTRICTABSOLUTELY = false;
    public bool NOSCREENFLASH = false;
    public bool TOSSED = false;
    public bool ALWAYSRESPAWN = false;
    public bool TRANSFER = false;
    public bool NOTELEPORTFREEZE = false;
    public bool NOSCREENBLINK = false;
    public bool ISHEALTH = false;
    public bool ISARMOR = false;

}
