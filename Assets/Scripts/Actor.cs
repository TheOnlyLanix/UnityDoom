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


    public string Paintype = "Normal";
    public string DeathType = "Normal";
    public string TeleFogSourceType = "TeleportFog";
    public string TeleFogDestType = "TeleportFog";

    public int RipperLevel = 0;
    public int RipLevelMin = 0;
    public int RipLevelMax = 0;
    public int DefThreshold = 100;

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

    bool IsPointerEqual(int ptr_select1, int ptr_select2)
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
    void A_ActiveAndUnblock()
    {

    }

    void A_ActiveSound()
    {

    }

    void A_AlertMonsters(float maxdist, int flags)
    {

    }

    void A_BabyMetal()
    {

    }

    void A_Bang4Cloud()
    {

    }

    void A_BarrelDestroy()
    {

    }

    void A_BasicAttack(int meleedamage, AudioSource meleesound, Actor missiletype, float missileheight)
    {

    }

    void A_BetaSkullAttack()
    {

    }

    void A_BFGSpray(Actor spraytype, int numrays, int damagecount, float angle, float distance, float vrange, int damage, int flags)
    {
        numrays = 40;
        damagecount = 15;
        angle = 90;
        distance = 16 * 64;
        vrange = 32;

    }

    void A_BishopMissileWeave()
    {

    }

    void A_Blast(int flags, float strength, float radius, float speed, Actor blasteffect, AudioSource blastsound)
    {
        strength = 255;
        radius = 255;
        speed = 20;
    }

    void A_BossDeath()
    {

    }

    void A_BrainAwake()
    {

    }

    void A_BrainDie()
    {

    }

    void A_BrainExplode()
    {

    }

    void A_BrainPain()
    {

    }

    void A_BrainScream()
    {

    }

    void A_BrainSpit(Actor spawntype)   // needs special treatment for default
    {

    }

    void A_BruisAttack()
    {

    }

    void A_BspiAttack()
    {

    }

    void A_BulletAttack()
    {

    }

    void A_Burst(Actor chunktype)
    {

    }

    bool A_CallSpecial(int special, int arg1 = 0, int arg2 = 0, int arg3 = 0, int arg4 = 0, int arg5 = 0)
    {
        return false;
    }

    void A_CentaurDefend()
    {

    }

    void A_ChangeFlag(string flagname, bool value)
    {

    }

    void A_ChangeVelocity(float x, float y, float z, int flags, int ptr)
    {

    }

    //void A_Chase(state melee = "*", state missile = "none", int flags = 0)

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
    void A_CheckPlayerDone()
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
    void A_CheckTerrain()
    {

    }

    void A_ClassBossHealth()
    {

    }

    void A_ClearLastHeard()
    {

    }

    int A_ClearOverlays(int sstart = 0, int sstop = 0, bool safety = true)
    {
        return 0;
    }

    void A_ClearShadow()
    {

    }

    void A_ClearSoundTarget()
    {

    }

    void A_ClearTarget()
    {

    }

    void A_ComboAttack()
    {

    }

    void A_CopyFriendliness(int ptr_source)
    {

    }

    bool A_CopySpriteFrame(int from, int to, int flags = 0)
    {
        return false;
    }

    void A_Countdown()
    {

    }

    //void A_CountdownArg(int argnum, state targstate = "")


    void A_CPosAttack()
    {

    }

    void A_CPosRefire()
    {

    }

    void A_CStaffMissileSlither()
    {

    }

    void A_CustomBulletAttack(float spread_xy, float spread_z, int numbullets, int damageperbullet, int ptr, string pufftype = "BulletPuff", float range = 0, int flags = 0, string missile = "", float Spawnheight = 32, float Spawnofs_xy = 0)
    {

    }

    void A_CustomComboAttack(string missiletype, float spawnheight, int damage, AudioClip meleesound, string damagetype = "none", bool bleed = true)
    {

    }

    void A_CustomMeleeAttack(int damage, AudioClip meleesound, AudioClip misssound, string damagetype = "none", bool bleed = true)
    {

    }

    void A_CustomMissile(string missiletype, int ptr, float spawnheight = 32, float spawnofs_xy = 0, float angle = 0, int flags = 0, float pitch = 0)
    {

    }

    void A_CustomRailgun(int damage, int spawnofs_xy, Color color1, Color color2, int flags, int aim, float maxdiff, float spread_xy, float spread_z, float range, int duration, string puffType = "BulletPuff", float sparsity = 1.0f, float driftspeed = 1.0f, string spawnclass = "none", float spawnofs_z = 0, int spiraloffset = 270, int limit = 0)
    {

    }

    void A_CyberAttack()
    {

    }

    void A_DamageChildren(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None")
    {

    }

    void A_DamageMaster(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None")
    {

    }

    void A_DamageSelf(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None")
    {

    }

    void A_DamageSiblings(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None")
    {

    }

    void A_DamageTarget(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None")
    {

    }

    void A_DamageTracer(int amount, int src, int inflict, string damagetype = "none", int flags = 0, string filter = "None", string species = "None")
    {

    }

    void A_DeQueueCorpse()
    {

    }
    void A_Detonate()
    {

    }
    void A_Die(string damagetype = "none")
    {

    }
    void A_DropFire()
    {

    }
    void A_DropInventory(string itemtype)
    {

    }

    void A_DropItem(string item, int dropamount = -1, int chance = 256)
    {

    }

    void A_DropWeaponPieces(Actor p1, Actor p2, Actor p3)
    {

    }

    void A_DualPainAttack(string spawntype = "LostSoul")
    {

    }

    int A_Explode(int flags, int damage = -1, int distance = -1, bool alert = false, int fulldamagedistance = 0, int nails = 0, int naildamage = 10, string pufftype = "BulletPuff", string damagetype = "none")
    {
        return 0;
    }

    void A_ExtChase(bool usemelee, bool usemissile, bool playactive = true, bool nightmarefast = false)
    {

    }
    void A_FaceConsolePlayer(float MaxTurnAngle = 0) // [TP] no-op
    {

    }

    void A_FaceMaster(float max_turn = 0, float max_pitch = 270, float ang_offset = 0, float pitch_offset = 0, int flags = 0, float z_ofs = 0)
    {

    }

    bool A_FaceMovementDirection(int ptr, float offset = 0, float anglelimit = 0, float pitchlimit = 0, int flags = 0)
    {
        return false;
    }

    void A_FaceTarget(float max_turn = 0, float max_pitch = 270, float ang_offset = 0, float pitch_offset = 0, int flags = 0, float z_ofs = 0)
    {

    }

    void A_FaceTracer(float max_turn = 0, float max_pitch = 270, float ang_offset = 0, float pitch_offset = 0, int flags = 0, float z_ofs = 0)
    {

    }

    void A_FadeIn(float reduce = 0.1f, int flags = 0)
    {

    }

    void A_FadeOut(float reduce = 0.1f, int flags = 1)
    {

    }

    void A_FadeTo(float target, float amount = 0.1f, int flags = 0)
    {

    }

    void A_Fall()
    {

    }

    void A_FastChase()
    {

    }

    void A_FatAttack1(string spawntype = "FatShot")
    {

    }


    void A_FatAttack2(string spawntype = "FatShot")
    {

    }

    void A_FatAttack3(string spawntype = "FatShot")
    {

    }

    void A_FatRaise()
    {

    }

    void A_Feathers()
    {

    }

    void A_Fire(float spawnheight = 0f)
    {

    }

    void A_FireAssaultGun()
    {

    }

    void A_FireCrackle()
    {

    }

    void A_FLoopActiveSound()
    {

    }

    void A_FreezeDeath()
    {

    }

    void A_FreezeDeathChunks()
    {

    }

    void A_GenericFreezeDeath()
    {

    }

    void A_GetHurt()
    {

    }

    bool A_GiveInventory(string itemtype, int amount, int giveto)
    {
        return false;
    }

    void A_GiveQuestItem(int itemno)
    {

    }

    int A_GiveToChildren(string itemtype, int amount)
    {
        return 0;
    }


    int A_GiveToSiblings(string itemtype, int amount)
    {
        return 0;
    }

    bool A_GiveToTarget(string itemtype, int amount, int forward_ptr)
    {
        return false;
    }

    void A_Gravity()
    {

    }

    void A_HeadAttack()
    {

    }

    void A_HideThing()
    {

    }

    void A_Hoof()
    {

    }

    void A_IceGuyDie()
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



    void A_KeenDie(int doortag = 666)
    {

    }

    void A_KillChildren(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None")
    {

    }

    void A_KillMaster(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None")
    {

    }

    void A_KillSiblings(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None")
    {

    }

    void A_KillTarget(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None")
    {

    }

    void A_KillTracer(int src, int inflict, int flags, string damagetype = "none", string filter = "None", string species = "None")
    {

    }

    void A_KlaxonBlare()
    {

    }

    bool A_LineEffect(int boomspecial = 0, int tag = 0)
    {
        return false;
    }

    void A_Log(string whattoprint)
    {

    }

    void A_Logfloat(float whattoprint)
    {

    }

    void A_Logint(int whattoprint)
    {

    }

    void A_Look()
    {

    }

    void A_Look2()
    {

    }

    //void A_LookEx(int flags = 0, float minseedist = 0, float maxseedist = 0, float maxheardist = 0, float fov = 0, state label = "");

    void A_LoopActiveSound()
    {

    }
    void A_LowGravity()
    {

    }
    void A_M_Saw(AudioClip fullsound, AudioClip hitsound, int damage = 2, string pufftype = "BulletPuff")
    {

    }

    void A_MeleeAttack()
    {

    }
    void A_Metal()
    {

    }
    void A_MissileAttack()
    {

    }
    void A_MonsterRail()
    {

    }
    //state A_MonsterRefire(int chance, state label);
    void A_Mushroom(string spawntype = "FatShot", int numspawns = 0, int flags = 0, float vrange = 4.0f, float hrange = 0.5f)
    {

    }

    void A_NoBlocking()
    {

    }

    void A_NoGravity()
    {

    }

    //bool A_Overlay(int layer, state start = "", bool nooverride = FALSE)

    void A_OverlayFlags(int layer, int flags, bool set)
    {

    }

    void A_OverlayOffset(int layer, float wx = 0, float wy = 32, int flags = 0)
    {

    }

    void A_Pain()
    {

    }

    void A_PainAttack(string spawntype = "LostSoul", float angle = 0, int flags = 0, int limit = -1)
    {

    }
    void A_PainDie(string spawntype = "LostSoul")
    {

    }


    void A_PigPain()
    {

    }

    void A_PlayerScream()
    {

    }

    //void state A_PlayerSkinCheck(state label)

    void A_PlaySound(float attenuation, AudioClip whattoplay, int slot, float volume = 1.0f, bool looping = false)
    {

    }

    void A_PlaySoundEx(AudioClip whattoplay, string slot, bool looping = false, int attenuation = 0)
    {

    }

    void A_PlayWeaponSound(AudioClip whattoplay)
    {

    }

    void A_PosAttack()
    {

    }

    void A_Print(string whattoprint, float time = 0, string fontname = "")
    {

    }

    void A_PrintBold(string whattoprint, float time = 0, string fontname = "")
    {

    }

    void A_Punch()
    {

    }

    void A_Quake(int intensity, int duration, int damrad, int tremrad, AudioClip sfx)
    {

    }

    void A_QuakeEx(int intensityX, int intensityY, int intensityZ, int duration, int damrad, int tremrad, AudioClip sfx, int flags = 0, float mulWaveX = 1, float mulWaveY = 1, float mulWaveZ = 1, int falloff = 0, int highpoint = 0, float rollintensity = 0, float rollWave = 0)
    {

    }

    void A_QueueCorpse()
    {

    }

    void A_RadiusDamageSelf(int damage = 128, float distance = 128, int flags = 0, string flashtype = "None")
    {

    }

    int A_RadiusGive(string itemtype, float distance, int flags, int amount = 0, string filter = "None", string species = "None", float mindist = 0f, int limit = 0)
    {
        return 0;
    }

    void A_RadiusThrust(int flags, int force = 128, int distance = -1, int fullthrustdistance = 0)
    {

    }

    void A_RaiseChildren(bool copy)
    {

    }

    void A_RaiseMaster(bool copy)
    {

    }

    void A_RaiseSiblings(bool copy)
    {

    }

    void A_RearrangePointers(int newtarget, int newmaster, int newtracer, int flags = 0)
    {

    }

    void A_Recoil(float xyvel)
    {

    }

    void A_RemoveChildren(bool removeall, int flags, string filter = "None", string species = "None")
    {

    }

    void A_RemoveForcefield()
    {

    }

    void A_RemoveMaster(int flags = 0, string filter = "None", string species = "None")
    {

    }

    void A_RemoveSiblings(bool removeall = false, int flags = 0, string filter = "None", string species = "None")
    {

    }

    void A_RemoveTarget(int flags = 0, string filter = "None", string species = "None")
    {

    }

    void A_RemoveTracer(int flags = 0, string filter = "None", string species = "None")
    {

    }

    void A_Remove(int removee, int flags = 0, string filter = "None", string species = "None")
    {

    }

    void A_ResetHealth(int ptr)
    {

    }

    void A_Respawn(int flags = 1)
    {

    }

    void A_RocketInFlight()
    {

    }

    void A_SargAttack()
    {

    }

    void A_ScaleVelocity(float scale, int ptr)
    {

    }
    void A_Scream()
    {

    }

    void A_ScreamAndUnblock()
    {

    }

    void A_SeekerMissile(int threshold, int turnmax, int flags = 0, int chance = 50, int distance = 10)
    {

    }

    bool A_SelectWeapon(string whichweapon, int flags)
    {
        return false;
    }

    void A_SentinelBob()
    {

    }

    void A_SentinelRefire()
    {

    }

    void A_SetAngle(float angle, int flags, int ptr)
    {

    }

    void A_SetArg(int pos, int value)
    {

    }

    void A_SetBlend(Color color1, float alpha, int tics, Color color2)
    {

    }

    void A_SetChaseThreshold(int threshold, bool def, int ptr)
    {

    }

    void A_SetDamageType(string damagetype)
    {

    }

    void A_Setfloat()
    {

    }

    void A_SetfloatBobPhase(int bob)
    {

    }

    void A_SetfloatSpeed(float speed, int ptr)
    {

    }

    void A_SetFloorClip()
    {

    }

    void A_SetGravity(float gravity)
    {

    }

    void A_SetHealth(int health, int ptr)
    {

    }

    bool A_SetInventory(string itemtype, int amount, int ptr, bool beyondMax)
    {
        return false;
    }

    void A_SetInvulnerable()
    {

    }

    void A_SetMass(int mass)
    {

    }

    void A_SetPainthreshold(int threshold, int ptr)
    {

    }

    void A_SetPitch(float pitch, int flags, int ptr)
    {

    }

    void A_SetReflective()
    {

    }

    void A_SetReflectiveInvulnerable()
    {

    }

    void A_SetRenderStyle(float alpha, int style)
    {

    }

    void A_SetRipperLevel(int level)
    {

    }

    void A_SetRipMin(int minimum)
    {

    }

    void A_SetRipMax(int maximum)
    {

    }

    void A_SetRoll(float roll, int flags, int ptr)
    {

    }

    void A_SetScale(float scalex, float scaley, int ptr, bool usezero)
    {

    }

    void A_SetShadow()
    {

    }

    void A_SetShootable()
    {

    }

    void A_SetSolid()
    {

    }

    void A_SetSpecial(int spec, int arg0 = 0, int arg1 = 0, int arg2 = 0, int arg3 = 0, int arg4 = 0)
    {

    }

    void A_SetSpecies(string species, int ptr)
    {

    }

    void A_SetSpeed(float speed, int ptr)
    {

    }

    bool A_SetSpriteAngle(float angle, int ptr)
    {
        return false;
    }

    bool A_SetSpriteRotation(float angle, int ptr)
    {
        return false;
    }

    void A_SetTeleFog(string oldpos, string newpos)
    {

    }

    void A_SetTics(int tics)
    {

    }

    void A_SetTranslation(string transname)
    {

    }

    void A_SetTranslucent(float alpha, int style = 0)
    {

    }

    void A_SetUserArray(string varname, int index, int value)
    {

    }

    void A_SetUserArrayfloat(string varname, int index, float value)
    {

    }

    void A_SetUserVar(string varname, int value)
    {

    }

    void A_SetUserVarfloat(string varname, float value)
    {

    }

    bool A_SetVisibleRotation(float anglestart, float angleend, float pitchstart, float pitchend, int flags, int ptr)
    {
        return false;
    }

    void A_ShootGun()
    {

    }

    void A_SkelFist()
    {

    }

    void A_SkelMissile()
    {

    }

    void A_SkelWhoosh()
    {

    }

    void A_SkullAttack(float speed = 20)
    {

    }

    void A_SkullPop(string skulltype = "BloodySkull")
    {

    }

    void A_SpawnDebris(string spawntype, bool transfer_translation = false, float mult_h = 1, float mult_v = 1)
    {

    }

    void A_SpawnFly(string spawntype = "none")   // needs special treatment for default
    {

    }

    bool A_SpawnItem(string itemtype = "Unknown", float distance = 0, float zheight = 0, bool useammo = true, bool transfer_translation = false)
    {
        return false;
    }

    bool A_SpawnItemEx(string itemtype, float xofs = 0, float yofs = 0, float zofs = 0, float xvel = 0, float yvel = 0, float zvel = 0, float angle = 0, int flags = 0, int failchance = 0, int tid = 0)
    {
        return false;
    }

    void A_SpawnParticle(Color color1, int flags = 0, int lifetime = 35, float size = 1, float angle = 0, float xoff = 0, float yoff = 0, float zoff = 0, float velx = 0, float vely = 0, float velz = 0, float accelx = 0, float accely = 0, float accelz = 0, float startalphaf = 1, float fadestepf = -1, float sizestep = 0)
    {

    }

    void A_SpawnSound()
    {

    }

    void A_SpidRefire()
    {

    }

    void A_SPosAttack()
    {

    }

    void A_SPosAttackUseAtkSound()
    {

    }

    void A_StartFire()
    {

    }

    void A_Stop()
    {

    }

    void A_StopSound(int slot) // Bad default but that's what is originally was...
    {

    }

    void A_StopSoundEx(string slot)
    {

    }

    void A_SwapTeleFog()
    {

    }

    int A_TakeFromChildren(string itemtype, int amount = 0)
    {
        return 0;
    }

    int A_TakeFromSiblings(string itemtype, int amount = 0)
    {
        return 0;
    }

    bool A_TakeFromTarget(string itemtype, int amount, int flags, int forward_ptr)
    {
        return false;
    }

    bool A_TakeInventory(string itemtype, int amount, int flags, int giveto)
    {
        return false;
    }

    //state, bool A_Teleport(state teleportstate = "", class<SpecialSpot> targettype = "BossSpot",
    //                                   class<Actor> fogtype = "TeleportFog", int flags = 0, float mindist = 0, float maxdist = 0,
    //                                   int ptr = AAPTR_DEFAULT);

    bool A_ThrowGrenade(string itemtype, float zheight = 0, float xyvel = 0, float zvel = 0, bool useammo = true)
    {
        return false;
    }

    void A_TossGib()
    {

    }

    void A_Tracer()
    {

    }

    void A_Tracer2()
    {

    }

    void A_TransferPointer(int ptr_source, int ptr_recipient, int sourcefield, int recipientfield, int flags)
    {

    }

    void A_TroopAttack()
    {

    }

    void A_TurretLook()
    {

    }

    void A_Turn(float angle = 0)
    {

    }

    void A_UnHideThing()
    {

    }

    void A_Unsetfloat()
    {

    }

    void A_UnSetFloorClip()
    {

    }

    void A_UnSetInvulnerable()
    {

    }

    void A_UnSetReflective()
    {

    }

    void A_UnSetReflectiveInvulnerable()
    {

    }

    void A_UnSetShootable()
    {

    }

    void A_UnsetSolid()
    {

    }

    void A_VileAttack(AudioClip snd, int initialdmg = 20, int blastdmg = 70, int blastradius = 70, float thrustfac = 1.0f, string damagetype = "Fire", int flags = 0)
    {

    }

    void A_VileChase()
    {

    }

    void A_VileStart()
    {

    }

    void A_VileTarget(string fire = "ArchvileFire")
    {

    }

  void A_Wander(int flags = 0)
    {

    }

    //state, bool A_Warp(int ptr_destination, float xofs = 0, float yofs = 0, float zofs = 0, float angle = 0,
    //                                 int flags = 0, state success_state = "", float heightoffset = 0, float radiusoffset = 0,
    //                                 float pitch = 0);

    void A_WeaponOffset(float wx = 0, float wy = 32, int flags = 0)
    {

    }

    void A_Weave(int xspeed, int yspeed, float xdist, float ydist)
    {

    }

    void A_WolfAttack(int flags, AudioClip whattoplay, float snipe = 1.0f, int maxdamage = 64, int blocksize = 128, int pointblank = 2, int longrange = 4, float runspeed = 160.0f, string pufftype = "BulletPuff")
    {

    }

    void A_XScream()
    {

    }

    int ACS_NamedExecute(string script, int mapnum = 0, int arg1 = 0, int arg2 = 0, int arg3 = 0)
    {
        return 0;
    }

    int ACS_NamedSuspend(string script, int mapnum = 0)
    {
        return 0;
    }

    int ACS_NamedTerminate(string script, int mapnum = 0)
    {
        return 0;
    }

    int ACS_NamedLockedExecute(string script, int mapnum = 0, int arg1 = 0, int arg2 = 0, int lck=0)
    {
        return 0;
    }

    int ACS_NamedLockedExecuteDoor(string script, int mapnum = 0, int arg1 = 0, int arg2 = 0, int lck = 0)
    {
        return 0;
    }

    int ACS_NamedExecuteWithResult(string script, int arg1 = 0, int arg2 = 0, int arg3 = 0, int arg4 = 0)
    {
        return 0;
    }

    void ACS_NamedExecuteAlways(string script, int mapnum = 0, int arg1 = 0, int arg2 = 0, int arg3 = 0)
    {

    }

    public List<State> actorStates = new List<State>
    {
        new State
        {
           type = "Spawn",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "TNT1", sprInd = "A", time = -1, function = ""},
               new StateInfo{function = "Stop" }
           }
           
       },
       new State
       {
           type = "Null",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "TNT1", sprInd = "A", time = 1, function = ""},
               new StateInfo{function = "Stop"}
           }
       },
       new State
       {
           type = "GenericFreezeDeath",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "####", sprInd = "#", time = 5, function = "A_GenericFreezeDeath" },
               new StateInfo{spr = "----", sprInd = "A", time = 1, function = "A_FreezeDeathChunks" },
               new StateInfo{function = "stop"}
           }
       },
        new State
       {
           type = "GenericCrush",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "POL5", sprInd = "A", time = -1, function = ""},
               new StateInfo {function = "stop"}
           }
       }
    };

    public Actor Clone()
    {
        return (Actor)this.MemberwiseClone();
    }

}

