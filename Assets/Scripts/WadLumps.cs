using UnityEngine;
using System.Collections.Generic;
using System;

public class WadLumps : MonoBehaviour
{

}

//////////WAD//////////

[System.Serializable]
public class WAD
{
    //Header info

    public string identification; //an ASCII string which must be either "IWAD" or "PWAD"
    public int numlumps; //a 4-byte (long) integer which is the number of lumps in the wad
    public int infotableofs; //a long integer - the file offset to the start of the directory

    //Directory
    public List<DrctEntry> directory = new List<DrctEntry>();

    public List<DoomMap> maps = new List<DoomMap>(); //maps and associated lumps
    [HideInInspector]
    public List<Sprite> sprites = new List<Sprite>(); //sprites
    public Dictionary<string, Material> flats = new Dictionary<string, Material>(); //flats (ceiling and floor textures)
    public Dictionary<string, Material> textures = new Dictionary<string, Material>(); //textures
    public List<string> pnames = new List<string>(); //pnames lump
    public List<Color32[,]> patches = new List<Color32[,]>(); //store the patches as textures
    //more to come

}

//////////WAD Directory//////////

public class DrctEntry
{
    public int filepos; //the file offset to the start of the lump
    public int size; //the size of the lump in bytes
    public string name;  //an 8-byte ASCII string, the name of the lump, padded with zeros.
}



/////--------------------/////Map/////--------------------/////
//EXCLUDING REJECT, BLOCKMAP, SEGS, SSECTORS, and NODES lumps

//This class ties all of the seperate map-lumps together

public class DoomMap
{
    public string name;


    public List<THINGS> things = new List<THINGS>();
    public List<LINEDEFS> linedefs = new List<LINEDEFS>();
    public List<SIDEDEFS> sidedefs = new List<SIDEDEFS>();
    public List<Vector3> vertexes = new List<Vector3>();
    public List<SECTORS> sectors = new List<SECTORS>();

}

//"short" is a signed 16-bit integer (-32768..32767), stored in lo-hi format.

//////////THINGS Lump//////////

//10 bytes of data EACH (inside a wad file)
//[System.Serializable]
public class THINGS
{
    public int xPos;
    public int yPos;
    public int angle;
    public int thingType;
    public int thingOptions;
}

//////////LINEDEFS Lump//////////
//14 bytes of data
public class LINEDEFS
{
    public int firstVertIndex = -1; //2
    public int lastVertIndex = -1;  //2
    public int flags = -1;     //2
    public int types = -1;     //2
    public int tag = -1;       //2
    public int side1Index = -1;  //2
    public int side2Index = -1;   //2

    public DoomMap map;

    public SIDEDEFS side1 = null;
    public SIDEDEFS side2 = null;

    public Vector3 firstVert;
    public Vector3 lastVert;

    public void getSidedefs()
    {
        if (side1Index != -1)
        {
            side1 = map.sidedefs[side1Index];
        }

        if (side2Index != -1)
        {
            side2 = map.sidedefs[side2Index];
        }

    }

    public SECTORS getFrontSector()
    {

        if (side1 != null)
            return map.sectors[side1.secNum];
        else
            return null;
    }

    public SECTORS getBackSector()
    {
        if (side2 != null)
            return map.sectors[side2.secNum];
        else
            return null;
    }

    public void getVerts()
    {
        firstVert = map.vertexes[firstVertIndex]; //store the vertex info in the linedef
        lastVert = map.vertexes[lastVertIndex]; //store the vertex info in the linedef
    }


public float getLength()
{
    if (Vector3.Distance(lastVert, firstVert) > 0)
        return Vector3.Distance(lastVert, firstVert);
    else
        return 0;
}

public bool doubleSector()
{
    if (side1 == null || side2 == null)
        return false;
    else
        return (side1.secNum == side2.secNum);
}

public Vector3 frontVector() //points towards the front of the line
{
    return Vector3.Normalize(Vector3.Cross(Vector3.up, vector())) * 10f;
}

public Vector3 midPoint() //the midpoint of the line
{
    return (lastVert - firstVert) * 0.5f + firstVert;
}

public Vector3 vector()
{
    return lastVert - firstVert;
}


}

//////////SIDEDEFS Lump//////////
//30 bytes of data
//[System.Serializable]
public class SIDEDEFS
{
    public int xOfs;       //2
    public int yOfs;       //2 
    public string upTex;   //8
    public string midTex;  //8 
    public string lowTex;  //8
    public int secNum;     //2
}


//////////VERTEXES Lump//////////
//IS MADE OF VECTOR3's (y = 0)


//////////SECTORS Lump//////////
//26 bytes
public class SECTORS
{
    public int floorHeight;
    public int ceilingHeight;
    public string floorFlat;
    public string ceilingFlat;
    public int lightLevel;
    public int specialSec;
    public int sectorTag;

    public List<LINEDEFS> lines = new List<LINEDEFS>(); //store all of the edges for the sector.
    public List<int> verts = new List<int>(); //list of all the verts in the sector.
    public List<List<LINEDEFS>> borders = new List<List<LINEDEFS>>(); //loops of lines for the sector
    public List<List<LINEDEFS>> holes = new List<List<LINEDEFS>>(); //loops of lines for the sector
    public List<LINEDEFS> otherLines = new List<LINEDEFS>(); //lines internal to the sector (not borders)

    // filled info
    public List<SECTORS> neighbors = new List<SECTORS>(); //neighboring sectors
    public bool isDoor() { return floorHeight == ceilingHeight; }

    public int MinNeighborCeilingHeight()
    {
        if (neighbors.Count == 0)
            return ceilingHeight;

        int minHeight = neighbors[0].ceilingHeight;
        foreach(SECTORS neighbor in neighbors)
        {
            minHeight = (int)Math.Min(minHeight, neighbor.ceilingHeight);
        }

        return minHeight;
    }
}

/////--------------------/////End Map Lump Definitions/////--------------------/////


//////////PICTURES Lumps//////////
//[System.Serializable]
public class PICTURES
{
    //Header
    public int Width;
    public int Height;
    public int LeftOffset;
    public int TopOffset;


    //End Heaader//
}


public class PicColumn
{
    public int yOfs;
    public int pixelCount;
    public int[] pixels;
}

//////////PLAYPAL Lump//////////
public class PLAYPAL
{

    public List<Color32> colors = new List<Color32>();

}

public class TEXTUREx
{
    public int numTextures;
    public List<int> offset = new List<int>();

    public List<MapTexture> mtex = new List<MapTexture>();

}

public class MapTexture
{
    //maptexture_t
    public string name;
    public bool masked;
    public int width;
    public int height;
    //public int columndirectory;
    public int patchCount;
    public List<Material> patches = new List<Material>();

    public List<MapPatch> mPatch = new List<MapPatch>();

}

public class MapPatch
{
    //mappatch_t
    public int originx;
    public int originy;
    public int patch;
    //int stepdir; //unused
    //int colormap; //unused

}
