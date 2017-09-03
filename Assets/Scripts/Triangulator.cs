using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


namespace DoomTriangulator
{
    public class Triangulator : MonoBehaviour
    {
        public enum WallType { Upper, Middle, Lower };
        public enum GeneratingGo { Sector, Ceiling, Floor };

        // This class is the actual triangulator, you feed it a list of linedefs and it spits out triangles
        
        public static SubMesh CreateFloor(SECTORS sector, Material mat, GeneratingGo generating)
        {
            if (sector.isMovingCeiling && generating == GeneratingGo.Ceiling)
                return null;

            if (sector.isMovingFloor && generating != GeneratingGo.Floor)
                return null;

            Mesh mesh = new Mesh();
            VertexPool vertexPool = new VertexPool();
            List<Triangle2D> triangles = new List<Triangle2D>();
            List<Line> lines = new List<Line>();

            // convert linedefs to lines
            foreach (LINEDEFS linedef in sector.lines)
            {
                Vertex v1 = vertexPool.Get(linedef.firstVert.x, linedef.firstVert.z);
                Vertex v2 = vertexPool.Get(linedef.lastVert.x, linedef.lastVert.z);

                if (linedef.getFrontSector() == sector)
                    lines.Add(new Line(v1, v2, true, false));
                else
                    lines.Add(new Line(v1, v2, true, true));

            }

            // walk lines to make sure the sector isn't broken
            RepairSector(sector, lines);
            
            // go through every line and attach it to every other vertex.
            // reject the triangle if it isn't valid
            List<Line> unmarked = new List<Line>(lines);

            // ignore all initial lines that do not come from a linedef
            for (int i = 0; i < unmarked.Count; i++)
            {
                Line line = unmarked[i];
                if (!line.fromLinedef)
                {
                    unmarked.RemoveAt(i);
                    i--;
                }
            }

            while (unmarked.Count > 0)
            {
                Line line = unmarked.Last();
                unmarked.Remove(line);
                CreateTriangle(line, lines, unmarked, triangles, vertexPool.vertices);
            }

            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();
            foreach (Vertex vertex in vertexPool.vertices)
            {
                vertices.Add(new Vector3((float)vertex.x, sector.floorHeight, (float)vertex.y));
                uvs.Add(new Vector2((float)vertex.x / 64f, (float)vertex.y / 64f));

                normals.Add(Vector3.up);

            }

            List<int> triangleIndices = new List<int>();
            foreach (Triangle2D triangle in triangles)
            {
                double mx = (triangle.vertices[0].x + triangle.vertices[1].x + triangle.vertices[2].x) / 3f;
                double my = (triangle.vertices[0].y + triangle.vertices[1].y + triangle.vertices[2].y) / 3f;
                SortedList<double, Vertex> sorted = new SortedList<double, Vertex>();
                foreach (Vertex vertex in triangle.vertices)
                {
                    double theta = Math.Atan2(my - vertex.y, mx - vertex.x);
                    //while (sorted.ContainsKey(theta)) { theta += 0.1; } // maybe needed?
                    sorted.Add(theta, vertex);
                }
                triangleIndices.Add(vertexPool.vertices.IndexOf(sorted.Values[2]));
                triangleIndices.Add(vertexPool.vertices.IndexOf(sorted.Values[1]));
                triangleIndices.Add(vertexPool.vertices.IndexOf(sorted.Values[0]));

            }

            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.triangles = triangleIndices.ToArray();
            mesh.normals = normals.ToArray();

            SubMesh newSubMesh = new SubMesh();
            newSubMesh.mesh = mesh;
            newSubMesh.material = mat;

            return newSubMesh;

        }