public class Monster: Actor
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

    public string SeeSound = "shotguy/sight";
    public string AttackSound = "shotguy/attack";
    public string PainSound = "shotguy/pain";
    public string DeathSound = "shotguy/death";
    public string ActiveSound = "shotguy/active";
    public string Obituary = "$OB_SHOTGUY";
    public string DropItem = "Shotgun";


    public void Awake()
    {
        Health = 30;
        Radius = 20;
        Height = 56;
        Mass = 100;
        Speed = 8;
        PainChance = 170;
        sprite = "SPOS";

        actorStates = new List<State>
        {
            new State
            {
               type = "Spawn",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SPOS", sprInd = "AB", time = 10, function = "A_Look"},
                   new StateInfo{function = "Loop" }
               }

           },
           new State
           {
               type = "See",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SPOS", sprInd = "AABBCCDD", time = 3, function = "A_Chase"},
                   new StateInfo{function = "Loop"}
               }
           },
           new State
           {
               type = "Missile",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SPOS", sprInd = "E", time = 10, function = "A_FaceTarget" },
                   new StateInfo{spr = "SPOS", sprInd = "F", time = 10, function = "A_SPosAttackUseAtkSound" },
                   new StateInfo{spr = "SPOS", sprInd = "E", time = 10 },
                   new StateInfo{function = "See"}
               }
           },
            new State
            {
               type = "Pain",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SPOS", sprInd = "G", time = 3, function = ""},
                   new StateInfo{spr = "SPOS", sprInd = "G", time = 3, function = "A_Pain"},
                   new StateInfo {function = "See"}
               }
            },
            new State
            {
               type = "Death",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SPOS", sprInd = "H", time = 5, function = "" },
                   new StateInfo{spr = "SPOS", sprInd = "I", time = 5, function = "A_Scream" },
                   new StateInfo{spr = "SPOS", sprInd = "J", time = 5, function = "A_NoBlocking" },
                   new StateInfo{spr = "SPOS", sprInd = "K", time = 5},
                   new StateInfo{spr = "SPOS", sprInd = "L", time = -1},
                   new StateInfo{function = "Stop"}
               }
            },
            new State
            {
               type = "XDeath",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SPOS", sprInd = "M", time = 5, function = "" },
                   new StateInfo{spr = "SPOS", sprInd = "N", time = 5, function = "A_XScream" },
                   new StateInfo{spr = "SPOS", sprInd = "O", time = 5, function = "A_NoBlocking" },
                   new StateInfo{spr = "SPOS", sprInd = "PQRST", time = 5},
                   new StateInfo{spr = "SPOS", sprInd = "U", time = -1},
                   new StateInfo{function = "Stop"}
               }
            },
            new State
            {
               type = "Raise",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SPOS", sprInd = "L", time = 5},
                   new StateInfo{spr = "SPOS", sprInd = "KJIH", time = 5},
                   new StateInfo{function = "See"}
               }
            }
        };

    }
}

