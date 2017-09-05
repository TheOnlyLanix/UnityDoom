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

    //Flags (physics)
    public bool SOLID = false;
    public bool SHOOTABLE = false;
    public bool FLOAT = false;
    public bool NOGRAVITY = false;
    public bool WINDTHRUST = false;
    public bool PUSHABLE = false;
    public bool DONTFALL = false;
    public bool CANPASS = false;
    public bool ACTLIKEBRIDGE = false;
    public bool NOBLOCKMAP = false;
    public bool MOVEWITHSECTOR = false;
    public bool RELATIVETOFLOOR = false;
    public bool NOLIFTDROP = false;
    public bool SLIDESONWALLS = false;
    public bool NODROPOFF = false;
    public bool NOTRIGGER = false;
    public bool BLOCKEDBYSOLIDACTORS = false;

    //Flags (Behavior)
    public bool ALWAYSRESPAWN = false;
    public bool AMBUSH = false;
    public bool AVOIDMELEE = false;
    public bool BOSS = false;
    public bool DONTCORPSE = false;
    public bool DORMANT = false;
    public bool FRIENDLY = false;
    public bool JUMPDOWN = false;
    public bool LOOKALLAROUND = false;
    public bool MISSILEEVENMORE = false;
    public bool MISSILEMORE = false;
    public bool NEVERRESPAWN = false;
    public bool NOSPLASHALERT = false;
    public bool NOTARGETSWITCH = false;
    public bool NOVERTICALMELEERANGE = false;
    public bool QUICKTORETALIATE = false;
    public bool STANDSTILL = false;

    //Flags (Abilities)
    public bool CANNOTPUSH = false;
    public bool NOTELEPORT = false;
    public bool ACTIVATEIMPACT = false;
    public bool CANPUSHWALLS = false;
    public bool CANUSEWALLS = false;
    public bool ACTIVATEMCROSS = false;
    public bool ACTIVATEPCROSS = false;
    public bool CANTLEAVEFLOORPIC = false;
    public bool TELESTOMP = false;
    public bool NOTELESTOMP = false;
    public bool STAYMORPHED = false;
    public bool CANBLAST = false;
    public bool NOBLOCKMONST = false;
    public bool ALLOWTHRUFLAGS = false;
    public bool THRUGHOST = false;
    public bool THRUACTORS = false;
    public bool THRUSPECIES = false;
    public bool MTHRUSPECIES = false;
    public bool SPECTRAL = false;
    public bool FRIGHTENED = false;
    public bool FRIGHTENING = false;
    public bool NOTARGET = false;
    public bool NEVERTARGET = false;
    public bool NOINFIGHTSPECIES = false;
    public bool FORCEINFIGHTING = false;
    public bool NOINFIGHTING = false;
    public bool NOTIMEFREEZE = false;
    public bool NOFEAR = false;
    public bool CANTSEEK = false;
    public bool SEEINVISIBLE = false;
    public bool DONTTHRUST = false;
    public bool ALLOWPAIN = false;
    public bool USEKILLSCRIPTS = false;
    public bool NOKILLSCRIPTS = false;

    //Flags (Defenses)
    public bool INVULNERABLE = false;
    public bool BUDDHA = false;
    public bool REFLECTIVE = false;
    public bool SHIELDREFLECT = false;
    public bool DEFLECT = false;
    public bool MIRRORREFLECT = false;
    public bool AIMREFLECT = false;
    public bool THRUREFLECT = false;
    public bool NORADIUSDMG = false;
    public bool DONTBLAST = false;
    public bool GHOST = false;
    public bool DONTMORPH = false;
    public bool DONTSQUASH = false;
    public bool NOTELEOTHER = false;
    public bool HARMFRIENDS = false;
    public bool DOHARMSPECIES = false;
    public bool DONTHARMCLASS = false;
    public bool DONTHARMSPECIES = false;
    public bool NODAMAGE = false;
    public bool DONTRIP = false;
    public bool NOTELEFRAG = false;
    public bool ALWAYSTELEFRAG = false;
    public bool DONTDRAIN = false;
    public bool LAXTELEFRAGDMG = false;

    //Flags (Appearance/Sound)
    public bool BRIGHT = false;
    public bool INVISIBLE = false;
    public bool SHADOW = false;
    public bool NOBLOOD = false;
    public bool NOBLOODDECALS = false;
    public bool STEALTH = false;
    public bool FLOORCLIP = false;
    public bool SPAWNFLOAT = false;
    public bool SPAWNCEILING = false;
    public bool FLOATBOB = false;
    public bool NOICEDEATH = false;
    public bool DONTGIB = false;
    public bool DONTSPLASH = false;
    public bool DONTOVERLAP = false;
    public bool RANDOMIZE = false;
    public bool FIXMAPTHINGPOS = false;
    public bool FULLVOLACTIVE = false;
    public bool FULLVOLDEATH = false;
    public bool NOWALLBOUNCESND = false;
    public bool VISIBILITYPULSE = false;
    public bool ROCKETTRAIL = false;
    public bool GRENADETRAIL = false;
    public bool NOBOUNCESOUND = false;
    public bool NOSKIN = false;
    public bool DONTTRANSLATE = false;
    public bool NOPAIN = false;

    //Flags (Projectile)
    public bool MISSILE = false;
    public bool RIPPER = false;
    public bool NOBOSSRIP = false;
    public bool NODAMAGETHRUST = false;
    public bool DONTREFLECT = false;
    public bool FLOORHUGGER = false;
    public bool CEILINGHUGGER = false;
    public bool BLOODLESSIMPACT = false;
    public bool BLOODSPLATTER = false;
    public bool FOILINVUL = false;
    public bool FOILBUDDHA = false;
    public bool SEEKERMISSILE = false;
    public bool SCREENSEEKER = false;
    public bool SKYEXPLODE = false;
    public bool NOEXPLODEFLOOR = false;
    public bool STRIFEDAMAGE = false;
    public bool EXTREMEDEATH = false;
    public bool NOEXTREMEDEATH = false;
    public bool DEHEXPLOSION = false;
    public bool PIERCEARMOR = false;
    public bool FORCERADIUSDMG = false;
    public bool SPAWNSOUNDSOURCE = false;
    public bool PAINLESS = false;
    public bool FORCEPAIN = false;
    public bool CAUSEPAIN = false;
    public bool DONTSEEKINVISIBLE = false;
    public bool STEPMISSILE = false;
    public bool ADDITIVEPOISONDAMAGE = false;
    public bool ADDITIVEPOISONDURATION = false;
    public bool NOFORWARDFALL = false;
    public bool HITTARGET = false;
    public bool HITMASTER = false;
    public bool HITTRACER = false;
    public bool BOUNCEONWALLS = false;
    public bool BOUNCEONFLOORS = false;
    public bool BOUNCEONCEILINGS = false;
    public bool ALLOWBOUNCEONACTORS = false;
    public bool BOUNCEAUTOOFF = false;
    public bool BOUNCEAUTOOFFFLOORONLY = false;
    public bool BOUNCELIKEHERETIC = false;
    public bool BOUNCEONACTORS = false;
    public bool EXPLODEONWATER = false;
    public bool CANBOUNCEWATER = false;
    public bool MBFBOUNCER = false;
    public bool USEBOUNCESTATE = false;
    public bool DONTBOUNCEONSHOOTABLES = false;

    //Flags (Misc)
    public bool ICESHATTER = false;
    public bool DROPPED = false;
    public bool ISMONSTER = false;
    public bool CORPSE = false;
    public bool COUNTITEM = false;
    public bool COUNTKILL = false;
    public bool COUNTSECRET = false;
    public bool NOTDMATCH = false;
    public bool NONSHOOTABLE = false;
    public bool DROPOFF = false;
    public bool PUFFONACTORS = false;
    public bool ALLOWPARTICLES = false;
    public bool ALWAYSPUFF = false;
    public bool PUFFGETSOWNER = false;
    public bool FORCEDECAL = false;
    public bool NODECAL = false;
    public bool SYNCHRONIZED = false;
    public bool ALWAYSFAST = false;
    public bool NEVERFAST = false;
    public bool OLDRADIUSDMG = false;
    public bool USESPECIAL = false;
    public bool BUMPSPECIAL = false;
    public bool BOSSDEATH = false;
    public bool NOINTERACTION = false;
    public bool NOTAUTOAIMED = false;
    public bool NOMENU = false;
    public bool PICKUP = false;
    public bool TOUCHY = false;
    public bool VULNERABLE = false;
    public bool NOTONAUTOMAP = false;
    public bool WEAPONSPAWN = false;
    public bool GETOWNER = false;
    public bool NOCLIP = false;
    public bool NOSECTOR = false;
    public bool ICECORPSE = false;
    public bool JUSTHIT = false;
    public bool JUSTATTACKED = false;
    public bool TELEPORT = false;
    public bool BLASTED = false;
    public bool EXPLOCOUNT = false;
    public bool SKULLFLY = false;
    public bool SPECIALFIREDAMAGE = false;
    public bool SPECIALFLOORCLIP = false;
    public bool SUMMONEDMONSTER = false;
    public bool SPECIAL = false;

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

    bool CheckClass(Thing checkclass, int ptr_select, bool match_superclass) { return false; }

    public int CountInv(string itemtype, int ptr_select) { return 0; }

    public int CountProximity(string classname, float distance, int flags, int ptr) { return 0; }

    public float GetAngle(int flags, int ptr) { return 0f; }

    public float GetCrouchFactor(int ptr) { return 0f; }

    public float GetCVar(string cvar) { return 0f; }

    public float GetDistance(bool checkz, int ptr) { return 0f; }

    public int GetGibHealth() { return 0; }

    public int GetMissileDamage(int mask, int add, int ptr) { return 0; }

    public int GetPlayerInput(int inputnum, int ptr) { return 0; }

    public int GetSpawnHealth() { return 0; }

    public float GetSpriteAngle(int ptr) { return 0f; }

    public float GetSpriteRotation(int ptr) { return 0f; }

    public float GetZAt(float px, float py, float angle, int flags, int pick_pointer) { return 0f; }

    public bool IsPointerEqual(int ptr_select1, int ptr_select2) { return false; }

    public int OverlayID() { return 0; }

    public float OverlayX(int layer) { return 0f; }

    public float OverlayY(int layer) { return 0f; }


    // functions
    public void A_ActiveAndUnblock() { }

    public void A_ActiveSound() { }

    public void A_Bang4Cloud() { }

    public void A_BarrelDestroy() { }

    public void A_BasicAttack(int meleedamage, AudioSource meleesound, Actor missiletype, float missileheight) { }

    public void A_BetaSkullAttack() { }

    public void A_BFGSpray(Actor spraytype, int numrays, int damagecount, float angle, float distance, float vrange, int damage, int flags) { }

    public void A_BishopMissileWeave() { }

    public void A_Blast(int flags, float strength, float radius, float speed, Actor blasteffect, AudioSource blastsound) { }

    public void A_BulletAttack() { }

    public bool A_CallSpecial(int special, int arg1 = 0, int arg2 = 0, int arg3 = 0, int arg4 = 0, int arg5 = 0) { return false; }

    public void A_ChangeFlag(string flagname, bool value) { }

    public void A_ChangeVelocity(float x, float y, float z, int flags, int ptr) { }


    string A_CheckBlock(string block, int flags, int ptr, float xofs = 0, float yofs = 0, float zofs = 0, float angle = 0) { return ""; }

    string A_CheckCeiling(string label) { return ""; }

    string A_CheckFlag(string flagname, string label, int check_pointer) { return ""; }

    string A_CheckFloor(string label) { return ""; }

    string A_CheckLOF(string jump, int flags, float range, float minrange, float angle, float pitch, float offsetheight, float offsetwidth, int ptr_target, float offsetforward) { return ""; }

    public void A_CheckPlayerDone() { }

    string A_CheckProximity(string jump, Actor classname, float distance, int count = 1, int flags = 0) { return ""; }

    string A_CheckRange(float distance, string label, bool two_dimension = false) { return ""; }

    string A_CheckSight(string label) { return ""; }

    string A_CheckSightOrRange(float distance, string label, bool two_dimension = false) { return ""; }

    string A_CheckSpecies(string jump, string species, int ptr) { return ""; }
    
    public void A_CheckTerrain() { }

    public void A_ClassBossHealth() { }

    public int A_ClearOverlays(int sstart = 0, int sstop = 0, bool safety = true) { return 0; }

    public void A_ClearShadow() { }

    public bool A_CopySpriteFrame(int from, int to, int flags = 0) { return false; }

    public void A_Countdown() { }

    public void A_CountdownArg(int argnum, string targstate = "") { }

    public void A_CStaffMissileSlither() { }

    public void A_CustomBulletAttack(float spread_xy, float spread_z, int numbullets, int damageperbullet, int ptr, string pufftype = "BulletPuff", float range = 0, int flags = 0, string missile = "", float Spawnheight = 32, float Spawnofs_xy = 0) { }

    public void A_CustomComboAttack(string missiletype, float spawnheight, int damage, AudioClip meleesound, string damagetype = "none", bool bleed = true) { }

    public void A_CustomMeleeAttack(int damage, AudioClip meleesound, AudioClip misssound, string damagetype = "none", bool bleed = true) { }

    public void A_CustomMissile(string missiletype, int ptr, float spawnheight = 32, float spawnofs_xy = 0, float angle = 0, int flags = 0, float pitch = 0) { }

    public void A_CustomRailgun(int damage, int spawnofs_xy, Color color1, Color color2, int flags, int aim, float maxdiff, float spread_xy, float spread_z, float range, int duration, string puffType = "BulletPuff", float sparsity = 1.0f, float driftspeed = 1.0f, string spawnclass = "none", float spawnofs_z = 0, int spiraloffset = 270, int limit = 0) { }

    public void A_DeQueueCorpse() { }

    public void A_Detonate() { }

    public void A_DropFire() { }

    public void A_DropInventory(string itemtype) { }

    public void A_DropItem(string item, int dropamount = -1, int chance = 256) { }

    public void A_DropWeaponPieces(Actor p1, Actor p2, Actor p3) { }

    public int A_Explode(int flags, int damage = -1, int distance = -1, bool alert = false, int fulldamagedistance = 0, int nails = 0, int naildamage = 10, string pufftype = "BulletPuff", string damagetype = "none") { return 0; }

    public void A_FaceConsolePlayer(float MaxTurnAngle = 0) { } // [TP] no-op

    public bool A_FaceMovementDirection(int ptr, float offset = 0, float anglelimit = 0, float pitchlimit = 0, int flags = 0) { return false; }

    public void A_FadeIn(float reduce = 0.1f, int flags = 0) { }

    public void A_FadeOut(float reduce = 0.1f, int flags = 1) { }

    public void A_FadeTo(float target, float amount = 0.1f, int flags = 0) { }

    public void A_Fall() { }

    public void A_FastChase() { }

    public void A_Feathers() { }

    public void A_Fire(float spawnheight = 0f) { }

    public void A_FireAssaultGun() { }

    public void A_FireCrackle() { }

    public void A_FLoopActiveSound() { }

    public void A_FreezeDeath() { }

    public void A_FreezeDeathChunks() { }

    public void A_GenericFreezeDeath() { }

    public void A_GetHurt() { }

    public bool A_GiveInventory(string itemtype, int amount, int giveto) { return false; }

    public void A_GiveQuestItem(int itemno) { }

    public int A_GiveToChildren(string itemtype, int amount) { return 0; }

    public int A_GiveToSiblings(string itemtype, int amount) { return 0; }

    public bool A_GiveToTarget(string itemtype, int amount, int forward_ptr) { return false; }

    public void A_Gravity() { }

    public void A_HideThing() { }

    public void A_IceGuyDie() { }

    string A_Jump(int chance, string label) { return ""; }

    string A_JumpIf(bool expression, string label) { return ""; }

    string A_JumpIfArmorType(string Type, string label, int amount = 1) { return ""; }

    string A_JumpIfCloser(float distance, string label, bool noz) { return ""; }

    string A_JumpIfHealthLower(int health, string label, int ptr_selector) { return ""; }

    string A_JumpIfHigherOrLower(string high, string low, float offsethigh = 0, float offsetlow = 0, bool includeHeight = true) { return ""; }

    string A_JumpIfintargetInventory(string itemtype, int amount, string label, int forward_ptr) { return ""; }

    string A_JumpIfintargetLOS(string label, float fov = 0, int flags = 0, float dist_max = 0, float dist_close = 0) { return ""; }

    string A_JumpIfInventory(string itemtype, int itemamount, string label, int owner) { return ""; }

    string A_JumpIfMasterCloser(float distance, string label, bool noz) { return ""; }

    string A_JumpIfTargetInLOS(string label, float fov = 0, int flags = 0, float dist_max = 0, float dist_close = 0) { return ""; }

    string A_JumpIfTargetInsideMeleeRange(string label) { return ""; }

    string A_JumpIfTargetOutsideMeleeRange(string label) { return ""; }

    string A_JumpIfTracerCloser(float distance, string label, bool noz) { return ""; }

    public void A_KlaxonBlare() { }

    public bool A_LineEffect(int boomspecial = 0, int tag = 0) { return false; }

    public void A_Log(string whattoprint) { }

    public void A_Logfloat(float whattoprint) { }

    public void A_Logint(int whattoprint) { }

    public void A_LoopActiveSound() { }

    public void A_LowGravity() { }

    public void A_M_Saw(AudioClip fullsound, AudioClip hitsound, int damage = 2, string pufftype = "BulletPuff") { }

    public void A_MeleeAttack() { }

    public void A_MissileAttack() { }

    public void A_MonsterRail() { }

    string A_MonsterRefire(int chance, string label) { return ""; }

    public void A_Mushroom(string spawntype = "FatShot", int numspawns = 0, int flags = 0, float vrange = 4.0f, float hrange = 0.5f) { }

    public void A_NoBlocking() { }

    public void A_NoGravity() { }

    bool A_Overlay(int layer, string start = "", bool nooverride = false) { return false; }

    public void A_OverlayFlags(int layer, int flags, bool set) { }

    public void A_OverlayOffset(int layer, float wx = 0, float wy = 32, int flags = 0) { }

    public void A_Pain() { }

    public void A_PigPain() { }

    public void A_PlayerScream() { }

    public string A_PlayerSkinCheck(string label) { return ""; }

    public void A_PlaySound(float attenuation, AudioClip whattoplay, int slot, float volume = 1.0f, bool looping = false) { }

    public void A_PlaySoundEx(AudioClip whattoplay, string slot, bool looping = false, int attenuation = 0) { }

    public void A_PlayWeaponSound(AudioClip whattoplay) { }

    public void A_Print(string whattoprint, float time = 0, string fontname = "") { }

    public void A_PrintBold(string whattoprint, float time = 0, string fontname = "") { }

    public void A_Punch() { }

    public void A_Quake(int intensity, int duration, int damrad, int tremrad, AudioClip sfx) { }

    public void A_QuakeEx(int intensityX, int intensityY, int intensityZ, int duration, int damrad, int tremrad, AudioClip sfx, int flags = 0, float mulWaveX = 1, float mulWaveY = 1, float mulWaveZ = 1, int falloff = 0, int highpoint = 0, float rollintensity = 0, float rollWave = 0) { }

    public void A_QueueCorpse() { }

    public void A_RadiusDamageSelf(int damage = 128, float distance = 128, int flags = 0, string flashtype = "None") { }

    public int A_RadiusGive(string itemtype, float distance, int flags, int amount = 0, string filter = "None", string species = "None", float mindist = 0f, int limit = 0) { return 0; }

    public void A_RadiusThrust(int flags, int force = 128, int distance = -1, int fullthrustdistance = 0) { }

    public void A_RearrangePointers(int newtarget, int newmaster, int newtracer, int flags = 0) { }

    public void A_Recoil(float xyvel) { }

    public void A_RemoveForcefield() { }

    public void A_ResetHealth(int ptr) { }

    public void A_Respawn(int flags = 1) { }

    public void A_RocketInFlight() { }

    public void A_SargAttack() { }

    public void A_ScaleVelocity(float scale, int ptr) { }

    public void A_Scream() { }

    public void A_ScreamAndUnblock() { }

    public void A_SeekerMissile(int threshold, int turnmax, int flags = 0, int chance = 50, int distance = 10) { }

    public bool A_SelectWeapon(string whichweapon, int flags) { return false; }

    public void A_SentinelBob() { }

    public void A_SentinelRefire() { }

    public void A_SetAngle(float angle, int flags, int ptr) { }

    public void A_SetArg(int pos, int value) { }

    public void A_SetBlend(Color color1, float alpha, int tics, Color color2) { }

    public void A_SetChaseThreshold(int threshold, bool def, int ptr) { }

    public void A_SetDamageType(string damagetype) { }

    public void A_Setfloat() { }

    public void A_SetfloatBobPhase(int bob) { }

    public void A_SetfloatSpeed(float speed, int ptr) { }

    public void A_SetFloorClip() { }

    public void A_SetGravity(float gravity) { }

    public void A_SetHealth(int health, int ptr) { }

    public bool A_SetInventory(string itemtype, int amount, int ptr, bool beyondMax) { return false; }

    public void A_SetInvulnerable() { }

    public void A_SetMass(int mass) { }

    public void A_SetPainthreshold(int threshold, int ptr) { }

    public void A_SetPitch(float pitch, int flags, int ptr) { }

    public void A_SetReflective() { }

    public void A_SetReflectiveInvulnerable() { }

    public void A_SetRenderStyle(float alpha, int style) { }

    public void A_SetRipperLevel(int level) { }

    public void A_SetRipMin(int minimum) { }

    public void A_SetRipMax(int maximum) { }

    public void A_SetRoll(float roll, int flags, int ptr) { }

    public void A_SetScale(float scalex, float scaley, int ptr, bool usezero) { }

    public void A_SetShadow() { }

    public void A_SetShootable() { }

    public void A_SetSolid() { }

    public void A_SetSpecial(int spec, int arg0 = 0, int arg1 = 0, int arg2 = 0, int arg3 = 0, int arg4 = 0) { }

    public void A_SetSpecies(string species, int ptr) { }

    public void A_SetSpeed(float speed, int ptr) { }

    public bool A_SetSpriteAngle(float angle, int ptr) { return false; }

    public bool A_SetSpriteRotation(float angle, int ptr) { return false; }

    public void A_SetTeleFog(string oldpos, string newpos) { }

    public void A_SetTics(int tics) { }

    public void A_SetTranslation(string transname) { }

    public void A_SetTranslucent(float alpha, int style = 0) { }

    public void A_SetUserArray(string varname, int index, int value) { }

    public void A_SetUserArrayfloat(string varname, int index, float value) { }

    public void A_SetUserVar(string varname, int value) { }

    public void A_SetUserVarfloat(string varname, float value) { }

    public bool A_SetVisibleRotation(float anglestart, float angleend, float pitchstart, float pitchend, int flags, int ptr) { return false; }

    public void A_ShootGun() { }

    public void A_SkullPop(string skulltype = "BloodySkull") { }

    public void A_SpawnDebris(string spawntype, bool transfer_translation = false, float mult_h = 1, float mult_v = 1) { }

    public void A_SpawnFly(string spawntype = "none") { }   // needs special treatment for default

    public bool A_SpawnItem(string itemtype = "Unknown", float distance = 0, float zheight = 0, bool useammo = true, bool transfer_translation = false) { return false; }

    public bool A_SpawnItemEx(string itemtype, float xofs = 0, float yofs = 0, float zofs = 0, float xvel = 0, float yvel = 0, float zvel = 0, float angle = 0, int flags = 0, int failchance = 0, int tid = 0) { return false; }

    public void A_SpawnParticle(Color color1, int flags = 0, int lifetime = 35, float size = 1, float angle = 0, float xoff = 0, float yoff = 0, float zoff = 0, float velx = 0, float vely = 0, float velz = 0, float accelx = 0, float accely = 0, float accelz = 0, float startalphaf = 1, float fadestepf = -1, float sizestep = 0) { }

    public void A_SpawnSound() { }

    public void A_StartFire() { }

    public void A_Stop() { }

    public void A_StopSound(int slot) { } // Bad default but that's what is originally was...

    public void A_StopSoundEx(string slot) { }

    public void A_SwapTeleFog() { }

    public int A_TakeFromChildren(string itemtype, int amount = 0) { return 0; }

    public int A_TakeFromSiblings(string itemtype, int amount = 0) { return 0; }

    public bool A_TakeFromTarget(string itemtype, int amount, int flags, int forward_ptr) { return false; }

    public bool A_TakeInventory(string itemtype, int amount, int flags, int giveto) { return false; }

    public bool A_ThrowGrenade(string itemtype, float zheight = 0, float xyvel = 0, float zvel = 0, bool useammo = true) { return false; }

    public void A_TossGib() { }

    public void A_Tracer() { }

    public void A_Tracer2() { }

    public void A_Turn(float angle = 0) { }

    public void A_UnHideThing() { }

    public void A_Unsetfloat() { }

    public void A_UnSetFloorClip() { }

    public void A_UnSetInvulnerable() { }

    public void A_UnSetReflective() { }

    public void A_UnSetReflectiveInvulnerable() { }

    public void A_UnSetShootable() { }

    public void A_UnsetSolid() { }

    public bool A_Warp(int ptr_destination, float xofs = 0, float yofs = 0, float zofs = 0, float angle = 0, int flags = 0, string successState = "", float heightoffset = 0, float radiusoffset = 0, float pitch = 0) { return false; }

    public void A_WeaponOffset(float wx = 0, float wy = 32, int flags = 0) { }

    public void A_Weave(int xspeed, int yspeed, float xdist, float ydist) { }

    public void A_XScream() { }

    public int ACS_NamedExecute(string script, int mapnum = 0, int arg1 = 0, int arg2 = 0, int arg3 = 0) { return 0; }

    public int ACS_NamedSuspend(string script, int mapnum = 0) { return 0; }

    public int ACS_NamedTerminate(string script, int mapnum = 0) { return 0; }

    public int ACS_NamedLockedExecute(string script, int mapnum = 0, int arg1 = 0, int arg2 = 0, int lck = 0) { return 0; }

    public int ACS_NamedLockedExecuteDoor(string script, int mapnum = 0, int arg1 = 0, int arg2 = 0, int lck = 0) { return 0; }

    public int ACS_NamedExecuteWithResult(string script, int arg1 = 0, int arg2 = 0, int arg3 = 0, int arg4 = 0) { return 0; }

    public void A_CS_NamedExecuteAlways(string script, int mapnum = 0, int arg1 = 0, int arg2 = 0, int arg3 = 0) { }


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
