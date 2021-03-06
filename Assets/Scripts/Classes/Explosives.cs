﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosives : Actor
{
}

public class Projectile : Actor
{
    public int FastSpeed = 0;
    public string Decal = "";

    public void SetFlags()
    {
        NOBLOCKMAP = true;
        NOGRAVITY = true;
        DROPOFF = true;
        MISSILE = true;
        ACTIVATEIMPACT = true;
        ACTIVATEPCROSS = true;
        NOTELEPORT = true;
    }
}


public class ArachnotronPlasma : Projectile
{

    
    public void Awake()
    {
        RANDOMIZE = true;
        SetFlags();

        Radius = 13;
        Height = 8;
        Speed = 25;
        Name = "ArachnotronPlasma";
        //sprite = "APLS";

        Sounds = new Dictionary<string, string>
        {
            { "SeeSound", "baby/attack" },
            { "DeathSound", "baby/shotx"}

        };

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "APLS", sprInd = "AB", time = 5},
                        new StateInfo{function = "Loop" }
                   }

               }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "APBX", sprInd = "ABCDE", time = 5},
                        new StateInfo{function = "Stop" }
                   }

               }
            }
        };

    }
}


public class ArchvileFire : Actor
{


    public void Awake()
    {
        NOBLOCKMAP = true;
        NOGRAVITY = true;

        Name = "ArchvileFire";
        Alpha = 1;
        sprite = "FIRE";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "FIRE", sprInd = "A", time = 2, function = "A_StartFire"},
                        new StateInfo{spr = "FIRE", sprInd = "BAB", time = 2, function = "A_Fire"},
                        new StateInfo{spr = "FIRE", sprInd = "C", time = 2, function = "A_FireCrackle"},
                        new StateInfo{spr = "FIRE", sprInd = "BCBCDCDCDEDED", time = 2, function = "A_Fire"},
                        new StateInfo{spr = "FIRE", sprInd = "E", time = 2, function = "A_FireCrackle"},
                        new StateInfo{spr = "FIRE", sprInd = "FEFEFGHGHGH", time = 2, function = "A_Fire"},
                        new StateInfo{function = "Stop" }
                   }

               }
            }
        };

    }
}

public class BaronBall : Projectile
{

    public void Awake()
    {
        RANDOMIZE = true;
        //RenderStyle Add
        Name = "BaronBall";
        SetFlags();

        Radius = 6;
        Height = 16;
        Speed = 15;
        FastSpeed = 20;
        Damage = 8;
        Alpha = 1;

        Sounds = new Dictionary<string, string>
        {
            { "SeeSound", "baron/attack" },
            { "DeathSound", "baron/shotx"}

        };

        Decal ="BaronScorch";

        sprite = "BAL7";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "BAL7", sprInd = "AB", time = 4},
                        new StateInfo{function = "Loop" }
                   }

               }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "BAL7", sprInd = "CDE", time = 6},
                        new StateInfo{function = "Stop" }
                   }

               }
            }
        };

    }
}

public class CacodemonBall : Projectile
{

    public void Awake()
    {
        RANDOMIZE = true;
        //RenderStyle Add
        Name = "CacodemonBall";
        SetFlags();

        Radius = 6;
        Height = 8;
        Speed = 10;
        FastSpeed = 20;
        Damage = 5;
        Alpha = 1;

        Sounds = new Dictionary<string, string>
        {
            { "SeeSound", "caco/attack" },
            { "DeathSound", "caco/shotx"}
        };

        sprite = "BAL2";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "BAL2", sprInd = "AB", time = 4},
                        new StateInfo{function = "Loop" }
                   }

               }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "BAL2", sprInd = "CDE", time = 6},
                        new StateInfo{function = "Stop" }
                   }

               }
            }
        };

    }
}

public class BFGBall : Projectile
{

    public void Awake()
    {
        RANDOMIZE = true;
        SetFlags();

        //RenderStyle Add
        Name = "BFGBall";
        Radius = 13;
        Height = 8;
        Speed = 25;
        Damage = 100;
        Alpha = 0.75f;

        Sounds = new Dictionary<string, string>
        {
            { "DeathSound", "weapons/bfgx"}
        };

        Obituary = "$OB_MPBFG_BOOM";

        sprite = "BFS1";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "BFS1", sprInd = "AB", time = 4},
                        new StateInfo{function = "Loop" }
                   }
               }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "BFE1", sprInd = "AB", time = 8},
                        new StateInfo{spr = "BFE1", sprInd = "C", time = 8, function = "A_BFGSpray"},
                        new StateInfo{spr = "BFE1", sprInd = "DEF", time = 8},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class BFGExtra : Actor
{

    public void Awake()
    {
        NOBLOCKMAP = true;
        NOGRAVITY = true;
        //RenderStlye Add
        Name = "BFGExtra";
        Alpha = 0.75f;
        DamageType = "BFGSplash";

        Obituary = "$OB_MPBFG_BOOM";

        sprite = "BFE2";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "BFE2", sprInd = "ABCD", time = 8},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}


public class BulletPuff : Actor
{