public class ChaingunGuy : Monster
{

    //Monster
    //+FLOORCLIP

    public string SeeSound = "chainguy/sight";
    public string AttackSound = "chainguy/attack";
    public string PainSound = "chainguy/pain";
    public string DeathSound = "chainguy/death";
    public string ActiveSound = "chainguy/active";
    public string Obituary = "$OB_CHAINGUY";
    public string DropItem = "Chaingun";


    public void Awake()
    {
        Health = 70;
        Radius = 20;
        Height = 56;
        Mass = 100;

        Speed = 8;
        PainChance = 170;

        sprite = "CPOS";

        actorStates = new List<State>
       {
           new State
           {
               type = "Spawn",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "CPOS", sprInd = "AB", time = 10, function = "A_Look"},
                   new StateInfo{function = "Loop" }
               }

           },
           new State
           {
               type = "See",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "CPOS", sprInd = "AABBCCDD", time = 3, function = "A_Chase"},
                   new StateInfo{function = "Loop"}
               }
           },
           new State
           {
               type = "Missile",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "CPOS", sprInd = "E", time = 10, function = "A_FaceTarget" },
                   new StateInfo{spr = "CPOS", sprInd = "FE", time = 4, function = "A_CPosAttack" },
                   new StateInfo{spr = "CPOS", sprInd = "F", time = 1, function = "A_CPosRefire" },
                   new StateInfo{function = "Missile+1"}
               }
           },
           new State
           {
              type = "Pain",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "CPOS", sprInd = "G", time = 3, function = ""},
                  new StateInfo{spr = "CPOS", sprInd = "G", time = 3, function = "A_Pain"},
                  new StateInfo {function = "See"}
              }
           },
           new State
           {
              type = "Death",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "CPOS", sprInd = "H", time = 5, function = "" },
                  new StateInfo{spr = "CPOS", sprInd = "I", time = 5, function = "A_Scream" },
                  new StateInfo{spr = "CPOS", sprInd = "J", time = 5, function = "A_NoBlocking" },
                  new StateInfo{spr = "CPOS", sprInd = "KLM", time = 5},
                  new StateInfo{spr = "CPOS", sprInd = "N", time = -1},
                  new StateInfo{function = "Stop"}
              }
            },
           new State
           {
              type = "XDeath",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "CPOS", sprInd = "O", time = 5, function = "" },
                  new StateInfo{spr = "CPOS", sprInd = "P", time = 5, function = "A_XScream" },
                  new StateInfo{spr = "CPOS", sprInd = "Q", time = 5, function = "A_NoBlocking" },
                  new StateInfo{spr = "CPOS", sprInd = "RS", time = 5},
                  new StateInfo{spr = "CPOS", sprInd = "T", time = -1},
                  new StateInfo{function = "Stop"}
              }
           },
           new State
           {
              type = "Raise",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "CPOS", sprInd = "N", time = 5},
                  new StateInfo{spr = "CPOS", sprInd = "MLKJIH", time = 5},
                  new StateInfo{function = "See"}
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

    public string SeeSound = "baron/sight";
    public string PainSound = "baron/pain";
    public string DeathSound = "baron/death";
    public string ActiveSound = "baron/active";
    public string Obituary = "$OB_BARON";
    public string HitObituary = "$OB_BARONHIT";

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

        actorStates = new List<State>
        {
            new State
            {
               type = "Spawn",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "BOSS", sprInd = "AB", time = 10, function = "A_Look"},
                   new StateInfo{function = "Loop" }
               }

           },
           new State
           {
               type = "See",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "BOSS", sprInd = "AABBCCDD", time = 3, function = "A_Chase"},
                   new StateInfo{function = "Loop"}
               }
           },
           new State
           {
               type = "Melee"
           },
           new State
           {
               type = "Missile",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "BOSS", sprInd = "EF", time = 8, function = "A_FaceTarget" },
                   new StateInfo{spr = "BOSS", sprInd = "G", time = 8, function = "A_BruisAttack" },
                   new StateInfo{function = "See"}
               }
           },
            new State
            {
               type = "Pain",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "BOSS", sprInd = "H", time = 2, function = ""},
                   new StateInfo{spr = "BOSS", sprInd = "H", time = 2, function = "A_Pain"},
                   new StateInfo {function = "See"}
               }
            },
            new State
            {
               type = "Death",
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
            },
            new State
            {
               type = "Raise",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "BOSS", sprInd = "O", time = 8},
                   new StateInfo{spr = "BOSS", sprInd = "NMLKJI", time = 8},
                   new StateInfo{function = "See"}
               }
            }
        };
    }
}