        static void RepairSector(SECTORS sector, List<Line> lines)
        {
            if (lines.Count < 2)
            {
                // sector with a single line, don't repair
                return;
            }

            // generate a list of all vertices
            List<Vertex> vertices = new List<Vertex>();
            foreach(Line line in lines)
            {
                if (!vertices.Contains(line.vertices[0]))
                    vertices.Add(line.vertices[0]);

                if (!vertices.Contains(line.vertices[1]))
                    vertices.Add(line.vertices[1]);
            }

            // find all vertices that only have one line
            List<Vertex> loneVertices = new List<Vertex>();
            foreach(Vertex vertex in vertices)
            {
                int count = 0;

                foreach (Line line in lines)
                {
                    if (line.vertices.Contains(vertex))
                        count++;
                }

                if (count < 2)
                    loneVertices.Add(vertex);
            }

            if (loneVertices.Count == 0)
            {
                // nothing wrong here
                return;
            }

            Debug.Log("BAD SECTOR: " + sector.sectorIndex);

            // find lines that intersect
            foreach (Line line1 in lines)
            {
                foreach (Line line2 in lines)
                {
                    if (line1 == line2)
                        continue;

                    Vertex intersect = line1.Intersection(line2);
                    if (intersect == null)
                        continue;

                    Debug.Log("REPAIRING: intersection, SECTOR: " + sector.sectorIndex);

                    // find closest vertex to intersection point
                    Vertex closestVertex = line1.vertices[0];
                    foreach (Vertex vertex in line1.vertices)
                    {
                        if (vertex.DistanceTo(intersect) < closestVertex.DistanceTo(intersect))
                            closestVertex = vertex;
                    }
                    foreach (Vertex vertex in line2.vertices)
                    {
                        if (vertex.DistanceTo(intersect) < closestVertex.DistanceTo(intersect))
                            closestVertex = vertex;
                    }

                    // move closest to intersection point
                    closestVertex.x = intersect.x;
                    closestVertex.y = intersect.y;

                    // replace closest line1 vertex with closest vertex
                    if (line1.vertices[0].DistanceTo(intersect) < line1.vertices[1].DistanceTo(intersect))
                    {
                        line1.vertices[0] = closestVertex;
                        loneVertices.Remove(line1.vertices[0]);
                    }
                    else
                    {
                        line1.vertices[1] = closestVertex;
                        loneVertices.Remove(line1.vertices[1]);
                    }

                    // replace closest line2 vertex with closest vertex
                    if (line2.vertices[0].DistanceTo(intersect) < line2.vertices[1].DistanceTo(intersect))
                    {
                        line2.vertices[0] = closestVertex;
                        loneVertices.Remove(line2.vertices[0]);
                    }
                    else
                    {
                        line2.vertices[1] = closestVertex;
                        loneVertices.Remove(line2.vertices[1]);
                    }
                }
            }

            if (loneVertices.Count == 0)
            {
                // sector was repaired
                return;
            }
            
            // generate every combination of vertices in pairs
            SortedList<double, VertexPair> pairs = new SortedList<double, VertexPair>();
            foreach(Vertex v1 in loneVertices)
            {
                foreach (Vertex v2 in loneVertices)
                {
                    if (v1 == v2)
                        continue;

                    // make sure pair is unique
                    foreach(VertexPair pair in pairs.Values)
                    {
                        if (pair.vertices.Contains(v1) && pair.vertices.Contains(v2))
                        {
                            goto nextPair;
                        }
                    }

                    VertexPair vertexPair = new VertexPair(v1, v2);
                    double dist = vertexPair.Distance();
                    while (pairs.ContainsKey(dist)) { dist += 0.001; }
                    pairs.Add(dist, vertexPair);

                    nextPair:;
                }
            }

            // generate a line for every pair, sorted by distance
            while (pairs.Count > 0)
            {
                // grab a pair with the least distance between them
                VertexPair pair = pairs.Values.First();
                pairs.RemoveAt(0);

                // generate the line between the two vertices
                Line line = new Line(pair.vertices[0], pair.vertices[1], false, false);

                // check for collision
                foreach (Line line2 in lines)
                {
                    if (line.Intersects(line2))
                    {
                        goto nextPair;
                    }
                }

                // add the line to the list
                lines.Add(line);
                Debug.Log("REPAIRING: missing line, SECTOR: " + sector.sectorIndex);
                Debug.DrawLine(new Vector3((float)pair.vertices[0].x, 0, (float)pair.vertices[0].y), new Vector3((float)pair.vertices[1].x, 0, (float)pair.vertices[1].y), Color.red, 10000);

                // remove any pairs that contain a shared vertex to this one
                for (int i = 0; i < pairs.Count; i++)
                {
                    if (pairs.Values[i].vertices.Contains(pair.vertices[0]) || pairs.Values[i].vertices.Contains(pair.vertices[1]))
                    {
                        pairs.RemoveAt(i);
                        i--;
                        continue;
                    }
                }

                nextPair:;
            }
        }

        class VertexPair
        {
            public Vertex[] vertices = new Vertex[2];
            public VertexPair(Vertex a, Vertex b)
            {
                vertices[0] = a;
                vertices[1] = b;
            }

            public double Distance()
            {
                return vertices[0].DistanceTo(vertices[1]);
            }
        }

        public static SubMesh CreateCeiling(SubMesh floor, SECTORS sector, Material mat, GeneratingGo generating)
        {
            if (sector.isMovingFloor && generating == GeneratingGo.Floor)
                return null;

            if (sector.isMovingFloor && generating != GeneratingGo.Floor)
                floor = CreateFloor(sector, mat, GeneratingGo.Floor);

            if (sector.isMovingCeiling && generating != GeneratingGo.Ceiling)
                return null;

            if (sector.isMovingCeiling && generating == GeneratingGo.Ceiling)
                floor = CreateFloor(sector, mat, sector.isMovingFloor ? GeneratingGo.Floor : GeneratingGo.Sector);

            //reverse
            Mesh ceiling = new Mesh();

            List<Vector3> tmpVerts = new List<Vector3>(floor.mesh.vertices);
            List<Vector2> tmpUvs = new List<Vector2>(floor.mesh.uv);
            List<int> tmpTris = new List<int>(floor.mesh.triangles);
            List<Vector3> tmpNrm = new List<Vector3>(floor.mesh.normals);


            for (int a = 0; a < tmpVerts.Count; a++)
            {
                tmpVerts[a] = new Vector3(tmpVerts[a].x, sector.ceilingHeight, tmpVerts[a].z); //change height for ceiling
                tmpNrm[a] = Vector3.down;
            }


            for (int i = 0; i < tmpTris.Count; i += 3)
            {
                int tri = tmpTris[i];

                tmpTris[i] = tmpTris[i + 2];
                tmpTris[i + 2] = tri;
            }


            ceiling.vertices = tmpVerts.ToArray();
            ceiling.uv = tmpUvs.ToArray();
            ceiling.triangles = tmpTris.ToArray();
            ceiling.normals = tmpNrm.ToArray();



            SubMesh newSubMesh = new SubMesh();
            newSubMesh.mesh = ceiling;
            newSubMesh.material = mat;


            return newSubMesh;
        }

