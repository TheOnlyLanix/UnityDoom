using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using DoomTriangulator;
using System;


public class mapCreator : MonoBehaviour
{

    public DoomMap openedMap;
    public Transform thingParent;
    public Transform sectorParent;
    public Transform mapParent;
    public GameObject player;
    public wadReader reader;
    public int secNum = 0;
    public GameObject skybox;
    //Map stuff
    public int mapSelected = 0;
    public Button Map_Next;
    public Button Map_Prev;
    public Texture2D sky;
    public Skybox skyboxScript;
    private bool hasOpenedAllDoors = false; // TODO: temporary

    /// <summary>
    /// 0 = I 'm too young to die,
    /// 1 = Hey, Not Too Rough,
    /// 2 = Hurt Me Plenty,
    /// 3 = Ultra-Violence,
    /// 4 = NIGHTMARE,
    /// </summary>
    public int skill;

    mus2mid musmid;
    public MIDIPlayer midiplayer;

    public void Awake()
    {
        CreateSkybox();
        musmid = GetComponent<mus2mid>();
    }

    static Dictionary<int, Type> MonsterType = new Dictionary<int, Type>
    {
        {3004, typeof(ZombieMan)        },
        {3001, typeof(DoomImp)          },
        {9,    typeof(ShotgunGuy)       },
        {65,   typeof(ChaingunGuy)      },
        {3003, typeof(BaronOfHell)      },
        {68,   typeof(Arachnotron)      },
        {7,    typeof(SpiderMastermind) },
        {3002, typeof(Demon)            },
        {58,   typeof(Spectre)          },
        {64,   typeof(Archvile)         },
        {69,   typeof(HellKnight)       },
        {16,   typeof(CyberDemon)       },
        {67,   typeof(Fatso)            },
        {3006, typeof(LostSoul)         },
        {71,   typeof(PainElemental)    },
        {66,   typeof(Revenant)         },
        {84,   typeof(WolfensteinSS)    },
        {3005, typeof(Cacodemon)        }
    };

    static Dictionary<int, Type> DecorationType = new Dictionary<int, Type>
    {
        {70,   typeof(BurningBarrel)        },
        {73,   typeof(HangNoGuts)           },
        {74,   typeof(HangBNoBrain)         },
        {75,   typeof(HangTLookingDown)     },
        {76,   typeof(HangTSkull)           },
        {77,   typeof(HangTLookingUp)       },
        {78,   typeof(HangTNoBrain)         },
        {79,   typeof(ColonGibs)            },
        {80,   typeof(SmallBloodPool)       },
        {81,   typeof(BrainStem)            },
        {85,   typeof(TechLamp)             },
        {86,   typeof(TechLamp2)            },
        {10,   typeof(GibbedMarine)         },
        {12,   typeof(GibbedMarine)         },
        {15,   typeof(DeadMarine)           },
        {18,   typeof(DeadZombieMan)        },
        {19,   typeof(DeadShotgunGuy)       },
        {20,   typeof(DeadDoomImp)          },
        {21,   typeof(DeadDemon)            },
        {22,   typeof(DeadCacodemon)        },
        {23,   typeof(DeadLostSoul)         },
        {24,   typeof(RealGibs)             },//Gibs
        {25,   typeof(DeadStick)            },
        {26,   typeof(LiveStick)            },
        {27,   typeof(HeadOnAStick)         },
        {28,   typeof(HeadsOnAStick)        },
        {29,   typeof(HeadCandles)          },
        {30,   typeof(TallGreenColumn)      },
        {31,   typeof(ShortGreenColumn)     },
        {32,   typeof(TallRedColumn)        },
        {33,   typeof(ShortRedColumn)       },
        {34,   typeof(Candlestick)          },
        {35,   typeof(Candelabra)           },
        {36,   typeof(HeartColumn)          },
        {37,   typeof(SkullColumn)          },
        {41,   typeof(EvilEye)              },
        {42,   typeof(FloatingSkull)        },
        {43,   typeof(TorchTree)            },
        {44,   typeof(BlueTorch)            },
        {45,   typeof(GreenTorch)           },
        {46,   typeof(RedTorch)             },
        {47,   typeof(Stalagtite)           },
        {48,   typeof(TechPillar)           },
        {49,   typeof(BloodyTwitch)         },
        {50,   typeof(Meat2)                },
        {51,   typeof(Meat3)                },
        {52,   typeof(Meat4)                },
        {53,   typeof(Meat5)                },
        {54,   typeof(BigTree)              },
        {55,   typeof(ShortBlueTorch)       },
        {56,   typeof(ShortGreenTorch)      },
        {57,   typeof(ShortRedTorch)        },
        {59,   typeof(NonsolidMeat2)        },
        {60,   typeof(NonsolidMeat3)        },
        {61,   typeof(NonsolidMeat4)        },
        {62,   typeof(NonsolidMeat5)        },
        {63,   typeof(NonsolidTwitch)       },
        {2028, typeof(Column)               },
        {2035, typeof(ExplosiveBarrel)      },
        {5050, typeof(Stalagmite)           },

    };

