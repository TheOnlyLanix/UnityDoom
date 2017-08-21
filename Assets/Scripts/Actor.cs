using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Actor : MonoBehaviour
{
    public int Scale = 1;
    public int Health = 1000;
    public int ReactionTime = 8;
    public int Radius = 20;
    public int Height = 16;
    public int Mass = 100;
    //RenderStyle Normal
    public int Alpha = 1;
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
    public string Name = "Actor";
    public string sprite = "TNT1";

    public string SeeSound = "grunt/sight";
    public string AttackSound = "grunt/attack";
    public string PainSound = "grunt/pain";
    public string DeathSound = "grunt/death";
    public string ActiveSound = "grunt/active";
    public string Obituary = "$OB_ZOMBIE";
    public string HitObituary = "$OB_CACOHIT";
    public string DropItem = "";

    public string Paintype = "Normal";
    public string DeathType = "Normal";
    public string TeleFogSourceType = "TeleportFog";
    public string TeleFogDestType = "TeleportFog";

    public int RipperLevel = 0;
    public int RipLevelMin = 0;
    public int RipLevelMax = 0;
    public int DefThreshold = 100;

    public int MaxTargetRange = 0;
    public int MiniMissileChance = 0;
    public int MeleeThreshold = 0;

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
    bool CheckClass(Thing checkclass, int ptr_select, bool match_superclass)
    {
        return false;
    }

    //public int CountInv(class<Inventory> itemtype, public int ptr_select);

    //public int CountProximity(class<Actor> classname, public float distance, public int flags = 0, public int ptr = AAPTR_DEFAULT);

    public float GetAngle(int flags, int ptr)
    {
        return 0f;
    }

    public float GetCrouchFactor(int ptr)
    {
        return 0f;
    }

    public float GetCVar(string cvar)
    {
        return 0f;
    }

    public float GetDistance(bool checkz, int ptr)
    {
        return 0f;
    }

    public int GetGibHealth()
    {
        return 0;
    }

    public int GetMissileDamage(int mask, int add, int ptr)
    {
        return 0;
    }

    public int GetPlayerInput(int inputnum, int ptr)
    {
        return 0;
    }

    public int GetSpawnHealth()
    {
        return 0;
    }

    public float GetSpriteAngle(int ptr)
    {
        return 0;
    }

    public float GetSpriteRotation(int ptr)
    {
        return 0;
    }

    public float GetZAt(float px, float py, float angle, int flags, int pick_pointer)
    {
        return 0f;
    }

    public bool IsPointerEqual(int ptr_select1, int ptr_select2)
    {
        return false;
    }

    public int OverlayID()
    {
        return 0;
    }

    public float OverlayX(int layer)
    {
        return 0f;
    }

    public float OverlayY(int layer)
    {
        return 0f;
    }


    // functions
    public void A_ActiveAndUnblock()
    {

    }

    public void A_ActiveSound()
    {

    }

    public void A_AlertMonsters(float maxdist, int flags)
    {

    }

    public void A_BabyMetal()
    {

    }

    public void A_Bang4Cloud()
    {

    }

    public void A_BarrelDestroy()
    {

    }

    public void A_BasicAttack(int meleedamage, AudioSource meleesound, Actor missiletype, float missileheight)
    {

    }

    public void A_BetaSkullAttack()
    {

    }

    public void A_BFGSpray(Actor spraytype, int numrays, int damagecount, float angle, float distance, float vrange, int damage, int flags)
    {
        numrays = 40;
        damagecount = 15;
        angle = 90;
        distance = 16 * 64;
        vrange = 32;

    }

    public void A_BishopMissileWeave()
    {

    }

    public void A_Blast(int flags, float strength, float radius, float speed, Actor blasteffect, AudioSource blastsound)
    {
        strength = 255;
        radius = 255;
        speed = 20;
    }

    public void A_BossDeath()
    {

    }

    public void A_BrainAwake()
    {

    }

    public void A_BrainDie()
    {

    }

    public void A_BrainExplode()
    {

    }

    public void A_BrainPain()
    {

    }

    public void A_BrainScream()
    {

    }

    public void A_BrainSpit(Actor spawntype)   // needs special treatment for default
    {

    }

    public void A_BruisAttack()
    {

    }

    public void A_BspiAttack()
    {

    }

    public void A_BulletAttack()
    {

    }

    public void A_Burst(Actor chunktype)
    {

    }

    public bool A_CallSpecial(int special, int arg1 = 0, int arg2 = 0, int arg3 = 0, int arg4 = 0, int arg5 = 0)
    {
        return false;
    }

    public void A_CentaurDefend()
    {

    }

    public void A_ChangeFlag(string flagname, bool value)
    {

    }

    public void A_ChangeVelocity(float x, float y, float z, int flags, int ptr)
    {

    }

    //public void A_Chase(state melee = "*", state missile = "none", int flags = 0)

    /*
    state A_CheckBlock(state block, int flags = 0, int ptr = AAPTR_DEFAULT, float xofs = 0, float yofs = 0,
                                     float zofs = 0, float angle = 0);

    state A_CheckCeiling(state label);

    state A_CheckFlag(string flagname, state label, int check_pointer = AAPTR_DEFAULT);

    state A_CheckFloor(state label);

    state A_CheckLOF(state jump, int flags = 0, float range = 0, float minrange = 0, float angle = 0,
                                   float pitch = 0, float offsetheight = 0, float offsetwidth = 0,
                                   int ptr_target = AAPTR_DEFAULT, float offsetforward = 0);
*/
    public void A_CheckPlayerDone()
    {

    }

    /*
    state A_CheckProximity(state jump, class<Actor> classname, float distance, int count = 1, int flags = 0,
                                       int ptr = AAPTR_DEFAULT);

    state A_CheckRange(float distance, state label, bool two_dimension = false);

    state A_CheckSight(state label);

    state A_CheckSightOrRange(float distance, state label, bool two_dimension = false);

    state A_CheckSpecies(state jump, name species = "", int ptr = AAPTR_DEFAULT);
    */
    public void A_CheckTerrain()
    {

    }

    public void A_ClassBossHealth()
    {

    }

    public void A_ClearLastHeard()
    {

    }

    public int A_ClearOverlays(int sstart = 0, int sstop = 0, bool safety = true)
    {
        return 0;
    }

    public void A_ClearShadow()
    {

    }

    public void A_ClearSoundTarget()
    {

    }

    public void A_ClearTarget()
    {

    }

    public void A_ComboAttack()
    {

    }

    public void A_CopyFriendliness(int ptr_source)
    {

    }

    public bool A_CopySpriteFrame(int from, int to, int flags = 0)
    {
        return false;
    }

    public void A_Countdown()
    {

    }

    //public void A_CountdownArg(int argnum, state targstate = "")


    public void A_CPosAttack()
    {

    }

    public void A_CPosRefire()
    {

    }

    public void A_CStaffMissileSlither()
    {

    }

    public void A_CustomBulletAttack(float spread_xy, float spread_z, int numbullets, int damageperbullet, int ptr, string pufftype = "BulletPuff", float range = 0, int flags = 0, string missile = "", float Spawnheight = 32, float Spawnofs_xy = 0)
    {

    }

    public void A_CustomComboAttack(string missiletype, float spawnheight, int damage, AudioClip meleesound, string damagetype = "none", bool bleed = true)
    {

    }

    public void A_CustomMeleeAttack(int damage, AudioClip meleesound, AudioClip misssound, string damagetype = "none", bool bleed = true)
    {

    }

    public void A_CustomMissile(string missiletype, int ptr, float spawnheight = 32, float spawnofs_xy = 0, float angle = 0, int flags = 0, float pitch = 0)
    {

    }

    public void A_CustomRailgun(int damage, int spawnofs_xy, Color color1, Color color2, int flags, int aim, float maxdiff, float spread_xy, float spread_z, float range, int duration, string puffType = "BulletPuff", float sparsity = 1.0f, float driftspeed = 1.0f, string spawnclass = "none", float spawnofs_z = 0, int spiraloffset = 270, int limit = 0)
    {

    }

    public void A_CyberAttack()
    {

    }

    public void A_DamageChildren(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None")
    {

    }

    public void A_DamageMaster(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None")
    {

    }

    public void A_DamageSelf(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None")
    {

    }

    public void A_DamageSiblings(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None")
    {

    }

    public void A_DamageTarget(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None")
    {

    }

    public void A_DamageTracer(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None")
    {

    }

    public void A_DeQueueCorpse()
    {

    }
    public void A_Detonate()
    {

    }
    public void A_Die(string damagetype = "none")
    {

    }
    public void A_DropFire()
    {

    }
    public void A_DropInventory(string itemtype)
    {

    }

    public void A_DropItem(string item, int dropamount = -1, int chance = 256)
    {

    }

    public void A_DropWeaponPieces(Actor p1, Actor p2, Actor p3)
    {

    }

    public void A_DualPainAttack(string spawntype = "LostSoul")
    {

    }

    public int A_Explode(int flags, int damage = -1, int distance = -1, bool alert = false, int fulldamagedistance = 0, int nails = 0, int naildamage = 10, string pufftype = "BulletPuff", string damagetype = "none")
    {
        return 0;
    }

    public void A_ExtChase(bool usemelee, bool usemissile, bool playactive = true, bool nightmarefast = false)
    {

    }
    public void A_FaceConsolePlayer(float MaxTurnAngle = 0) // [TP] no-op
    {

    }

    public void A_FaceMaster(float max_turn = 0, float max_pitch = 270, float ang_offset = 0, float pitch_offset = 0, int flags = 0, float z_ofs = 0)
    {

    }

    public bool A_FaceMovementDirection(int ptr, float offset = 0, float anglelimit = 0, float pitchlimit = 0, int flags = 0)
    {
        return false;
    }

    public void A_FaceTarget(float max_turn = 0, float max_pitch = 270, float ang_offset = 0, float pitch_offset = 0, int flags = 0, float z_ofs = 0)
    {

    }

    public void A_FaceTracer(float max_turn = 0, float max_pitch = 270, float ang_offset = 0, float pitch_offset = 0, int flags = 0, float z_ofs = 0)
    {

    }

    public void A_FadeIn(float reduce = 0.1f, int flags = 0)
    {

    }

    public void A_FadeOut(float reduce = 0.1f, int flags = 1)
    {

    }

    public void A_FadeTo(float target, float amount = 0.1f, int flags = 0)
    {

    }

    public void A_Fall()
    {

    }

    public void A_FastChase()
    {

    }

    public void A_FatAttack1(string spawntype = "FatShot")
    {

    }


    public void A_FatAttack2(string spawntype = "FatShot")
    {

    }

    public void A_FatAttack3(string spawntype = "FatShot")
    {

    }

    public void A_FatRaise()
    {

    }

    public void A_Feathers()
    {

    }

    public void A_Fire(float spawnheight = 0f)
    {

    }

    public void A_FireAssaultGun()
    {

    }

    public void A_FireCrackle()
    {

    }

    public void A_FLoopActiveSound()
    {

    }

    public void A_FreezeDeath()
    {

    }

    public void A_FreezeDeathChunks()
    {

    }

    public void A_GenericFreezeDeath()
    {

    }

    public void A_GetHurt()
    {

    }

    public bool A_GiveInventory(string itemtype, int amount, int giveto)
    {
        return false;
    }

    public void A_GiveQuestItem(int itemno)
    {

    }

    public int A_GiveToChildren(string itemtype, int amount)
    {
        return 0;
    }


    public int A_GiveToSiblings(string itemtype, int amount)
    {
        return 0;
    }

    public bool A_GiveToTarget(string itemtype, int amount, int forward_ptr)
    {
        return false;
    }

    public void A_Gravity()
    {

    }

    public void A_HeadAttack()
    {

    }

    public void A_HideThing()
    {

    }

    public void A_Hoof()
    {

    }

    public void A_IceGuyDie()
    {

    }


    /*
    state A_Jump(int chance = 256, state label, ...);
    state A_JumpIf(bool expression, state label);
    state A_JumpIfArmorType(name Type, state label, int amount = 1);
    state A_JumpIfCloser(float distance, state label, bool noz = FALSE);
    state A_JumpIfHealthLower(int health, state label, int ptr_selector = AAPTR_DEFAULT);
    state A_JumpIfHigherOrLower(state high, state low, float offsethigh = 0, float offsetlow = 0, bool includeHeight = TRUE,
                                              int ptr = AAPTR_TARGET);
    state A_JumpIfintargetInventory(class<Inventory> itemtype, int amount, state label,
                                                int forward_ptr = AAPTR_DEFAULT);
    state A_JumpIfintargetLOS(state label, float fov = 0, int flags = 0, float dist_max = 0,
                                            float dist_close = 0);
    state A_JumpIfInventory(class<Inventory> itemtype, int itemamount, state label,
                                        int owner = AAPTR_DEFAULT);
    state A_JumpIfMasterCloser(float distance, state label, bool noz = FALSE);
    state A_JumpIfTargetInLOS(state label, float fov = 0, int flags = 0, float dist_max = 0,
                                            float dist_close = 0);
    state A_JumpIfTargetInsideMeleeRange(state label);
    state A_JumpIfTargetOutsideMeleeRange(state label);
    state A_JumpIfTracerCloser(float distance, state label, bool noz = FALSE);
    */



    public void A_KeenDie(int doortag = 666)
    {

    }

    public void A_KillChildren(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None")
    {

    }

    public void A_KillMaster(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None")
    {

    }

    public void A_KillSiblings(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None")
    {

    }

    public void A_KillTarget(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None")
    {

    }

    public void A_KillTracer(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None")
    {

    }

    public void A_KlaxonBlare()
    {

    }

    public bool A_LineEffect(int boomspecial = 0, int tag = 0)
    {
        return false;
    }

    public void A_Log(string whattoprint)
    {

    }

    public void A_Logfloat(float whattoprint)
    {

    }

    public void A_Logint(int whattoprint)
    {

    }

    public void A_Look()
    {

    }

    public void A_Look2()
    {

    }

    //public void A_LookEx(int flags = 0, float minseedist = 0, float maxseedist = 0, float maxheardist = 0, float fov = 0, state label = "");

    public void A_LoopActiveSound()
    {

    }
    public void A_LowGravity()
    {

    }
    public void A_M_Saw(AudioClip fullsound, AudioClip hitsound, int damage = 2, string pufftype = "BulletPuff")
    {

    }

    public void A_MeleeAttack()
    {

    }
    public void A_Metal()
    {

    }
    public void A_MissileAttack()
    {

    }
    public void A_MonsterRail()
    {

    }
    //state A_MonsterRefire(int chance, state label);
    public void A_Mushroom(string spawntype = "FatShot", int numspawns = 0, int flags = 0, float vrange = 4.0f, float hrange = 0.5f)
    {

    }

    public void A_NoBlocking()
    {

    }

    public void A_NoGravity()
    {

    }

    //bool A_Overlay(int layer, state start = "", bool nooverride = FALSE)

    public void A_OverlayFlags(int layer, int flags, bool set)
    {

    }

    public void A_OverlayOffset(int layer, float wx = 0, float wy = 32, int flags = 0)
    {

    }

    public void A_Pain()
    {

    }

    public void A_PainAttack(string spawntype = "LostSoul", float angle = 0, int flags = 0, int limit = -1)
    {

    }
    public void A_PainDie(string spawntype = "LostSoul")
    {

    }


    public void A_PigPain()
    {

    }

    public void A_PlayerScream()
    {

    }

    //void state A_PlayerSkinCheck(state label)

    public void A_PlaySound(float attenuation, AudioClip whattoplay, int slot, float volume = 1.0f, bool looping = false)
    {

    }

    public void A_PlaySoundEx(AudioClip whattoplay, string slot, bool looping = false, int attenuation = 0)
    {

    }

    public void A_PlayWeaponSound(AudioClip whattoplay)
    {

    }

    public void A_PosAttack()
    {

    }

    public void A_Print(string whattoprint, float time = 0, string fontname = "")
    {

    }

    public void A_PrintBold(string whattoprint, float time = 0, string fontname = "")
    {

    }

    public void A_Punch()
    {

    }

    public void A_Quake(int intensity, int duration, int damrad, int tremrad, AudioClip sfx)
    {

    }

    public void A_QuakeEx(int intensityX, int intensityY, int intensityZ, int duration, int damrad, int tremrad, AudioClip sfx, int flags = 0, float mulWaveX = 1, float mulWaveY = 1, float mulWaveZ = 1, int falloff = 0, int highpoint = 0, float rollintensity = 0, float rollWave = 0)
    {

    }

    public void A_QueueCorpse()
    {

    }

    public void A_RadiusDamageSelf(int damage = 128, float distance = 128, int flags = 0, string flashtype = "None")
    {

    }

    public int A_RadiusGive(string itemtype, float distance, int flags, int amount = 0, string filter = "None", string species = "None", float mindist = 0f, int limit = 0)
    {
        return 0;
    }

    public void A_RadiusThrust(int flags, int force = 128, int distance = -1, int fullthrustdistance = 0)
    {

    }

    public void A_RaiseChildren(bool copy)
    {

    }

    public void A_RaiseMaster(bool copy)
    {

    }

    public void A_RaiseSiblings(bool copy)
    {

    }

    public void A_RearrangePointers(int newtarget, int newmaster, int newtracer, int flags = 0)
    {

    }

    public void A_Recoil(float xyvel)
    {

    }

    public void A_RemoveChildren(bool removeall, int flags, string filter = "None", string species = "None")
    {

    }

    public void A_RemoveForcefield()
    {

    }

    public void A_RemoveMaster(int flags = 0, string filter = "None", string species = "None")
    {

    }

    public void A_RemoveSiblings(bool removeall = false, int flags = 0, string filter = "None", string species = "None")
    {

    }

    public void A_RemoveTarget(int flags = 0, string filter = "None", string species = "None")
    {

    }

    public void A_RemoveTracer(int flags = 0, string filter = "None", string species = "None")
    {

    }

    public void A_Remove(int removee, int flags = 0, string filter = "None", string species = "None")
    {

    }

    public void A_ResetHealth(int ptr)
    {

    }

    public void A_Respawn(int flags = 1)
    {

    }

    public void A_RocketInFlight()
    {

    }

    public void A_SargAttack()
    {

    }

    public void A_ScaleVelocity(float scale, int ptr)
    {

    }
    public void A_Scream()
    {

    }

    public void A_ScreamAndUnblock()
    {

    }

    public void A_SeekerMissile(int threshold, int turnmax, int flags = 0, int chance = 50, int distance = 10)
    {

    }

    public bool A_SelectWeapon(string whichweapon, int flags)
    {
        return false;
    }

    public void A_SentinelBob()
    {

    }

    public void A_SentinelRefire()
    {

    }

    public void A_SetAngle(float angle, int flags, int ptr)
    {

    }

    public void A_SetArg(int pos, int value)
    {

    }

    public void A_SetBlend(Color color1, float alpha, int tics, Color color2)
    {

    }

    public void A_SetChaseThreshold(int threshold, bool def, int ptr)
    {

    }

    public void A_SetDamageType(string damagetype)
    {

    }

    public void A_Setfloat()
    {

    }

    public void A_SetfloatBobPhase(int bob)
    {

    }

    public void A_SetfloatSpeed(float speed, int ptr)
    {

    }

    public void A_SetFloorClip()
    {

    }

    public void A_SetGravity(float gravity)
    {

    }

    public void A_SetHealth(int health, int ptr)
    {

    }

    public bool A_SetInventory(string itemtype, int amount, int ptr, bool beyondMax)
    {
        return false;
    }

    public void A_SetInvulnerable()
    {

    }

    public void A_SetMass(int mass)
    {

    }

    public void A_SetPainthreshold(int threshold, int ptr)
    {

    }

    public void A_SetPitch(float pitch, int flags, int ptr)
    {

    }

    public void A_SetReflective()
    {

    }

    public void A_SetReflectiveInvulnerable()
    {

    }

    public void A_SetRenderStyle(float alpha, int style)
    {

    }

    public void A_SetRipperLevel(int level)
    {

    }

    public void A_SetRipMin(int minimum)
    {

    }

    public void A_SetRipMax(int maximum)
    {

    }

    public void A_SetRoll(float roll, int flags, int ptr)
    {

    }

    public void A_SetScale(float scalex, float scaley, int ptr, bool usezero)
    {

    }

    public void A_SetShadow()
    {

    }

    public void A_SetShootable()
    {

    }

    public void A_SetSolid()
    {

    }

    public void A_SetSpecial(int spec, int arg0 = 0, int arg1 = 0, int arg2 = 0, int arg3 = 0, int arg4 = 0)
    {

    }

    public void A_SetSpecies(string species, int ptr)
    {

    }

    public void A_SetSpeed(float speed, int ptr)
    {

    }

    public bool A_SetSpriteAngle(float angle, int ptr)
    {
        return false;
    }

    public bool A_SetSpriteRotation(float angle, int ptr)
    {
        return false;
    }

    public void A_SetTeleFog(string oldpos, string newpos)
    {

    }

    public void A_SetTics(int tics)
    {

    }

    public void A_SetTranslation(string transname)
    {

    }

    public void A_SetTranslucent(float alpha, int style = 0)
    {

    }

    public void A_SetUserArray(string varname, int index, int value)
    {

    }

    public void A_SetUserArrayfloat(string varname, int index, float value)
    {

    }

    public void A_SetUserVar(string varname, int value)
    {

    }

    public void A_SetUserVarfloat(string varname, float value)
    {

    }

    public bool A_SetVisibleRotation(float anglestart, float angleend, float pitchstart, float pitchend, int flags, int ptr)
    {
        return false;
    }

    public void A_ShootGun()
    {

    }

    public void A_SkelFist()
    {

    }

    public void A_SkelMissile()
    {

    }

    public void A_SkelWhoosh()
    {

    }

    public void A_SkullAttack(float speed = 20)
    {

    }

    public void A_SkullPop(string skulltype = "BloodySkull")
    {

    }

    public void A_SpawnDebris(string spawntype, bool transfer_translation = false, float mult_h = 1, float mult_v = 1)
    {

    }

    public void A_SpawnFly(string spawntype = "none")   // needs special treatment for default
    {

    }

    public bool A_SpawnItem(string itemtype = "Unknown", float distance = 0, float zheight = 0, bool useammo = true, bool transfer_translation = false)
    {
        return false;
    }

    public bool A_SpawnItemEx(string itemtype, float xofs = 0, float yofs = 0, float zofs = 0, float xvel = 0, float yvel = 0, float zvel = 0, float angle = 0, int flags = 0, int failchance = 0, int tid = 0)
    {
        return false;
    }

    public void A_SpawnParticle(Color color1, int flags = 0, int lifetime = 35, float size = 1, float angle = 0, float xoff = 0, float yoff = 0, float zoff = 0, float velx = 0, float vely = 0, float velz = 0, float accelx = 0, float accely = 0, float accelz = 0, float startalphaf = 1, float fadestepf = -1, float sizestep = 0)
    {

    }

    public void A_SpawnSound()
    {

    }

    public void A_SpidRefire()
    {

    }

    public void A_SPosAttack()
    {

    }

    public void A_SPosAttackUseAtkSound()
    {

    }

    public void A_StartFire()
    {

    }

    public void A_Stop()
    {

    }

    public void A_StopSound(int slot) // Bad default but that's what is originally was...
    {

    }

    public void A_StopSoundEx(string slot)
    {

    }

    public void A_SwapTeleFog()
    {

    }

    public int A_TakeFromChildren(string itemtype, int amount = 0)
    {
        return 0;
    }

    public int A_TakeFromSiblings(string itemtype, int amount = 0)
    {
        return 0;
    }

    public bool A_TakeFromTarget(string itemtype, int amount, int flags, int forward_ptr)
    {
        return false;
    }

    public bool A_TakeInventory(string itemtype, int amount, int flags, int giveto)
    {
        return false;
    }

    //state, bool A_Teleport(state teleportstate = "", class<SpecialSpot> targettype = "BossSpot",
    //                                   class<Actor> fogtype = "TeleportFog", int flags = 0, float mindist = 0, float maxdist = 0,
    //                                   int ptr = AAPTR_DEFAULT);

    public bool A_ThrowGrenade(string itemtype, float zheight = 0, float xyvel = 0, float zvel = 0, bool useammo = true)
    {
        return false;
    }

    public void A_TossGib()
    {

    }

    public void A_Tracer()
    {

    }

    public void A_Tracer2()
    {

    }

    public void A_TransferPointer(int ptr_source, int ptr_recipient, int sourcefield, int recipientfield, int flags)
    {

    }

    public void A_TroopAttack()
    {

    }

    public void A_TurretLook()
    {

    }

    public void A_Turn(float angle = 0)
    {

    }

    public void A_UnHideThing()
    {

    }

    public void A_Unsetfloat()
    {

    }

    public void A_UnSetFloorClip()
    {

    }

    public void A_UnSetInvulnerable()
    {

    }

    public void A_UnSetReflective()
    {

    }

    public void A_UnSetReflectiveInvulnerable()
    {

    }

    public void A_UnSetShootable()
    {

    }

    public void A_UnsetSolid()
    {

    }

    public void A_VileAttack(AudioClip snd, int initialdmg = 20, int blastdmg = 70, int blastradius = 70, float thrustfac = 1.0f, string damagetype = "Fire", int flags = 0)
    {

    }

    public void A_VileChase()
    {

    }

    public void A_VileStart()
    {

    }

    public void A_VileTarget(string fire = "ArchvileFire")
    {

    }

    public void A_Wander(int flags = 0)
    {

    }

    //state, bool A_Warp(int ptr_destination, float xofs = 0, float yofs = 0, float zofs = 0, float angle = 0,
    //                                 int flags = 0, state success_state = "", float heightoffset = 0, float radiusoffset = 0,
    //                                 float pitch = 0);

    public void A_WeaponOffset(float wx = 0, float wy = 32, int flags = 0)
    {

    }

    public void A_Weave(int xspeed, int yspeed, float xdist, float ydist)
    {

    }

    public void A_WolfAttack(int flags, AudioClip whattoplay, float snipe = 1.0f, int maxdamage = 64, int blocksize = 128, int pointblank = 2, int longrange = 4, float runspeed = 160.0f, string pufftype = "BulletPuff")
    {

    }

    public void A_XScream()
    {

    }

    public int ACS_NamedExecute(string script, int mapnum = 0, int arg1 = 0, int arg2 = 0, int arg3 = 0)
    {
        return 0;
    }

    public int ACS_NamedSuspend(string script, int mapnum = 0)
    {
        return 0;
    }

    public int ACS_NamedTerminate(string script, int mapnum = 0)
    {
        return 0;
    }

    public int ACS_NamedLockedExecute(string script, int mapnum = 0, int arg1 = 0, int arg2 = 0, int lck = 0)
    {
        return 0;
    }

    public int ACS_NamedLockedExecuteDoor(string script, int mapnum = 0, int arg1 = 0, int arg2 = 0, int lck = 0)
    {
        return 0;
    }

    public int ACS_NamedExecuteWithResult(string script, int arg1 = 0, int arg2 = 0, int arg3 = 0, int arg4 = 0)
    {
        return 0;
    }

    public void A_CS_NamedExecuteAlways(string script, int mapnum = 0, int arg1 = 0, int arg2 = 0, int arg3 = 0)
    {

    }

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

public class Monster : Actor
{
    /*
    SHOOTABLE
    COUNTKILL
    SOLID
    CANPUSHWALLS
    CANUSEWALLS
    ACTIVATEMCROSS
    CANPASS
    ISMONSTER
    */

    //other monster stuff
}


public class ShotgunGuy : Monster
{


    //Monster
    //+FLOORCLIP




    public void Awake()
    {
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

    //Monster
    //+FLOORCLIP

    public void Awake()
    {
        Health = 70;
        Radius = 20;
        Height = 56;
        Mass = 100;
        Speed = 8;
        PainChance = 170;
        sprite = "CPOS";
        Name = "Chaingun Guy";


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
    //Monster
    //+FLOORCLIP
    //+BOSSDEATH



    public virtual void Awake()
    {
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

        SeeSound = "baron/sight";
        PainSound = "baron/pain";
        DeathSound = "baron/death";
        ActiveSound = "baron/active";
        Obituary = "$OB_BARON";
        HitObituary = "$OB_BARONHIT";

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
    //Monster
    //+FLOORCLIP



    public void Awake()
    {

        SeeSound = "grunt/sight";
        AttackSound = "grunt/attack";
        PainSound = "grunt/pain";
        DeathSound = "grunt/death";
        ActiveSound = "grunt/active";
        Obituary = "$OB_ZOMBIE";
        DropItem = "Clip";


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
    //Monster
    //+FLOORCLIP


    public void Awake()
    {
        Health = 60;
        Radius = 20;
        Height = 56;
        Mass = 100;
        Speed = 8;
        PainChance = 200;
        sprite = "TROO";
        Name = "Imp";

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

    //Monster
    //+FLOORCLIP
    //+BOSSDEATH



    public void Awake()
    {
        Health = 500;
        Radius = 64;
        Height = 64;
        Mass = 600;
        Speed = 12;
        PainChance = 128;
        sprite = "BSPI";
        Name = "Arachnotron";

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
    //Monster
    //+BOSS
    //+MISSILEMORE
    //+FLOORCLIP
    //+NORADIUSDMG
    //+DONTMORPH
    //+BOSSDEATH



    public void Awake()
    {
        Health = 3000;
        Radius = 128;
        Height = 100;
        Mass = 1000;
        Speed = 12;
        PainChance = 40;
        sprite = "SPID";
        Name = "Spider Mastermind";

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

    //+FLOORCLIP


    public virtual void Awake()
    {
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

        SeeSound = "demon/sight";
        AttackSound = "demon/attack";
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

    //+SHADOW
    //RenderStyle = OptFuzzy
    //Aplha = 0.5f;

    public override void Awake()
    {
        Name = "Spectre";

        OnAwake();
        SeeSound = "spectre/sight";
        AttackSound = "spectre/attack";
        PainSound = "spectre/pain";
        DeathSound = "spectre/death";
        ActiveSound = "spectre/active";
        Obituary = "$OB_SPECTREHIT";
    }


}



public class Archvile : Monster
{
    //+QUICKTORETALIATE
    //+FLOORCLIP
    //+NOTARGET



    public void Awake()
    {
        Health = 700;
        Radius = 20;
        Height = 56;
        Mass = 500;
        Speed = 15;
        PainChance = 10;
        sprite = "VILE";
        Name = "Archvile";

        SeeSound = "vile/sight";
        AttackSound = "vile/attack";
        PainSound = "vile/pain";
        DeathSound = "vile/death";
        ActiveSound = "vile/active";
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

        //-BOSSDEATH

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
    //Monster

    //+BOSS
    //+MISSILEMORE
    //+FLOORCLIP
    //+NORADIUSDMG
    //+DONTMORPH
    //+BOSSDEATH



    public void Awake()
    {
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
        AttackSound = "cyber/attack";
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

    //Monster
    //+FLOORCLIP
    //+BOSSDEATH



    public void Awake()
    {
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
    //Monster
    //+FLOAT
    //+NOGRAVITY
    //+MISSILEMORE
    //+DONTFALL
    //+NOTICEDEATH


    //RenderStyle = SoulTrans;

    public void Awake()
    {
        Health = 100;
        Radius = 16;
        Height = 56;
        Mass = 50;
        Speed = 8;
        Damage = 3;
        PainChance = 256;
        sprite = "SKUL";
        Name = "Lost Soul";

        SeeSound = "skull/sight";
        AttackSound = "skull/attack";
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
    //Monster
    //+FLOAT
    //+NOGRAVITY

    public void Awake()
    {
        Health = 400;
        Radius = 31;
        Height = 56;
        Mass = 400;
        Speed = 8;
        PainChance = 128;
        sprite = "PAIN";
        Name = "Pain Elemental";

        SeeSound = "pain/sight";
        AttackSound = "pain/attack";
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

    //Monster

    //+MISSILEMORE
    //+FLOORCLIP


    public void Awake()
    {
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
    //Monster
    //+FLOORCLIP



    public void Awake()
    {
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

    //Monster
    //+FLOORCLIP


    public void Awake()
    {
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
