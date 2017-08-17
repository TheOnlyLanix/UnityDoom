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

    public Transform mapParent;
    public GameObject player;
    public wadReader reader;

    public int secNum = 0;

    //Map stuff
    public int mapSelected = 0;
    public Button Map_Next;
    public Button Map_Prev;
    private bool hasOpenedAllDoors = false; // TODO: temporary

    public void buttonMapNextClicked()
    {


        for (int i = 0; i < mapParent.childCount; i++)
        {
            GameObject.Destroy(mapParent.GetChild(i).gameObject);
        }

        mapSelected += 1;
        openedMap = reader.newWad.maps[mapSelected - 1];
        
        //use this if you want to pick a selection of maps to choose from instead of going through the whole list
        //int[] mapMapper = { 15, 17, 28 };
        //openedMap = reader.newWad.maps[mapMapper[(mapSelected - 1) % mapMapper.Length] - 1];

        fillInfo(openedMap); //fill in any missing map information
        drawMap();
    }

    public void buttonMapPrevCLicked()
    {
        if (mapSelected > 0)
        {

            for (int i = 0; i < mapParent.childCount; i++)
            {
                GameObject.Destroy(mapParent.GetChild(i).gameObject);
            }
            mapSelected -= 1;
            openedMap = reader.newWad.maps[mapSelected-1];
            fillInfo(openedMap); //fill in any missing map information
            drawMap();

            openedMap = reader.newWad.maps[mapSelected];
        }
    }


    public void buttonMapOpenDoorsClicked()
    {
        if (hasOpenedAllDoors) { return; }
        foreach(GameObject go in openedMap.doors)
        {
            go.transform.Translate(new Vector3(0, 64f, 0));
        }
        hasOpenedAllDoors = true;
    }

    void drawMap()
    {
        for (int i = 0; i < openedMap.sectors.Count(); i++)    //start with a loop for each sector
        {
            //if(i != 157) { continue; }
            SECTORS sector = openedMap.sectors[i];
            CreateMapObject(sector, "Sector_" + i, Triangulator.GeneratingGo.Sector);

            if (sector.isDoor)
                openedMap.doors.Add(CreateMapObject(sector, "Sector_" + i + "_Door", Triangulator.GeneratingGo.Door));

            if (sector.isMovingFloor)
                CreateMapObject(sector, "Sector_" + i + "_MovingFloor", Triangulator.GeneratingGo.Floor);
        }

        player.transform.position = new Vector3(openedMap.things[0].xPos, 60, openedMap.things[0].yPos);
        hasOpenedAllDoors = false;
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
        go.transform.parent = mapParent;

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

            //tag local sector as door
            if (back != null && LinedefTypes.localDoors.Contains(line.types))
                back.isDoor = true;

            //tag remote sector(s) as door
            if (line.tag != 0 && LinedefTypes.remoteDoors.Contains(line.types) && map.sectorsByTag.ContainsKey(line.tag))
                map.sectorsByTag[line.tag].ForEach(x => x.isDoor = true);

            //tag sector(s) as moving floor
            if (line.tag != 0 && LineDefTypes.types.ContainsKey(line.types) && map.sectorsByTag.ContainsKey(line.tag))
            {
                foreach (SECTORS sector in map.sectorsByTag[line.tag])
                    sector.isMovingFloor = true;
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
                        int[] movementBounds = LineDefTypes.types[line.types].GetMovementBoundY(reader.newWad, sector);
                        sector.movementBounds[0] = Math.Min(sector.movementBounds[0], movementBounds[0]);
                        sector.movementBounds[1] = Math.Max(sector.movementBounds[1], movementBounds[1]);
                    }
                }
                else if (back != null)
                {
                    int[] movementBounds = LineDefTypes.types[line.types].GetMovementBoundY(reader.newWad, back);
                    back.movementBounds[0] = Math.Min(back.movementBounds[0], movementBounds[0]);
                    back.movementBounds[1] = Math.Max(back.movementBounds[1], movementBounds[1]);
                }
            }
        }
    }
}