        public static List<SubMesh> CreateWalls(SECTORS sector, WAD wad, GeneratingGo generating)
        {
            List<SubMesh> sMeshs = new List<SubMesh>();

            foreach (LINEDEFS line in sector.lines)
            {
                SECTORS fSector = line.getFrontSector();
                SECTORS bSector = line.getBackSector();

                // check to see if this line is totally broken
                if (fSector == null && bSector == null)
                    continue;

                // figure out which sector this wall actually belongs to

                bool hasBothSectors = (fSector != null && bSector != null);
                bool genMidWalls = (generating == GeneratingGo.Sector);
                bool genLowerWalls = hasBothSectors;
                bool genUpperWalls = hasBothSectors;

                if(hasBothSectors)
                {
                    if (sector.isMovingCeiling)
                    {
                        genLowerWalls = genLowerWalls ? (generating != GeneratingGo.Ceiling) : false;
                        genUpperWalls = genUpperWalls ? (generating == GeneratingGo.Ceiling) : false;
                    }

                    if (sector.isMovingFloor)
                    {
                        genLowerWalls = genLowerWalls ? (generating == GeneratingGo.Floor) : false;
                        genUpperWalls = genUpperWalls ? (generating != GeneratingGo.Floor) : false;
                    }
                }

                // create all walls
                if (genMidWalls)
                    sMeshs.AddRange(CreateMidWalls(sector, line, wad));

                if (genLowerWalls)
                    sMeshs.AddRange(CreateLowerWalls(sector, line, wad));

                if (genUpperWalls)
                    sMeshs.AddRange(CreateUpperWalls(sector, line, wad));
            }

            return sMeshs;
        }
        
        public static SubMesh CreateWall(SECTORS sector, LINEDEFS line, Material texture, float startHeight, float endHeight, bool flipped, WallType wallType)
        {
            List<Vector3> tmpVerts = new List<Vector3>();
            List<Vector2> tmpUv = new List<Vector2>();

            SubMesh sMesh = new SubMesh();
            sMesh.mesh = new Mesh();

            sMesh.mesh.vertices = new Vector3[4];
            sMesh.mesh.uv = new Vector2[4];

            // create the vertices
            tmpVerts.Add(new Vector3(line.firstVert.x, startHeight, line.firstVert.z));
            tmpVerts.Add(new Vector3(line.firstVert.x, endHeight, line.firstVert.z));
            tmpVerts.Add(new Vector3(line.lastVert.x, endHeight, line.lastVert.z));
            tmpVerts.Add(new Vector3(line.lastVert.x, startHeight, line.lastVert.z));

            // remember things for uv generation
            SIDEDEFS side = flipped ? line.side2 : line.side1;
            float xOffset = side.xOfs;
            float yOffset = side.yOfs;
            float textureWidth = texture.mainTexture.width;
            float textureHeight = texture.mainTexture.height;
            float wallHeight = endHeight - startHeight;
            float wallWidth = line.getLength();

            // check upper and lower unpegged flags
            bool lowerUnpegged = (wallType == WallType.Upper) ? ((line.flags & 0x0008) == 0) : ((line.flags & 0x0010) != 0);

            // generate UVs
            if (lowerUnpegged)
            {
                if (wallType == WallType.Lower)
                {
                    float sectorHeight = sector.ceilingHeight - sector.floorHeight;
                    tmpUv.Add(new Vector2(xOffset / textureWidth, (sectorHeight + yOffset) / textureHeight));
                    tmpUv.Add(new Vector2(xOffset / textureWidth, (sectorHeight - wallHeight + yOffset) / textureHeight));
                    tmpUv.Add(new Vector2((wallWidth + xOffset) / textureWidth, (sectorHeight - wallHeight + yOffset) / textureHeight));
                    tmpUv.Add(new Vector2((wallWidth + xOffset) / textureWidth, (sectorHeight + yOffset) / textureHeight));
                }
                else
                {
                    tmpUv.Add(new Vector2(xOffset / textureWidth, (textureHeight + yOffset) / textureHeight));
                    tmpUv.Add(new Vector2(xOffset / textureWidth, (textureHeight - wallHeight + yOffset) / textureHeight));
                    tmpUv.Add(new Vector2((wallWidth + xOffset) / textureWidth, (textureHeight - wallHeight + yOffset) / textureHeight));
                    tmpUv.Add(new Vector2((wallWidth + xOffset) / textureWidth, (textureHeight + yOffset) / textureHeight));
                }
            }
            else
            {
                tmpUv.Add(new Vector2(xOffset / textureWidth, (wallHeight + yOffset) / textureHeight));
                tmpUv.Add(new Vector2(xOffset / textureWidth, yOffset / textureHeight));
                tmpUv.Add(new Vector2((wallWidth + xOffset) / textureWidth, yOffset / textureHeight));
                tmpUv.Add(new Vector2((wallWidth + xOffset) / textureWidth, (wallHeight + yOffset) / textureHeight));
            }

            // set mesh data
            sMesh.mesh.uv = tmpUv.ToArray();
            sMesh.mesh.vertices = tmpVerts.ToArray();
            sMesh.material = texture;

            if (flipped)
            {
                sMesh.mesh.triangles = new int[6] { 2, 1, 0, 0, 3, 2 };
                sMesh.mesh.normals = new Vector3[4] { -line.frontVector(), -line.frontVector(), -line.frontVector(), -line.frontVector() };
            }
            else
            {
                sMesh.mesh.triangles = new int[6] { 0, 1, 2, 2, 3, 0 };
                sMesh.mesh.normals = new Vector3[4] { line.frontVector(), line.frontVector(), line.frontVector(), line.frontVector() };
            }

            return sMesh;
        }

