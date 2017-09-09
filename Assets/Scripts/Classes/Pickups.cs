using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Weapons

public class BFG9000 : Actor                 // BFG 9000
{
    public void Awake()
    {
        Name = "BFG9000";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BFUG", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.BFG9000 = true;
        return true;
    }
}
public class Chaingun : Actor                // Chaingun
{
    public void Awake()
    {
        Name = "Chaingun";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "MGUN", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.chaingun = true;
        return true;
    }
}
public class Chainsaw : Actor                // Chainsaw
{
    public void Awake()
    {
        Name = "Chainsaw";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "CSAW", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.chainsaw = true;
        return true;
    }
}
public class Fist : Actor                    // Punch (yes thats right)
{

}
public class Pistol : Actor                  // Pistol
{
    public void Awake()
    {
        Name = "Pistol";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "PIST", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.pistol = true;
        return true;
    }
}
public class PlasmaRifle : Actor             // Plasma Gun
{
    public void Awake()
    {
        Name = "PlasmaRifle";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "PLAS", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.plasmaRifle = true;
        return true;
    }
}
public class RocketLauncher : Actor          // Rocket Launcher
{
    public void Awake()
    {
        Name = "RocketLauncher";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "LAUN", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.rocketLauncher = true;
        return true;
    }
}
public class Shotgun : Actor                 // Shotgun
{
    public void Awake()
    {
        Name = "Shotgun";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SHOT", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.shotgun = true;
        return true;
    }
}
public class SuperShotgun : Actor            // Double-barreled Shotgun
{
    public void Awake()
    {
        Name = "SuperShotgun";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SGN2", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.superShotgun = true;
        return true;
    }
}

//Ammo

public class Backpack : Actor              // Backpack (Increase carrying capacity)
{
    public void Awake()
    {
        Height = 26;
        Name = "Backpack";

        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BPAK", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }

               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (!inv.backpack)
        {
            inv.backpack = true;

            //increase the max ammo limit
            inv.bullMax = 400;
            inv.shellMax = 100;
            inv.cellMax = 600;
            inv.rcktMax = 100;
        }

        //give ammo
        if (inv.bull < inv.bullMax - 10)
            inv.bull += 10;
        else
            inv.bull = inv.bullMax;

        if (inv.shell < inv.shellMax - 4)
            inv.shell += 4;
        else
            inv.shell = inv.shellMax;

        if (inv.cell < inv.cellMax - 20)
            inv.cell += 20;
        else
            inv.cell = inv.cellMax;

        if (inv.rckt < inv.rcktMax - 1)
            inv.rckt += 1;
        else
            inv.rckt = inv.rcktMax;

        return true;
    }
}

public class Cell : Actor                    // Cell
{
    public void Awake()
    {
        Name = "Cell";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "CELL", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (inv.cell < inv.cellMax - 20)
        {
            inv.cell += 20;
            return true;
        }
        else
        {
            inv.cell = inv.cellMax;
            return false;
        }
    }
}
public class CellPack : Actor                // Cell Pack
{
    public void Awake()
    {
        Name = "CellPack";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "CELP", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (inv.cell < inv.cellMax - 100)
        {
            inv.cell += 100;
            return true;
        }
        else
        {
            inv.cell = inv.cellMax;
            return false;
        }   
    }
}
public class Clip : Actor                    // Ammo Clip
{
    public void Awake()
    {
        Name = "Clip";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "CLIP", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (inv.bull < inv.bullMax - 10)
        {
            inv.bull += 10;
            return true;
        }
        else
        {
            inv.bull = inv.bullMax;
            return false;
        }
            
    }
}
public class ClipBox : Actor                 // Box of Bullets
{
    public void Awake()
    {
        Name = "ClipBox";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "AMMO", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (inv.bull < inv.bullMax - 50)
        {
            inv.bull += 50;
            return true;
        }
        else
        {
            inv.bull = inv.bullMax;
            return false;
        }   
    }
}
public class RocketAmmo : Actor              // Rocket
{
    public void Awake()
    {
        Name = "RocketAmmo";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "ROCK", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (inv.rckt < inv.rcktMax - 1)
        {
            inv.rckt += 1;
            return true;
        }
        else
        {
            inv.rckt = inv.rcktMax;
            return false;
        }
    }
}
public class RocketBox : Actor               // Box of Rockets
{
    public void Awake()
    {
        Name = "RocketBox";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BROK", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (inv.rckt < inv.rcktMax - 5)
        {
            inv.rckt += 5;
            return true;
        }
        else
        {
            inv.rckt = inv.rcktMax;
            return true;
        }
    }
}
public class Shell : Actor                   // 4 Shells
{
    public void Awake()
    {
        Name = "Shell";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SHEL", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (inv.shell < inv.shellMax - 4)
        {
            inv.shell += 4;
            return true;
        }
        else
        {
            inv.shell = inv.shellMax;
            return false;
        }
    }
}
public class ShellBox : Actor                // Box of Shells
{
    public void Awake()
    {
        Name = "ShellBox";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SBOX", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (inv.shell < inv.shellMax - 20)
        {
            inv.shell += 20;
            return true;
        } 
        else
        {
            inv.shell = inv.shellMax;
            return false;
        }
    }
}

