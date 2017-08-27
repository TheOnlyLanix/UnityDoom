using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : Actor
{


}

public class Column : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Column";
        Radius = 16;
        Height = 48;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "COLU", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class BurningBarrel : Props
{

    public void Awake()
    {
        SOLID = true;

        Radius = 16;
        Height = 32;
        ProjectilePassHeight = -16;
        Name = "Burning Barrel";
        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "FCAN", sprInd = "ABC", time = 4},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}


public class ExplosiveBarrel : Props
{

    public void Awake()
    {
        SOLID = true;
        SHOOTABLE = true;
        NOBLOOD = true;
        ACTIVATEMCROSS = true;
        DONTGIB = true;
        OLDRADIUSDMG = true;
        Name = "Explosive Barrel";
        Health = 20;
        Radius = 10;
        Height = 42;

        Sounds = new Dictionary<string, string>
        {
            { "DeathSound", "world/barrelx" }
        };

        Obituary = "$OB_BARREL"; // "%o went boom."

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "BAR1", sprInd = "AB", time = 6},
                        new StateInfo{function = "Loop" }
                   }
               }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "FCAN", sprInd = "A", time = 5},
                        new StateInfo{spr = "FCAN", sprInd = "B", time = 5, function = "A_Scream"},
                        new StateInfo{spr = "FCAN", sprInd = "C", time = 5},
                        new StateInfo{spr = "FCAN", sprInd = "D", time = 5, function = "A_Explode"},
                        new StateInfo{spr = "FCAN", sprInd = "E", time = 10},
                        new StateInfo{spr = "TNT1", sprInd = "A", time = 1050, function = "A_BarrelDestroy"},
                        new StateInfo{spr = "TNT1", sprInd = "A", time = 5, function = "A_Respawn"},
                        new StateInfo{function = "Wait" }
                   }
               }
            }
        };
    }
}

public class TechLamp : Props
{

    public void Awake()
    {
        SOLID = true;

        Radius = 16;
        Height = 80;
        ProjectilePassHeight = -16;
        Name = "Tech Lamp";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "TLMP", sprInd = "ABCD", time = 4},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}


public class TechLamp2 : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Tech Lamp 2";
        Radius = 16;
        Height = 60;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "TLP2", sprInd = "ABCD", time = 4},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}

public class TechPillar : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Tech Pillar";
        Radius = 16;
        Height = 128;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "ELEC", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}


public class BigTree : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Big Tree";

        Radius = 32;
        Height = 108;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "TRE2", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class BlueTorch : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Blue Torch";

        Radius = 16;
        Height = 68;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "TBLU", sprInd = "ABCD", time = 4},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}

public class Candelabra : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Candelabra";

        Radius = 16;
        Height = 60;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "CBRA", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class Candlestick : Props
{

    public void Awake()
    {
        Name = "Candlestick";

        Radius = 20;
        Height = 14;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "CAND", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class EvilEye : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Evil Eye";

        Radius = 16;
        Height = 54;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "CEYE", sprInd = "ABCB", time = 6},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}

public class FloatingSkull : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Floating Skull";

        Radius = 16;
        Height = 26;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "FSKU", sprInd = "ABC", time = 6},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}

public class GreenTorch : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Green Torch";

        Radius = 16;
        Height = 68;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "TGRN", sprInd = "ABCD", time = 4},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}

public class HeadCandles : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Head Candles";

        Radius = 16;
        Height = 42;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "POL3", sprInd = "AB", time = 6},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}

public class HeadOnAStick : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Head On A Stick";

        Radius = 16;
        Height = 56;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "POL4", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}


public class HeadsOnAStick : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Heads On A Stick";

        Radius = 16;
        Height = 64;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "POL2", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}


public class HeartColumn : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Heart Column";

        Radius = 16;
        Height = 40;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "COL5", sprInd = "AB", time = 14},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}

public class RedTorch : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Red Torch";

        Radius = 16;
        Height = 68;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "TRED", sprInd = "ABCD", time = 4},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}