    static Dictionary<int, Type> PickupType = new Dictionary<int, Type>
    {
        {82,     typeof(SuperShotgun)          },
        {83,     typeof(Megasphere)            },
        {5,      typeof(BlueCard)              },
        {6,      typeof(YellowCard)            },
        {8,      typeof(Backpack)              },
        {13,     typeof(RedCard)               },
        {17,     typeof(CellPack)              },
        {38,     typeof(RedSkull)              },
        {39,     typeof(YellowSkull)           },
        {40,     typeof(RedSkull)              },
        {2001,   typeof(Shotgun)               },
        {2002,   typeof(Chaingun)              },
        {2003,   typeof(RocketLauncher)        },
        {2004,   typeof(PlasmaRifle)           },
        {2005,   typeof(Chainsaw)              },
        {2006,   typeof(BFG9000)               },
        {2007,   typeof(Clip)                  },
        {2008,   typeof(Shell)                 },
        {2010,   typeof(RocketAmmo)            },
        {2011,   typeof(Stimpack)              },
        {2012,   typeof(Medikit)               },
        {2013,   typeof(Soulsphere)            },
        {2014,   typeof(HealthBonus)           },
        {2015,   typeof(ArmorBonus)            },
        {2018,   typeof(GreenArmor)            },
        {2019,   typeof(BlueArmor)             },
        {2022,   typeof(InvulnerabilitySphere) },
        {2023,   typeof(Berserk)               },
        {2024,   typeof(BlurSphere)            },
        {2025,   typeof(RadSuit)               },
        {2026,   typeof(Allmap)                },
        {2045,   typeof(Infrared)              },
        {2046,   typeof(RocketBox)             },
        {2047,   typeof(Cell)                  },
        {2048,   typeof(ClipBox)               },
        {2049,   typeof(ShellBox)              },
        {5010,   typeof(Pistol)                }

    };


    //This is a collection of all the animated textures in doom 1 and 2.
    //the string in the dictionary is the name that should be present in the map
    //the list of strings is each frame of that animated texture
    public Dictionary<string, List<string>> AnimatedTextures = new Dictionary<string, List<string>>
    {
        //Flats
        {"NUKAGE1", new List<string>{ "NUKAGE1", "NUKAGE2", "NUKAGE3"} },
        {"FWATER1", new List<string>{ "FWATER1", "FWATER2", "FWATER3", "FWATER4" } },
        {"SWATER1", new List<string>{ "SWATER1", "SWATER2", "SWATER3", "SWATER4" } },
        {"LAVA1", new List<string>{ "LAVA1", "LAVA2", "LAVA3", "LAVA4" } },
        {"BLOOD1", new List<string>{ "BLOOD1", "BLOOD2", "BLOOD3" } },
        {"RROCK05", new List<string>{ "RROCK05", "RROCK06", "RROCK07", "RROCK08" } },
        {"SLIME01", new List<string>{ "SLIME01", "SLIME02", "SLIME03", "SLIME04" } },
        {"SLIME05", new List<string>{ "SLIME05", "SLIME06", "SLIME07", "SLIME08" } },
        {"SLIME09", new List<string>{ "SLIME09", "SLIME10", "SLIME11", "SLIME12" } },

        //Textures (Walls)
        {"BLODGR1", new List<string>{ "BLODGR1", "BLODGR2", "BLODGR3", "BLODGR4" } },
        {"BLODRIP1", new List<string>{ "BLODRIP1", "BLODRIP2", "BLODRIP3", "BLODRIP4" } },
        {"FIREBLU1", new List<string>{ "FIREBLU1", "FIREBLU2" } },
        {"FIRELAV3", new List<string>{ "FIRELAV3", "FIRELAVA" } },
        {"FIREMAG1", new List<string>{ "FIREMAG1", "FIREMAG2", "FIREMAG3" } },
        {"FIREWALA", new List<string>{ "FIREWALA", "FIREWALB", "FIREWALL" } },
        {"GSTFONT1", new List<string>{ "GSTFONT1", "GSTFONT2", "GSTFONT3" } },
        {"ROCKRED1", new List<string>{ "ROCKRED1", "ROCKRED2", "ROCKRED3" } },
        {"SLADRIP1", new List<string>{ "SLADRIP1", "SLADRIP2", "SLADRIP3" } },
        {"BFALL1", new List<string>{ "BFALL1", "BFALL2", "BFALL3", "BFALL4" } },
        {"SFALL1", new List<string>{ "SFALL1", "SFALL2", "SFALL3", "SFALL4" } },
        {"WFALL1", new List<string>{ "WFALL1", "WFALL2", "WFALL3", "WFALL4" } },
        {"DBRAIN1", new List<string>{ "DBRAIN1", "DBRAIN2", "DBRAIN3", "DBRAIN4" } }
    };



