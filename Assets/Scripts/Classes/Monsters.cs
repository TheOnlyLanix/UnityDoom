using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : Actor
{
    public int RipperLevel = 0;
    public int RipLevelMin = 0;
    public int RipLevelMax = 0;
    public int DefThreshold = 100;

    public int MaxTargetRange = 0;
    public int MiniMissileChance = 0;
    public int MeleeThreshold = 0;

    public void SetFlags()
    {
        //These flags are set by default on every monster
        SHOOTABLE = true;
        COUNTKILL = true;
        SOLID = true;
        CANPUSHWALLS = true;
        CANUSEWALLS = true;
        ACTIVATEMCROSS = true;
        CANPASS = true;
        ISMONSTER = true;
    }

    

    public void A_VileAttack(AudioClip snd, int initialdmg = 20, int blastdmg = 70, int blastradius = 70, float thrustfac = 1.0f, string damagetype = "Fire", int flags = 0) { }

    public void A_VileChase() { }

    public void A_VileStart() { }

    public void A_VileTarget(string fire = "ArchvileFire") { }

    public void A_Wander(int flags = 0) { }

    public void A_SpidRefire() { }

    public void A_SPosAttack() { }

    public void A_SPosAttackUseAtkSound() { }

    public void A_WolfAttack(int flags, AudioClip whattoplay, float snipe = 1.0f, int maxdamage = 64, int blocksize = 128, int pointblank = 2, int longrange = 4, float runspeed = 160.0f, string pufftype = "BulletPuff") { }

    public void A_TroopAttack() { }

    public void A_SkelFist() { }

    public void A_SkelMissile() { }

    public void A_SkelWhoosh() { }

    public void A_SkullAttack(float speed = 20) { }

    public void A_BossDeath() { }

    public void A_BabyMetal() { }

    public void A_BrainAwake() { }

    public void A_BrainDie() { }

    public void A_BrainExplode() { }

    public void A_BrainPain() { }

    public void A_BrainScream() { }

    public void A_BrainSpit(Actor spawntype) { }  // needs special treatment for default

    public void A_BruisAttack() { }

    public void A_BspiAttack() { }

    public void A_PosAttack() { }

    public void A_PainAttack(string spawntype = "LostSoul", float angle = 0, int flags = 0, int limit = -1) { }

    public void A_PainDie(string spawntype = "LostSoul") { }

    public void A_KeenDie(int doortag = 666) { }

    public void A_Hoof() { }

    public void A_HeadAttack() { }

    public void A_FatAttack1(string spawntype = "FatShot") { }

    public void A_FatAttack2(string spawntype = "FatShot") { }

    public void A_FatAttack3(string spawntype = "FatShot") { }

    public void A_FatRaise() { }

    public void A_DualPainAttack(string spawntype = "LostSoul") { }

    public void A_CyberAttack() { }

    public void A_CPosAttack() { }

    public void A_ComboAttack() { }

    public void A_CPosRefire() { }

    public void A_Look() { }

    public void A_Look2() { }

    public void A_LookEx(int flags, float minseedist, float maxseedist, float maxheardist, float fov, string label) { }

    public void A_Chase(string meleeState = "Melee", string missileState = "Missile", int flags = 0) { }

    public void A_FaceTarget(float max_turn = 0, float max_pitch = 270, float ang_offset = 0, float pitch_offset = 0, int flags = 0, float z_ofs = 0) { }

    public void A_FaceTracer(float max_turn = 0, float max_pitch = 270, float ang_offset = 0, float pitch_offset = 0, int flags = 0, float z_ofs = 0) { }

    public void A_ClearSoundTarget() { }

    public void A_ClearTarget() { }

    public void A_CopyFriendliness(int ptr_source) { }

    public void A_ClearLastHeard() { }

    public void A_CentaurDefend() { }

    public void A_Burst(Actor chunktype) { }

    public void A_AlertMonsters(float maxdist, int flags) { }

    public void A_Die(string damagetype = "none") { }

    public void A_DamageChildren(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None") { }

    public void A_DamageMaster(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None") { }

    public void A_DamageSelf(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None") { }

    public void A_DamageSiblings(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None") { }

    public void A_DamageTarget(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None") { }

    public void A_DamageTracer(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None") { }

    public void A_ExtChase(bool usemelee, bool usemissile, bool playactive = true, bool nightmarefast = false) { }

    public void A_FaceMaster(float max_turn = 0, float max_pitch = 270, float ang_offset = 0, float pitch_offset = 0, int flags = 0, float z_ofs = 0) { }

    public void A_KillChildren(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None") { }

    public void A_KillMaster(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None") { }

    public void A_KillSiblings(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None") { }

    public void A_KillTarget(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None") { }

    public void A_KillTracer(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None") { }

    public void A_Metal() { }

    public void A_RemoveChildren(bool removeall, int flags, string filter = "None", string species = "None") { }

    public void A_RemoveMaster(int flags = 0, string filter = "None", string species = "None") { }

    public void A_RemoveSiblings(bool removeall = false, int flags = 0, string filter = "None", string species = "None") { }

    public void A_RemoveTarget(int flags = 0, string filter = "None", string species = "None") { }

    public void A_RemoveTracer(int flags = 0, string filter = "None", string species = "None") { }

    public void A_Remove(int removee, int flags = 0, string filter = "None", string species = "None") { }

    public void A_RaiseChildren(bool copy) { }

    public void A_RaiseMaster(bool copy) { }

    public void A_RaiseSiblings(bool copy) { }

    bool A_Teleport(string teleportState = "", string targettype = "BossSpot", string fogtype = "TeleportFog", int flags = 0, float mindist = 0, float maxdist = 0) { return false; }

    public void A_TransferPointer(int ptr_source, int ptr_recipient, int sourcefield, int recipientfield, int flags) { }

    public void A_TurretLook() { }
}


public class ShotgunGuy : Monster
{



    public virtual void Awake()
    {
        FLOORCLIP = true;
        SetFlags();

        Health = 30;
        Radius = 20;
        Height = 56;
        Mass = 100;
        Speed = 8;
        PainChance = 170;
        sprite = "SPOS";
        Name = "Shotgun Guy";

        SeeSound = "shotguy/sight";
        AttackSound = "shotguy/attack";
        PainSound = "shotguy/pain";
        DeathSound = "shotguy/death";
        ActiveSound = "shotguy/active";
        Obituary = "$OB_SHOTGUY";
        DropItem = "Shotgun";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "SPOS", sprInd = "AB", time = 10, function = "A_Look"},
                        new StateInfo{function = "Loop" }
                   }

               }
            },
           { "See", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SPOS", sprInd = "AABBCCDD", time = 3, function = "A_Chase"},
                       new StateInfo{function = "Loop"}
                   }
               }
            },
           { "Missile", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SPOS", sprInd = "E", time = 10, function = "A_FaceTarget" },
                       new StateInfo{spr = "SPOS", sprInd = "F", time = 10, function = "A_SPosAttackUseAtkSound" },
                       new StateInfo{spr = "SPOS", sprInd = "E", time = 10 },
                       new StateInfo{function = "See"}
                   }
               }
            },
            { "Pain", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SPOS", sprInd = "G", time = 3, function = ""},
                       new StateInfo{spr = "SPOS", sprInd = "G", time = 3, function = "A_Pain"},
                       new StateInfo {function = "See"}
                   }
                }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SPOS", sprInd = "H", time = 5, function = "" },
                       new StateInfo{spr = "SPOS", sprInd = "I", time = 5, function = "A_Scream" },
                       new StateInfo{spr = "SPOS", sprInd = "J", time = 5, function = "A_NoBlocking" },
                       new StateInfo{spr = "SPOS", sprInd = "K", time = 5},
                       new StateInfo{spr = "SPOS", sprInd = "L", time = -1},
                       new StateInfo{function = "Stop"}
                   }
                }
            },
            { "XDeath", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SPOS", sprInd = "M", time = 5, function = "" },
                       new StateInfo{spr = "SPOS", sprInd = "N", time = 5, function = "A_XScream" },
                       new StateInfo{spr = "SPOS", sprInd = "O", time = 5, function = "A_NoBlocking" },
                       new StateInfo{spr = "SPOS", sprInd = "PQRST", time = 5},
                       new StateInfo{spr = "SPOS", sprInd = "U", time = -1},
                       new StateInfo{function = "Stop"}
                   }
                }
            },
            { "Raise", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SPOS", sprInd = "L", time = 5},
                       new StateInfo{spr = "SPOS", sprInd = "KJIH", time = 5},
                       new StateInfo{function = "See"}
                   }
                }
            }
        };

    }
}