public class ShortBlueTorch : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Short Blue Torch";

        Radius = 16;
        Height = 37;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "SMBT", sprInd = "ABCD", time = 4},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}

public class ShortGreenColumn : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Short Green Column";

        Radius = 16;
        Height = 40;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "COL2", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class ShortGreenTorch : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Short Green Torch";

        Radius = 16;
        Height = 37;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "SMGT", sprInd = "ABCD", time = 4},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}

public class ShortRedColumn : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Short Red Column";

        Radius = 16;
        Height = 40;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "COL4", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class ShortRedTorch : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Short Red Torch";

        Radius = 16;
        Height = 37;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "SMRT", sprInd = "ABCD", time = 4},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}

public class SkullColumn : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Skull Column";

        Radius = 16;
        Height = 40;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "COL6", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class Stalagtite : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Stalagtite";

        Radius = 16;
        Height = 40;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "SMIT", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class Stalagmite : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Stalagmite";

        Radius = 16;
        Height = 48;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "SMT2", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class RealGibs : Props
{

    public void Awake()
    {
        Name = "Real Gibs";

        DROPOFF = true;
        CORPSE = true;
        NOTELEPORT = true;
        DONTGIB = true;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "POL5", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class TallGreenColumn : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Tall Green Column";

        Radius = 16;
        Height = 52;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "COL1", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class TallRedColumn : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Tall Red Column";

        Radius = 16;
        Height = 52;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "COL3", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class TorchTree : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Torch Tree";

        Radius = 16;
        Height = 52;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "TRE1", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class DeadCacodemon : Cacodemon
{

    public override void Awake()
    {
        Name = "Dead Cacodemon";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{function = "Death+5" }
                   }
               }
            }
        };
    }
}

public class DeadDemon : Demon
{

    public override void Awake()
    {
        Name = "Dead Demon";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{function = "Death+5" }
                   }
               }
            }
        };
    }
}

public class DeadDoomImp : DoomImp
{

    public override void Awake()
    {
        Name = "Dead Doom Imp";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{function = "Death+4" }
                   }
               }
            }
        };
    }
}

public class DeadLostSoul : LostSoul
{

    public override void Awake()
    {
        Name = "Dead Lost Soul";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{function = "Death+5" }
                   }
               }
            }
        };
    }
}

public class DeadMarine : Props
{

    public void Awake()
    {
        Name = "Dead Marine";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "PLAY", sprInd = "N", time = -1 },
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class DeadShotgunGuy : ShotgunGuy
{

    public override void Awake()
    {
        Name = "Dead Shotgun Guy";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{function = "Death+4" }
                   }
               }
            }
        };
    }
}

public class DeadZombieMan : ZombieMan
{

    public override void Awake()
    {
        Name = "Dead Zombie Man";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{function = "Death+4" }
                   }
               }
            }
        };
    }
}

public class GibbedMarine : Props
{

    public void Awake()
    {
        Name = "Gibbed Marine";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "PLAY", sprInd = "W", time = -1 },
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class BloodyTwitch : Props
{

    public virtual void Awake()
    {
        SOLID = true;
        NOGRAVITY = true;
        SPAWNCEILING = true;
        Name = "Bloody Twitch";

        Radius = 16;
        Height = 68;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "GOR1", sprInd = "A", time = 10},
                        new StateInfo{spr = "GOR1", sprInd = "B", time = 15},
                        new StateInfo{spr = "GOR1", sprInd = "C", time = 8},
                        new StateInfo{spr = "GOR1", sprInd = "B", time = 6},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}

public class BrainStem : Props
{

