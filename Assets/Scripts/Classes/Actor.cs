using System.Collections.Generic;
using UnityEngine;


public class Actor : MonoBehaviour
{
    public int Scale = 1;
    public int Health = 1000;

    public int Radius = 20;
    public int Height = 16;
    public int Mass = 100;
    //RenderStyle Normal
    public float Alpha = 1;
    public int MinMissileChance = 200;
    public int MeleeRange = 44;
    public int MaxDropoffHeight = 24;
    public int MaxStepHeight = 24;
    public float BounceFactor = 0.7f;
    public float WallBounceFactor = 0.75f;
    public int BounceCount = -1;
    public int floatSpeed = 4;
    public int floatBobPhase = -1; // randomly initialize by default
    public int Gravity = 1;
    public float DamageFactor = 1.0f;
    public float PushFactor = 0.25f;
    public int WeaveIndexXY = 0;
    public int WeaveIndexZ = 16;
    public int DesignatedTeam = 255;
    public int Speed = 10;
    public int PainChance = 100;
    public int Damage = 10;
    public int ReactionTime = 8;
    public int ProjectilePassHeight = 0;
    public float VSpeed = 0;
    public string DamageType = "";

    public Dictionary<string, string> Sounds = new Dictionary<string, string>();

    public string Name = "Actor";
    public string sprite = "";

    public string Obituary = "";
    public string HitObituary = "";
    public string DropItem = "";

    public string Paintype = "";
    public string DeathType = "";
    public string TeleFogSourceType = "";
    public string TeleFogDestType = "";

    //Flags
    public bool SOLID = false;
    public bool SHOOTABLE = false;
    public bool FLOAT = false;
    public bool NOGRAVITY = false;
    public bool FLOORCLIP = false;
    public bool BOSSDEATH = false;
    public bool BOSS = false;
    public bool MISSILEMORE = false;
    public bool NORADIUSDMG = false;
    public bool DONTMORPH = false;
    public bool SHADOW = false;
    public bool QUICKTORETALIATE = false;
    public bool NOTARGET = false;
    public bool DONTFALL = false;
    public bool COUNTKILL = false;
    public bool CANPUSHWALLS = false;
    public bool CANUSEWALLS = false;
    public bool ACTIVATEMCROSS = false;
    public bool CANPASS = false;
    public bool ISMONSTER = false;
    public bool SPAWNCEILING = false;
    public bool NOBLOCKMAP = false;
    public bool DROPOFF = false;
    public bool MISSILE = false;
    public bool ACTIVATEIMPACT = false;
    public bool ACTIVATEPCROSS = false;
    public bool NOTELEPORT = false;
    public bool RANDOMIZE = false;
    public bool DEHEXPLOSION = false;
    public bool ROCKETTRAIL = false;
    public bool OLDRADIUSDMG = false;
    public bool NOCLIP = false;
    public bool NOSECTOR = false;
    public bool NOBLOOD = false;
    public bool DONTGIB = false;
    public bool MOVEWITHSECTOR = false;
    public bool CORPSE = false;

    public string pickupSound = "DSITEMUP";
    public float bonusTime = 0f;
    public enum BloodType
    {
        Blood,
        BloodSplatter,
        AxeBlood
    };

    public int ExplosionDamage = 128;
    public int MissileHeight = 32;
    public int SpriteAngle = 0;
    public int SpriteRotation = 0;

    public Color StencilColor = new Color(0, 0, 0);

    List<int> VisibleAngles = new List<int>();
    List<int> VisiblePitch = new List<int>();

    // Functions

    public virtual bool PickedUp(DoomPlayer player, Inventory inv) { return false; }

    public Dictionary<string, State> actorStates = new Dictionary<string, State>
    {
        {"Spawn", new State
            {
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "TNT1", sprInd = "A", time = -1, function = ""},
                   new StateInfo{function = "Stop" }
               }

           }
        },
        {"Null", new State
           {
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "TNT1", sprInd = "A", time = 1, function = ""},
                   new StateInfo{function = "Stop"}
               }
           }
        },
       {"GenericFreezeDeath", new State
           {
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "####", sprInd = "#", time = 5, function = "A_GenericFreezeDeath" },
                   new StateInfo{spr = "----", sprInd = "A", time = 1, function = "A_FreezeDeathChunks" },
                   new StateInfo{function = "stop"}
               }
           }
        },
        { "GenericCrush", new State
           {
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "POL5", sprInd = "A", time = -1, function = ""},
                   new StateInfo {function = "stop"}
               }
           }
        }
    };

    public Actor Clone()
    {
        return (Actor)this.MemberwiseClone();
    }
}