public class ChaingunGuy : Monster
{


    public virtual void Awake()
    {

        FLOORCLIP = true;

        Health = 70;
        Radius = 20;
        Height = 56;
        Mass = 100;
        Speed = 8;
        PainChance = 170;
        sprite = "CPOS";
        Name = "Chaingun Guy";
        SetFlags();

        SeeSound = "chainguy/sight";
        AttackSound = "chainguy/attack";
        PainSound = "chainguy/pain";
        DeathSound = "chainguy/death";
        ActiveSound = "chainguy/active";
        Obituary = "$OB_CHAINGUY";
        DropItem = "Chaingun";



        actorStates = new Dictionary<string, State>
       {
           { "Spawn", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "CPOS", sprInd = "AB", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }

               }
            },
           { "See", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "CPOS", sprInd = "AABBCCDD", time = 3, function = "A_Chase"},
                       new StateInfo{function = "Loop"}
                   }
               }
            },
           { "Missile", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "CPOS", sprInd = "E", time = 10, function = "A_FaceTarget" },
                       new StateInfo{spr = "CPOS", sprInd = "FE", time = 4, function = "A_CPosAttack" },
                       new StateInfo{spr = "CPOS", sprInd = "F", time = 1, function = "A_CPosRefire" },
                       new StateInfo{function = "Missile+1"}
                   }
               }
            },
           { "Pain", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "CPOS", sprInd = "G", time = 3, function = ""},
                      new StateInfo{spr = "CPOS", sprInd = "G", time = 3, function = "A_Pain"},
                      new StateInfo {function = "See"}
                  }
               }
            },
           { "Death", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "CPOS", sprInd = "H", time = 5, function = "" },
                      new StateInfo{spr = "CPOS", sprInd = "I", time = 5, function = "A_Scream" },
                      new StateInfo{spr = "CPOS", sprInd = "J", time = 5, function = "A_NoBlocking" },
                      new StateInfo{spr = "CPOS", sprInd = "KLM", time = 5},
                      new StateInfo{spr = "CPOS", sprInd = "N", time = -1},
                      new StateInfo{function = "Stop"}
                  }
                }
            },
           { "XDeath", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "CPOS", sprInd = "O", time = 5, function = "" },
                      new StateInfo{spr = "CPOS", sprInd = "P", time = 5, function = "A_XScream" },
                      new StateInfo{spr = "CPOS", sprInd = "Q", time = 5, function = "A_NoBlocking" },
                      new StateInfo{spr = "CPOS", sprInd = "RS", time = 5},
                      new StateInfo{spr = "CPOS", sprInd = "T", time = -1},
                      new StateInfo{function = "Stop"}
                  }
               }
            },
           { "Raise", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "CPOS", sprInd = "N", time = 5},
                      new StateInfo{spr = "CPOS", sprInd = "MLKJIH", time = 5},
                      new StateInfo{function = "See"}
                  }
               }
           }
        };
    }
}


public class BaronOfHell : Monster
{


    public virtual void Awake()
    {
        FLOORCLIP = true;
        BOSSDEATH = true;
        OnAwake();
    }

    public void OnAwake()
    {
        Health = 1000;
        Radius = 24;
        Height = 64;
        Mass = 1000;
        Speed = 8;
        PainChance = 50;
        sprite = "BOSS";
        Name = "Baron of Hell";
        SetFlags();
        SeeSound = "baron/sight";
        PainSound = "baron/pain";
        DeathSound = "baron/death";
        ActiveSound = "baron/active";
        Obituary = "$OB_BARON";
        HitObituary = "$OB_BARONHIT";
        AttackSound = "baron/attack";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BOSS", sprInd = "AB", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }

               }
            },
           { "See", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BOSS", sprInd = "AABBCCDD", time = 3, function = "A_Chase"},
                       new StateInfo{function = "Loop"}
                   }
               }
           },
           { "Melee", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BOSS", sprInd = "EF", time = 8, function = "A_FaceTarget" },
                       new StateInfo{spr = "BOSS", sprInd = "G", time = 8, function = "A_MeleeAttack" },
                       new StateInfo{function = "See"}
                   }
               }
            },
            { "Missile", new State
                {
                    info = new List<StateInfo>
                    {
                        new StateInfo{spr = "BOSS", sprInd = "EF", time = 8, function = "A_FaceTarget" },
                        new StateInfo{spr = "BOSS", sprInd = "G", time = 8, function = "A_BruisAttack" },
                        new StateInfo{function = "See"}
                    }
                }
            },
            { "Pain", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BOSS", sprInd = "H", time = 2, function = ""},
                       new StateInfo{spr = "BOSS", sprInd = "H", time = 2, function = "A_Pain"},
                       new StateInfo {function = "See"}
                   }
                }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BOSS", sprInd = "I", time = 8, function = "" },
                       new StateInfo{spr = "BOSS", sprInd = "J", time = 8, function = "A_Scream" },
                       new StateInfo{spr = "BOSS", sprInd = "K", time = 8},
                       new StateInfo{spr = "BOSS", sprInd = "L", time = 8, function = "A_NoBlocking" },
                       new StateInfo{spr = "BOSS", sprInd = "MN", time = 8},
                       new StateInfo{spr = "BOSS", sprInd = "O", time = -1, function = "A_BossDeath"},
                       new StateInfo{function = "Stop"}
                   }
                }
            },
            { "Raise", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BOSS", sprInd = "O", time = 8},
                       new StateInfo{spr = "BOSS", sprInd = "NMLKJI", time = 8},
                       new StateInfo{function = "See"}
                   }
                }
            }
        };
    }
}