public class ZombieMan : Monster
{
    //Monster
    //+FLOORCLIP

    public string SeeSound = "grunt/sight";
    public string AttackSound = "grunt/attack";
    public string PainSound = "grunt/pain";
    public string DeathSound = "grunt/death";
    public string ActiveSound = "grunt/active";
    public string Obituary = "$OB_ZOMBIE";
    public string DropItem = "Clip";

    public void Awake()
    {
        Health = 20;
        Radius = 20;
        Height = 56;
        Speed = 8;
        PainChance = 200;
        sprite = "POSS";
        Name = "ZombieMan";
        actorStates = new List<State>
    {
        new State
        {
           type = "Spawn",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "POSS", sprInd = "AB", time = 10, function = "A_Look"},
               new StateInfo{function = "Loop" }
           }

       },
       new State
       {
           type = "See",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "POSS", sprInd = "AABBCCDD", time = 4, function = "A_Chase"},
               new StateInfo{function = "Loop"}
           }
       },
       new State
       {
           type = "Missile",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "POSS", sprInd = "E", time = 10, function = "A_FaceTarget" },
               new StateInfo{spr = "POSS", sprInd = "F", time = 8, function = "A_PosAttack" },
               new StateInfo{spr = "POSS", sprInd = "E", time = 8},
               new StateInfo{function = "See"}
           }
       },
        new State
        {
           type = "Pain",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "POSS", sprInd = "G", time = 3, function = ""},
               new StateInfo{spr = "POSS", sprInd = "G", time = 3, function = "A_Pain"},
               new StateInfo {function = "See"}
           }
        },
        new State
        {
           type = "Death",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "POSS", sprInd = "H", time = 5, function = "" },
               new StateInfo{spr = "POSS", sprInd = "I", time = 5, function = "A_Scream" },
               new StateInfo{spr = "POSS", sprInd = "J", time = 5, function = "A_NoBlocking" },
               new StateInfo{spr = "POSS", sprInd = "K", time = 5},
               new StateInfo{spr = "CPOS", sprInd = "L", time = -1},
               new StateInfo{function = "Stop"}
           }
        },
        new State
        {
           type = "XDeath",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "POSS", sprInd = "M", time = 5, function = "" },
               new StateInfo{spr = "POSS", sprInd = "N", time = 5, function = "A_XScream" },
               new StateInfo{spr = "POSS", sprInd = "O", time = 5, function = "A_NoBlocking" },
               new StateInfo{spr = "POSS", sprInd = "PQRST", time = 5},
               new StateInfo{spr = "POSS", sprInd = "U", time = -1},
               new StateInfo{function = "Stop"}
           }
        },
        new State
        {
           type = "Raise",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "POSS", sprInd = "K", time = 5},
               new StateInfo{spr = "POSS", sprInd = "JIH", time = 5},
               new StateInfo{function = "See"}
           }
        }
    };
    }
}

