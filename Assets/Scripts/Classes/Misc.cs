using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misc : Actor
{

}


public class BossBrain : Actor
{

    public void Awake()
    {
        SOLID = true;
        SHOOTABLE = true;
        OLDRADIUSDMG = true;
        Name = "BossBrain";

        Health = 250;
        Mass = 10000000;
        Radius = 16;
        Height = 56;
        PainChance = 255;

        Sounds = new Dictionary<string, string>
        {
            { "PainSound", "brain/pain" },
            { "DeathSound", "brain/death" }
        };

        actorStates = new Dictionary<string, State>
        {
           { "BrainExplode", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "MISL", sprInd = "BC", time = 10},
                       new StateInfo{spr = "MISL", sprInd = "D", time = 10, function = "A_BrainExplode"},
                       new StateInfo{function = "Stop"}
                   }
               }
           },
           { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BBRN", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }

                }
            },
           { "Pain", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "BBRN", sprInd = "B", time = 3, function = "A_BrainPain"},
                      new StateInfo {function = "Spawn"}
                  }
               }
            },
           { "Death", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "BBRN", sprInd = "A", time = 100, function = "A_BrainScream" },
                      new StateInfo{spr = "BBRN", sprInd = "AA", time = 10},
                      new StateInfo{spr = "BBRN", sprInd = "A", time = -1, function = "A_BrainDie" },
                      new StateInfo{function = "Stop"}
                  }
               }
            }
        };
    }
}

public class BossRocket : Rocket
{

    public override void Awake()
    {
        Name = "BossRocket";

        actorStates = new Dictionary<string, State>
        {
           { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "MISL", sprInd = "BC", time = 10},
                       new StateInfo{spr = "MISL", sprInd = "D", time = 10, function = "A_BrainExplode"},
                       new StateInfo{function = "Stop" }
                   }
                }
            }
        };
    }
}

public class BossEye : Actor
{

    public void Awake()
    {
        Height = 32;
        NOBLOCKMAP = true;
        NOSECTOR = true;
        Name = "BossEye";

        actorStates = new Dictionary<string, State>
        {
           { "Spawn", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SSWV", sprInd = "A", time = 10, function = "A_Look"},
                       new StateInfo{function = "Loop"}
                   }
               }
           },
           { "See", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SSWV", sprInd = "A", time = 181, function = "A_BrainAwake"},
                       new StateInfo{spr = "SSWV", sprInd = "A", time = 150, function = "A_BrainSpit"},
                       new StateInfo{function = "Wait" }
                   }

                }
            },
        };
    }
}


public class BossTarget : SpecialSpot
{

    public void Awake()
    {
        Name = "BossTarget";

        Height = 32;
        NOBLOCKMAP = true;
        NOSECTOR = true;
    }
}

public class SpecialSpot : Actor
{
    public void A_SpawnSingleItem(Actor type, int fail_sp = 0, int fail_co = 0, int fail_dm = 0) { }
}


public class SpawnShot : Projectile
{

    public void Awake()
    {
        Name = "SpawnShot";
        SetFlags();
        Height = 32;
        Radius = 6;
        Speed = 10;
        Damage = 3;

        NOCLIP = true;
        ACTIVATEPCROSS = false;
        RANDOMIZE = true;

        Sounds = new Dictionary<string, string>
        {
            { "SeeSound", "brain/spit" },
            { "DeathSound", "brain/cubeboom" }
        };

        actorStates = new Dictionary<string, State>
        {
           { "Spawn", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BOSF", sprInd = "A", time = 3, function = "A_SpawnSound"},
                       new StateInfo{spr = "BOSF", sprInd = "BCD", time = 3, function = "A_SpawnFly"},
                       new StateInfo{function = "Loop"}
                   }
               }
           }
        };
    }
}

public class SpawnFire : Actor
{

    public void Awake()
    {
        Name = "SpawnFire";

        Height = 78;
        NOBLOCKMAP = true;
        NOGRAVITY = true;

        actorStates = new Dictionary<string, State>
        {
           { "Spawn", new State
               {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "FIRE", sprInd = "ABCDEFGH", time = 4, function = "A_Fire"},
                       new StateInfo{function = "Stop"}
                   }
               }
           }
        };
    }
}

public class CommanderKeen : Actor
{

    public void Awake()
    {
        Name = "CommanderKeen";

        Health = 100;
        Mass = 10000000;
        Radius = 16;
        Height = 72;
        PainChance = 256;

        SOLID = true;
        SPAWNCEILING = true;
        NOGRAVITY = true;
        SHOOTABLE = true;
        COUNTKILL = true;
        ISMONSTER = true;

        Sounds = new Dictionary<string, string>
        {
            { "PainSound", "keen/pain" },
            { "DeathSound", "keen/death" }
        };

        actorStates = new Dictionary<string, State>
        {
           { "Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "KEEN", sprInd = "A", time = -1},
                       new StateInfo{function = "Loop" }
                   }

                }
            },
           { "Pain", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "KEEN", sprInd = "M", time = 4},
                      new StateInfo{spr = "KEEN", sprInd = "M", time = 8, function = "A_Pain" },
                      new StateInfo{function = "Spawn"}
                  }
               }
            },
            { "Death", new State
               {
                  info = new List<StateInfo>
                  {
                      new StateInfo{spr = "KEEN", sprInd = "AB", time = 6},
                      new StateInfo{spr = "KEEN", sprInd = "C", time = 6, function = "A_Scream"},
                      new StateInfo{spr = "KEEN", sprInd = "DEFGH", time = 6},
                      new StateInfo{spr = "KEEN", sprInd = "I", time = 6},
                      new StateInfo{spr = "KEEN", sprInd = "J", time = 6},
                      new StateInfo{spr = "KEEN", sprInd = "K", time = 6, function = "A_KeenDie"},
                      new StateInfo{spr = "KEEN", sprInd = "L", time = -1},
                      new StateInfo {function = "Stop"}
                  }
               }
            }
        };
    }
}

