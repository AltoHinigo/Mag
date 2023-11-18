using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    //public Cell CellPrefab;
    //public Vector3 CellSize = new Vector3(1,1,0);
    //public HintRenderer HintRenderer;
    [SerializeField] private float blockSize;
    [SerializeField] private int Width = 5;
    [SerializeField] private int Height = 5;
    [SerializeField] private Object spawnPoint;
    [SerializeField] private UnityEngine.Object LType;
    [SerializeField] private UnityEngine.Object IType;
    [SerializeField] private UnityEngine.Object TType;
    [SerializeField] private UnityEngine.Object XType;
    [SerializeField] private UnityEngine.Object OType;

    private Vector3 spawnPos = new Vector3(0,0,0);
    //public Cell2D _CellPrefab;
    //public Vector3 _CellSize = new Vector3(20, -3, 20);
    
    //public MazeSpawner2D M2D;


        public Maze maze;
    private void Start()
    {
        //M2D = new MazeSpawner2D();
        //M2D.CellPrefab = _CellPrefab;
        //M2D.CellSize = _CellSize;
        ///MazeGenerator generator = new MazeGenerator();
        ///maze = generator.GenerateMaze();

        /*Width = 5; Height = 5;
        MazeGeneratorCell[,] cells = new MazeGeneratorCell[Width, Height];

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y] = new MazeGeneratorCell { X = x, Y = y , WallBottom = false, WallLeft = false};
            }
        }

        cells[1, 1].WallLeft = true;
        cells[1, 1].WallBottom = true;
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            cells[0, x].WallLeft = true;
            cells[x, 0].WallBottom = true;
            cells[cells.GetLength(0)-1, x].WallLeft = true;
            cells[x, cells.GetLength(0)-1].WallBottom = true;
        }

        maze.cells = cells;*/
        //M2D.maze = maze;
        //M2D.gen();
        spawnPos = spawnPoint.GetComponent<Transform>().position;
        Gen();

        //UnityEditor.StaticOcclusionCulling.Compute();
    }

    void Gen()
    {
        MazeGenerator generator = new MazeGenerator();
        generator.Width = Width;
        generator.Height = Height;
        maze = generator.GenerateMaze();

        bool upWall = false;
        bool rightWall = false;
        bool leftWall = false;
        bool bottomWall = false;

        byte countOfFreeWays = 0;

        Object ren;

        for (int y = 0; y < maze.cells.GetLength(1)-1; y++)
        {
            for (int x = 0; x < maze.cells.GetLength(0)-1; x++)
            {
                if (x + 1 != maze.cells.GetLength(0)/* && x != 0*/)//не за границей массива
                    rightWall = maze.cells[x + 1, y].WallLeft;
                else
                    rightWall = true;
                if (y + 1 != maze.cells.GetLength(1))
                    upWall = maze.cells[x, y + 1].WallBottom;
                else
                    upWall = true;
                leftWall = maze.cells[x, y].WallLeft;
                bottomWall = maze.cells[x, y].WallBottom;

                if (!leftWall) countOfFreeWays++;
                if (!bottomWall) countOfFreeWays++;
                if (!upWall) countOfFreeWays++;
                if (!rightWall) countOfFreeWays++;
                switch (countOfFreeWays)
                {
                    case 1:
                        if (!leftWall)//O
                        {
                            ren = Instantiate(OType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, 180, 0));
                            CopyLightMap(OType,ren);
                            break;
                        }
                        if (!rightWall)//O
                        {
                            ren = Instantiate(OType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, 0, 0));
                            CopyLightMap(OType, ren);
                            break;
                        }
                        if (!upWall)//O
                        {
                            ren = Instantiate(OType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, -90, 0));
                            CopyLightMap(OType, ren);
                            break;
                        }
                        ren = Instantiate(OType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, 90, 0));
                        CopyLightMap(OType, ren);
                        break;
                    case 2:
                        if (leftWall && rightWall)//I
                        {
                            ren = Instantiate(IType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, 0, 0));
                            CopyLightMap(IType, ren);
                            break;
                        }
                        if (bottomWall && upWall)//I
                        {
                            ren = Instantiate(IType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, 90, 0));
                            CopyLightMap(IType, ren);
                            break;
                        }
                        if (upWall)
                            if (leftWall)//L
                            {
                                ren = Instantiate(LType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, 90, 0));
                                CopyLightMap(LType, ren);
                                break;
                            }
                            else
                            {
                                ren = Instantiate(LType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, 180, 0));
                                CopyLightMap(LType, ren);
                                break;
                            }
                        if (bottomWall)
                            if (rightWall)
                            {
                                ren = Instantiate(LType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, -90, 0));
                                CopyLightMap(LType, ren);
                                break;
                            }
                            else
                            {
                                ren = Instantiate(LType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, 0, 0));
                                CopyLightMap(LType, ren);
                                break;
                            }
                        break;
                    case 3:
                        if (leftWall)
                        {
                            ren = Instantiate(TType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, 0, 0));
                            CopyLightMap(TType, ren);
                            break;
                        }
                        if (rightWall)
                        {
                            ren = Instantiate(TType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, 180, 0));
                            CopyLightMap(TType, ren);
                            break;
                        }
                        if (bottomWall)
                        {
                            ren = Instantiate(TType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, -90, 0));
                            CopyLightMap(TType, ren);
                            break;
                        }
                        ren = Instantiate(TType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, 90, 0));
                        CopyLightMap(TType, ren);
                        break;
                    case 4:
                        ren = Instantiate(XType, spawnPos + new Vector3(blockSize * x, 0, blockSize * y), Quaternion.Euler(0, 0, 0));
                        CopyLightMap(XType, ren);
                        break;
                    default:
                        Debug.LogError("countOfFreeWays = 0 " + bottomWall + leftWall + upWall + rightWall);
                        break;
                    /*finally:
                        Debug.LogError("countOfFreeWays = 0 " + bottomWall + leftWall + upWall + rightWall);*/
                }
                
                countOfFreeWays = 0;
                /*Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity);

                c.WallLeft.SetActive(leftWall);
                c.WallBottom.SetActive(bottomWall);*/
            }
        }

        //HintRenderer.DrawPath();
    }

    private void CopyLightMap(Object Ref, Object obj)
    {
        int i = 0;
        List<MeshRenderer> _childMeshRenderer = new List<MeshRenderer>();
        foreach (MeshRenderer childMeshRenderer in Ref.GetComponentsInChildren<MeshRenderer>())
            _childMeshRenderer.Add(childMeshRenderer);
        foreach (MeshRenderer childMeshRenderer in obj.GetComponentsInChildren<MeshRenderer>())
        {
            childMeshRenderer.lightmapIndex = _childMeshRenderer[i].lightmapIndex;
            childMeshRenderer.lightmapScaleOffset = _childMeshRenderer[i].lightmapScaleOffset;
            i++;
        }
    }
}