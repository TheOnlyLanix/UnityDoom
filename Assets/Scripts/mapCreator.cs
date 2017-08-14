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

    public void buttonMapNextClicked()
    {


        for (int i = 0; i < mapParent.childCount; i++)
        {
            GameObject.Destroy(mapParent.GetChild(i).gameObject);
        }

        mapSelected += 1;
        //mapSelected = 13;
        openedMap = reader.newWad.maps[mapSelected-1];
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


    void drawMap()
    {
        for (int i = 0; i < openedMap.sectors.Count(); i++)    //start with a loop for each sector
        {
            //if(i != 157) { continue; }
            CreateSector(i);
        }

        player.transform.position = new Vector3(openedMap.things[0].xPos, 60, openedMap.things[0].yPos);

    }
    
    private GameObject CreateDoomObject(string name, Mesh mesh, Material[] materials, int lightLevel)
    {
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
        propBlock.SetColor("_Color", new Color(lightLevel / 255f, lightLevel / 255f, lightLevel / 255f, 1));
        rend.SetPropertyBlock(propBlock);

        //fill in the materials and mesh for the gameobject
        go.GetComponent<MeshRenderer>().materials = materials;
        go.GetComponent<MeshFilter>().mesh = mesh;
        go.AddComponent<MeshCollider>();
        return go;
    }

    private void CreateSector(int sectorIndex)
    {
        SECTORS sector = openedMap.sectors[sectorIndex];

        //create lists for combining floor and ceiling meshes
        List<SubMesh> sectorMeshes = new List<SubMesh>();
        Material[] sectorMaterials = new Material[2]; //theres a material for the floor and the ceiling

        //create floors, walls, and ceilings
        sectorMeshes.Add(Triangulator.CreateFloor(sector, reader.newWad.flats[sector.floorFlat])); //Create floor (the hardest part)
        sectorMeshes.AddRange(Triangulator.CreateWalls(sector, reader.newWad, false)); //create walls
        if (!sector.isDoor)
            sectorMeshes.Add(Triangulator.CreateCeiling(sectorMeshes[0].mesh, sector, reader.newWad.flats[sector.ceilingFlat])); //create ceiling

        //combine the meshes
        Mesh mesh = new Mesh();
        Triangulator.CombineSubmeshes(ref mesh, sectorMeshes, ref sectorMaterials);

        //create the game object
        CreateDoomObject("Sector_" + sectorIndex, mesh, sectorMaterials, sector.lightLevel);

        if (sector.isDoor)
            CreateDoor(sectorIndex);
    }

    private void CreateDoor(int sectorIndex)
    {
        SECTORS sector = openedMap.sectors[sectorIndex];

        //create lists for combining floor and ceiling meshes
        List<SubMesh> sectorMeshes = new List<SubMesh>();
        Material[] sectorMaterials = new Material[2]; //theres a material for the floor and the ceiling

        //create floors, walls, and ceilings
        SubMesh floor = Triangulator.CreateFloor(sector, reader.newWad.flats[sector.floorFlat]); //Create floor (the hardest part)
        sectorMeshes.AddRange(Triangulator.CreateWalls(sector, reader.newWad, true)); //create walls
        sectorMeshes.Add(Triangulator.CreateCeiling(floor.mesh, sector, reader.newWad.flats[sector.ceilingFlat])); //create ceiling

        //combine the meshes
        Mesh mesh = new Mesh();
        Triangulator.CombineSubmeshes(ref mesh, sectorMeshes, ref sectorMaterials);

        //create the game object
        CreateDoomObject("Sector_" + sectorIndex + "_Door", mesh, sectorMaterials, sector.lightLevel);
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
            if(!map.sectorsByTag.ContainsKey(sector.sectorTag))
                map.sectorsByTag.Add(sector.sectorTag, new List<SECTORS> { sector });
            else
                map.sectorsByTag[sector.sectorTag].Add(sector);
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
            {
                back.isDoor = true;
            }

            //tag remote sector as door
            if (line.tag != 0 && LinedefTypes.remoteDoors.Contains(line.types) && map.sectorsByTag.ContainsKey(line.tag))
            {
                foreach (SECTORS sector in map.sectorsByTag[line.tag])
                    sector.isDoor = true;
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

                if(back != null)
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
        foreach(SECTORS sector in map.sectors)
        {
            foreach(LINEDEFS line in sector.lines)
            {
                if (line.getFrontSector() != sector && line.getFrontSector() != null && !sector.neighbors.Contains(line.getFrontSector()))
                    sector.neighbors.Add(line.getFrontSector());

                if (line.getBackSector() != sector && line.getBackSector() != null && !sector.neighbors.Contains(line.getBackSector()))
                    sector.neighbors.Add(line.getBackSector());
            }
        }
    }

    public static class LinedefTypes
    {
        public static int[] localDoors = { 1, 26, 28, 27, 31, 32, 33, 34, 46, 117, 118 };
        public static int[] remoteDoors = { 4, 29, 90, 63, 2, 103, 86, 61, 3, 50, 75, 42, 16, 76, 108, 111, 105, 114, 109, 112, 106, 115, 110, 113, 107, 116, 133, 99, 135, 134, 137, 136 };
    }
}