public class ZombieMan : Monster
{

    public virtual void Awake()
    {
        FLOORCLIP = true;

        SeeSound = "grunt/sight";
        AttackSound = "grunt/attack";
        PainSound = "grunt/pain";
        DeathSound = "grunt/death";
        ActiveSound = "grunt/active";
        Obituary = "$OB_ZOMBIE";
        DropItem = "Clip";
        SetFlags();

        Health = 20;
        Radius = 20;
        Height = 56;
        Speed = 8;
        PainChance = 200;
        sprite = "POSS";
        Name = "Former Human";
        actorStates = new Dictionary<string, State>
    {
        { "Spawn", new State
            {
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "POSS", sprInd = "AB", time = 10, function = "A_Look"},
                   new StateInfo{function = "Loop" }
               }

           }
        },
        { "See", new State
            {
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "POSS", sprInd = "AABBCCDD", time = 4, function = "A_Chase"},
                   new StateInfo{function = "Loop"}
               }
            }
        },
        { "Missile", new State
            {
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "POSS", sprInd = "E", time = 10, function = "A_FaceTarget" },
                   new StateInfo{spr = "POSS", sprInd = "F", time = 8, function = "A_PosAttack" },
                   new StateInfo{spr = "POSS", sprInd = "E", time = 8},
                   new StateInfo{function = "See"}
               }
            }
        },
        { "Pain", new State
            {
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "POSS", sprInd = "G", time = 3, function = ""},
                   new StateInfo{spr = "POSS", sprInd = "G", time = 3, function = "A_Pain"},
                   new StateInfo {function = "See"}
               }
            }
        },
        { "Death", new State
            {
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "POSS", sprInd = "H", time = 5, function = "" },
                   new StateInfo{spr = "POSS", sprInd = "I", time = 5, function = "A_Scream" },
                   new StateInfo{spr = "POSS", sprInd = "J", time = 5, function = "A_NoBlocking" },
                   new StateInfo{spr = "POSS", sprInd = "K", time = 5},
                   new StateInfo{spr = "CPOS", sprInd = "L", time = -1},
                   new StateInfo{function = "Stop"}
               }
            }
        },
        { "XDeath", new State
            {
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "POSS", sprInd = "M", time = 5, function = "" },
                   new StateInfo{spr = "POSS", sprInd = "N", time = 5, function = "A_XScream" },
                   new StateInfo{spr = "POSS", sprInd = "O", time = 5, function = "A_NoBlocking" },
                   new StateInfo{spr = "POSS", sprInd = "PQRST", time = 5},
                   new StateInfo{spr = "POSS", sprInd = "U", time = -1},
                   new StateInfo{function = "Stop"}
               }
            }
        },
        { "Raise", new State
            {
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "POSS", sprInd = "K", time = 5},
                   new StateInfo{spr = "POSS", sprInd = "JIH", time = 5},
                   new StateInfo{function = "See"}
               }
            }
        }
    };
    }
}

public class DoomImp : Monster
{

    public virtual void Awake()
    {
        FLOORCLIP = true;

        Health = 60;
        Radius = 20;
        Height = 56;
        Mass = 100;
        Speed = 8;
        PainChance = 200;
        sprite = "TROO";
        Name = "Imp";
        SetFlags();
        SeeSound = "imp/sight";
        AttackSound = "imp/attack";
        PainSound = "imp/pain";
        DeathSound = "imp/death";
        ActiveSound = "imp/active";
        Obituary = "$OB_IMP";
        HitObituary = "$OB_IMPHIT";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "TROO", sprInd = "AB", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }

               }
            },
            { "See", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "TROO", sprInd = "AABBCCDD", time = 3, function = "A_Chase"},
                       new StateInfo{function = "Loop"}
                   }
                }
            },
            { "Melee", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "TROO", sprInd = "EF", time = 8, function = "A_FaceTarget"},
                       new StateInfo{spr = "TROO", sprInd = "G", time = 10, function = "A_MeleeAttack"},
                       new StateInfo{function = "Loop"}
                   }
                }
            },
            { "Missile", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "TROO", sprInd = "EF", time = 8, function = "A_FaceTarget" },
                       new StateInfo{spr = "TROO", sprInd = "G", time = 6, function = "A_TroopAttack" },
                       new StateInfo{function = "See"}
                   }
                }
            },
            { "Pain", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "TROO", sprInd = "H", time = 2, function = ""},
                       new StateInfo{spr = "TROO", sprInd = "H", time = 2, function = "A_Pain"},
                       new StateInfo {function = "See"}
                   }
                }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "TROO", sprInd = "I", time = 5 },
                       new StateInfo{spr = "TROO", sprInd = "J", time = 5, function = "A_Scream" },
                       new StateInfo{spr = "TROO", sprInd = "K", time = 5},
                       new StateInfo{spr = "TROO", sprInd = "L", time = 5, function = "A_NoBlocking" },
                       new StateInfo{spr = "TROO", sprInd = "M", time = -1},
                       new StateInfo{function = "Stop"}
                   }
                }
            },
            { "XDeath", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "TROO", sprInd = "N", time = 5, function = "" },
                       new StateInfo{spr = "TROO", sprInd = "O", time = 5, function = "A_XScream" },
                       new StateInfo{spr = "TROO", sprInd = "P", time = 5 },
                       new StateInfo{spr = "TROO", sprInd = "Q", time = 5, function = "A_NoBlocking"},
                       new StateInfo{spr = "TROO", sprInd = "RST", time = 5},
                       new StateInfo{spr = "TROO", sprInd = "U", time = -1},
                       new StateInfo{function = "Stop"}
                   }
                }
            },
            { "Raise", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "TROO", sprInd = "ML", time = 8},
                       new StateInfo{spr = "TROO", sprInd = "KJI", time = 6},
                       new StateInfo{function = "See"}
                   }
                }
            }
        };
    }
}