        public static List<SubMesh> CreateMidWalls(SECTORS sector, LINEDEFS line, WAD wad)            // midtex
        {
            float startHeight = 0;
            float endHeight = 0;

            // figure out start and end heights
            if (line.getFrontSector() != null && line.getBackSector() != null)
            {
                startHeight = Math.Max(line.getFrontSector().floorHeight, line.getBackSector().floorHeight);
                endHeight = Math.Min(line.getFrontSector().ceilingHeight, line.getBackSector().ceilingHeight);
            }
            else if(line.getFrontSector() != null && line.getBackSector() == null)
            {
                SECTORS fSector = line.getFrontSector();
                startHeight = fSector.floorHeight;
                endHeight = fSector.ceilingHeight;

                if (fSector.isMovingCeiling)
                    endHeight = fSector.ceilingBounds[1];

                if (fSector.isMovingFloor)
                {
                    startHeight = Math.Min(startHeight, fSector.floorBounds[0]);
                    endHeight = Math.Max(endHeight, fSector.floorBounds[1]);
                }
            }
            else if (line.getFrontSector() == null && line.getBackSector() != null)
            {
                startHeight = line.getBackSector().floorHeight;
                endHeight = line.getBackSector().ceilingHeight;
            }

            // generate a wall for each textured side
            List<SubMesh> walls = new List<SubMesh>();
            if (line.getFrontSector() == sector && line.side1 != null && wad.textures.ContainsKey(line.side1.midTex))
            {
                Material texture = wad.textures[line.side1.midTex];

                if (line.getFrontSector() != null && line.getBackSector() != null)
                    endHeight = Math.Min(startHeight + texture.mainTexture.height, endHeight);  //dont tile middle textures with 2 sides

                walls.Add(CreateWall(line.getFrontSector(), line, texture, startHeight, endHeight, false, WallType.Middle));
            }

            if (line.getBackSector() == sector && line.side2 != null && wad.textures.ContainsKey(line.side2.midTex))
            {
                Material texture = wad.textures[line.side2.midTex];

                if (line.getFrontSector() != null && line.getBackSector() != null)
                    endHeight = Math.Min(startHeight + texture.mainTexture.height, endHeight);  //dont tile middle textures with 2 sides

                walls.Add(CreateWall(line.getBackSector(), line, texture, startHeight, endHeight, true, WallType.Middle));
            }

            // generate skybox hole textures
            if (line.getFrontSector() == sector && line.side1 != null && !wad.textures.ContainsKey(line.side1.midTex) && sector.ceilingFlat.StartsWith("F_SKY"))
            {
                SECTORS bSector = line.getBackSector();
                if (bSector == null || (bSector.floorHeight == bSector.ceilingHeight && !bSector.isMovingCeiling && !bSector.isMovingFloor))
                {
                    Material texture = wad.flats[sector.ceilingFlat];
                    startHeight = line.getFrontSector().floorHeight;
                    endHeight = line.getFrontSector().ceilingHeight;
                    walls.Add(CreateWall(line.getFrontSector(), line, texture, startHeight, endHeight, false, WallType.Middle));
                }
            }

            return walls;
        }

        public static List<SubMesh> CreateLowerWalls(SECTORS sector, LINEDEFS line, WAD wad)            // lowtex
        {
            List<SubMesh> walls = new List<SubMesh>();

            // figure out start and end heights
            SECTORS otherSector = (sector == line.getFrontSector()) ? line.getBackSector() : line.getFrontSector();
            float startHeight = Math.Min(sector.floorHeight + sector.floorBounds[0] - sector.floorBounds[1], otherSector.floorHeight + otherSector.floorBounds[0] - otherSector.floorBounds[1]);
            float endHeight = sector.floorHeight;

            // generate a wall for each textured side
            if (line.getBackSector() == sector && line.side1 != null && wad.textures.ContainsKey(line.side1.lowTex))
                walls.Add(CreateWall(line.getFrontSector(), line, wad.textures[line.side1.lowTex], startHeight, endHeight, false, WallType.Lower));

            if (line.getFrontSector() == sector && line.side2 != null && wad.textures.ContainsKey(line.side2.lowTex))
                walls.Add(CreateWall(line.getBackSector(), line, wad.textures[line.side2.lowTex], startHeight, endHeight, true, WallType.Lower));

            return walls;
        }

