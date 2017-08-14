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

        // This class is the actual triangulator, you feed it a list of linedefs and it spits out triangles
        
        public static SubMesh CreateFloor(SECTORS sector, Material mat)
        {
            Mesh mesh = new Mesh();
            VertexPool vertexPool = new VertexPool();
            List<Triangle2D> triangles = new List<Triangle2D>();
            List<Line> lines = new List<Line>();


            foreach (LINEDEFS linedef in sector.lines)
            {
                Vertex v1 = vertexPool.Get(linedef.firstVert.x, linedef.firstVert.z);
                Vertex v2 = vertexPool.Get(linedef.lastVert.x, linedef.lastVert.z);

                if (linedef.getFrontSector() == sector)
                    lines.Add(new Line(v1, v2, true, false));
                else
                    lines.Add(new Line(v1, v2, true, true));

            }

            // go through every line and attach it to every other vertex.
            // reject the triangle if it isn't valid
            List<Line> unmarked = new List<Line>(lines);
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
                    //while (sorted.ContainsKey(theta)) { theta += 0.0001; } // maybe needed?
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

        public static SubMesh CreateCeiling(Mesh floor, SECTORS sector, Material mat)
        {
            //reverse
            Mesh ceiling = new Mesh();

            List<Vector3> tmpVerts = new List<Vector3>(floor.vertices);
            List<Vector2> tmpUvs = new List<Vector2>(floor.uv);
            List<int> tmpTris = new List<int>(floor.triangles);
            List<Vector3> tmpNrm = new List<Vector3>(floor.normals);


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

        public static List<SubMesh> CreateWalls(SECTORS sector, WAD wad, bool generatingDoor)
        {
            List<SubMesh> sMeshs = new List<SubMesh>();

            foreach (LINEDEFS line in sector.lines)
            {
                SECTORS fSector = line.getFrontSector();
                SECTORS bSector = line.getBackSector();

                // check to see if this line is totally broken
                if (fSector == null && bSector == null)
                    continue;

                // if this line exists in both sectors, only draw if this is the back sector
                // otherwise we will draw this wall twice
                if (fSector != null && bSector != null && bSector != sector)
                    continue;

                if (sector.isDoor())
                {
                    // special door logic
                    if (generatingDoor)
                    {
                        // door objects only contain upper walls that border a front and back sector
                        if (fSector != null && bSector != null)
                            sMeshs.AddRange(CreateUpperWalls(sector, line, wad));
                    }
                    else
                    {
                        // sector objects contain all mid and lower walls
                        sMeshs.AddRange(CreateMidWalls(line, wad));
                        sMeshs.AddRange(CreateLowerWalls(line, wad));
                        // sector objects also contain upper walls of lines who only have one sector
                        if (fSector == null || bSector == null)
                            sMeshs.AddRange(CreateUpperWalls(sector, line, wad));
                    }
                }
                else
                {
                    // regular sector, create all walls
                    sMeshs.AddRange(CreateMidWalls(line, wad));
                    sMeshs.AddRange(CreateLowerWalls(line, wad));
                    sMeshs.AddRange(CreateUpperWalls(sector, line, wad));
                }
            }

            return sMeshs;
        }
        
        public static SubMesh CreateWall(LINEDEFS line, SECTORS sector, Material texture, float startHeight, float endHeight, bool flipped, WallType wallType)
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

        public static List<SubMesh> CreateMidWalls(LINEDEFS line, WAD wad)            // midtex
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
                startHeight = line.getFrontSector().floorHeight;
                endHeight = line.getFrontSector().ceilingHeight;
                if (line.getFrontSector().isDoor())
                    endHeight = line.getFrontSector().MinNeighborCeilingHeight();
            }
            else if (line.getFrontSector() == null && line.getBackSector() != null)
            {
                startHeight = line.getBackSector().floorHeight;
                endHeight = line.getBackSector().ceilingHeight;
            }
            else
            {
                Debug.Log("??");
            }

            // generate a wall for each textured side
            List<SubMesh> walls = new List<SubMesh>();
            if (line.side1 != null && wad.textures.ContainsKey(line.side1.midTex))
            {
                Material texture = wad.textures[line.side1.midTex];

                if (line.getFrontSector() != null && line.getBackSector() != null)
                    endHeight = Math.Min(startHeight + texture.mainTexture.height, endHeight);  //dont tile middle textures with 2 sides

                walls.Add(CreateWall(line, line.getFrontSector(), texture, startHeight, endHeight, false, WallType.Middle));
            }

            if (line.side2 != null && wad.textures.ContainsKey(line.side2.midTex))
            {
                Material texture = wad.textures[line.side2.midTex];

                if (line.getFrontSector() != null && line.getBackSector() != null)
                    endHeight = Math.Min(startHeight + texture.mainTexture.height, endHeight);  //dont tile middle textures with 2 sides

                walls.Add(CreateWall(line, line.getBackSector(), texture, startHeight, endHeight, true, WallType.Middle));
            }
            
            return walls;
        }

        public static List<SubMesh> CreateLowerWalls(LINEDEFS line, WAD wad)            // lowtex
        {
            List<SubMesh> walls = new List<SubMesh>();
            float startHeight = 0;
            float endHeight = 0;

            // figure out start and end heights
            if (line.getFrontSector() != null && line.getBackSector() != null)
            {
                startHeight = Math.Min(line.getFrontSector().floorHeight, line.getBackSector().floorHeight);
                endHeight = Math.Max(line.getFrontSector().floorHeight, line.getBackSector().floorHeight);
            }
            else
            {
                return walls;
            }

            // generate a wall for each textured side
            if (line.side1 != null && wad.textures.ContainsKey(line.side1.lowTex))
                walls.Add(CreateWall(line, line.getFrontSector(), wad.textures[line.side1.lowTex], startHeight, endHeight, false, WallType.Lower));

            if (line.side2 != null && wad.textures.ContainsKey(line.side2.lowTex))
                walls.Add(CreateWall(line, line.getBackSector(), wad.textures[line.side2.lowTex], startHeight, endHeight, true, WallType.Lower));

            return walls;
        }

        public static List<SubMesh> CreateUpperWalls(SECTORS sector, LINEDEFS line, WAD wad)            // uppertex
        {
            List<SubMesh> walls = new List<SubMesh>();
            float startHeight = 0;
            float endHeight = 0;

            // figure out start and end heights
            if (line.getFrontSector() != null && line.getBackSector() != null)
            {
                startHeight = Math.Min(line.getFrontSector().ceilingHeight, line.getBackSector().ceilingHeight);
                endHeight = Math.Max(line.getFrontSector().ceilingHeight, line.getBackSector().ceilingHeight);
            }
            else
            {
                return walls;
            }

            // generate a wall for each textured side
            if (line.side1 != null && wad.textures.ContainsKey(line.side1.upTex))
                walls.Add(CreateWall(line, line.getFrontSector(), wad.textures[line.side1.upTex], startHeight, endHeight, false, WallType.Upper));

            if (line.side2 != null && wad.textures.ContainsKey(line.side2.upTex))
                walls.Add(CreateWall(line, line.getBackSector(), wad.textures[line.side2.upTex], startHeight, endHeight, true, WallType.Upper));

            return walls;
        }

        public static void CombineSubmeshes(ref Mesh mesh, List<SubMesh> submeshes, ref Material[] materials)
        {
            mesh.Clear();

            List<Material> materialList = new List<Material>();
            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();

            // add info from each submesh into lists
            foreach (SubMesh submesh in submeshes)
            {
                vertices.AddRange(submesh.mesh.vertices);
                uvs.AddRange(submesh.mesh.uv);
                normals.AddRange(submesh.mesh.normals);
                materialList.Add(submesh.material);
            }

            // convert lists to arrays and apply to mesh
            mesh.vertices = vertices.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.subMeshCount = submeshes.Count();
            materials = materialList.ToArray();

            // set triangles correctly according to new vertex offsets
            int onMesh = 0;
            int vertexCount = 0;
            foreach (SubMesh submesh in submeshes)
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
            // TODO: sort vertices by least distance
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

                // validate that it has an area
                if (!triangle.ValidArea())
                {
                    if (debugTriangle) { Debug.Log("InvalidArea"); }
                    goto continueSearch;
                }

                // validate normals
                if (!triangle.ValidNormals())
                {
                    if (debugTriangle) { Debug.Log("InvalidNormals"); }
                    goto continueSearch;
                }

                // validate uniqueness
                foreach (Triangle2D triangle2 in triangles)
                {
                    if (triangle.Identical(triangle2))
                    {
                        if (debugTriangle) { Debug.Log("IsntUnique"); }
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
                            Debug.Log("TrianglesCross");
                        }
                        goto continueSearch;
                    }
                }

                // validate that it doesn't contain any vertices
                foreach (Vertex vertex2 in vertices)
                {
                    if (triangle.vertices.Contains(vertex2)) { continue; }
                    for (double tx = vertex2.x - 0.01; tx <= vertex2.x + 0.01; tx += 0.01)
                    {
                        for (double ty = vertex2.y - 0.01; ty <= vertex2.y + 0.01; ty += 0.01)
                        {
                            Vertex tmp = new Vertex(tx, ty);
                            if (triangle.Inside(tmp))
                            {
                                if (debugTriangle) { Debug.Log("ContainsVert"); }
                                goto continueSearch;
                            }
                        }
                    }
                }

                // passed validation
                // keep track of new lines
                if (!lines.Contains(l2)) { lines.Add(l2); unmarked.Add(l2); }
                if (!lines.Contains(l3)) { lines.Add(l3); unmarked.Add(l3); }
                if (debugTriangle) { Debug.Log("Added"); }
                triangles.Add(triangle);

                continueSearch:;
            }
        }
    }

    public class VertexPool
    {
        // the vertex pool saves every vertex we've created
        // it is used to eliminate duplicates on conversion

        public static double MinTolerance = 0.001;

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
        public static double MinTolerance = 0.001;

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
            return this.Area() > 0;
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
        
        public bool Intersects(Line line)
        {
            /* RIPPED FROM SLADE (Doom Editor) */
            Vertex intersect = new Vertex(0, 0);

            // First, simple check for two parallel horizontal or vertical lines
            if ((vertices[0].x == vertices[1].x && line.vertices[0].x == line.vertices[1].x) || (vertices[0].y == vertices[1].y && line.vertices[0].y == line.vertices[1].y))
            {
                return false;
            }

            // Second, check if the lines share any endpoints
            if ((vertices[0].x == line.vertices[0].x && vertices[0].y == line.vertices[0].y) ||
                    (vertices[1].x == line.vertices[1].x && vertices[1].y == line.vertices[1].y) ||
                    (vertices[0].x == line.vertices[1].x && vertices[0].y == line.vertices[1].y) ||
                    (vertices[1].x == line.vertices[0].x && vertices[1].y == line.vertices[0].y))
            {
                return false;
            }

            // Third, check bounding boxes
            if (Math.Max(vertices[0].x, vertices[1].x) < Math.Min(line.vertices[0].x, line.vertices[1].x) ||
                    Math.Max(line.vertices[0].x, line.vertices[1].x) < Math.Min(vertices[0].x, vertices[1].x) ||
                    Math.Max(vertices[0].y, vertices[1].y) < Math.Min(line.vertices[0].y, line.vertices[1].y) ||
                    Math.Max(line.vertices[0].y, line.vertices[1].y) < Math.Min(vertices[0].y, vertices[1].y))
            {
                return false;
            }

            // Fourth, check for two perpendicular horizontal or vertical lines
            if (vertices[0].x == vertices[1].x && line.vertices[0].y == line.vertices[1].y)
            {
                intersect.x = vertices[0].x;
                intersect.y = line.vertices[0].y;
                return true;
            }
            if (vertices[0].y == vertices[1].y && line.vertices[0].x == line.vertices[1].x)
            {
                intersect.x = line.vertices[0].x;
                intersect.y = vertices[0].y;
                return true;
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
                return false;

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
                return true;

            // Intersection point does not lie on both lines
            return false;
        }


        public bool ConnectedTo(Line linedef)
        {
            foreach (Vertex vertex in vertices)
            {
                foreach (Vertex vertex2 in linedef.vertices)
                {
                    if (vertex.DistanceTo(vertex2) < 0.001f)
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