public class DoomImp : Monster
{
    //Monster
    //+FLOORCLIP
    
    public string SeeSound = "imp/sight";
    public string AttackSound = "imp/attack";
    public string PainSound = "imp/pain";
    public string DeathSound = "imp/death";
    public string ActiveSound = "imp/active";
    public string Obituary = "$OB_IMP";
    public string HitObituary = "$OB_IMPHIT";

    public void Awake()
    {
        Health = 60;
        Radius = 20;
        Height = 56;
        Mass = 100;
        Speed = 8;
        PainChance = 200;
        sprite = "TROO";
        Name = "DoomImp";
        actorStates = new List<State>
    {
        new State
        {
           type = "Spawn",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "TROO", sprInd = "AB", time = 10, function = "A_Look"},
               new StateInfo{function = "Loop" }
           }

       },
       new State
       {
           type = "See",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "TROO", sprInd = "AABBCCDD", time = 3, function = "A_Chase"},
               new StateInfo{function = "Loop"}
           }
       },
       new State
       {
           type = "Melee"
       },
       new State
       {
           type = "Missile",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "TROO", sprInd = "EF", time = 8, function = "A_FaceTarget" },
               new StateInfo{spr = "TROO", sprInd = "G", time = 6, function = "A_TroopAttack" },
               new StateInfo{function = "See"}
           }
       },
        new State
        {
           type = "Pain",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "TROO", sprInd = "H", time = 2, function = ""},
               new StateInfo{spr = "TROO", sprInd = "H", time = 2, function = "A_Pain"},
               new StateInfo {function = "See"}
           }
        },
        new State
        {
           type = "Death",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "TROO", sprInd = "I", time = 5 },
               new StateInfo{spr = "TROO", sprInd = "J", time = 5, function = "A_Scream" },
               new StateInfo{spr = "TROO", sprInd = "K", time = 5},
               new StateInfo{spr = "TROO", sprInd = "L", time = 5, function = "A_NoBlocking" },
               new StateInfo{spr = "TROO", sprInd = "M", time = -1},
               new StateInfo{function = "Stop"}
           }
        },
        new State
        {
           type = "XDeath",
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
        },
        new State
        {
           type = "Raise",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "TROO", sprInd = "ML", time = 8},
               new StateInfo{spr = "TROO", sprInd = "KJI", time = 6},
               new StateInfo{function = "See"}
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

    public string SeeSound = "baby/sight";
    public string AttackSound = "baby/attack";
    public string PainSound = "baby/pain";
    public string DeathSound = "baby/death";
    public string ActiveSound = "baby/active";
    public string Obituary = "$OB_BABY";

    public void Awake()
    {
        Health = 500;
        Radius = 64;
        Height = 64;
        Mass = 600;
        Speed = 12;
        PainChance = 128;
        sprite = "BSPI";

        actorStates = new List<State>
    {
        new State
        {
           type = "Spawn",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "BSPI", sprInd = "AB", time = 10, function = "A_Look"},
               new StateInfo{function = "Loop" }
           }

       },
       new State
       {
           type = "See",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "BSPI", sprInd = "A", time = 20},
               new StateInfo{spr = "BSPI", sprInd = "A", time = 3, function = "A_BabyMetal"},
               new StateInfo{spr = "BSPI", sprInd = "ABBCC", time = 3, function = "A_Chase"},
               new StateInfo{spr = "BSPI", sprInd = "D", time = 3, function = "A_BabyMetal"},
               new StateInfo{spr = "BSPI", sprInd = "DEEFF", time = 3, function = "A_Chase"},
               new StateInfo{function = "See+1"}
           }
       },
       new State
       {
           type = "Missile",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "BSPI", sprInd = "A", time = 20, function = "A_FaceTarget" },
               new StateInfo{spr = "BSPI", sprInd = "G", time = 4, function = "A_BspiAttack" },
               new StateInfo{spr = "BSPI", sprInd = "H", time = 4},
               new StateInfo{spr = "BSPI", sprInd = "H", time = 1, function = "A_SpidRefire" },
               new StateInfo{function = "Missile+1"}
           }
       },
        new State
        {
           type = "Pain",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "BSPI", sprInd = "I", time = 3, function = ""},
               new StateInfo{spr = "BSPI", sprInd = "I", time = 3, function = "A_Pain"},
               new StateInfo {function = "See+1"}
           }
        },
        new State
        {
           type = "Death",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "BSPI", sprInd = "J", time = 20, function = "A_Scream" },
               new StateInfo{spr = "BSPI", sprInd = "K", time = 7, function = "A_NoBlocking" },
               new StateInfo{spr = "BSPI", sprInd = "LMNO", time = 7},
               new StateInfo{spr = "BSPI", sprInd = "P", time = -1, function = "A_BossDeath" },
               new StateInfo{function = "Stop"}
           }
        },
        new State
        {
           type = "Raise",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "BSPI", sprInd = "P", time = 5},
               new StateInfo{spr = "BSPI", sprInd = "ONMLKJ", time = 5},
               new StateInfo{function = "See+1"}
           }
        }
    };
    }
}