public class Arachnotron : Monster
{
    public virtual void Awake()
    {
        FLOORCLIP = true;
        BOSSDEATH = true;

        Health = 500;
        Radius = 64;
        Height = 64;
        Mass = 600;
        Speed = 12;
        PainChance = 128;
        sprite = "BSPI";
        Name = "Arachnotron";
        SetFlags();
        SeeSound = "baby/sight";
        AttackSound = "baby/attack";
        PainSound = "baby/pain";
        DeathSound = "baby/death";
        ActiveSound = "baby/active";
        Obituary = "$OB_BABY";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BSPI", sprInd = "AB", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }

               }
            },
           { "See", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BSPI", sprInd = "A", time = 20},
                       new StateInfo{spr = "BSPI", sprInd = "A", time = 3, function = "A_BabyMetal"},
                       new StateInfo{spr = "BSPI", sprInd = "ABBCC", time = 3, function = "A_Chase"},
                       new StateInfo{spr = "BSPI", sprInd = "D", time = 3, function = "A_BabyMetal"},
                       new StateInfo{spr = "BSPI", sprInd = "DEEFF", time = 3, function = "A_Chase"},
                       new StateInfo{function = "See+1"}
                   }
               }
           },
           { "Missile", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BSPI", sprInd = "A", time = 20, function = "A_FaceTarget" },
                       new StateInfo{spr = "BSPI", sprInd = "G", time = 4, function = "A_BspiAttack" },
                       new StateInfo{spr = "BSPI", sprInd = "H", time = 4},
                       new StateInfo{spr = "BSPI", sprInd = "H", time = 1, function = "A_SpidRefire" },
                       new StateInfo{function = "Missile+1"}
                   }
               }
           },
            { "Pain", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BSPI", sprInd = "I", time = 3, function = ""},
                       new StateInfo{spr = "BSPI", sprInd = "I", time = 3, function = "A_Pain"},
                       new StateInfo {function = "See+1"}
                   }
                }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BSPI", sprInd = "J", time = 20, function = "A_Scream" },
                       new StateInfo{spr = "BSPI", sprInd = "K", time = 7, function = "A_NoBlocking" },
                       new StateInfo{spr = "BSPI", sprInd = "LMNO", time = 7},
                       new StateInfo{spr = "BSPI", sprInd = "P", time = -1, function = "A_BossDeath" },
                       new StateInfo{function = "Stop"}
                   }
                }
            },
            { "Raise", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BSPI", sprInd = "P", time = 5},
                       new StateInfo{spr = "BSPI", sprInd = "ONMLKJ", time = 5},
                       new StateInfo{function = "See+1"}
                   }
                }
            }
        };
    }
}


public class SpiderMastermind : Monster
{

    public virtual void Awake()
    {
        BOSS = true;
        MISSILEMORE = true;
        FLOORCLIP = true;
        NORADIUSDMG = true;
        DONTMORPH = true;
        BOSSDEATH = true;
        SetFlags();

        Health = 3000;
        Radius = 128;
        Height = 100;
        Mass = 1000;
        Speed = 12;
        PainChance = 40;
        sprite = "SPID";
        Name = "Spider Mastermind";
                SetFlags();
        MinMissileChance = 160;
        SeeSound = "spider/sight";
        AttackSound = "spider/attack";
        PainSound = "spider/pain";
        DeathSound = "spider/death";
        ActiveSound = "spider/active";
        Obituary = "$OB_SPIDER";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SPID", sprInd = "AB", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }

               }
            },
           { "See", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SPID", sprInd = "A", time = 3, function = "A_Metal"},
                       new StateInfo{spr = "SPID", sprInd = "ABB", time = 3, function = "A_Chase"},
                       new StateInfo{spr = "SPID", sprInd = "C", time = 3, function = "A_Metal"},
                       new StateInfo{spr = "SPID", sprInd = "CDD", time = 3, function = "A_Chase"},
                       new StateInfo{spr = "SPID", sprInd = "E", time = 3, function = "A_Metal"},
                       new StateInfo{spr = "SPID", sprInd = "EFF", time = 3, function = "A_Chase"},
                       new StateInfo{function = "Loop"}
                   }
               }
           },
           { "Missile", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SPID", sprInd = "A", time = 20, function = "A_FaceTarget" },
                       new StateInfo{spr = "SPID", sprInd = "G", time = 4, function = "A_SPosAttackUseAtkSound" },
                       new StateInfo{spr = "SPID", sprInd = "H", time = 4, function = "A_SPosAttackUseAtkSound" },
                       new StateInfo{spr = "SPID", sprInd = "H", time = 1, function = "A_SpidRefire" },
                       new StateInfo{function = "Missile+1"}
                   }
               }
           },
            { "Pain", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SPID", sprInd = "I", time = 3, function = ""},
                       new StateInfo{spr = "SPID", sprInd = "I", time = 3, function = "A_Pain"},
                       new StateInfo {function = "See"}
                   }
                }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SPID", sprInd = "J", time = 20, function = "A_Scream" },
                       new StateInfo{spr = "SPID", sprInd = "K", time = 10, function = "A_NoBlocking" },
                       new StateInfo{spr = "SPID", sprInd = "LMNOPQR", time = 10},
                       new StateInfo{spr = "SPID", sprInd = "S", time = 30},
                       new StateInfo{spr = "SPID", sprInd = "S", time = -1, function = "A_BossDeath" },
                       new StateInfo{function = "Stop"}
                   }
                }
            }
        };
    }
}


public class Demon : Monster
{



    public virtual void Awake()
    {
        FLOORCLIP = true;

        OnAwake();
    }

