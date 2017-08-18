using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Actor : MonoBehaviour
{
    int Scale = 1;
    int Health = 1000;
    int ReactionTime = 8;
    int Radius = 20;
    int Height = 16;
    int Mass = 100;
    //RenderStyle Normal
    int Alpha = 1;
    int MinMissileChance = 200;
    int MeleeRange = 44;
    int MaxDropoffHeight = 24;
    int MaxStepHeight = 24;
    float BounceFactor = 0.7f;
    float WallBounceFactor = 0.75f;
    int BounceCount = -1;
    int FloatSpeed = 4;
    int FloatBobPhase = -1; // randomly initialize by default
    int Gravity = 1;
    float DamageFactor = 1.0f;
    float PushFactor = 0.25f;
    int WeaveIndexXY = 0;
    int WeaveIndexZ = 16;
    int DesignatedTeam = 255;

    string PainType = "Normal";
    string DeathType = "Normal";
    string TeleFogSourceType = "TeleportFog";
    string TeleFogDestType = "TeleportFog";

    int RipperLevel = 0;
    int RipLevelMin = 0;
    int RipLevelMax = 0;
    int DefThreshold = 100;

    enum BloodType
    {
        Blood,
        BloodSplatter,
        AxeBlood
    };

    int ExplosionDamage = 128;
    int MissileHeight = 32;
    int SpriteAngle = 0;
    int SpriteRotation = 0;

    Color StencilColor = new Color(0, 0, 0);

    List<int> VisibleAngles = new List<int>();
    List<int> VisiblePitch = new List<int>();

    // Functions
    bool CheckClass(Thing checkclass, int ptr_select, bool match_superclass)
    {
        return false;
    }

    //int CountInv(class<Inventory> itemtype, int ptr_select);

    //int CountProximity(class<Actor> classname, float distance, int flags = 0, int ptr = AAPTR_DEFAULT);

    float GetAngle(int flags, int ptr)
    {
        return 0f;
    }

    float GetCrouchFactor(int ptr)
    {
        return 0f;
    }

    float GetCVar(string cvar)
    {
        return 0f;
    }

    float GetDistance(bool checkz, int ptr)
    {
        return 0f;
    }

    int GetGibHealth()
    {
        return 0;
    }

    int GetMissileDamage(int mask, int add, int ptr)
    {
        return 0;
    }

    int GetPlayerInput(int inputnum, int ptr)
    {
        return 0;
    }

    int GetSpawnHealth()
    {
        return 0;
    }

    float GetSpriteAngle(int ptr)
    {
        return 0;
    }

    float GetSpriteRotation(int ptr)
    {
        return 0;
    }

    float GetZAt(float px, float py, float angle, int flags, int pick_pointer)
    {
        return 0f;
    }

    bool IsPointerEqual(int ptr_select1, int ptr_select2)
    {
        return false;
    }

    int OverlayID()
    {
        return 0;
    }

    float OverlayX(int layer)
    {
        return 0f;
    }

    float OverlayY(int layer)
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
    state A_JumpIfInTargetInventory(class<Inventory> itemtype, int amount, state label,
                                                int forward_ptr = AAPTR_DEFAULT);
    state A_JumpIfInTargetLOS(state label, float fov = 0, int flags = 0, float dist_max = 0,
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

    void A_LogFloat(float whattoprint)
    {

    }

    void A_LogInt(int whattoprint)
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

    void A_QuakeEx(int intensityX, int intensityY, int intensityZ, int duration, int damrad, int tremrad, AudioClip sfx, int flags = 0, float mulWaveX = 1, float mulWaveY = 1, float mulWaveZ = 1, int falloff = 0, int highpoint = 0, float rollIntensity = 0, float rollWave = 0)
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

    void A_SetFloat()
    {

    }

    void A_SetFloatBobPhase(int bob)
    {

    }

    void A_SetFloatSpeed(float speed, int ptr)
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

    void A_SetPainThreshold(int threshold, int ptr)
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

    void A_SetUserArrayFloat(string varname, int index, float value)
    {

    }

    void A_SetUserVar(string varname, int value)
    {

    }

    void A_SetUserVarFloat(string varname, float value)
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

    void A_UnsetFloat()
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

    List<List<State>> actorStates = new List<List<State>>();

    void fillStates()
    {

    }
    /*
    States 
    {
      Spawn:
        TNT1 A -1
        Stop
      Null:
        TNT1 A 1
        Stop
      GenericFreezeDeath:
        // Generic freeze death frames. Woo!
        "####" "#" 5 A_GenericFreezeDeath
        "----" A 1 A_FreezeDeathChunks
        Wait
      GenericCrush:
        POL5 A -1
        Stop
    }
    */

    // Internal functions
    //state __decorate_internal_state__(state);

}