        public static List<SubMesh> CreateUpperWalls(SECTORS sector, LINEDEFS line, WAD wad)            // uppertex
        {
            List<SubMesh> walls = new List<SubMesh>();

            SECTORS otherSector = (sector == line.getFrontSector()) ? line.getBackSector() : line.getFrontSector();

            // figure out how much extra height we have to add to a moving ceiling
            float[] addMovementHeight = new float[2];
            addMovementHeight[0] = Math.Min(Math.Min(sector.ceilingHeight, otherSector.ceilingHeight), sector.ceilingBounds[0]);
            addMovementHeight[1] = Math.Max(Math.Max(sector.ceilingHeight, otherSector.ceilingHeight), sector.ceilingBounds[1]);

            // figure out start and end heights
            float startHeight = sector.ceilingHeight;
            float endHeight = Math.Max(otherSector.ceilingHeight, sector.ceilingHeight + (addMovementHeight[1] - addMovementHeight[0]));

            // this is an ugly hack in order to get DOOM2 MAP31 to have correct doors:
            bool backSectorDoor = line.getBackSector() != null && line.getBackSector().isMovingCeiling;

            // generate a wall for each textured side
            if (line.getBackSector() == sector && line.side1 != null && wad.textures.ContainsKey(line.side1.upTex))
                walls.Add(CreateWall(line.getFrontSector(), line, wad.textures[line.side1.upTex], startHeight, endHeight, false, WallType.Upper));

            if (line.getFrontSector() == sector && line.side2 != null && wad.textures.ContainsKey(line.side2.upTex) && !backSectorDoor)
                walls.Add(CreateWall(line.getBackSector(), line, wad.textures[line.side2.upTex], startHeight, endHeight, true, WallType.Upper));

            // generate skybox hole textures
            if (line.getFrontSector() == sector && line.side1 != null && !wad.textures.ContainsKey(line.side1.upTex))
            {
                if (line.getFrontSector().ceilingFlat.StartsWith("F_SKY") && line.getBackSector().ceilingFlat.StartsWith("F_SKY"))
                {
                    Material texture = wad.flats[sector.ceilingFlat];
                    walls.Add(CreateWall(line.getFrontSector(), line, texture, startHeight, endHeight, false, WallType.Upper));
                }
            }

            return walls;
        }

        public static void CreateSkybox(GameObject skybox)
        {
            Mesh skymesh = skybox.GetComponent<MeshFilter>().mesh;
            int[] newtris = new int[skymesh.triangles.Length];

            for(int i = 0; i < skymesh.triangles.Length; i+=3)
            {
                newtris[i] = skymesh.triangles[i];
                newtris[i + 1] = skymesh.triangles[i + 2];
                newtris[i + 2] = skymesh.triangles[i + 1];
            }
            skymesh.triangles = newtris;
        }

        public static void CombineAsMesh(ref Mesh mesh, List<SubMesh> submeshes, ref Material[] materials)
        {
            mesh.Clear();

            List<SubMesh> combinedSubmeshes = new List<SubMesh>();

            // group every submesh by material
            Dictionary<Material, List<SubMesh>> submeshByMaterial = new Dictionary<Material, List<SubMesh>>();
            foreach(SubMesh submesh in submeshes)
            {
                if (!submeshByMaterial.ContainsKey(submesh.material))
                    submeshByMaterial.Add(submesh.material, new List<SubMesh> { submesh });
                else
                    submeshByMaterial[submesh.material].Add(submesh);
            }

            // combine grouped submeshes into one submesh
            foreach(List<SubMesh> listSubmeshes in submeshByMaterial.Values)
                combinedSubmeshes.Add(CombineAsSubMesh(listSubmeshes));

            List<Material> materialList = new List<Material>();
            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();
            
            // add info from each submesh into lists
            foreach (SubMesh submesh in combinedSubmeshes)
            {
                vertices.AddRange(submesh.mesh.vertices);
                uvs.AddRange(submesh.mesh.uv);
                normals.AddRange(submesh.mesh.normals);
                materialList.Add(submesh.material);
            }

            // convert lists to arrays and apply to mesh
            mesh.vertices = vertices.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.subMeshCount = combinedSubmeshes.Count();
            materials = materialList.ToArray();

            // set triangles correctly according to new vertex offsets
            int onMesh = 0;
            int vertexCount = 0;
            foreach (SubMesh submesh in combinedSubmeshes)
            {
                int[] triangles = new int[submesh.mesh.triangles.Count()];
                for (int i = 0; i < submesh.mesh.triangles.Count(); i++)
                {
                    triangles[i] = submesh.mesh.triangles[i] + vertexCount;
                }
                mesh.SetTriangles(triangles, onMesh);
                vertexCount += submesh.mesh.vertices.Count();
                onMesh++;
            }

        }

        public static SubMesh CombineAsSubMesh(List<SubMesh> submeshes)
        {
            SubMesh submesh = new SubMesh();
            submesh.material = submeshes.First().material;
            submesh.mesh = new Mesh();

            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();

            // add info from each submesh into lists
            foreach (SubMesh smesh in submeshes)
            {
                if (smesh.material != submesh.material)
                    throw new Exception("Combining submeshes with different materials isn't allowed");

                vertices.AddRange(smesh.mesh.vertices);
                uvs.AddRange(smesh.mesh.uv);
                normals.AddRange(smesh.mesh.normals);
            }

            // convert lists to arrays and apply to mesh
            submesh.mesh.vertices = vertices.ToArray();
            submesh.mesh.uv = uvs.ToArray();
            submesh.mesh.subMeshCount = submeshes.Count();

            // set triangles correctly according to new vertex offsets
            int onMesh = 0;
            int vertexCount = 0;
            foreach (SubMesh smesh in submeshes)
            {
                int[] triangles = new int[smesh.mesh.triangles.Count()];
                for (int i = 0; i < smesh.mesh.triangles.Count(); i++)
                {
                    triangles[i] = smesh.mesh.triangles[i] + vertexCount;
                }
                submesh.mesh.SetTriangles(triangles, onMesh);
                vertexCount += smesh.mesh.vertices.Count();
                onMesh++;
            }

            return submesh;
        }