public class SpiderMastermind : Monster
{
    //Monster
    new int MinMissileChance = 160;
    //+BOSS
    //+MISSILEMORE
    //+FLOORCLIP
    //+NORADIUSDMG
    //+DONTMORPH
    //+BOSSDEATH

    public string SeeSound = "spider/sight";
    public string AttackSound = "spider/attack";
    public string PainSound = "spider/pain";
    public string DeathSound = "spider/death";
    public string ActiveSound = "spider/active";
    public string Obituary = "$OB_SPIDER";

    public void Awake()
    {
        Health = 3000;
        Radius = 128;
        Height = 100;
        Mass = 1000;
        Speed = 12;
        PainChance = 40;
        sprite = "SPID";

        actorStates = new List<State>
        {
            new State
            {
               type = "Spawn",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SPID", sprInd = "AB", time = 10, function = "A_Look"},
                   new StateInfo{function = "Loop" }
               }

           },
           new State
           {
               type = "See",
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
           },
           new State
           {
               type = "Missile",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SPID", sprInd = "A", time = 20, function = "A_FaceTarget" },
                   new StateInfo{spr = "SPID", sprInd = "G", time = 4, function = "A_SPosAttackUseAtkSound" },
                   new StateInfo{spr = "SPID", sprInd = "H", time = 4, function = "A_SPosAttackUseAtkSound" },
                   new StateInfo{spr = "SPID", sprInd = "H", time = 1, function = "A_SpidRefire" },
                   new StateInfo{function = "Missile+1"}
               }
           },
            new State
            {
               type = "Pain",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SPID", sprInd = "I", time = 3, function = ""},
                   new StateInfo{spr = "SPID", sprInd = "I", time = 3, function = "A_Pain"},
                   new StateInfo {function = "See"}
               }
            },
            new State
            {
               type = "Death",
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
        };
    }
}


public class Demon : Monster
{

    //+FLOORCLIP

    public string SeeSound = "demon/sight";
    public string AttackSound = "demon/attack";
    public string PainSound = "demon/pain";
    public string DeathSound = "demon/death";
    public string ActiveSound = "demon/active";
    public string Obituary = "$OB_DEMONHIT";

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

        actorStates = new List<State>
    {
        new State
        {
           type = "Spawn",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "SARG", sprInd = "AB", time = 10, function = "A_Look"},
               new StateInfo{function = "Loop" }
           }

       },
       new State
       {
           type = "See",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "SARG", sprInd = "AABBCCDD", time = 2, function = "A_Chase"},
               new StateInfo{function = "Loop"}
           }
       },
       new State
       {
           type = "Melee",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "SARG", sprInd = "EF", time = 8, function = "A_FaceTarget"},
               new StateInfo{spr = "SARG", sprInd = "G", time = 8, function = "A_SargAttack"},
               new StateInfo{function = "See"}
           }
       },
       new State
       {
          type = "Pain",
          info = new List<StateInfo>
          {
              new StateInfo{spr = "SARG", sprInd = "H", time = 2, function = ""},
              new StateInfo{spr = "SARG", sprInd = "H", time = 2, function = "A_Pain"},
              new StateInfo {function = "See"}
          }
       },
       new State
       {
          type = "Death",
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
       },
       new State
       {
          type = "Raise",
          info = new List<StateInfo>
          {
              new StateInfo{spr = "SARG", sprInd = "N", time = 4},
              new StateInfo{spr = "SARG", sprInd = "MLKJI", time = 5},
              new StateInfo{function = "See"}
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

    public string SeeSound = "vile/sight";
    public string AttackSound = "vile/attack";
    public string PainSound = "vile/pain";
    public string DeathSound = "vile/death";
    public string ActiveSound = "vile/active";
    public string Obituary = "$OB_VILE";
    public int MaxTargetRange = 896;

    public void Awake()
    {
        Health = 700;
        Radius = 20;
        Height = 56;
        Mass = 500;
        Speed = 15;
        PainChance = 10;
        sprite = "VILE";

        actorStates = new List<State>
    {
        new State
        {
           type = "Spawn",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "VILE", sprInd = "AB", time = 10, function = "A_Look"},
               new StateInfo{function = "Loop" }
           }

       },
       new State
       {
           type = "See",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "VILE", sprInd = "AABBCCDDEEFF", time = 2, function = "A_VileChase"},
               new StateInfo{function = "Loop"}
           }
       },
       new State
       {
           type = "Missile",
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
       },
       new State
       {
          type = "Heal",
          info = new List<StateInfo>
          {
              new StateInfo{spr = "VILE", sprInd = "---", time = 10},
              new StateInfo {function = "See"}
          }
       },
       new State
       {
          type = "Pain",
          info = new List<StateInfo>
          {
              new StateInfo{spr = "VILE", sprInd = "Q", time = 5, function = ""},
              new StateInfo{spr = "VILE", sprInd = "Q", time = 5, function = "A_Pain"},
              new StateInfo {function = "See"}
          }
       },
       new State
       {
          type = "Death",
          info = new List<StateInfo>
          {
              new StateInfo{spr = "VILE", sprInd = "Q", time = 7 },
              new StateInfo{spr = "VILE", sprInd = "R", time = 7, function = "A_Scream" },
              new StateInfo{spr = "VILE", sprInd = "S", time = 7, function = "A_NoBlocking" },
              new StateInfo{spr = "VILE", sprInd = "TUVWXY", time = 7},
              new StateInfo{spr = "VILE", sprInd = "Z", time = -1},
              new StateInfo{function = "Stop"}
          }
       },
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

        sprite = "BOS2";

        actorStates = new List<State>
    {
        new State
        {
           type = "Spawn",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "BOS2", sprInd = "AB", time = 10, function = "A_Look"},
               new StateInfo{function = "Loop" }
           }

       },
       new State
       {
           type = "See",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "BOS2", sprInd = "AABBCCDD", time = 3, function = "A_Chase"},
               new StateInfo{function = "Loop"}
           }
       },
       new State
       {
           type = "Melee"
       },
       new State
       {
           type = "Missile",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "BOS2", sprInd = "EF", time = 8, function = "A_FaceTarget" },
               new StateInfo{spr = "BOS2", sprInd = "G", time = 8, function = "A_BruisAttack" },
               new StateInfo{function = "See"}
           }
       },
        new State
        {
           type = "Pain",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "BOS2", sprInd = "H", time = 2},
               new StateInfo{spr = "BOS2", sprInd = "H", time = 2, function = "A_Pain"},
               new StateInfo {function = "See"}
           }
        },
        new State
        {
           type = "Death",
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
        },
        new State
        {
           type = "Raise",
           info = new List<StateInfo>
           {
               new StateInfo{spr = "BOS2", sprInd = "O", time = 8},
               new StateInfo{spr = "BOS2", sprInd = "NMLKJI", time = 8},
               new StateInfo{function = "See"}
           }
        }
    };
    }
    
}

public class CyberDemon : Monster
{
    //Monster
    public int MiniMissileChance = 160;
    //+BOSS
    //+MISSILEMORE
    //+FLOORCLIP
    //+NORADIUSDMG
    //+DONTMORPH
    //+BOSSDEATH

    public string SeeSound = "cyber/sight";
    public string AttackSound = "cyber/attack";
    public string PainSound = "cyber/pain";
    public string DeathSound = "cyber/death";
    public string ActiveSound = "cyber/active";
    public string Obituary = "$OB_CYBORG";