    public void Awake()
    {
        NOBLOCKMAP = true;
        MOVEWITHSECTOR = true;
        Name = "Brain Stem";

        Radius = 20;
        Height = 4;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "BRS1", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class ColonGibs : Props
{

    public void Awake()
    {
        NOBLOCKMAP = true;
        MOVEWITHSECTOR = true;
        Name = "Colon Gibs";

        Radius = 20;
        Height = 4;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "POB1", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class DeadStick : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Dead Stick";

        Radius = 16;
        Height = 64;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "POL1", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class HangBNoBrain : Props
{

    public void Awake()
    {
        SOLID = true;
        NOGRAVITY = true;
        SPAWNCEILING = true;
        Name = "HangB No Brain";

        Radius = 16;
        Height = 88;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "HDB2", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class HangNoGuts : Props
{

    public void Awake()
    {
        SOLID = true;
        NOGRAVITY = true;
        SPAWNCEILING = true;
        Name = "Hang No Guts";

        Radius = 16;
        Height = 88;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "HDB1", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class HangTLookingDown : Props
{

    public void Awake()
    {
        SOLID = true;
        NOGRAVITY = true;
        SPAWNCEILING = true;
        Name = "HangT Looking Down";

        Radius = 16;
        Height = 64;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "HDB3", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class HangTLookingUp : Props
{

    public void Awake()
    {
        SOLID = true;
        NOGRAVITY = true;
        SPAWNCEILING = true;
        Name = "HangT Looking Up";

        Radius = 16;
        Height = 64;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "HDB5", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class HangTNoBrain : Props
{

    public void Awake()
    {
        SOLID = true;
        NOGRAVITY = true;
        SPAWNCEILING = true;
        Name = "HangT No Brain";

        Radius = 16;
        Height = 64;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "HDB6", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class HangTSkull : Props
{

    public void Awake()
    {
        SOLID = true;
        NOGRAVITY = true;
        SPAWNCEILING = true;
        Name = "HangT Skull";

        Radius = 16;
        Height = 64;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "HDB4", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class LiveStick : Props
{

    public void Awake()
    {
        SOLID = true;
        Name = "Live Stick";

        Radius = 16;
        Height = 64;
        ProjectilePassHeight = -16;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "POL6", sprInd = "A", time = 6},
                        new StateInfo{spr = "POL6", sprInd = "B", time = 8},
                        new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }
}


public class Meat2 : Props
{

    public virtual void Awake()
    {
        SOLID = true;
        NOGRAVITY = true;
        SPAWNCEILING = true;
        Name = "Meat 2";

        Radius = 16;
        Height = 84;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "GOR2", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}


public class Meat3 : Props
{

    public virtual void Awake()
    {
        SOLID = true;
        NOGRAVITY = true;
        SPAWNCEILING = true;
        Name = "Meat 3";

        Radius = 16;
        Height = 84;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "GOR3", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class Meat4 : Props
{

    public virtual void Awake()
    {
        SOLID = true;
        NOGRAVITY = true;
        SPAWNCEILING = true;
        Name = "Meat 4";

        Radius = 16;
        Height = 68;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "GOR4", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class Meat5 : Props
{

    public virtual void Awake()
    {
        SOLID = true;
        NOGRAVITY = true;
        SPAWNCEILING = true;
        Name = "Meat 5";

        Radius = 16;
        Height = 52;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "GOR5", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class NonsolidMeat2 : Meat2
{

    public override void Awake()
    {
        Name = "Nonsolid Meat 2";

        SOLID = false;
        Radius = 20;

    }
}

public class NonsolidMeat3 : Meat3
{

    public override void Awake()
    {
        Name = "Nonsolid Meat 3";

        SOLID = false;
        Radius = 20;

    }
}

public class NonsolidMeat4 : Meat4
{

    public override void Awake()
    {
        Name = "Nonsolid Meat 4";

        SOLID = false;
        Radius = 20;

    }
}

public class NonsolidMeat5 : Meat5
{

    public override void Awake()
    {
        Name = "Nonsolid Meat 5";

        SOLID = false;
        Radius = 20;

    }
}

public class NonsolidTwitch : BloodyTwitch
{

    public override void Awake()
    {
        Name = "Nonsolid Twitch";

        SOLID = false;
        Radius = 20;

    }
}

public class SmallBloodPool : Props
{

    public virtual void Awake()
    {
        NOBLOCKMAP = true;
        MOVEWITHSECTOR = true;
        Name = "Small Blood Pool";

        Radius = 20;
        Height = 1;

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "POB2", sprInd = "A", time = -1},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}