        private static Line GetLine(List<Line> lines, Vertex v1, Vertex v2)
        {
            // grab a line if we've already created it
            foreach (Line line in lines)
            {
                if (line.vertices.Contains(v1) && line.vertices.Contains(v2))
                {
                    return line;
                }
            }

            // create a new line if we do not have it yet
            return new Line(v1, v2, false, false);
        }

        private static void CreateTriangle(Line line, List<Line> lines, List<Line> unmarked, List<Triangle2D> triangles, List<Vertex> vertices)
        {
            // TODO: sort vertices by least distance?
            foreach (Vertex vertex in vertices)
            {
                if (line.vertices.Contains(vertex))
                {
                    // do not try to create a third point based on a point we already have
                    goto continueSearch;
                }

                // grab or create the other two lines
                Line l2 = GetLine(lines, line.vertices[0], vertex);
                Line l3 = GetLine(lines, line.vertices[1], vertex);

                // create triangle
                Triangle2D triangle = new Triangle2D(line, l2, l3);

                // select a triangle for debugging
                bool debugTriangle = false;
                debugTriangle = triangle.Similar(new Vertex[] { new Vertex(2240, 3520), new Vertex(2304, 3200), new Vertex(2688, 3264) });
                if (debugTriangle)
                {
                    triangle = triangle;
                }

                // validate that it has an area
                if (!triangle.ValidArea())
                {
                    if (debugTriangle) { Debug.Log("InvalidArea " + triangle.DebugPrint()); }
                    goto continueSearch;
                }

                // validate normals
                if (!triangle.ValidNormals())
                {
                    if (debugTriangle) { Debug.Log("InvalidNormals " + triangle.DebugPrint()); }
                    goto continueSearch;
                }

                // validate uniqueness
                foreach (Triangle2D triangle2 in triangles)
                {
                    if (triangle.Identical(triangle2))
                    {
                        if (debugTriangle) { Debug.Log("IsntUnique " + triangle.DebugPrint()); }
                        goto continueSearch;
                    }
                }

                // validate that it doesn't cross any lines
                foreach (Line line2 in lines)
                {
                    if (triangle.vertices.Contains(line2.vertices[0]) && triangle.vertices.Contains(line2.vertices[1]))
                    {
                        continue;
                    }
                    if (triangle.lines.Contains(line2)) { continue; }

                    if (triangle.Intersects(line2))
                    {
                        if (debugTriangle)
                        {
                            Debug.Log("TrianglesCross " + triangle.DebugPrint());
                        }
                        goto continueSearch;
                    }
                }

                // validate that it doesn't contain any vertices
                foreach (Vertex vertex2 in vertices)
                {
                    if (triangle.vertices.Contains(vertex2)) { continue; }
                    for (double tx = vertex2.x - 0.1; tx <= vertex2.x + 0.1; tx += 0.1)
                    {
                        for (double ty = vertex2.y - 0.1; ty <= vertex2.y + 1; ty += 0.1)
                        {
                            Vertex tmp = new Vertex(tx, ty);
                            if (triangle.Inside(tmp))
                            {
                                if (debugTriangle) { Debug.Log("ContainsVert " + triangle.DebugPrint()); }
                                goto continueSearch;
                            }
                        }
                    }
                }

                // passed validation
                // keep track of new lines
                if (!lines.Contains(l2)) { lines.Add(l2); unmarked.Add(l2); }
                if (!lines.Contains(l3)) { lines.Add(l3); unmarked.Add(l3); }
                if (debugTriangle) { Debug.Log("Added " + triangle.DebugPrint()); }
                triangles.Add(triangle);
                return;

                continueSearch:;
            }
        }
    }

    public class VertexPool
    {
        // the vertex pool saves every vertex we've created
        // it is used to eliminate duplicates on conversion

        public static double MinTolerance = 0.1;

        public List<Vertex> vertices = new List<Vertex>();
        public Vertex Get(double x, double y)
        {
            // check to see if we already have this vertex
            foreach (Vertex vertex in vertices)
            {
                if (vertex.DistanceTo(x, y) <= VertexPool.MinTolerance)
                {
                    return vertex;
                }
            }

            // create a new one if it isn't in the list
            Vertex v = new Vertex(x, y);
            vertices.Add(v);
            return v;
        }
    }

    public class Vertex
    {
        // the vertex is a point in space :P
        public double x;
        public double y;

        public Vertex(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double DistanceTo(Vertex vertex)
        {
            return DistanceTo(vertex.x, vertex.y);
        }

        public double DistanceTo(double x, double y)
        {
            // a^2 + b^2 = c^2 ;)
            return Math.Sqrt(Math.Pow(this.x - x, 2) + Math.Pow(this.y - y, 2));
        }
    }