    public void OnAwake()
    {
        Health = 150;
        Radius = 30;
        Height = 56;
        Mass = 400;
        Speed = 10;
        PainChance = 180;
        sprite = "SARG";
        Name = "Demon";
        SetFlags();
        SeeSound = "demon/sight";
        AttackSound = "demon/melee";
        PainSound = "demon/pain";
        DeathSound = "demon/death";
        ActiveSound = "demon/active";
        Obituary = "$OB_DEMONHIT";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SARG", sprInd = "AB", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }

               }
            },
            { "See", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SARG", sprInd = "AABBCCDD", time = 2, function = "A_Chase"},
                       new StateInfo{function = "Loop"}
                   }
                }
            },
            { "Melee", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SARG", sprInd = "EF", time = 8, function = "A_FaceTarget"},
                       new StateInfo{spr = "SARG", sprInd = "G", time = 8, function = "A_SargAttack"},
                       new StateInfo{function = "See"}
                   }
                }
            },
            { "Pain", new State
                {
                      info = new List<StateInfo>
                      {
                          new StateInfo{spr = "SARG", sprInd = "H", time = 2, function = ""},
                          new StateInfo{spr = "SARG", sprInd = "H", time = 2, function = "A_Pain"},
                          new StateInfo {function = "See"}
                      }
                }
            },
            { "Death", new State
                {
                    info = new List<StateInfo>
                    {
                        new StateInfo{spr = "SARG", sprInd = "I", time = 8 },
                        new StateInfo{spr = "SARG", sprInd = "J", time = 8, function = "A_Scream" },
                        new StateInfo{spr = "SARG", sprInd = "K", time = 4},
                        new StateInfo{spr = "SARG", sprInd = "L", time = 4, function = "A_NoBlocking" },
                        new StateInfo{spr = "SARG", sprInd = "M", time = 4},
                        new StateInfo{spr = "SARG", sprInd = "N", time = -1},
                        new StateInfo{function = "Stop"}
                    }
                }
            },
            { "Raise", new State
                {
                    info = new List<StateInfo>
                    {
                        new StateInfo{spr = "SARG", sprInd = "N", time = 4},
                        new StateInfo{spr = "SARG", sprInd = "MLKJI", time = 5},
                        new StateInfo{function = "See"}
                    }
                }
            }
        };
    }
}

public class Spectre : Demon
{

    public override void Awake()
    {
        SHADOW = true;
        //RenderStyle = OptFuzzy
        Alpha = 0.5f;
        SetFlags();

        Name = "Spectre";

        OnAwake();
        SeeSound = "spectre/sight";
        AttackSound = "spectre/melee";
        PainSound = "spectre/pain";
        DeathSound = "spectre/death";
        ActiveSound = "spectre/active";
        Obituary = "$OB_SPECTREHIT";
    }


}



public class Archvile : Monster
{



    public virtual void Awake()
    {
        QUICKTORETALIATE = true;
        FLOORCLIP = true;
        NOTARGET = true;
        SetFlags();
        Health = 700;
        Radius = 20;
        Height = 56;
        Mass = 500;
        Speed = 15;
        PainChance = 10;
        sprite = "VILE";
        Name = "Archvile";

        SeeSound = "vile/sight";
        PainSound = "vile/pain";
        DeathSound = "vile/death";
        ActiveSound = "vile/active";

        /*
        VileRaiseSound = "vile/raise";
        VileStartSound = "vile/start";
        VileStopSound = "vile/stop";
        VileFireStartSound = "vile/firestrt";
        VileFireCrackleSound = "vile/firecrkl";
        */

        Obituary = "$OB_VILE";
        MaxTargetRange = 896;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "VILE", sprInd = "AB", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }
               }
            },
            { "See", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "VILE", sprInd = "AABBCCDDEEFF", time = 2, function = "A_VileChase"},
                       new StateInfo{function = "Loop"}
                   }
                }
            },
            { "Missile", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "VILE", sprInd = "G", time = 0, function = "A_VileStart"},
                       new StateInfo{spr = "VILE", sprInd = "G", time = 10, function = "A_FaceTarget"},
                       new StateInfo{spr = "VILE", sprInd = "H", time = 8, function = "A_VileTarget"},
                       new StateInfo{spr = "VILE", sprInd = "IJKLMN", time = 8, function = "A_FaceTarget"},
                       new StateInfo{spr = "VILE", sprInd = "O", time = 8, function = "A_VileAttack"},
                       new StateInfo{spr = "VILE", sprInd = "P", time = 20},
                       new StateInfo{function = "See"}
                   }
                }
            },
            { "Heal", new State
                {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "VILE", sprInd = "---", time = 10},
                      new StateInfo {function = "See"}
                  }
                }
            },
            { "Pain", new State
                {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "VILE", sprInd = "Q", time = 5, function = ""},
                      new StateInfo{spr = "VILE", sprInd = "Q", time = 5, function = "A_Pain"},
                      new StateInfo {function = "See"}
                  }
                }
            },
            { "Death", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "VILE", sprInd = "Q", time = 7 },
                      new StateInfo{spr = "VILE", sprInd = "R", time = 7, function = "A_Scream" },
                      new StateInfo{spr = "VILE", sprInd = "S", time = 7, function = "A_NoBlocking" },
                      new StateInfo{spr = "VILE", sprInd = "TUVWXY", time = 7},
                      new StateInfo{spr = "VILE", sprInd = "Z", time = -1},
                      new StateInfo{function = "Stop"}
                  }
               }
            }
        };
    }

}

public class HellKnight : BaronOfHell
{
    public override void Awake()
    {
        OnAwake();
        Health = 500;

        BOSSDEATH = false;
        SetFlags();
        SeeSound = "knight/sight";
        PainSound = "knight/pain";
        DeathSound = "knight/death";
        ActiveSound = "knight/active";
        Obituary = "$OB_KNIGHT";
        HitObituary = "$OB_KNIGHTHIT";
        Name = "Hell Knight";
        sprite = "BOS2";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BOS2", sprInd = "AB", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }

               }
            },
            { "See", new State
                {
                    info = new List<StateInfo>
                    {
                        new StateInfo{spr = "BOS2", sprInd = "AABBCCDD", time = 3, function = "A_Chase"},
                        new StateInfo{function = "Loop"}
                    }
                }
            },
            { "Melee", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BOS2", sprInd = "EF", time = 8, function = "A_FaceTarget" },
                       new StateInfo{spr = "BOS2", sprInd = "G", time = 8, function = "A_BruisAttack" },
                       new StateInfo{function = "See"}
                   }
                }
            },
            { "Missile", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BOS2", sprInd = "EF", time = 8, function = "A_FaceTarget" },
                       new StateInfo{spr = "BOS2", sprInd = "G", time = 8, function = "A_MeleeAttack" },
                       new StateInfo{function = "See"}
                   }
                }
            },
            { "Pain", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BOS2", sprInd = "H", time = 2},
                       new StateInfo{spr = "BOS2", sprInd = "H", time = 2, function = "A_Pain"},
                       new StateInfo {function = "See"}
                   }
                }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BOS2", sprInd = "I", time = 8, function = "" },
                       new StateInfo{spr = "BOS2", sprInd = "J", time = 8, function = "A_Scream" },
                       new StateInfo{spr = "BOS2", sprInd = "K", time = 8},
                       new StateInfo{spr = "BOS2", sprInd = "L", time = 8, function = "A_NoBlocking" },
                       new StateInfo{spr = "BOS2", sprInd = "MN", time = 8},
                       new StateInfo{spr = "BOSS", sprInd = "O", time = -1},
                       new StateInfo{function = "Stop"}
                   }
                }
            },
            { "Raise", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BOS2", sprInd = "O", time = 8},
                       new StateInfo{spr = "BOS2", sprInd = "NMLKJI", time = 8},
                       new StateInfo{function = "See"}
                   }
                }
            }
        };
    }

}