    public void Awake()
    {
        NOBLOCKMAP = true;
        NOGRAVITY = true;
        //RenderStlye Translucent
        //AllowParticles = true;
        RANDOMIZE = true;
        Name = "BulletPuff";

        Alpha = 0.5f;
        VSpeed = 1;
        Mass = 5;
        DamageType = "BFGSplash";

        Obituary = "$OB_MPBFG_BOOM";

        sprite = "PUFF";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "PUFF", sprInd = "A", time = 4},
                        new StateInfo{spr = "PUFF", sprInd = "B", time = 4},
                        new StateInfo{function = "Stop" }//Intentional fall-through??
                   }
               }
            },
            { "Melee", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "PUFF", sprInd = "CD", time = 4},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}


public class DoomImpBall : Projectile
{

    public void Awake()
    {
        RANDOMIZE = true;
        SetFlags();

        //RenderStyle Add
        Name = "DoomImpBall";
        Radius = 6;
        Height = 8;
        Speed = 10;
        Damage = 3;
        Alpha = 1;
        FastSpeed = 20;

        Sounds = new Dictionary<string, string>
        {
            { "SeeSound", "imp/attack" },
            { "DeathSound", "imp/shotx"}
        };

        sprite = "BAL1";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "BAL1", sprInd = "AB", time = 4},
                        new StateInfo{function = "Loop" }
                   }
               }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "BAL1", sprInd = "CDE", time = 6},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}


public class FatShot : Projectile
{

    public void Awake()
    {
        RANDOMIZE = true;
        SetFlags();

        //RenderStyle Add
        Name = "FatShot";
        Radius = 6;
        Height = 8;
        Speed = 20;
        Damage = 8;
        Alpha = 1;

        Sounds = new Dictionary<string, string>
        {
            { "SeeSound", "fatso/attack" },
            { "DeathSound", "fatso/shotx"}
        };

        sprite = "MANF";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "MANF", sprInd = "AB", time = 4},
                        new StateInfo{function = "Loop" }
                   }
               }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "MISL", sprInd = "B", time = 8},
                        new StateInfo{spr = "MISL", sprInd = "C", time = 6},
                        new StateInfo{spr = "MISL", sprInd = "D", time = 4},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class PlasmaBall : Projectile
{

    public void Awake()
    {
        RANDOMIZE = true;
        SetFlags();

        //RenderStyle Add
        Name = "PlasmaBall";
        Radius = 13;
        Height = 8;
        Speed = 25;
        Damage = 5;
        Alpha = 0.75f;
        
        Sounds = new Dictionary<string, string>
        {
            { "SeeSound", "weapons/plasmaf" },
            { "DeathSound", "weapons/plasmax"}
        };

        Obituary = "$OB_MPPLASMARIFLE";

        sprite = "PLSS";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "PLSS", sprInd = "AB", time = 6},
                        new StateInfo{function = "Loop" }
                   }
               }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "PLSE", sprInd = "ABCDE", time = 4},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class RevenantTracer : Projectile
{

    public void Awake()
    {
        //SeekerMissile = true;
        RANDOMIZE = true;
        SetFlags();

        //RenderStyle Add
        Name = "RevenantTracer";
        Radius = 11;
        Height = 8;
        Speed = 10;
        Damage = 10;

        Sounds = new Dictionary<string, string>
        {
            { "SeeSound", "skeleton/attack" },
            { "DeathSound", "skeleton/tracex"}
        };

        sprite = "FATB";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "FATB", sprInd = "AB", time = 2, function = "A_Tracer"},
                        new StateInfo{function = "Loop" }
                   }
               }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "FBXP", sprInd = "A", time = 8},
                        new StateInfo{spr = "FBXP", sprInd = "B", time = 6},
                        new StateInfo{spr = "FBXP", sprInd = "C", time = 4},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class RevenantTracerSmoke : Actor
{

    public void Awake()
    {
        NOBLOCKMAP = true;
        NOGRAVITY = true;
        NOTELEPORT = true;
        //RenderStyle Translucent
        Alpha = 0.5f;
        sprite = "PUFF";
        Name = "RevenantTracerSmoke";
        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "PUFF", sprInd = "ABABC", time = 4},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}

public class Rocket : Projectile
{

    public virtual void Awake()
    {
        SetFlags();
        RANDOMIZE = true;
        DEHEXPLOSION = true;
        ROCKETTRAIL = true;


        Name = "Rocket";
        Radius = 11;
        Height = 8;
        Speed = 20;
        Damage = 20;

        Sounds = new Dictionary<string, string>
        {
            { "SeeSound", "weapons/rocklf" },
            { "DeathSound", "weapons/rocklx" }
        };

        Obituary = "$OB_MPROCKET";

        sprite = "MISL";

        actorStates = new Dictionary<string, State>
        {
            { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "MISL", sprInd = "A", time = 1},
                        new StateInfo{function = "Loop" }
                   }
               }
            },
            { "Death", new State
                {
                   info = new List<StateInfo>
                   {
                        new StateInfo{spr = "MISL", sprInd = "B", time = 8, function = "A_Explode"},
                        new StateInfo{spr = "MISL", sprInd = "C", time = 6},
                        new StateInfo{spr = "MISL", sprInd = "D", time = 4},
                        new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }
}