    public class Triangle2D
    {
        public static double MinTolerance = 0.1;

        public Line[] lines = new Line[3];
        public Vertex[] vertices = new Vertex[3];

        public Triangle2D(Line line1, Line line2, Line line3)
        {
            lines[0] = line1;
            lines[1] = line2;
            lines[2] = line3;

            int onvert = 0;
            foreach (Line line in lines)
            {
                if (!vertices.Contains(line.vertices[0]))
                {
                    vertices[onvert++] = line.vertices[0];
                }
                if (!vertices.Contains(line.vertices[1]))
                {
                    vertices[onvert++] = line.vertices[1];
                }
            }

        }

        public bool ValidArea()
        {
            List<double> thetas = new List<double>();
            double mx = (vertices[0].x + vertices[1].x + vertices[2].x) / 3.0;
            double my = (vertices[0].y + vertices[1].y + vertices[2].y) / 3.0;

            // make sure that every vertex has a different angle from the center
            // this is needed to make the triangle point the right way
            foreach(Vertex vertex in vertices)
            {
                double theta = Math.Atan2(my - vertex.y, mx - vertex.x);
                if(thetas.Contains(theta))
                {
                    return false;
                }
                else
                {
                    thetas.Add(theta);
                }
            }

            // make sure that this triangle isn't actually a line
            return this.Area() > 0.1;
        }

        public double Area()
        {
            /* RIPPED FROM http://james-ramsden.com/area-of-a-triangle-in-3d-c-code/ */
            double a = vertices[0].DistanceTo(vertices[1]);
            double b = vertices[1].DistanceTo(vertices[2]);
            double c = vertices[2].DistanceTo(vertices[0]);
            double s = (a + b + c) / 2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }

        public bool ValidNormals()
        {
            // in order to check normals we do a raycast from every line that came from a linedef
            // we shoot a ray in the normal direction from the midpoint of a line,
            // if it collides with any line in the triangle the normal is facing the right way

            // without checking this concave areas would be filled in
            foreach (Line line in lines)
            {
                // only lines that come from a linedef have a correct normal
                if (!line.fromLinedef) { continue; }

                // find direction to push line
                double dx = Math.Cos(line.Normal());
                double dy = Math.Sin(line.Normal());

                // find line starting position
                double sx = (line.vertices[0].x + line.vertices[1].x) / 2.0;
                double sy = (line.vertices[0].y + line.vertices[1].y) / 2.0;

                // find line ending position
                double elength = 10000;
                double ex = sx + dx * elength;
                double ey = sy + dy * elength;

                // create line and check if it collides with triangle
                Line temp = new Line(new Vertex(sx, sy), new Vertex(ex, ey), false, false);
                if (!temp.Intersects(this, line))
                {
                    // if the line doesn't intersect the triangle it is facing the wrong way
                    return false;
                }
            }

            // all lines intersected triangle
            return true;
        }