    public void Awake()
    {
        Health = 4000;
        Radius = 40;
        Height = 110;
        Mass = 1000;
        Speed = 16;
        PainChance = 20;
        sprite = "CYBR";

        actorStates = new List<State>
        {
            new State
            {
               type = "Spawn",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "CYBR", sprInd = "AB", time = 10, function = "A_Look"},
                   new StateInfo{function = "Loop" }
               }

           },
           new State
           {
               type = "See",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "CYBR", sprInd = "A", time = 3, function = "A_Hoof"},
                   new StateInfo{spr = "CYBR", sprInd = "ABBCC", time = 3, function = "A_Chase"},
                   new StateInfo{spr = "CYBR", sprInd = "D", time = 3, function = "A_Metal"},
                   new StateInfo{spr = "CYBR", sprInd = "D", time = 3, function = "A_Chase"},
                   new StateInfo{function = "Loop"}
               }
           },
           new State
           {
               type = "Missile",
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
           },
           new State
           {
              type = "Pain",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "CYBR", sprInd = "G", time = 10, function = "A_Pain"},
                  new StateInfo {function = "See"}
              }
           },
           new State
           {
              type = "Death",
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
           },
        };
    }

}

public class Fatso : Monster
{

    //Monster
    //+FLOORCLIP
    //+BOSSDEATH

    public string SeeSound = "fatso/sight";
    public string AttackSound = "fatso/attack";
    public string PainSound = "fatso/pain";
    public string DeathSound = "fatso/death";
    public string ActiveSound = "fatso/active";
    public string Obituary = "$OB_FATSO";


    public void Awake()
    {
        Health = 600;
        Radius = 48;
        Height = 64;
        Mass = 1000;
        Speed = 8;
        PainChance = 80;
        sprite = "FATT";

        actorStates = new List<State>
        {
            new State
            {
               type = "Spawn",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "FATT", sprInd = "AB", time = 15, function = "A_Look"},
                   new StateInfo{function = "Loop" }
               }

           },
           new State
           {
               type = "See",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "FATT", sprInd = "AABBCCDDEEFF", time = 4, function = "A_Chase"},
                   new StateInfo{function = "Loop"}
               }
           },
           new State
           {
               type = "Missile",
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
           },
           new State
           {
              type = "Pain",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "FATT", sprInd = "J", time = 3},
                  new StateInfo{spr = "FATT", sprInd = "J", time = 3, function = "A_Pain"},
                  new StateInfo {function = "See"}
              }
           },
           new State
           {
              type = "Death",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "FATT", sprInd = "K", time = 6 },
                  new StateInfo{spr = "FATT", sprInd = "L", time = 6, function = "A_Scream" },
                  new StateInfo{spr = "FATT", sprInd = "M", time = 6, function = "A_NoBLocking" },
                  new StateInfo{spr = "FATT", sprInd = "NOPQRS", time = 6},
                  new StateInfo{spr = "FATT", sprInd = "T", time = -1, function = "A_BossDeath"},
                  new StateInfo{function = "Stop"}
              }
           },
           new State
           {
               type = "Raise",
               info = new List<StateInfo>
               {
                  new StateInfo{spr = "FATT", sprInd = "R", time = 5},
                  new StateInfo{spr = "FATT", sprInd = "QPONMLK", time = 5},
                  new StateInfo{function = "See"}
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

    public string SeeSound = "skull/sight";
    public string AttackSound = "skull/attack";
    public string PainSound = "skull/pain";
    public string DeathSound = "skull/death";
    public string ActiveSound = "skull/active";
    //RenderStyle = SoulTrans;
    public string Obituary = "$OB_SKULL";

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

        actorStates = new List<State>
        {
            new State
            {
               type = "Spawn",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SKUL", sprInd = "AB", time = 10, function = "A_Look"},
                   new StateInfo{function = "Loop" }
               }

           },
           new State
           {
               type = "See",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SKUL", sprInd = "AB", time = 6, function = "A_Chase"},
                   new StateInfo{function = "Loop"}
               }
           },
           new State
           {
               type = "Missile",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SKUL", sprInd = "C", time = 10, function = "A_FaceTarget"},
                   new StateInfo{spr = "SKUL", sprInd = "D", time = 4, function = "A_SkullAttack"},
                   new StateInfo{spr = "SKUL", sprInd = "CD", time = 4},
                   new StateInfo{function = "Missile+2"}
               }
           },
           new State
           {
              type = "Pain",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "SKUL", sprInd = "E", time = 3},
                  new StateInfo{spr = "SKUL", sprInd = "E", time = 3, function = "A_Pain"},
                  new StateInfo {function = "See"}
              }
           },
           new State
           {
              type = "Death",
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
           },
        };
    }
}


public class PainElemental : Monster
{
    //Monster
    //+FLOAT
    //+NOGRAVITY

    public string SeeSound = "pain/sight";
    public string AttackSound = "pain/attack";
    public string PainSound = "pain/pain";
    public string DeathSound = "pain/death";
    public string ActiveSound = "pain/active";

    public void Awake()
    {
        Health = 400;
        Radius = 31;
        Height = 56;
        Mass = 400;
        Speed = 8;
        PainChance = 128;
        sprite = "PAIN";

        actorStates = new List<State>
        {
            new State
            {
               type = "Spawn",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "PAIN", sprInd = "A", time = 10, function = "A_Look"},
                   new StateInfo{function = "Loop" }
               }

           },
           new State
           {
               type = "See",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "PAIN", sprInd = "AABBCC", time = 3, function = "A_Chase"},
                   new StateInfo{function = "Loop"}
               }
           },
           new State
           {
               type = "Missile",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "PAIN", sprInd = "D", time = 5, function = "A_FaceTarget"},
                   new StateInfo{spr = "PAIN", sprInd = "E", time = 5, function = "A_FaceTarget"},
                   new StateInfo{spr = "PAIN", sprInd = "F", time = 5, function = "A_FaceTarget"},
                   new StateInfo{spr = "PAIN", sprInd = "F", time = 0, function = "A_PainAttack"},
                   new StateInfo{function = "See"}
               }
           },
           new State
           {
              type = "Pain",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "PAIN", sprInd = "G", time = 6},
                  new StateInfo{spr = "PAIN", sprInd = "G", time = 6, function = "A_Pain"},
                  new StateInfo {function = "See"}
              }
           },
           new State
           {
              type = "Death",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "PAIN", sprInd = "H", time = 8 },
                  new StateInfo{spr = "PAIN", sprInd = "I", time = 8, function = "A_Scream" },
                  new StateInfo{spr = "PAIN", sprInd = "JK", time = 8},
                  new StateInfo{spr = "PAIN", sprInd = "L", time = 8, function = "A_PainDie" },
                  new StateInfo{spr = "PAIN", sprInd = "M", time = 8},
                  new StateInfo{function = "Stop"}
              }
           },
           new State
           {
               type = "Raise",
               info = new List<StateInfo>
               {
                  new StateInfo{spr = "PAIN", sprInd = "MLKJIH", time = 8},
                  new StateInfo{function = "See"}
               }
           }
        };
    }
}


public class Revenant : Monster
{

    //Monster
    public int MeleeThreshold = 196;
    //+MISSILEMORE
    //+FLOORCLIP

    public string SeeSound = "skeleton/sight";
    public string AttackSound = "skeleton/attack";
    public string PainSound = "skeleton/pain";
    public string DeathSound = "skeleton/death";
    public string ActiveSound = "skeleton/active";
    public string HitObituary = "$OB_UNDEADHIT"; // "%o was punched by a revenant."
    public string Obituary = "$OB_UNDEAD"; // "%o couldn't evade a revenant's fireball."