    IEnumerator CreateMap()
    {
        //destroy all children of map parent
        for (int i = 0; i < mapParent.childCount; i++)
        {
            GameObject.Destroy(mapParent.GetChild(i).gameObject);
        }
        yield return (new WaitForEndOfFrame());

        //use this if you want to select one map in particular
        //if (mapSelected == 1) { mapSelected = 23; }

        openedMap = reader.newWad.maps[mapSelected - 1];

        //use this if you want to pick a selection of maps to choose from instead of going through the whole list
        //int[] mapMapper = { 3, 20, 26, 28, 30, 35 };
        //openedMap = reader.newWad.maps[mapMapper[(mapSelected - 1) % mapMapper.Length] - 1];

        //display map
        Debug.Log("MAP: " + mapSelected);

        //read and play music
        MUS mus = reader.ReadMusEntry(mapSelected - 1);
        midiplayer.PlayMusic(musmid.WriteMidi(mus, mus.name));

        //fill in any missing map information
        fillInfo(openedMap);
        float minx = 0;
        float miny = 0;
        foreach(Vector3 vert in openedMap.vertexes)
        {
            if (vert.x > minx)
                minx = vert.x;
            if (vert.z > miny)
                miny = vert.z;
        }
        skybox.transform.position = new Vector3(minx, 0, miny);

        //add sectors and monsters to map
        AddSectors();
        SetSkyboxTexture();
        AddThings();
        skyboxScript.ChangeSky();

    }

    void SetSkyboxTexture()
    {
        MeshRenderer smr = skybox.GetComponent<MeshRenderer>();
        smr.material.mainTexture = sky;
    }

    public void buttonMapNextClicked()
    {
        mapSelected++;
        StartCoroutine(CreateMap());

    }

    public void buttonMapPrevCLicked()
    {
        mapSelected--;
        StartCoroutine(CreateMap());
    }

    public void buttonMapOpenDoorsClicked()
    {
        if (!hasOpenedAllDoors)
        {
            GameObject[] gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            foreach (GameObject go in gameObjects)
            {
                if (go.name.Contains("_Door"))
                {
                    go.transform.Translate(new Vector3(0, 64f, 0));
                }
            }
            hasOpenedAllDoors = true;
        }
    }

    void AddSectors()
    {
        sectorParent = new GameObject("sectorParent").transform;
        sectorParent.parent = mapParent;

        for (int i = 0; i < openedMap.sectors.Count(); i++)    //start with a loop for each sector
        {
            //if(i != 31) { continue; }

            SECTORS sector = openedMap.sectors[i];
            CreateMapObject(sector, "Sector_" + i, Triangulator.GeneratingGo.Sector);

            if (sector.isMovingCeiling)
                CreateMapObject(sector, sector.isDoor ? ("Sector_" + i + "_Door") : ("Sector_" + i + "_MovingCeiling"), Triangulator.GeneratingGo.Ceiling);

            if (sector.isMovingFloor)
                CreateMapObject(sector, "Sector_" + i + "_MovingFloor", Triangulator.GeneratingGo.Floor);

        }

        hasOpenedAllDoors = false;
    }