public class CyberDemon : Monster
{

    public virtual void Awake()
    {
        BOSS = true;
        MISSILEMORE = true;
        FLOORCLIP = true;
        NORADIUSDMG = true;
        DONTMORPH = true;
        BOSSDEATH = true;
        SetFlags();

        Health = 4000;
        Radius = 40;
        Height = 110;
        Mass = 1000;
        Speed = 16;
        PainChance = 20;
        sprite = "CYBR";
        Name = "Cyberdemon";

        MiniMissileChance = 160;

        SeeSound = "cyber/sight";
        PainSound = "cyber/pain";
        DeathSound = "cyber/death";
        ActiveSound = "cyber/active";
        Obituary = "$OB_CYBORG";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "CYBR", sprInd = "AB", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }

               }
            },
            { "See", new State
                {
                    info = new List<StateInfo>
                    {
                        new StateInfo{spr = "CYBR", sprInd = "A", time = 3, function = "A_Hoof"},
                        new StateInfo{spr = "CYBR", sprInd = "ABBCC", time = 3, function = "A_Chase"},
                        new StateInfo{spr = "CYBR", sprInd = "D", time = 3, function = "A_Metal"},
                        new StateInfo{spr = "CYBR", sprInd = "D", time = 3, function = "A_Chase"},
                        new StateInfo{function = "Loop"}
                    }
                }
            },
            { "Missile", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "CYBR", sprInd = "E", time = 6, function = "A_FaceTarget"},
                        new StateInfo{spr = "CYBR", sprInd = "F", time = 12, function = "A_CyberAttack"},
                        new StateInfo{spr = "CYBR", sprInd = "E", time = 12, function = "A_FaceTarget"},
                        new StateInfo{spr = "CYBR", sprInd = "F", time = 12, function = "A_CyberAttack"},
                        new StateInfo{spr = "CYBR", sprInd = "E", time = 12, function = "A_FaceTarget"},
                        new StateInfo{spr = "CYBR", sprInd = "F", time = 12, function = "A_CyberAttack"},
                        new StateInfo{function = "See"}
                   }
               }
            },
            { "Pain", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "CYBR", sprInd = "G", time = 10, function = "A_Pain"},
                        new StateInfo {function = "See"}
                   }
                }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "CYBR", sprInd = "H", time = 10 },
                        new StateInfo{spr = "CYBR", sprInd = "I", time = 10, function = "A_Scream" },
                        new StateInfo{spr = "CYBR", sprInd = "JKL", time = 10 },
                        new StateInfo{spr = "CYBR", sprInd = "M", time = 10, function = "A_NoBlocking"},
                        new StateInfo{spr = "CYBR", sprInd = "NO", time = 10},
                        new StateInfo{spr = "CYBR", sprInd = "P", time = 30},
                        new StateInfo{spr = "CYBR", sprInd = "P", time = -1, function = "A_BossDeath"},
                        new StateInfo{function = "Stop"}
                    }
                }
           }
        };
    }

}

public class Fatso : Monster
{


    public virtual void Awake()
    {
        FLOORCLIP = true;
        BOSSDEATH = true;
        SetFlags();
        Health = 600;
        Radius = 48;
        Height = 64;
        Mass = 1000;
        Speed = 8;
        PainChance = 80;
        sprite = "FATT";
        Name = "Mancubus";

        SeeSound = "fatso/sight";
        AttackSound = "fatso/attack";
        PainSound = "fatso/pain";
        DeathSound = "fatso/death";
        ActiveSound = "fatso/active";
        Obituary = "$OB_FATSO";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "FATT", sprInd = "AB", time = 15, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }

               }
            },
            { "See", new State
                {
                    info = new List<StateInfo>
                    {
                        new StateInfo{spr = "FATT", sprInd = "AABBCCDDEEFF", time = 4, function = "A_Chase"},
                        new StateInfo{function = "Loop"}
                    }
                }
            },
            { "Missile", new State
                {
                    info = new List<StateInfo>
                    {
                        new StateInfo{spr = "FATT", sprInd = "G", time = 20, function = "A_FatRaise"},
                        new StateInfo{spr = "FATT", sprInd = "H", time = 10, function = "A_FatAttack1"},
                        new StateInfo{spr = "FATT", sprInd = "IG", time = 5, function = "A_FaceTarget"},
                        new StateInfo{spr = "FATT", sprInd = "H", time = 10, function = "A_FatAttack2"},
                        new StateInfo{spr = "FATT", sprInd = "IG", time = 5, function = "A_FaceTarget"},
                        new StateInfo{spr = "FATT", sprInd = "H", time = 10, function = "A_FatAttack3"},
                        new StateInfo{spr = "FATT", sprInd = "IG", time = 5, function = "A_FaceTarget"},
                        new StateInfo{function = "See"}
                    }
                }
            },
            { "Pain", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "FATT", sprInd = "J", time = 3},
                       new StateInfo{spr = "FATT", sprInd = "J", time = 3, function = "A_Pain"},
                       new StateInfo {function = "See"}
                   }
                }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "FATT", sprInd = "K", time = 6 },
                       new StateInfo{spr = "FATT", sprInd = "L", time = 6, function = "A_Scream" },
                       new StateInfo{spr = "FATT", sprInd = "M", time = 6, function = "A_NoBLocking" },
                       new StateInfo{spr = "FATT", sprInd = "NOPQRS", time = 6},
                       new StateInfo{spr = "FATT", sprInd = "T", time = -1, function = "A_BossDeath"},
                       new StateInfo{function = "Stop"}
                   }
                }
            },
            { "Raise", new State
                {
                    info = new List<StateInfo>
                    {
                       new StateInfo{spr = "FATT", sprInd = "R", time = 5},
                       new StateInfo{spr = "FATT", sprInd = "QPONMLK", time = 5},
                       new StateInfo{function = "See"}
                    }
                }
            }
        };
    }
}

public class LostSoul : Monster
{
    //RenderStyle = SoulTrans;