    public void Awake()
    {
        Health = 300;
        Radius = 20;
        Height = 56;
        Mass = 500;
        Speed = 10;
        PainChance = 100;
        sprite = "SKEL";

        actorStates = new List<State>
        {
            new State
            {
               type = "Spawn",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SKEL", sprInd = "AB", time = 10, function = "A_Look"},
                   new StateInfo{function = "Loop" }
               }

           },
           new State
           {
               type = "See",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SKEL", sprInd = "AABBCCDDEEFF", time = 2, function = "A_Chase"},
                   new StateInfo{function = "Loop"}
               }
           },
           new State
           {
               type = "Melee",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SKEL", sprInd = "G", time = 0, function = "A_FaceTarget"},
                   new StateInfo{spr = "SKEL", sprInd = "G", time = 6, function = "A_SkelWoosh"},
                   new StateInfo{spr = "SKEL", sprInd = "H", time = 6, function = "A_FaceTarget"},
                   new StateInfo{spr = "SKEL", sprInd = "I", time = 6, function = "A_SkelFist"},
                   new StateInfo{function = "See"}
               }
           },
           new State
           {
               type = "Missile",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SKEL", sprInd = "J", time = 0, function = "A_FaceTarget"},
                   new StateInfo{spr = "SKEL", sprInd = "J", time = 10, function = "A_FaceTarget"},
                   new StateInfo{spr = "SKEL", sprInd = "K", time = 10, function = "A_SkelMissile"},
                   new StateInfo{spr = "SKEL", sprInd = "K", time = 10, function = "A_FaceTarget"},
                   new StateInfo{function = "See"}
               }
           },
           new State
           {
              type = "Pain",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "SKEL", sprInd = "L", time = 5},
                  new StateInfo{spr = "SKEL", sprInd = "L", time = 5, function = "A_Pain"},
                  new StateInfo {function = "See"}
              }
           },
           new State
           {
              type = "Death",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "SKEL", sprInd = "LM", time = 7},
                  new StateInfo{spr = "SKEL", sprInd = "N", time = 7, function = "A_Scream" },
                  new StateInfo{spr = "SKEL", sprInd = "O", time = 7, function = "A_NoBlocking"},
                  new StateInfo{spr = "SKEL", sprInd = "P", time = 7 },
                  new StateInfo{spr = "SKEL", sprInd = "Q", time = -1},
                  new StateInfo{function = "Stop"}
              }
           },
           new State
           {
               type = "Raise",
               info = new List<StateInfo>
               {
                  new StateInfo{spr = "SKEL", sprInd = "Q", time = 5},
                  new StateInfo{spr = "SKEL", sprInd = "PONML", time = 5},
                  new StateInfo{function = "See"}
               }
           }
        };
    }
}

public class WolfensteinSS : Monster
{
    //Monster
    //+FLOORCLIP

    public string SeeSound = "wolfss/sight";
    public string AttackSound = "wolfss/attack";
    public string PainSound = "wolfss/pain";
    public string DeathSound = "wolfss/death";
    public string ActiveSound = "wolfss/active";
    public string Obituary = "$OB_WOLFSS"; // "%o couldn't evade a revenant's fireball."
    public string Dropitem = "Clip";

    public void Awake()
    {
        Health = 50;
        Radius = 20;
        Height = 56;
        Speed = 8;
        PainChance = 170;
        sprite = "SSWV";

        actorStates = new List<State>
        {
            new State
            {
               type = "Spawn",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SSWV", sprInd = "AB", time = 10, function = "A_Look"},
                   new StateInfo{function = "Loop" }
               }

           },
           new State
           {
               type = "See",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "SSWV", sprInd = "AABBCCDD", time = 3, function = "A_Chase"},
                   new StateInfo{function = "Loop"}
               }
           },
           new State
           {
               type = "Missile",
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
           },
           new State
           {
              type = "Pain",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "SSWV", sprInd = "H", time = 3},
                  new StateInfo{spr = "SSWV", sprInd = "H", time = 3, function = "A_Pain"},
                  new StateInfo {function = "See"}
              }
           },
           new State
           {
              type = "Death",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "SSWV", sprInd = "I", time = 5},
                  new StateInfo{spr = "SSWV", sprInd = "J", time = 5, function = "A_Scream" },
                  new StateInfo{spr = "SSWV", sprInd = "K", time = 5, function = "A_NoBlocking"},
                  new StateInfo{spr = "SSWV", sprInd = "L", time = 5 },
                  new StateInfo{spr = "SSWV", sprInd = "M", time = -1},
                  new StateInfo{function = "Stop"}
              }
           },
           new State
           {
              type = "XDeath",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "SSWV", sprInd = "N", time = 5},
                  new StateInfo{spr = "SSWV", sprInd = "O", time = 5, function = "A_XScream" },
                  new StateInfo{spr = "SSWV", sprInd = "P", time = 5, function = "A_NoBlocking"},
                  new StateInfo{spr = "SSWV", sprInd = "QRSTU", time = 5 },
                  new StateInfo{spr = "SSWV", sprInd = "V", time = -1},
                  new StateInfo{function = "Stop"}
              }
           },
           new State
           {
               type = "Raise",
               info = new List<StateInfo>
               {
                  new StateInfo{spr = "SSWV", sprInd = "M", time = 5},
                  new StateInfo{spr = "SSWV", sprInd = "LKJI", time = 5},
                  new StateInfo{function = "See"}
               }
           }
        };
    }
}

public class Cacodemon : Monster
{

    //Monster
    //+FLOORCLIP

    public string SeeSound = "caco/sight";
    public string AttackSound = "caco/attack";
    public string PainSound = "caco/pain";
    public string DeathSound = "caco/death";
    public string ActiveSound = "caco/active";
    public string Obituary = "$OB_CACO";
    public string HitObituary = "$OB_CACOHIT";

    public void Awake()
    {
        Health = 400;
        Radius = 31;
        Height = 56;
        Speed = 8;
        PainChance = 128;
        sprite = "HEAD";

        actorStates = new List<State>
        {
            new State
            {
               type = "Spawn",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "HEAD", sprInd = "A", time = 10, function = "A_Look"},
                   new StateInfo{function = "Loop" }
               }

           },
           new State
           {
               type = "See",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "HEAD", sprInd = "A", time = 3, function = "A_Chase"},
                   new StateInfo{function = "Loop"}
               }
           },
           new State
           {
               type = "Missile",
               info = new List<StateInfo>
               {
                   new StateInfo{spr = "HEAD", sprInd = "BC", time = 5, function = "A_FaceTarget"},
                   new StateInfo{spr = "HEAD", sprInd = "D", time = 5, function = "A_HeadAttack"},
                   new StateInfo{function = "See"}
               }
           },
           new State
           {
              type = "Pain",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "HEAD", sprInd = "E", time = 3},
                  new StateInfo{spr = "HEAD", sprInd = "E", time = 3, function = "A_Pain"},
                  new StateInfo{spr = "HEAD", sprInd = "F", time = 6},
                  new StateInfo {function = "See"}
              }
           },
           new State
           {
              type = "Death",
              info = new List<StateInfo>
              {
                  new StateInfo{spr = "HEAD", sprInd = "G", time = 8},
                  new StateInfo{spr = "HEAD", sprInd = "H", time = 8, function = "A_Scream" },
                  new StateInfo{spr = "HEAD", sprInd = "IJ", time = 8},
                  new StateInfo{spr = "HEAD", sprInd = "K", time = 8, function = "A_NoBlocking" },
                  new StateInfo{spr = "HEAD", sprInd = "L", time = -1, function = "A_SetFloorClip"},
                  new StateInfo{function = "Stop"}
              }
           },
           new State
           {
               type = "Raise",
               info = new List<StateInfo>
               {
                  new StateInfo{spr = "HEAD", sprInd = "L", time = 8, function = "A_UnSetFloorClip"},
                  new StateInfo{spr = "HEAD", sprInd = "KJIHG", time = 8},
                  new StateInfo{function = "See"}
               }
           }
        };
    }
}