    void AddThings()
    {
        thingParent = new GameObject("thingParent").transform;
        thingParent.parent = mapParent;

        foreach(THINGS thing in openedMap.things)
        {

            //Multiplayer only thing
            if (((thing.thingOptions & 0x10) >> 4) == 1)
                continue;

            float Zpos = 0;
            
            RaycastHit hit;
            //getting the Z(Y) height
            if (Physics.Raycast(new Vector3(thing.xPos, 1000, thing.yPos), Vector3.down, out hit))
                Zpos = hit.point.y + 1;

            Vector3 pos = new Vector3(thing.xPos, Zpos, thing.yPos);

            GameObject newThing = new GameObject(thing.thingType.ToString());
            newThing.transform.parent = thingParent;
            newThing.transform.position = pos;

            if(MonsterType.ContainsKey(thing.thingType))//If its a monster
            {
                //Only spawn the monster if it is supposed to be on the current skill level
                if(skill == 0 || skill == 1)
                    if ((thing.thingOptions & 0x01) != 1)
                        continue;
                else if (skill == 2)
                    if ((thing.thingOptions & 0x02) != 1)
                        continue;
                else if (skill == 3 || skill == 4)
                    if ((thing.thingOptions & 0x04) != 1)
                        continue;

                newThing.AddComponent(MonsterType[thing.thingType]);
                newThing.AddComponent<Rigidbody>();
                ThingController controller = newThing.AddComponent<ThingController>();
                controller.OnCreate(reader.newWad.sprites, thing, reader.newWad.sounds);
                newThing.layer = 10;

            }
            else if (DecorationType.ContainsKey(thing.thingType))//if its a Decoration
            {
                newThing.AddComponent(DecorationType[thing.thingType]);

                ThingController controller = newThing.AddComponent<ThingController>();
                controller.OnCreate(reader.newWad.sprites, thing, reader.newWad.sounds);
            }
            else if (PickupType.ContainsKey(thing.thingType))//if its a Pickup
            {
                newThing.AddComponent(PickupType[thing.thingType]);

                PickupController controller = newThing.AddComponent<PickupController>();
                controller.OnCreate(reader.newWad.sprites, thing, reader.newWad.sounds);
            }
            else if (thing.thingType == 1)//player 1
            {
                player.transform.position = pos;
            }
        }
    }

    public void CreateSkybox()
    {
        Triangulator.CreateSkybox(skybox);
    }

    private GameObject CreateMapObject(SECTORS sector, string name, Triangulator.GeneratingGo generating)
    {
        //create lists for combining floor and ceiling meshes
        List<SubMesh> sectorMeshes = new List<SubMesh>();
        Material[] sectorMaterials = new Material[2]; //theres a material for the floor and the ceiling

        //create floor
        SubMesh floor = Triangulator.CreateFloor(sector, reader.newWad.flats[sector.floorFlat], generating);
        sectorMeshes.Add(floor);

        //create ceiling
        sectorMeshes.Add(Triangulator.CreateCeiling(floor, sector, reader.newWad.flats[sector.ceilingFlat], generating));

        //set the skybox's texture
        //DOOM2 SPECIFIC!!!!
        if (mapSelected <= 11)
        {
            Texture2D newSky = (Texture2D)reader.newWad.textures["SKY1"].mainTexture;
            sky = newSky;
            skybox.GetComponent<MeshRenderer>().material.SetColor("_LowerFogColor", AverageColorTop(newSky));
            skybox.GetComponent<MeshRenderer>().material.SetColor("_UpperFogColor", AverageColorBottom(newSky));
        }
        else if (mapSelected > 11 && mapSelected <= 20)
        {
            Texture2D newSky = (Texture2D)reader.newWad.textures["SKY2"].mainTexture;
            sky = newSky;
            skybox.GetComponent<MeshRenderer>().material.SetColor("_LowerFogColor", AverageColorTop(newSky));
            skybox.GetComponent<MeshRenderer>().material.SetColor("_UpperFogColor", AverageColorBottom(newSky));
        }
        else if (mapSelected > 20 && mapSelected <= 32)
        {
            Texture2D newSky = (Texture2D)reader.newWad.textures["SKY3"].mainTexture;
            sky = newSky;
            skybox.GetComponent<MeshRenderer>().material.SetColor("_LowerFogColor", AverageColorTop(newSky));
            skybox.GetComponent<MeshRenderer>().material.SetColor("_UpperFogColor", AverageColorBottom(newSky));
        }

        //create walls
        sectorMeshes.AddRange(Triangulator.CreateWalls(sector, reader.newWad, generating));

        //remove null submeshes
        sectorMeshes.RemoveAll(x => x == null);

        //combine the meshes
        Mesh mesh = new Mesh();
        Triangulator.CombineAsMesh(ref mesh, sectorMeshes, ref sectorMaterials);

        Renderer rend;
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();

        //Setup GameObject
        GameObject go = new GameObject();
        go.AddComponent<MeshFilter>();
        go.AddComponent<MeshRenderer>();
        go.name = name;
        go.transform.parent = sectorParent;

        //Set up some variables
        rend = go.GetComponent<Renderer>();

        //Set the color for the material independently (for setting light level)
        propBlock.SetColor("_Color", new Color(sector.lightLevel / 255f, sector.lightLevel / 255f, sector.lightLevel / 255f, 1));
        rend.SetPropertyBlock(propBlock);

        //fill in the materials and mesh for the gameobject
        go.GetComponent<MeshRenderer>().materials = sectorMaterials;
        go.GetComponent<MeshFilter>().mesh = mesh;
        go.AddComponent<MeshCollider>();

        foreach (Material mat in go.GetComponent<MeshRenderer>().materials)
        {
            foreach(string str in AnimatedTextures.Keys)
            {
                if (AnimatedTextures[str].Contains(mat.mainTexture.name) && go.GetComponent<TextureAnimator>() == null)
                {
                    TextureAnimator texAnim = go.AddComponent<TextureAnimator>();
                    texAnim.wad = reader.newWad;

                    break;
                }
            }
        }

        return go;
    }