    public virtual void Awake()
    {
        FLOAT = true;
        NOGRAVITY = true;
        MISSILEMORE = true;
        DONTFALL = true;
        SetFlags();

        Health = 100;
        Radius = 16;
        Height = 56;
        Mass = 50;
        Speed = 8;
        Damage = 3;
        PainChance = 256;
        sprite = "SKUL";
        Name = "Lost Soul";


        AttackSound = "skull/melee";
        PainSound = "skull/pain";
        DeathSound = "skull/death";
        ActiveSound = "skull/active";
        Obituary = "$OB_SKULL";

        actorStates = new Dictionary<string, State>
        {
           { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SKUL", sprInd = "AB", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }

               }
           },
           { "See", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SKUL", sprInd = "AB", time = 6, function = "A_Chase"},
                       new StateInfo{function = "Loop"}
                   }
               }
           },
           { "Missile", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SKUL", sprInd = "C", time = 10, function = "A_FaceTarget"},
                       new StateInfo{spr = "SKUL", sprInd = "D", time = 4, function = "A_SkullAttack"},
                       new StateInfo{spr = "SKUL", sprInd = "CD", time = 4},
                       new StateInfo{function = "Missile+2"}
                   }
               }
           },
           { "Pain", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "SKUL", sprInd = "E", time = 3},
                      new StateInfo{spr = "SKUL", sprInd = "E", time = 3, function = "A_Pain"},
                      new StateInfo {function = "See"}
                  }
               }
           },
           { "Death", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "SKUL", sprInd = "F", time = 6 },
                      new StateInfo{spr = "SKUL", sprInd = "G", time = 6, function = "A_Scream" },
                      new StateInfo{spr = "SKUL", sprInd = "H", time = 6},
                      new StateInfo{spr = "SKUL", sprInd = "I", time = 6, function = "A_NoBlocking"},
                      new StateInfo{spr = "SKUL", sprInd = "J", time = 6},
                      new StateInfo{spr = "SKUL", sprInd = "K", time = 6},
                      new StateInfo{function = "Stop"}
                  }
               }
           }
        };
    }
}


public class PainElemental : Monster
{

    public virtual void Awake()
    {
        FLOAT = true;
        NOGRAVITY = true;
        SetFlags();

        Health = 400;
        Radius = 31;
        Height = 56;
        Mass = 400;
        Speed = 8;
        PainChance = 128;
        sprite = "PAIN";
        Name = "Pain Elemental";

        SeeSound = "pain/sight";
        PainSound = "pain/pain";
        DeathSound = "pain/death";
        ActiveSound = "pain/active";

        actorStates = new Dictionary<string, State>
        {
           { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "PAIN", sprInd = "A", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }

               }
           },
           { "See", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "PAIN", sprInd = "AABBCC", time = 3, function = "A_Chase"},
                       new StateInfo{function = "Loop"}
                   }
               }
           },
           { "Missile", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "PAIN", sprInd = "D", time = 5, function = "A_FaceTarget"},
                       new StateInfo{spr = "PAIN", sprInd = "E", time = 5, function = "A_FaceTarget"},
                       new StateInfo{spr = "PAIN", sprInd = "F", time = 5, function = "A_FaceTarget"},
                       new StateInfo{spr = "PAIN", sprInd = "F", time = 0, function = "A_PainAttack"},
                       new StateInfo{function = "See"}
                   }
               }
           },
           { "Pain", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "PAIN", sprInd = "G", time = 6},
                      new StateInfo{spr = "PAIN", sprInd = "G", time = 6, function = "A_Pain"},
                      new StateInfo {function = "See"}
                  }
               }
           },
           { "Death", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "PAIN", sprInd = "H", time = 8 },
                      new StateInfo{spr = "PAIN", sprInd = "I", time = 8, function = "A_Scream" },
                      new StateInfo{spr = "PAIN", sprInd = "JK", time = 8},
                      new StateInfo{spr = "PAIN", sprInd = "L", time = 8, function = "A_PainDie" },
                      new StateInfo{spr = "PAIN", sprInd = "M", time = 8},
                      new StateInfo{function = "Stop"}
                  }
               }
           },
           { "Raise", new State
               {
                   info = new List<StateInfo>
                   {
                      new StateInfo{spr = "PAIN", sprInd = "MLKJIH", time = 8},
                      new StateInfo{function = "See"}
                   }
               }
           }
        };
    }
}


public class Revenant : Monster
{

    public virtual void Awake()
    {

        MISSILEMORE = true;
        FLOORCLIP = true;
        SetFlags();

        Health = 300;
        Radius = 20;
        Height = 56;
        Mass = 500;
        Speed = 10;
        PainChance = 100;
        sprite = "SKEL";
        Name = "Revenant";

        MeleeThreshold = 196;

        SeeSound = "skeleton/sight";
        AttackSound = "skeleton/attack";
        PainSound = "skeleton/pain";
        DeathSound = "skeleton/death";
        ActiveSound = "skeleton/active";
        HitObituary = "$OB_UNDEADHIT"; // "%o was punched by a revenant."
        Obituary = "$OB_UNDEAD"; // "%o couldn't evade a revenant's fireball."

        actorStates = new Dictionary<string, State>
        {
           { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SKEL", sprInd = "AB", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }

               }
           },
           { "See", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SKEL", sprInd = "AABBCCDDEEFF", time = 2, function = "A_Chase"},
                       new StateInfo{function = "Loop"}
                   }
               }
           },
           { "Melee", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SKEL", sprInd = "G", time = 0, function = "A_FaceTarget"},
                       new StateInfo{spr = "SKEL", sprInd = "G", time = 6, function = "A_SkelWoosh"},
                       new StateInfo{spr = "SKEL", sprInd = "H", time = 6, function = "A_FaceTarget"},
                       new StateInfo{spr = "SKEL", sprInd = "I", time = 6, function = "A_SkelFist"},
                       new StateInfo{function = "See"}
                   }
               }
           },
           { "Missile", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SKEL", sprInd = "J", time = 0, function = "A_FaceTarget"},
                       new StateInfo{spr = "SKEL", sprInd = "J", time = 10, function = "A_FaceTarget"},
                       new StateInfo{spr = "SKEL", sprInd = "K", time = 10, function = "A_SkelMissile"},
                       new StateInfo{spr = "SKEL", sprInd = "K", time = 10, function = "A_FaceTarget"},
                       new StateInfo{function = "See"}
                   }
               }
           },
           { "Pain", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "SKEL", sprInd = "L", time = 5},
                      new StateInfo{spr = "SKEL", sprInd = "L", time = 5, function = "A_Pain"},
                      new StateInfo {function = "See"}
                  }
               }
           },
           { "Death", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "SKEL", sprInd = "LM", time = 7},
                      new StateInfo{spr = "SKEL", sprInd = "N", time = 7, function = "A_Scream" },
                      new StateInfo{spr = "SKEL", sprInd = "O", time = 7, function = "A_NoBlocking"},
                      new StateInfo{spr = "SKEL", sprInd = "P", time = 7 },
                      new StateInfo{spr = "SKEL", sprInd = "Q", time = -1},
                      new StateInfo{function = "Stop"}
                  }
               }
           },
           { "Raise", new State
               {
                   info = new List<StateInfo>
                   {
                      new StateInfo{spr = "SKEL", sprInd = "Q", time = 5},
                      new StateInfo{spr = "SKEL", sprInd = "PONML", time = 5},
                      new StateInfo{function = "See"}
                   }
               }
            }
        };
    }
}