//Powerups

public class Allmap : Actor                  // Computer Area Map
{
    public void Awake()
    {
        Name = "Allmap";
        pickupSound = "DSGETPOW";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "AMAP", sprInd = "ABCDCB", time = 6},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.map = true;
        return true;
    }
}

public class ArmorBonus : Actor              // Armor Helmet
{
    public void Awake()
    {
        Name = "ArmorBonus";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BON2", sprInd = "ABCDCB", time = 6},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {

        if (player.armor < player.armorMax - 1)
        {
            player.armor += 1;

            if (player.armorResist < 33)
                player.armorResist = 33;
            return true;
        }
        else
            return false;

    }
}

public class Berserk : Actor                 // Berserk Pack (Full Health+Super Strength)
{
    public void Awake()
    {
        Name = "Berserk";
        pickupSound = "DSGETPOW";
        bonusTime = 120f;//2 minutes
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "PSTR", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (player.health < 100)
            player.health = 100;

        player.bonusTime = 120;
        player.berserk = true;

        //Add the screen effect to the camera on the player
        PostProcessEffect ppe = player.gameObject.transform.GetChild(0).gameObject.AddComponent<PostProcessEffect>();
        ppe.shader = Shader.Find("Custom/Powerups/Berserk");
        ppe.bonusTime = 120f;
        return true;
    }
}

public class BlueArmor : Actor               // Heavy Armor
{
    public void Awake()
    {
        Name = "BlueArmor";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "ARM2", sprInd = "A", time = 6},
                       new StateInfo{spr = "ARM2", sprInd = "B", time = 6, bright = true, brightColor = Color.blue},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        player.armor = 200;

        if(player.armorResist < 50)
            player.armorResist = 50;

        return true;
    }
}

public class BlurSphere : Actor              // Partial Invisibility
{
    public void Awake()
    {
        Name = "BlurSphere";
        pickupSound = "DSGETPOW";
        bonusTime = 120f;//2 minutes
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "PINS", sprInd = "ABCD", time = 6},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        //Partial Invisibility
        player.blurSphere = true;
        player.bonusTime = 120;
        return true;
    }
}

public class GreenArmor : Actor              // Light Armor
{
    public void Awake()
    {
        Name = "GreenArmor";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "ARM1", sprInd = "A", time = 6},
                       new StateInfo{spr = "ARM1", sprInd = "B", time = 6, bright = true, brightColor = Color.green },
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (player.armor < 100)
        {
            player.armor = 100;

            if (player.armorResist < 33)
                player.armorResist = 33;

            return true;
        }
        else
            return false;
    }
}

public class HealthBonus : Actor             // Health Potion
{
    public void Awake()
    {
        Name = "HealthBonus";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BON1", sprInd = "ABCDCB", time = 6},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (player.health < player.healthMax)
        {
            player.health++;
            return true;
        }
        else
            return false;



    }
}

public class Infrared : Actor                // Light-Amp Goggles
{
    public void Awake()
    {
        Name = "Infrared";
        pickupSound = "DSGETPOW";
        bonusTime = 120f;//2 minutes
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "PVIS", sprInd = "A", time = 6},
                       new StateInfo{spr = "PVIS", sprInd = "B", time = 6, bright = true, brightColor = Color.yellow},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        //Add the screen effect to the camera on the player
        PostProcessEffect ppe = player.gameObject.transform.GetChild(0).gameObject.AddComponent<PostProcessEffect>();
        ppe.shader = Shader.Find("Custom/Powerups/LightAmp");
        ppe.bonusTime = 120f;
        return true;
    }
}

public class InvulnerabilitySphere : Actor   // Invulnerability
{
    public void Awake()
    {
        Name = "InvulnerabilitySphere";
        pickupSound = "DSGETPOW";
        bonusTime = 120f;//2 minutes
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "PINV", sprInd = "ABCD", time = 6},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        //invulnerability
        //Add the screen effect to the camera on the player
        PostProcessEffect ppe = player.gameObject.transform.GetChild(0).gameObject.AddComponent<PostProcessEffect>();
        ppe.shader = Shader.Find("Custom/Powerups/Invulnerability");
        ppe.bonusTime = 120f;
        player.invulnerability = true;
        return true;
    }
}

