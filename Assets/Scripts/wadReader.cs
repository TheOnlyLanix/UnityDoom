using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class wadReader : MonoBehaviour
{

    string wadFilePath;
    //string wadFilePath = "D:/Users/Aaron/Documents/Unity/DoomProject/Assets/DOOM2.wad";

    public WAD newWad;

    public string label;

    PLAYPAL playPal;

    public Shader DoomShader;
    enum type { Sprite, Flat, Patch };

    FileStream wadOpener;

    //End of UI Elements

    void Awake()
    {
        wadFilePath = Application.dataPath + "/DOOM2.wad";

        wadOpener = new FileStream(wadFilePath, FileMode.Open, FileAccess.Read);

        ReadWADHeader();
        ReadWADDirectory();
    }



    void Start()
    {
        playPal = ReadColorPalette(wadFilePath, "PLAYPAL");

        ReadWADPatches();
        ReadWADTextures();
        ReadWADFlats();
        ReadWADSprites();
        ReadMAPEntries();

    }


    void ReadWADHeader()
    {

        byte[] wadHead = new byte[12];

        //read the header of the wad file
        wadOpener.Read(wadHead, 0, wadHead.Length);

        //Type of wad it is IWAD/PWAD
        newWad.identification = new String(System.Text.Encoding.ASCII.GetChars(wadHead, 0, 4));
        newWad.numlumps = BitConverter.ToInt32(wadHead, 4);
        newWad.infotableofs = BitConverter.ToInt32(wadHead, 8);

        //Debug.Log(newWad.identification);
        //Debug.Log(BitConverter.ToInt32(wadHead, 4));
        //Debug.Log(BitConverter.ToInt32(wadHead, 8));
    }


    void ReadWADDirectory()
    {

        byte[] wadDir = new byte[newWad.numlumps * 16];

        //Debug.Log(wadDir.Length);


        wadOpener.Position = newWad.infotableofs;
        wadOpener.Read(wadDir, 0, wadDir.Length);



        for (int i = 0; i < wadDir.Length; i += 16)
        {
            DrctEntry newDirEntry = new DrctEntry();

            newDirEntry.filepos = BitConverter.ToInt32(wadDir, i);
            newDirEntry.size = BitConverter.ToInt32(wadDir, i + 4);
            newDirEntry.name = new String(System.Text.Encoding.ASCII.GetChars(wadDir, i + 8, 8));
            newDirEntry.name = newDirEntry.name.Replace("\0", "");

            newWad.directory.Add(newDirEntry);

        }
    }

    void ReadMAPEntries()
    {
        for (int i = 0; i < newWad.directory.Count - 1; i++)
        {
            if (newWad.directory[i + 1].name.Contains("THINGS") && newWad.directory[i].size == 0)
            {
                DoomMap newMap = new DoomMap();
                DrctEntry dirEntry = newWad.directory[i];


                newMap.name = dirEntry.name;

                //THINGS lump

                if (newWad.directory[i + 1].name.Contains("THINGS"))
                {
                    byte[] thingBytes = new byte[newWad.directory[i + 1].size];

                    wadOpener.Position = newWad.directory[i + 1].filepos;
                    wadOpener.Read(thingBytes, 0, thingBytes.Length);

                    for (int j = 0; j < newWad.directory[i + 1].size; j += 10)
                    {
                        THINGS newThing = new THINGS();

                        newThing.xPos = BitConverter.ToInt16(thingBytes, j + 0);
                        newThing.yPos = BitConverter.ToInt16(thingBytes, j + 2);
                        newThing.angle = BitConverter.ToInt16(thingBytes, j + 4);
                        newThing.thingType = BitConverter.ToInt16(thingBytes, j + 6);
                        newThing.thingOptions = BitConverter.ToInt16(thingBytes, j + 8);

                        newMap.things.Add(newThing);
                    }
                }

                //LINEDEEFS lump

                if (newWad.directory[i + 2].name.Contains("LINEDEFS"))
                {
                    byte[] lineDefBytes = new byte[newWad.directory[i + 2].size];

                    wadOpener.Position = newWad.directory[i + 2].filepos;
                    wadOpener.Read(lineDefBytes, 0, lineDefBytes.Length);

                    for (int j = 0; j < newWad.directory[i + 2].size; j += 14)
                    {
                        LINEDEFS newLineDef = new LINEDEFS();

                        newLineDef.firstVertIndex = BitConverter.ToInt16(lineDefBytes, j + 0);
                        newLineDef.lastVertIndex = BitConverter.ToInt16(lineDefBytes, j + 2);
                        newLineDef.flags = BitConverter.ToInt16(lineDefBytes, j + 4);
                        newLineDef.types = BitConverter.ToInt16(lineDefBytes, j + 6);
                        newLineDef.tag = BitConverter.ToInt16(lineDefBytes, j + 8);
                        newLineDef.side1Index = BitConverter.ToInt16(lineDefBytes, j + 10);
                        newLineDef.side2Index = BitConverter.ToInt16(lineDefBytes, j + 12);

                        newMap.linedefs.Add(newLineDef);
                    }
                }

                //SIDEDEFS lump

                if (newWad.directory[i + 3].name.Contains("SIDEDEFS"))
                {
                    byte[] sideDefBytes = new byte[newWad.directory[i + 3].size];

                    wadOpener.Position = newWad.directory[i + 3].filepos;
                    wadOpener.Read(sideDefBytes, 0, sideDefBytes.Length);

                    for (int j = 0; j < newWad.directory[i + 3].size; j += 30)
                    {
                        SIDEDEFS newSideDef = new SIDEDEFS();

                        newSideDef.xOfs = BitConverter.ToInt16(sideDefBytes, j + 0);
                        newSideDef.yOfs = BitConverter.ToInt16(sideDefBytes, j + 2);
                        newSideDef.upTex = new String(System.Text.Encoding.ASCII.GetChars(sideDefBytes, j + 4, 8)).ToUpper();
                        newSideDef.lowTex = new String(System.Text.Encoding.ASCII.GetChars(sideDefBytes, j + 12, 8)).ToUpper();
                        newSideDef.midTex = new String(System.Text.Encoding.ASCII.GetChars(sideDefBytes, j + 20, 8)).ToUpper();
                        newSideDef.secNum = BitConverter.ToInt16(sideDefBytes, j + 28);

                        newSideDef.upTex = newSideDef.upTex.Replace("\0", "");
                        newSideDef.lowTex = newSideDef.lowTex.Replace("\0", "");
                        newSideDef.midTex = newSideDef.midTex.Replace("\0", "");

                        newMap.sidedefs.Add(newSideDef);
                    }
                }

                //VERTEXES lump

                if (newWad.directory[i + 4].name.Contains("VERTEXES"))
                {
                    byte[] vertexBytes = new byte[newWad.directory[i + 4].size];

                    wadOpener.Position = newWad.directory[i + 4].filepos;
                    wadOpener.Read(vertexBytes, 0, vertexBytes.Length);

                    for (int j = 0; j < newWad.directory[i + 4].size; j += 4)
                    {
                        Vector3 newVertex = new Vector3(0, 0, 0);

                        newVertex.x = BitConverter.ToInt16(vertexBytes, j + 0);
                        newVertex.z = BitConverter.ToInt16(vertexBytes, j + 2);

                        newMap.vertexes.Add(newVertex);
                    }
                }

                //SECTORS lump

                if (newWad.directory[i + 8].name.Contains("SECTORS"))
                {
                    byte[] secBytes = new byte[newWad.directory[i + 8].size];

                    wadOpener.Position = newWad.directory[i + 8].filepos;
                    wadOpener.Read(secBytes, 0, secBytes.Length);

                    for (int j = 0; j < newWad.directory[i + 8].size; j += 26)
                    {
                        SECTORS newSec = new SECTORS();

                        newSec.floorHeight = BitConverter.ToInt16(secBytes, j + 0);
                        newSec.ceilingHeight = BitConverter.ToInt16(secBytes, j + 2);
                        newSec.floorFlat = new String(System.Text.Encoding.ASCII.GetChars(secBytes, j + 4, 8));
                        newSec.ceilingFlat = new String(System.Text.Encoding.ASCII.GetChars(secBytes, j + 12, 8));
                        newSec.lightLevel = BitConverter.ToInt16(secBytes, j + 20);
                        newSec.specialSec = BitConverter.ToInt16(secBytes, j + 22);
                        newSec.sectorTag = BitConverter.ToInt16(secBytes, j + 24);

                        newSec.floorFlat = newSec.floorFlat.Replace("\0", "");
                        newSec.ceilingFlat = newSec.ceilingFlat.Replace("\0", "");

                        newMap.sectors.Add(newSec);
                    }
                }

                newWad.maps.Add(newMap); //Store the map in newWad

            }
        }
    }

    void ReadWADSprites()
    {
        List<DrctEntry> sprites = new List<DrctEntry>();

        sprites = FindWADEntrys(type.Sprite);

        foreach (DrctEntry sprite in sprites)
        {
            PICTURES newPicture = new PICTURES();

            byte[] spriteBytes = new byte[sprite.size]; //the entire picture resource in bytes

            wadOpener.Position = sprite.filepos; //FAK U
            wadOpener.Read(spriteBytes, 0, spriteBytes.Length);

            //read the picture header (first 8 bytes of the picture resource) [0-7]
            newPicture.Width = BitConverter.ToInt16(spriteBytes, 0);
            newPicture.Height = BitConverter.ToInt16(spriteBytes, 2);
            newPicture.LeftOffset = BitConverter.ToInt16(spriteBytes, 4);
            newPicture.TopOffset = BitConverter.ToInt16(spriteBytes, 6);

            int[] pointers = new int[spriteBytes.Length];


            //Reading pointer data (4 bytes each, as many as the width skipping the header)
            for (int i = 8; i < 8 + newPicture.Width * 4; i += 4)
            {
                pointers[(i - 8) / 4] = BitConverter.ToInt32(spriteBytes, i);
            }

            Array.Resize<int>(ref pointers, pointers.Length);


            //Create a new texture fill it with CLEAR PIXELS
            Texture2D newTex = new Texture2D(newPicture.Width, newPicture.Height);

            Color[] texColors = new Color[newPicture.Width * newPicture.Height];
            for (int q = 0; q < texColors.Length; q++)
            {
                texColors[q] = Color.clear;
            }

            newTex.SetPixels(texColors);

            /*
                Column Bytes:
                first byte = the pixels from the top of the graphic that we start drawing at
                second byte = the amount of pixels that are drawn from the fist bytes location going DOWNWARD
                3rd+ byte = color data for each pixel directing it toward a palette 1st and last are NOT USED.
                FF byte = end of column 
            */

            for (int i = 0; i < newPicture.Width; i++) // (for each column)
            {

                int colPos = pointers[i]; //Position from start of picture to column position
                int colSize = 0; //Size of entire column in bytes

                for (int l = pointers[i]; l < spriteBytes.Length; l++) //Get the length of each column
                {
                    if (spriteBytes[l] == 255)
                    {
                        colSize = l - pointers[i];
                        break;
                    }
                }

                byte[] columnBytes = new byte[colSize]; //create columnBytes to store data bout the column

                for (int j = 0; j < columnBytes.Length; j++) //Filling columnBytes with data about the current column
                {
                    columnBytes[j] = spriteBytes[colPos + j];
                }

                if (columnBytes.Length > 0)
                {
                    int postSize = columnBytes[1] + 4;

                    for (int p = 0; p < columnBytes.Length; p += postSize) //for each post (need correct post size and amount of posts or ERROR)
                    {
                        if (p < columnBytes.Length)
                        {
                            for (int j = 0; j < columnBytes[p + 1]; j++) //FOR EACH PIXEL
                            {
                                int yPos; //the position of the pixel from the bottom of the image. (extrapolated from the top. and whatnot..)
                                          //J signifies which pixel (from TOP TO BOTTOM) we are on
                                label = (postSize) + " " + p;
                                yPos = (newPicture.Height - (columnBytes[p] + j)) - 1;
                                newTex.SetPixel(i, yPos, playPal.colors[columnBytes[p + j + 3]]);
                            }
                            postSize = columnBytes[p + 1] + 4;
                        }
                    }
                }
            }
            // newTex.SetPixel(i, j, new Color(columnBytes[i], columnBytes[i], columnBytes[i]));
            newTex.Apply();
            newTex.filterMode = FilterMode.Point;
            newTex.name = sprite.name;
            newWad.sprites.Add(newTex);
            
        }



    }


    void ReadWADFlats()
    {
        List<DrctEntry> flats = new List<DrctEntry>();
        flats = FindWADEntrys(type.Flat);

        foreach (DrctEntry flat in flats)
        {
            byte[] spriteBytes = new byte[flat.size]; //the entire picture resource in bytes

            //newFlat.pixels = new int[flat.size]; //new array the correct size of the flat


            wadOpener.Position = flat.filepos;
            wadOpener.Read(spriteBytes, 0, spriteBytes.Length);


            //new texture
            Texture2D newTex = new Texture2D((int)Mathf.Sqrt(flat.size), (int)Mathf.Sqrt(flat.size));

            Color[] texColors = new Color[(int)Mathf.Sqrt(flat.size) * (int)Mathf.Sqrt(flat.size)];

            for (int q = 0; q < texColors.Length; q++)
            {
                // texColors[q] = playPal.colors[newFlat.pixels[q]];
                texColors[q] = playPal.colors[spriteBytes[q]];
            }

            newTex.SetPixels(texColors);
            newTex.Apply();
            newTex.name = flat.name;
            newTex.filterMode = FilterMode.Point;
            Material newMat = new Material(DoomShader);
            newMat.mainTexture = newTex;
            newMat.SetFloat("_Mode", 1);

            newWad.flats.Add(flat.name, newMat);


        }

    }

    List<DrctEntry> ReadWADPnames()
    {
        DrctEntry pnames = new DrctEntry();

        int patchCount = 0;
        List<string> newPatches = new List<string>();


        foreach (DrctEntry entry in newWad.directory)//Find PNAMES
        {
            if (entry.name.Contains("PNAMES"))
            {
                pnames = entry;
                break;
            }
        }

        byte[] pbytes = new byte[pnames.size];



        wadOpener.Position = pnames.filepos;
        wadOpener.Read(pbytes, 0, pbytes.Length);

        patchCount = (int)BitConverter.ToUInt32(pbytes, 0); //number of patches

        for (int j = 4; j < pnames.size; j += 8)
        {
            string str1 = new String(System.Text.Encoding.ASCII.GetChars(pbytes, j, 8));
            str1 = str1.Replace("\0", "");

            if (!newPatches.Contains(str1))
            {
                newPatches.Add(str1); //fill the list with all patch names
            }


        }

        newWad.pnames = newPatches; //save info in wad class

        List<DrctEntry> patchEntries = new List<DrctEntry>();

        foreach (string str in newPatches)//Find PNAMES
        {
            foreach (DrctEntry entry in newWad.directory)
            {
                if (entry.name.ToUpper() == str.ToUpper())
                {

                    patchEntries.Add(entry);
                    break;
                }
            }
        }
        return patchEntries;
    }


    void ReadWADPatches()
    {
        List<DrctEntry> patches = ReadWADPnames();


        foreach (DrctEntry patch in patches)
        {
            PICTURES newPicture = new PICTURES();

            byte[] spriteBytes = new byte[patch.size]; //the entire picture resource in bytes


            wadOpener.Position = patch.filepos; //FAK U
            wadOpener.Read(spriteBytes, 0, spriteBytes.Length);

            //read the picture header (first 8 bytes of the picture resource) [0-7]
            newPicture.Width = BitConverter.ToInt16(spriteBytes, 0);
            newPicture.Height = BitConverter.ToInt16(spriteBytes, 2);
            newPicture.LeftOffset = BitConverter.ToInt16(spriteBytes, 4);
            newPicture.TopOffset = BitConverter.ToInt16(spriteBytes, 6);

            int[] pointers = new int[spriteBytes.Length];


            //Reading pointer data (4 bytes each, as many as the width skipping the header)
            for (int i = 8; i < 8 + newPicture.Width * 4; i += 4)
            {
                pointers[(i - 8) / 4] = BitConverter.ToInt32(spriteBytes, i);
            }

            Array.Resize<int>(ref pointers, pointers.Length);


            //Create a new texture fill it with CLEAR PIXELS
            Texture2D newTex = new Texture2D(newPicture.Width, newPicture.Height);
            Color32[,] newPixels = new Color32[newPicture.Width, newPicture.Height];


            Color[] texColors = new Color[newPicture.Width * newPicture.Height];
            for (int q = 0; q < texColors.Length; q++)
            {
                texColors[q] = Color.clear;
            }

            newTex.SetPixels(texColors);

            /*
                Column Bytes:
                first byte = the pixels from the top of the graphic that we start drawing at
                second byte = the amount of pixels that are drawn from the fist bytes location going DOWNWARD
                3rd+ byte = color data for each pixel directing it toward a palette 1st and last are NOT USED.
                FF byte = end of column 
            */

            for (int i = 0; i < newPicture.Width; i++) // (for each column)
            {

                int colPos = pointers[i]; //Position from start of picture to column position
                int colSize = 0; //Size of entire column in bytes

                for (int l = pointers[i]; l < spriteBytes.Length; l++) //Get the length of each column
                {
                    if (spriteBytes[l] == 255)
                    {
                        colSize = l - pointers[i];
                        break;
                    }
                }

                byte[] columnBytes = new byte[colSize]; //create columnBytes to store data bout the column

                for (int j = 0; j < columnBytes.Length; j++) //Filling columnBytes with data about the current column
                {
                    columnBytes[j] = spriteBytes[colPos + j];
                }

                if (columnBytes.Length > 0)
                {
                    int postSize = columnBytes[1] + 4;

                    for (int p = 0; p < columnBytes.Length; p += postSize) //for each post (need correct post size and amount of posts or ERROR)
                    {

                        if (p < columnBytes.Length)
                        {


                            for (int j = 0; j < columnBytes[p + 1]; j++) //FOR EACH PIXEL
                            {

                                int yPos; //the position of the pixel from the bottom of the image. (extrapolated from the top. and whatnot..)
                                          //J signifies which pixel (from TOP TO BOTTOM) we are on

                                label = (postSize) + " " + p;

                                yPos = (newPicture.Height - (columnBytes[p] + j)) - 1;

                                //newTex.SetPixel(i, yPos, );
                                newPixels[i, yPos] = playPal.colors[columnBytes[p + j + 3]];
                            }

                            postSize = columnBytes[p + 1] + 4;

                        }


                    }
                }

            }

            newWad.patches.Add(newPixels);
        }
    }

    void ReadWADTextures()
    {
        List<DrctEntry> texLumps = new List<DrctEntry>();


        foreach (DrctEntry entry in newWad.directory)//Find texture's lumps
        {
            if (entry.name.StartsWith("TEXTURE"))
            {
                texLumps.Add(entry);
            }
        }

        foreach (DrctEntry texEntry in texLumps)
        {
            TEXTUREx newTexture = new TEXTUREx();
            int texOfs = 0;

            byte[] tbytes = new byte[texEntry.size];


            wadOpener.Position = texEntry.filepos;
            wadOpener.Read(tbytes, 0, tbytes.Length);



            //Header////
            newTexture.numTextures = BitConverter.ToInt32(tbytes, 0);

            for (int i = 4; i <= (4 * newTexture.numTextures); i += 4)
            {
                newTexture.offset.Add(BitConverter.ToInt32(tbytes, i));
            }

            texOfs = (newTexture.numTextures * 4) + 4; //start of maptexture_t?????


            int lastPatchCount = 0;

            //for every texture
            foreach (int ofs in newTexture.offset)
            {
                MapTexture mtex = new MapTexture();

                //read the info
                mtex.name = new String(System.Text.Encoding.ASCII.GetChars(tbytes, ofs, 8));
                mtex.masked = BitConverter.ToBoolean(tbytes, ofs + 8);
                mtex.width = BitConverter.ToInt16(tbytes, ofs + 12);
                mtex.height = BitConverter.ToInt16(tbytes, ofs + 14);
                //mtex.columndirectory = BitConverter.ToInt32(tbytes, ofs + 16);
                mtex.patchCount = BitConverter.ToInt16(tbytes, ofs + 20);
                lastPatchCount = mtex.patchCount;

                mtex.name = mtex.name.Replace("\0", "");


                for (int a = ofs + 22; a < ofs + 22 + (10 * mtex.patchCount); a += 10)
                {
                    MapPatch mPatch = new MapPatch();

                    mPatch.originx = (int)BitConverter.ToInt16(tbytes, a);
                    mPatch.originy = (int)BitConverter.ToInt16(tbytes, a + 2);
                    mPatch.patch = (int)BitConverter.ToInt16(tbytes, a + 4);
                    mtex.mPatch.Add(mPatch);
                }

                newTexture.mtex.Add(mtex);//store the info in newTexture.mtex

                List<Texture2D> texPatches = new List<Texture2D>();

                Color32[,] texPixels = new Color32[mtex.width, mtex.height]; //store the pixels for the texture (L->R, U->D)

                foreach (MapPatch mpatch in mtex.mPatch) //for each patch in the texture
                {
                    int width = newWad.patches[mpatch.patch].GetLength(0);
                    int height = newWad.patches[mpatch.patch].GetLength(1);

                    for (int x = 0; x < width; x++) //for each width pixel
                    {
                        for (int y = 0; y < height; y++) //for each height pixel
                        {
                            Color pixel = newWad.patches[mpatch.patch][x, (height - 1) - y];
                            int xofs = x + mpatch.originx;
                            int yofs = y + mpatch.originy;

                            if (pixel.a > 0 && xofs >= 0 && yofs >= 0 && xofs < mtex.width && yofs < mtex.height)
                            {
                                texPixels[xofs, yofs] = pixel;
                            }

                        }
                    }

                }

                Color[] texPix = new Color[mtex.width * mtex.height];
                Material newMat = new Material(DoomShader);

                for (int i = 0; i < mtex.height; i++)
                {
                    for (int j = 0; j < mtex.width; j++)
                    {
                        // Debug.Log(i * mtex.height + j);
                        texPix[i * mtex.width + j] = texPixels[j, i]; //collapse the 2Darry "texPixels" to a 1D array "texPix"
                    }
                }
                Texture2D newTex = new Texture2D(mtex.width, mtex.height);
                newTex.filterMode = FilterMode.Point;
                newTex.SetPixels(texPix);
                newTex.Apply();
                newTex.name = mtex.name;
                newMat.mainTexture = newTex;
                newWad.textures.Add(newTex.name, newMat);
            }
        }
        
    }


    List<DrctEntry> FindWADEntrys(type find) //Possible Types: sprite, flat //returns number of found entries
    {
        List<DrctEntry> entryList = new List<DrctEntry>();

        DrctEntry markerStart = new DrctEntry();
        DrctEntry markerEnd = new DrctEntry();

        entryList.Clear(); //make sure the list of entries is empty

        if (find == type.Sprite)
        {

            //Look for sprites
            for (int i = 0; i < newWad.directory.Count; i++)
            {
                if (newWad.directory[i].name.Contains("S_START") && newWad.directory[i].size == 0) //The sprite marker
                {
                    markerStart = newWad.directory[i];
                    markerStart.filepos = i;
                }

                if (newWad.directory[i].name.Contains("S_END") && newWad.directory[i].size == 0)
                {
                    markerEnd = newWad.directory[i];
                    markerEnd.filepos = i;
                }
            }
        }

        if (find == type.Flat)
        {

            //Look for flats
            for (int i = 0; i < newWad.directory.Count; i++)
            {
                if (newWad.directory[i].name.Contains("F_START") && newWad.directory[i].size == 0) //The flat marker
                {
                    markerStart = newWad.directory[i];
                    markerStart.filepos = i;
                }

                if (newWad.directory[i].name.Contains("F_END") && newWad.directory[i].size == 0)
                {
                    markerEnd = newWad.directory[i];
                    markerEnd.filepos = i;
                }
            }
        }

        for (int i = 0; i < newWad.directory.Count; i++)
        {
            if (i > markerStart.filepos && i < markerEnd.filepos && newWad.directory[i].size > 0)
            {
                //all of the entries between the markers
                entryList.Add(newWad.directory[i]);

            }

        }
        return entryList;
    }


    public MUS ReadMusEntry(int songNumber)
    {
        List<DrctEntry> musList = new List<DrctEntry>();

        foreach (DrctEntry entry in newWad.directory) //find all the mus's in the wad
        {
            if (entry.name.StartsWith("D_"))
            {
                musList.Add(entry);
            }
        }

        DrctEntry mus = musList[songNumber]; //which song are we listening to today

        
        byte[] musBytes = new byte[mus.size];

        wadOpener.Position = mus.filepos;
        wadOpener.Read(musBytes, 0, musBytes.Length);

        MUS newMUS = new MUS();
        byte[] channelVolume = new byte[16];

        newMUS.name = mus.name;

        //read the header of the mus file
        newMUS.id = new String(System.Text.Encoding.ASCII.GetChars(musBytes, 0, 3));
        newMUS.scoreLen = BitConverter.ToUInt16(musBytes, 4);
        newMUS.scoreStart = BitConverter.ToUInt16(musBytes, 6);
        newMUS.channels = BitConverter.ToUInt16(musBytes, 8);
        newMUS.sec_channels = BitConverter.ToUInt16(musBytes, 10);
        newMUS.instrCnt = BitConverter.ToUInt16(musBytes, 12);
        newMUS.dummy = BitConverter.ToUInt16(musBytes, 14);

        List<int> instrList = new List<int>();//temporary list for storing instruments

        for (int j = 16; j < newMUS.scoreStart; j += 2)
            instrList.Add(BitConverter.ToUInt16(musBytes, j));//get the instrument

        newMUS.instruments = instrList.ToArray();//store the temporary list to the newMUS list

        int i = newMUS.scoreStart;
        while (i < musBytes.Length)
        {
            //read the 'score'
            Mus_Event newEvent = new Mus_Event();


            newEvent.channelNum = (byte)(musBytes[i] & 0x0F);
            newEvent.musEventType = (byte)((musBytes[i] & 0x70) >> 4);
            newEvent.last = (((musBytes[i] & 0x80) >> 7) == 1);
            i++; //finished reading the descriptor byte

            if (newEvent.musEventType == 0)//Release Note
            {
                newEvent.note = (byte)(musBytes[i] & 0x7F);
                i++; //finished reading the released note byte
            }
            else if (newEvent.musEventType == 1)//Play Note
            {
                bool volBit = ((musBytes[i] & 0x80) >> 7) == 1;
                newEvent.note = (byte)(musBytes[i] & 0x7F);
                i++; //finished reading the note byte

                if (volBit) //if the last bit of the first byte is set, get the new vol from the 2nd byte
                {
                    newEvent.vol = (byte)(musBytes[i] & 0x7F);
                    channelVolume[newEvent.channelNum] = newEvent.vol;
                    i++; //finished reading the vol byte
                }
                else //otherwise use the volume of the previous note on the channel is used. 
                {
                    newEvent.vol = channelVolume[newEvent.channelNum];
                }
            }
            else if (newEvent.musEventType == 2)//Pitch Wheel
            {
                newEvent.pitch = (byte)(musBytes[i]);
                i++;//finished reading pitch wheel byte
            }
            else if (newEvent.musEventType == 3)//System Event
            {
                newEvent.sysEventNum = (byte)(musBytes[i] & 0x7F);
                i++;//finished reading sys event byte
            }
            else if (newEvent.musEventType == 4)//Change Controller
            {
                newEvent.contNum = (byte)(musBytes[i] & 0x7F);
                i++;//finished reading controller number byte
                newEvent.contVal = (byte)(musBytes[i] & 0x7F);
                i++;//finished reading controller value byte
            }
            else if (newEvent.musEventType == 5 || newEvent.musEventType == 7)
            {
                i++;//???????extra byte??????????
            }
            else if (newEvent.musEventType == 6)//Score end (end of mus)
            {
                //END OF FILE EVENT 
                newEvent.scoreEnd = true; //set the events scoreEnd bool to true
                newEvent.time = newMUS.musEvents[newMUS.musEvents.Count - 1].time; // set the end time to the last event's for some reason?
                newMUS.musEvents.Add(newEvent); //save the event
                newWad.music.Add(newMUS);//save the MUS
                break;
            }

            if (newEvent.last) //delay
            {
                int delay = 0;

                bool moreDelay = true; //if there is another delay byte after the current byte

                while (moreDelay)
                {
                    moreDelay = (((BitConverter.ToChar(musBytes, i) & 0x80) >> 7) == 1); //set moreDelay by checking the LAST bit of the CURRENT BYTE

                    byte newByte = musBytes[i];
                    delay = delay * 128 + (newByte & 0x7F); //set the delay              

                    i++; //next byte

                }

                newEvent.time = delay;//set the new time info
            }

            newMUS.musEvents.Add(newEvent);//save the event information

        }
        return newMUS;
        //newWad.music.Add(newMUS);
    }




    PLAYPAL ReadColorPalette(string filePath, string name)
    {
        PLAYPAL pal = new PLAYPAL();
        foreach (DrctEntry entry in newWad.directory)
        {
            if (entry.name.Contains(name))
            {
                //we can assume this is the entry we are looking for 
                byte[] temp = new byte[768];


                wadOpener.Position = entry.filepos; //set the location to the beginning of a column
                wadOpener.Read(temp, 0, temp.Length); //fill columnBytes with bytes for the column

                //Debug.Log("Name: " + entry.name);
                //Debug.Log("Size: " + entry.size);
                //Debug.Log("Position: " + entry.filepos);

                for (int i = 0; i < 768; i += 3)
                {
                    pal.colors.Add(new Color32(temp[i], temp[i + 1], temp[i + 2], 255));
                }




            }
        }

        return pal;

    }
}
