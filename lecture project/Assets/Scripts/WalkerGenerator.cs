using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerGenerator : MonoBehaviour
{

    public enum Grid
    {
        FLOOR,
        WALL,
        EMPTY
    }

    public Grid[,] gridHandler;
    public List<WalkerObject> Walkers;
    public Tilemap tileMap;
    public Tile floor;
    public Tile wall;
    public int MapWidth = 30;
    public int MapHeight = 30;
    public int MaximumWalkers = 10;
    public int TileCount = default;
    public float FillPercentage = 0.4f;
    public float WaitTime = 0.05f;



    // Start is called before the first frame update
    void Start()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {
        gridHandler = new Grid[MapWidth, MapHeight];
        for (int x = 0; x < gridHandler.GetLength(0); x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1); y++)
            {
                gridHandler[x, y] = Grid.EMPTY;
            }
        }

        Walkers = new List<WalkerObject>();

        

        Vector3Int TileCenter = new Vector3Int(gridHandler.GetLength(0) / 2, gridHandler.GetLength(1) / 2, 0);

        for (int i = 0; i < 10; i++) {
            WalkerObject curWalker = new WalkerObject(new Vector2(TileCenter.x, TileCenter.y), GetDirection(), 0.5f);
            tileMap.SetTile(TileCenter, floor);
            Walkers.Add(curWalker);
        }
        TileCount++;
        //StartCoroutine(
            CreateFloors();
    }
    // Update is called once per frame
    void Update()
    {

    }

    Vector2 GetDirection()
    {
        int choice = Mathf.FloorToInt(UnityEngine.Random.value * 3.99f);
        switch (choice)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            case 3:
                return Vector2.right;
            default:
                return Vector2.zero;

        }
    }

    //IEnumerator 
         private void CreateFloors()
    {
        //while (((float)TileCount / (float)gridHandler.Length) < FillPercentage)
        for (int i =0; i< 50; i++)
        {
            bool hasCreatedFloor = false;
            foreach (WalkerObject curWalker in Walkers)
            {
                Vector3Int curPos = new Vector3Int((int)curWalker.position.x, (int)curWalker.position.y, 0);
                print("cur x: " + curWalker.position.x);
                print("cur y: " + curWalker.position.y);
                if (gridHandler[curPos.x, curPos.y] != Grid.FLOOR)
                {
                    tileMap.SetTile(curPos, floor);
                    TileCount++;
                    gridHandler[curPos.x, curPos.y] = Grid.FLOOR;

                    print("tile/grid: " + (float)TileCount / (float)gridHandler.Length);
                    hasCreatedFloor = true;
                }

                

                
            }

            print(i);
            //ChanceToRemove();
                ChanceToRedirect();
                //ChanceToCreate();
                UpdatePosition();
            

            /*if (hasCreatedFloor)
            {
                yield return new WaitForSeconds(WaitTime);
            }*/

        }

        //StartCoroutine(
            CreateWalls();
    }

    void ChanceToRemove()
    {
        int updatedCount = Walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange && Walkers.Count > i)
            {
                Walkers.RemoveAt(i);
                break;
            }
        }
    }

    void ChanceToRedirect()
    {

        for (int i = 0; i < Walkers.Count; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange)
            {
                WalkerObject curWalker = Walkers[i];
                curWalker.direction = GetDirection();
                Walkers[i] = curWalker;

            }
        }
    }

    void ChanceToCreate()
    {
        int updatedCount = Walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange && Walkers.Count < MaximumWalkers)
            {
                Vector2 newDIrection = GetDirection();
                Vector2 newPosition = Walkers[i].position;

                WalkerObject newWalker = new WalkerObject(newPosition, newDIrection, 0.5f);


                Walkers.Add(newWalker);

            }
        }
    }

    void UpdatePosition()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            //WalkerObject FoundWalker = Walkers[i];
            //FoundWalker.position += FoundWalker.direction;
            /* FoundWalker.position.x = Mathf.Clamp(FoundWalker.position.x, 1, gridHandler.GetLength(0) - 2);
             FoundWalker.position.y = Mathf.Clamp(FoundWalker.position.y, 1, gridHandler.GetLength(1) - 2);*/
            Walkers[i].position += Walkers[i].direction; 
                //= FoundWalker;
        }
    }

    //IEnumerator 
        private void CreateWalls()
    {
        print("creating walls");
        for (int x = 0; x < gridHandler.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1) - 1; y++)
            {
                if (gridHandler[x, y] == Grid.FLOOR)
                {
                    bool hasCreatedWall = false;

                    if (gridHandler[x + 1, y] == Grid.EMPTY)
                    {
                        tileMap.SetTile(new Vector3Int(x + 1, y, 0), wall);
                        gridHandler[x + 1, y] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (gridHandler[x - 1, y] == Grid.EMPTY)
                    {
                        tileMap.SetTile(new Vector3Int(x - 1, y, 0), wall);
                        gridHandler[x - 1, y] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (gridHandler[x, y + 1] == Grid.EMPTY)
                    {
                        tileMap.SetTile(new Vector3Int(x, y + 1, 0), wall);
                        gridHandler[x, y + 1] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (gridHandler[x, y - 1] == Grid.EMPTY)
                    {
                        tileMap.SetTile(new Vector3Int(x, y - 1, 0), wall);
                        gridHandler[x, y - 1] = Grid.WALL;
                        hasCreatedWall = true;
                    }

                   /* if (hasCreatedWall)
                    {
                        yield return new WaitForSeconds(WaitTime);
                    }*/
                }

            }
        }
    }
}
