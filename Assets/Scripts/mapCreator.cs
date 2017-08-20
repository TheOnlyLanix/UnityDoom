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
    public Transform monsterParent;
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
    private List<GameObject> doors = new List<GameObject>(); // TODO: temporary
    private bool hasOpenedAllDoors = false; // TODO: temporary

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


    IEnumerator CreateMap()
    {
        //destroy all children of map parent
        for (int i = 0; i < mapParent.childCount; i++)
        {
            GameObject.Destroy(mapParent.GetChild(i).gameObject);
        }
        yield return (new WaitForEndOfFrame());

        //use this if you want to select one map in particular
        //if (mapSelected == 1) { mapSelected = 9; }

        openedMap = reader.newWad.maps[mapSelected - 1];

        //use this if you want to pick a selection of maps to choose from instead of going through the whole list
        //int[] mapMapper = { 15, 17, 28 };
        //openedMap = reader.newWad.maps[mapMapper[(mapSelected - 1) % mapMapper.Length] - 1];

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
        AddMonsters();
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
        if (hasOpenedAllDoors) { return; }
        foreach(GameObject go in doors)
        {
            go.transform.Translate(new Vector3(0, 64f, 0));
        }
        hasOpenedAllDoors = true;
    }

    void AddSectors()
    {
        sectorParent = new GameObject("sectorParent").transform;
        sectorParent.parent = mapParent;

        doors = new List<GameObject>();

        for (int i = 0; i < openedMap.sectors.Count(); i++)    //start with a loop for each sector
        {
            //if(i != 157) { continue; }
            SECTORS sector = openedMap.sectors[i];
            CreateMapObject(sector, "Sector_" + i, Triangulator.GeneratingGo.Sector);

            if (sector.isDoor)
                doors.Add(CreateMapObject(sector, "Sector_" + i + "_Door", Triangulator.GeneratingGo.Door));

            if (sector.isMovingFloor)
                CreateMapObject(sector, "Sector_" + i + "_MovingFloor", Triangulator.GeneratingGo.Floor);
        }



        hasOpenedAllDoors = false;
    }

    void AddMonsters()
    {
        monsterParent = new GameObject("monsterParent").transform;
        monsterParent.parent = mapParent;

        foreach(THINGS thing in openedMap.things)
        {

            float Zpos = 0;
            
            RaycastHit hit;
            //getting the Z(Y) height
            if (Physics.Raycast(new Vector3(thing.xPos, 1000, thing.yPos), Vector3.down, out hit))
                Zpos = hit.point.y + 1;

            Vector3 pos = new Vector3(thing.xPos, Zpos, thing.yPos);

            GameObject newThing = new GameObject(thing.thingType.ToString());
            newThing.transform.parent = monsterParent;
            newThing.transform.position = pos;

            if(MonsterType.ContainsKey(thing.thingType))
            {
                newThing.AddComponent(MonsterType[thing.thingType]);
                MonsterController controller = newThing.AddComponent<MonsterController>();
                controller.OnCreate(reader.newWad.sprites, thing);
            }
                
            if (thing.thingType == 1)
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
        if (reader.newWad.flats[sector.ceilingFlat].mainTexture.name.StartsWith("F_SKY"))
        {
            sky = (Texture2D)reader.newWad.textures["SKY1"].mainTexture;
        }

        //create walls
        sectorMeshes.AddRange(Triangulator.CreateWalls(sector, reader.newWad, generating));

        //remove null submeshes
        sectorMeshes.RemoveAll(x => x == null);

        //combine the meshes
        Mesh mesh = new Mesh();
        Triangulator.CombineSubmeshes(ref mesh, sectorMeshes, ref sectorMaterials);

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
        rend.GetPropertyBlock(propBlock);
        propBlock.SetColor("_Color", new Color(sector.lightLevel / 255f, sector.lightLevel / 255f, sector.lightLevel / 255f, 1));
        rend.SetPropertyBlock(propBlock);

        //fill in the materials and mesh for the gameobject
        go.GetComponent<MeshRenderer>().materials = sectorMaterials;
        go.GetComponent<MeshFilter>().mesh = mesh;
        go.AddComponent<MeshCollider>();
        return go;
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
        // find and remember each sector's tag
        foreach (SECTORS sector in map.sectors)
        {
            if (sector.sectorTag == 0) { continue; }
            if (!map.sectorsByTag.ContainsKey(sector.sectorTag))
                map.sectorsByTag.Add(sector.sectorTag, new List<SECTORS> { sector });
            else
                map.sectorsByTag[sector.sectorTag].Add(sector);
        }

        // set sector's default movement bounds
        foreach (SECTORS sector in map.sectors)
        {
            sector.movementBounds = new int[2] { sector.floorHeight, sector.floorHeight };
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
                switch(LineDefTypes.types[line.types].category)
                {
                    case LineDefTypes.Category.Door:
                        LineDefTypes.LineDefDoorType doorType = (LineDefTypes.types[line.types] as LineDefTypes.LineDefDoorType);
                        
                        // tag door sectors
                        if (doorType.isLocal() && back != null)
                            back.isDoor = true;
                        else if (!doorType.isLocal() && map.sectorsByTag.ContainsKey(line.tag))
                            map.sectorsByTag[line.tag].ForEach(x => x.isDoor = true);

                        break;

                    case LineDefTypes.Category.Floor:
                        // tag floor sectors
                        map.sectorsByTag[line.tag].ForEach(x => x.isMovingFloor = true);
                        break;

                    case LineDefTypes.Category.Lift:
                        // tag lift sectors
                        map.sectorsByTag[line.tag].ForEach(x => x.isMovingFloor = true);
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
            SECTORS back = line.getBackSector();
            if (line.tag != 0 && LineDefTypes.types.ContainsKey(line.types))
            {
                if (map.sectorsByTag.ContainsKey(line.tag))
                {
                    foreach (SECTORS sector in map.sectorsByTag[line.tag])
                    {
                        int[] movementBounds = LineDefTypes.types[line.types].GetFloorMovementBound(reader.newWad, sector);
                        sector.movementBounds[0] = Math.Min(sector.movementBounds[0], movementBounds[0]);
                        sector.movementBounds[1] = Math.Max(sector.movementBounds[1], movementBounds[1]);
                    }
                }
                else if (back != null)
                {
                    int[] movementBounds = LineDefTypes.types[line.types].GetFloorMovementBound(reader.newWad, back);
                    back.movementBounds[0] = Math.Min(back.movementBounds[0], movementBounds[0]);
                    back.movementBounds[1] = Math.Max(back.movementBounds[1], movementBounds[1]);
                }
            }
        }
    }
}