public class Medikit : Actor                 // Medikit(+25 Health)
{
    public void Awake()
    {
        Name = "Medikit";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "MEDI", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (player.health < 75)
        {
            player.health += 25;
            return true;
        }
        else if (player.health < 100)
        {
            player.health = 100;
            return true;
        }
        else
            return false;
    }
}

public class Megasphere : Actor              // Megasphere (+200 Health/Armor)
{
    public void Awake()
    {
        Name = "Megasphere";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "MEGA", sprInd = "ABCD", time = 6},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        player.health = 200;
        player.armor = 200;
        player.armorResist = 50;
        return true;
    }
}

public class RadSuit : Actor                 // Radiation Suit
{
    public void Awake()
    {
        Name = "RadSuit";
        pickupSound = "DSGETPOW";
        bonusTime = 120f;//2 minutes
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SUIT", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        //Radiation resist
        //Add the screen effect to the camera on the player
        PostProcessEffect ppe = player.gameObject.transform.GetChild(0).gameObject.AddComponent<PostProcessEffect>();
        ppe.shader = Shader.Find("Custom/Powerups/Radsuit");
        ppe.bonusTime = 120f;
        return true;
    }
}

public class Soulsphere : Actor              // Soul Sphere (+100 Health)
{
    public void Awake()
    {
        Name = "Soulsphere";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "SOUL", sprInd = "ABCDCB", time = 6},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (player.health < player.healthMax - 100)
            player.health += 100;
        else
            player.health = 200;

        return true;
    }
}

public class Stimpack : Actor                // Stimpack(+10 Health)
{
    public void Awake()
    {
        Name = "Stimpack";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "STIM", sprInd = "A", time = -1},
                       new StateInfo{function = "Stop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        if (player.health < 90)
        {
            player.health += 10;
            return true;
        }  
        else if(player.health < 100)
        {
            player.health = 100;
            return true;
        }
        else
            return false;
    }
}

//Keys

public class BlueCard : Actor                // Blue Keycard
{
    public void Awake()
    {
        Name = "BlueCard";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BKEY", sprInd = "A", time = 10},
                       new StateInfo{spr = "BKEY", sprInd = "B", time = 10, bright = true, brightColor = Color.blue},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.blueCard = true;
        return true;
    }
}
public class BlueSkull : Actor               // Blue Skull Key
{
    public void Awake()
    {
        Name = "BlueSkull";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "BSKU", sprInd = "A", time = 10},
                       new StateInfo{spr = "BSKU", sprInd = "B", time = 10, bright = true, brightColor = Color.blue},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.blueSkull = true;
        return true;
    }
}
public class RedCard : Actor                 // Red Keycard
{
    public void Awake()
    {
        Name = "RedCard";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "RKEY", sprInd = "A", time = 10},
                       new StateInfo{spr = "RKEY", sprInd = "B", time = 10, bright = true, brightColor = Color.red},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.redCard = true;
        return true;
    }
}
public class RedSkull : Actor                // Red Skull Key
{
    public void Awake()
    {
        Name = "RedSkull";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "RSKU", sprInd = "A", time = 10},
                       new StateInfo{spr = "RSKU", sprInd = "B", time = 10, bright = true, brightColor = Color.red},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.redSkull = true;
        return true;
    }
}
public class YellowCard : Actor              // Yellow Keycard
{
    public void Awake()
    {
        Name = "YellowCard";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "YKEY", sprInd = "A", time = 10},
                       new StateInfo{spr = "YKEY", sprInd = "B", time = 10, bright = true, brightColor = Color.yellow},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.yellowCard = true;
        return true;
    }
}
public class YellowSkull : Actor             // Yellow Skull Key
{
    public void Awake()
    {
        Name = "YellowSkull";
        actorStates = new Dictionary<string, State>
        {
            {"Spawn", new State
                {
                   info = new List<StateInfo>
                   {
                       new StateInfo{spr = "YSKU", sprInd = "A", time = 10},
                       new StateInfo{spr = "YSKU", sprInd = "B", time = 10, bright = true, brightColor = Color.yellow},
                       new StateInfo{function = "Loop" }
                   }
               }
            }
        };
    }

    public override bool PickedUp(DoomPlayer player, Inventory inv)
    {
        inv.yellowSkull = true;
        return true;
    }
}