        public bool Intersects(Line line)
        {
            foreach (Line line2 in lines)
            {
                if (line2 == line) { continue; }

                if (line.Intersects(line2))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Inside(Vertex p)
        {
            /* TAKEN FROM: https://stackoverflow.com/questions/2049582/how-to-determine-if-a-point-is-in-a-2d-triangle */
            var s = vertices[0].y * vertices[2].x - vertices[0].x * vertices[2].y + (vertices[2].y - vertices[0].y) * p.x + (vertices[0].x - vertices[2].x) * p.y;
            var t = vertices[0].x * vertices[1].y - vertices[0].y * vertices[1].x + (vertices[0].y - vertices[1].y) * p.x + (vertices[1].x - vertices[0].x) * p.y;

            if ((s < 0) != (t < 0))
                return false;

            var A = -vertices[1].y * vertices[2].x + vertices[0].y * (vertices[2].x - vertices[1].x) + vertices[0].x * (vertices[1].y - vertices[2].y) + vertices[1].x * vertices[2].y;
            if (A < 0.0)
            {
                s = -s;
                t = -t;
                A = -A;
            }
            return s > 0 && t > 0 && (s + t) <= A;
        }

        public bool Similar(Vertex[] otherVertices)
        {
            List<Vertex> check = new List<Vertex>(otherVertices);
            foreach (Line bLine in lines)
            {
                foreach (Vertex vertex1 in bLine.vertices)
                {
                    foreach (Vertex vertex2 in check)
                    {
                        if (vertex1.DistanceTo(vertex2) < 0.1)
                        {
                            check.Remove(vertex2);
                            goto continueSimilarSearch;
                        }
                    }
                }
                return false;
                continueSimilarSearch:;
            }

            return true;
        }

        public bool Identical(Triangle2D triangle)
        {
            foreach (Vertex v1 in vertices)
            {
                if (!triangle.vertices.Contains(v1))
                {
                    return false;
                }
            }

            return true;
        }

        public string DebugPrint()
        {
            return "(" + vertices[0].x + ", " + vertices[0].y + ")"
                + "(" + vertices[1].x + ", " + vertices[1].y + ")"
                + "(" + vertices[2].x + ", " + vertices[2].y + ")";
        }
    }

    public class Line
    {
        public Vertex[] vertices;
        public bool fromLinedef = false;
        public bool normalFlip = false;

        public Line(Vertex a, Vertex b, bool fromLinedef, bool normalFlip)
        {
            this.vertices = new Vertex[] { a, b };
            this.fromLinedef = fromLinedef;
            this.normalFlip = normalFlip;

        }

        public double Normal()
        {
            if (normalFlip == false)
                return Math.Atan2(this.vertices[1].y - this.vertices[0].y, this.vertices[1].x - this.vertices[0].x) - Math.PI / 2.0;
            else
                return Math.Atan2(this.vertices[1].y - this.vertices[0].y, this.vertices[1].x - this.vertices[0].x) + Math.PI / 2.0;

        }

        public bool Intersects(Triangle2D triangle, Line ignoreLine)
        {
            foreach (Line line in triangle.lines)
            {
                if (line == ignoreLine) { continue; }
                if (this.Intersects(line))
                {
                    return true;
                }
            }
            return false;
        }
        
        public Vertex Intersection(Line line)
        {
            /* RIPPED FROM SLADE (Doom Editor) */
            Vertex intersect = new Vertex(0, 0);

            // First, simple check for two parallel horizontal or vertical lines
            if ((vertices[0].x == vertices[1].x && line.vertices[0].x == line.vertices[1].x) || (vertices[0].y == vertices[1].y && line.vertices[0].y == line.vertices[1].y))
            {
                return null;
            }

            // Second, check if the lines share any endpoints
            if ((vertices[0].x == line.vertices[0].x && vertices[0].y == line.vertices[0].y) ||
                    (vertices[1].x == line.vertices[1].x && vertices[1].y == line.vertices[1].y) ||
                    (vertices[0].x == line.vertices[1].x && vertices[0].y == line.vertices[1].y) ||
                    (vertices[1].x == line.vertices[0].x && vertices[1].y == line.vertices[0].y))
            {
                return null;
            }

            // Third, check bounding boxes
            if (Math.Max(vertices[0].x, vertices[1].x) < Math.Min(line.vertices[0].x, line.vertices[1].x) ||
                    Math.Max(line.vertices[0].x, line.vertices[1].x) < Math.Min(vertices[0].x, vertices[1].x) ||
                    Math.Max(vertices[0].y, vertices[1].y) < Math.Min(line.vertices[0].y, line.vertices[1].y) ||
                    Math.Max(line.vertices[0].y, line.vertices[1].y) < Math.Min(vertices[0].y, vertices[1].y))
            {
                return null;
            }

            // Fourth, check for two perpendicular horizontal or vertical lines
            if (vertices[0].x == vertices[1].x && line.vertices[0].y == line.vertices[1].y)
            {
                intersect.x = vertices[0].x;
                intersect.y = line.vertices[0].y;
                return intersect;
            }
            if (vertices[0].y == vertices[1].y && line.vertices[0].x == line.vertices[1].x)
            {
                intersect.x = line.vertices[0].x;
                intersect.y = vertices[0].y;
                return intersect;
            }

            // Not a simple case, do full intersection calculation

            // Calculate some values
            double a1 = vertices[1].y - vertices[0].y;
            double a2 = line.vertices[1].y - line.vertices[0].y;
            double b1 = vertices[0].x - vertices[1].x;
            double b2 = line.vertices[0].x - line.vertices[1].x;
            double c1 = (a1 * vertices[0].x) + (b1 * vertices[0].y);
            double c2 = (a2 * line.vertices[0].x) + (b2 * line.vertices[0].y);
            double det = a1 * b2 - a2 * b1;

            // Check for no intersection
            if (det == 0)
                return null;

            // Calculate intersection point
            intersect.x = (double)((b2 * c1 - b1 * c2) / det);
            intersect.y = (double)((a1 * c2 - a2 * c1) / det);

            // Round to nearest 3 decimal places
            intersect.x = (double)(Math.Floor(intersect.x * 1000.0 + 0.5) / 1000.0);
            intersect.y = (double)(Math.Floor(intersect.y * 1000.0 + 0.5) / 1000.0);

            // Check that the intersection point is on both lines
            if (Math.Min(vertices[0].x, vertices[1].x) <= intersect.x && intersect.x <= Math.Max(vertices[0].x, vertices[1].x) &&
                Math.Min(vertices[0].y, vertices[1].y) <= intersect.y && intersect.y <= Math.Max(vertices[0].y, vertices[1].y) &&
                Math.Min(line.vertices[0].x, line.vertices[1].x) <= intersect.x && intersect.x <= Math.Max(line.vertices[0].x, line.vertices[1].x) &&
                Math.Min(line.vertices[0].y, line.vertices[1].y) <= intersect.y && intersect.y <= Math.Max(line.vertices[0].y, line.vertices[1].y))
                return intersect;

            // Intersection point does not lie on both lines
            return null;
        }

        public bool Intersects(Line line)
        {
            return (Intersection(line) != null);
        }


        public bool ConnectedTo(Line linedef)
        {
            foreach (Vertex vertex in vertices)
            {
                foreach (Vertex vertex2 in linedef.vertices)
                {
                    if (vertex.DistanceTo(vertex2) < 0.1f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public class SubMesh
    {
        public Mesh mesh;
        public Material material;
    }

}