public class WolfensteinSS : Monster
{

    public virtual void Awake()
    {
        FLOORCLIP = true;
        SetFlags();

        Health = 50;
        Radius = 20;
        Height = 56;
        Speed = 8;
        PainChance = 170;
        sprite = "SSWV";
        Name = "WolfensteinSS";

        SeeSound = "wolfss/sight";
        AttackSound = "wolfss/attack";
        PainSound = "wolfss/pain";
        DeathSound = "wolfss/death";
        ActiveSound = "wolfss/active";
        Obituary = "$OB_WOLFSS"; // "%o couldn't evade a revenant's fireball."
        DropItem = "Clip";

        actorStates = new Dictionary<string, State>
        {
           { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SSWV", sprInd = "AB", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }

               }
           },
           { "See", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SSWV", sprInd = "AABBCCDD", time = 3, function = "A_Chase"},
                       new StateInfo{function = "Loop"}
                   }
               }
           },
           { "Missile", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SSWV", sprInd = "E", time = 10, function = "A_FaceTarget"},
                       new StateInfo{spr = "SSWV", sprInd = "F", time = 10, function = "A_FaceTarget"},
                       new StateInfo{spr = "SSWV", sprInd = "G", time = 4, function = "A_CPosAttack"},
                       new StateInfo{spr = "SSWV", sprInd = "F", time = 6, function = "A_FaceTarget"},
                       new StateInfo{spr = "SSWV", sprInd = "G", time = 4, function = "A_CPosAttack"},
                       new StateInfo{spr = "SSWV", sprInd = "F", time = 1, function = "A_CPosRefire"},
                       new StateInfo{function = "Missile+1"}
                   }
               }
           },
           { "Pain", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "SSWV", sprInd = "H", time = 3},
                      new StateInfo{spr = "SSWV", sprInd = "H", time = 3, function = "A_Pain"},
                      new StateInfo {function = "See"}
                  }
               }
            },
           { "Death", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "SSWV", sprInd = "I", time = 5},
                      new StateInfo{spr = "SSWV", sprInd = "J", time = 5, function = "A_Scream" },
                      new StateInfo{spr = "SSWV", sprInd = "K", time = 5, function = "A_NoBlocking"},
                      new StateInfo{spr = "SSWV", sprInd = "L", time = 5 },
                      new StateInfo{spr = "SSWV", sprInd = "M", time = -1},
                      new StateInfo{function = "Stop"}
                  }
               }
            },
           { "XDeath", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "SSWV", sprInd = "N", time = 5},
                      new StateInfo{spr = "SSWV", sprInd = "O", time = 5, function = "A_XScream" },
                      new StateInfo{spr = "SSWV", sprInd = "P", time = 5, function = "A_NoBlocking"},
                      new StateInfo{spr = "SSWV", sprInd = "QRSTU", time = 5 },
                      new StateInfo{spr = "SSWV", sprInd = "V", time = -1},
                      new StateInfo{function = "Stop"}
                  }
               }
            },
           { "Raise", new State
               {
                   info = new List<StateInfo>
                   {
                      new StateInfo{spr = "SSWV", sprInd = "M", time = 5},
                      new StateInfo{spr = "SSWV", sprInd = "LKJI", time = 5},
                      new StateInfo{function = "See"}
                   }
               }
            }
        };
    }
}

public class Cacodemon : Monster
{

    public virtual void Awake()
    {
        FLOAT = true;
        NOGRAVITY = true;
        SetFlags();
        Health = 400;
        Radius = 31;
        Height = 56;
        Speed = 8;
        PainChance = 128;
        sprite = "HEAD";
        Name = "Cacodemon";

        SeeSound = "caco/sight";
        AttackSound = "caco/attack";
        PainSound = "caco/pain";
        DeathSound = "caco/death";
        ActiveSound = "caco/active";
        Obituary = "$OB_CACO";
        HitObituary = "$OB_CACOHIT";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "HEAD", sprInd = "A", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop" }
                   }

                }
            },
           { "See", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "HEAD", sprInd = "A", time = 3, function = "A_Chase"},
                       new StateInfo{function = "Loop"}
                   }
               }
           },
           { "Missile", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "HEAD", sprInd = "BC", time = 5, function = "A_FaceTarget"},
                       new StateInfo{spr = "HEAD", sprInd = "D", time = 5, function = "A_HeadAttack"},
                       new StateInfo{function = "See"}
                   }
               }
            },
           { "Pain", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "HEAD", sprInd = "E", time = 3},
                      new StateInfo{spr = "HEAD", sprInd = "E", time = 3, function = "A_Pain"},
                      new StateInfo{spr = "HEAD", sprInd = "F", time = 6},
                      new StateInfo {function = "See"}
                  }
               }
            },
           { "Death", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "HEAD", sprInd = "G", time = 8},
                      new StateInfo{spr = "HEAD", sprInd = "H", time = 8, function = "A_Scream" },
                      new StateInfo{spr = "HEAD", sprInd = "IJ", time = 8},
                      new StateInfo{spr = "HEAD", sprInd = "K", time = 8, function = "A_NoBlocking" },
                      new StateInfo{spr = "HEAD", sprInd = "L", time = -1, function = "A_SetFloorClip"},
                      new StateInfo{function = "Stop"}
                  }
               }
            },
           { "Raise", new State
               {
                   info = new List<StateInfo>
                   {
                      new StateInfo{spr = "HEAD", sprInd = "L", time = 8, function = "A_UnSetFloorClip"},
                      new StateInfo{spr = "HEAD", sprInd = "KJIHG", time = 8},
                      new StateInfo{function = "See"}
                   }
               }
            }
        };
    }
}