    Color32 AverageColorTop(Texture2D tex)
    {
        Color32[] texColors = tex.GetPixels32();
        int total = tex.width;
        int r = 0; int g = 0; int b = 0;

        for (int i = (texColors.Length - tex.width); i < texColors.Length; i++)
        {
            r += texColors[i].r;
            g += texColors[i].g;
            b += texColors[i].b;
        }
        return new Color32((byte)(r / total), (byte)(g / total), (byte)(b / total), 0);
    }

    Color32 AverageColorBottom(Texture2D tex)
    {
        Color32[] texColors = tex.GetPixels32();
        int total = tex.width* (tex.height/2);
        int r = 0; int g = 0; int b = 0;

        for (int i = 0; i < tex.width * (tex.height / 2); i++)
        {
            r += texColors[i].r;
            g += texColors[i].g;
            b += texColors[i].b;
        }
        return new Color32((byte)(r / total), (byte)(g / total), (byte)(b / total), 0);
    }


    void drawVert(Vector3 pos)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

        go.transform.localScale = new Vector3(10, 10, 10);

        go.transform.parent = mapParent;
        go.transform.position = pos;
    }

    void fillInfo(DoomMap map)
    {
        // find and remember each sector's tag and index
        map.sectorsByTag.Clear();
        for (int i = 0; i < map.sectors.Count; i++)
        {
            SECTORS sector = map.sectors[i];
            sector.lines.Clear();
            sector.otherLines.Clear();
            sector.sectorIndex = i;
            if (sector.sectorTag == 0) { continue; }
            if (!map.sectorsByTag.ContainsKey(sector.sectorTag))
                map.sectorsByTag.Add(sector.sectorTag, new List<SECTORS> { sector });
            else
                map.sectorsByTag[sector.sectorTag].Add(sector);
        }

        // set sector's default movement bounds
        foreach (SECTORS sector in map.sectors)
        {
            sector.floorBounds = new int[2] { sector.floorHeight, sector.floorHeight };
            sector.ceilingBounds = new int[2] { sector.ceilingHeight, sector.ceilingHeight };
        }

        foreach (LINEDEFS line in map.linedefs)
        {
            line.map = map;
            line.getVerts(); //store the vertex information inside the linedef
            line.getSidedefs(); //store the sidedefs in the linedef for easier access

            SECTORS front = line.getFrontSector();
            SECTORS back = line.getBackSector();

            // tag dynamic sectors
            if (LineDefTypes.types.ContainsKey(line.types))
            {
                switch (LineDefTypes.types[line.types].category)
                {
                    case LineDefTypes.Category.Door:
                        LineDefTypes.LineDefDoorType doorType = (LineDefTypes.types[line.types] as LineDefTypes.LineDefDoorType);

                        // tag door sectors
                        if (doorType.isLocal() && back != null)
                        {
                            back.isMovingCeiling = true;
                            back.isDoor = true;
                        }
                        else if (!doorType.isLocal() && map.sectorsByTag.ContainsKey(line.tag))
                        {
                            map.sectorsByTag[line.tag].ForEach(x => x.isMovingCeiling = true);
                            map.sectorsByTag[line.tag].ForEach(x => x.isDoor = true);
                        }

                        break;

                    case LineDefTypes.Category.Floor:
                        // tag floor sectors
                        if (map.sectorsByTag.ContainsKey(line.tag))
                            map.sectorsByTag[line.tag].ForEach(x => x.isMovingFloor = true);
                        break;

                    case LineDefTypes.Category.Lift:
                        // tag lift sectors
                        if (map.sectorsByTag.ContainsKey(line.tag))
                            map.sectorsByTag[line.tag].ForEach(x => x.isMovingFloor = true);
                        break;

                    case LineDefTypes.Category.Ceiling:
                        // tag ceiling sectors
                        if (map.sectorsByTag.ContainsKey(line.tag))
                            map.sectorsByTag[line.tag].ForEach(x => x.isMovingCeiling = true);
                        break;

                    case LineDefTypes.Category.Crusher:
                        // tag crusher  sectors
                        if (map.sectorsByTag.ContainsKey(line.tag))
                            map.sectorsByTag[line.tag].ForEach(x => x.isMovingCeiling = true);
                        break;
                }
            }

            if (front == null && back == null) //if this line is BROKEN
            {
                continue; //we ignore it
            }
            else if (front == back) //if the front and the back are the same, it is inside a single sector
            {
                //if the line is internal to the sector, theres a special place in the sector class for them
                front.otherLines.Add(line);
            }
            else if (front != null) //if the front is defined, add the front
            {
                front.lines.Add(line);

                if (back != null)
                {
                    back.lines.Add(line);//add the line to the sector on its back as well..if it exists
                }
            }
            else
            {
                back.lines.Add(line); //if all else fails, the line must be backwards
            }
        }

        // find and remember each sector's neighboring sectors
        foreach (SECTORS sector in map.sectors)
        {
            foreach (LINEDEFS line in sector.lines)
            {
                if (line.getFrontSector() != sector && line.getFrontSector() != null && !sector.neighbors.Contains(line.getFrontSector()))
                    sector.neighbors.Add(line.getFrontSector());

                if (line.getBackSector() != sector && line.getBackSector() != null && !sector.neighbors.Contains(line.getBackSector()))
                    sector.neighbors.Add(line.getBackSector());
            }
        }

        //set sector's movement bounds
        foreach (LINEDEFS line in map.linedefs)
        {
            if (!LineDefTypes.types.ContainsKey(line.types)) { continue; }

            if (LineDefTypes.types[line.types].category == LineDefTypes.Category.Door)
            {
                LineDefTypes.LineDefDoorType doorType = LineDefTypes.types[line.types] as LineDefTypes.LineDefDoorType;
                if (doorType.isLocal())
                {
                    UpdateSectorBounds(line.getBackSector(), line);
                    continue;
                }
            }

            if (line.tag != 0 && map.sectorsByTag.ContainsKey(line.tag))
            {
                foreach (SECTORS sector in map.sectorsByTag[line.tag])
                {
                    UpdateSectorBounds(sector, line);
                }
            }
        }
    }

    void UpdateSectorBounds(SECTORS sector, LINEDEFS line)
    {
        if (sector == null) { return; }
        int[] floorBounds = LineDefTypes.types[line.types].GetFloorMovementBound(reader.newWad, sector);
        sector.floorBounds[0] = Math.Min(sector.floorBounds[0], floorBounds[0]);
        sector.floorBounds[1] = Math.Max(sector.floorBounds[1], floorBounds[1]);

        int[] ceilingBounds = LineDefTypes.types[line.types].GetCeilingMovementBound(reader.newWad, sector);
        sector.ceilingBounds[0] = Math.Min(sector.ceilingBounds[0], ceilingBounds[0]);
        sector.ceilingBounds[1] = Math.Max(sector.ceilingBounds[1], ceilingBounds[1]);
    }
}


