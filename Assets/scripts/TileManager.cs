using System;
using TMPro;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance { get; private set; }
    [SerializeField] private Tile tile;
    [SerializeField] private Player player;
    [SerializeField] private GameObject parent;
    [SerializeField] private bool tutorial;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private TextMeshProUGUI text;

    
    private Tile[,] tiles;
    private Player currPlayer;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one TileManager in scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        if (tutorial)
            CreateTutorialLevel();
        else
            CreateLevel();
    }

    private void Update()
    {
        if (tutorial)
            return;
        if (currPlayer.movement.currTile == null)
            return;
        if (currPlayer.movement.currTile.isEnd)
        {
            pauseMenu.Pause();
            text.gameObject.SetActive(true);
        }
    }

    private void CreateTutorialLevel()
    {
        int mapSize = 11;
        tiles = new Tile[mapSize, mapSize];
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                tiles[x, y] = Instantiate(tile,
                    new Vector3(x, y, 0),
                    Quaternion.identity,
                    parent.transform);
                tiles[x, y].pos.x = x;
                tiles[x, y].pos.y = y;
                
                // LWall
                if (x == 0)
                    tiles[x, y].bLWall = true;
                
                // RWall
                if (x == mapSize - 1)
                    tiles[x, y].bRWall = true;
                
                // UWall
                if (y == mapSize - 1)
                    tiles[x, y].bUWall = true;
                
                // DWall
                if (y == 0)
                    tiles[x, y].bDWall = true;
                
                // Middle Walls
                // Upper verticals
                if (x == 4 && y == 6)
                {
                    tiles[x, y].bRWall = true;
                    tiles[x, y].bDWall = true;
                }
                if (x == 5 && y == 6)
                {
                    tiles[x, y].bLWall = true;
                    tiles[x, y].bRWall = true;
                }
                if (x == 6 && y == 6)
                {
                    tiles[x, y].bLWall = true;
                    tiles[x, y].bDWall = true;
                }
                //horizontals
                if (x == 4 && y == 5)
                {
                    tiles[x, y].bUWall = true;
                    tiles[x, y].bDWall = true;
                }
                if (x == 6 && y == 5)
                {
                    tiles[x, y].bUWall = true;
                    tiles[x, y].bDWall = true;
                }
                // Down verticals
                if (x == 4 && y == 4)
                {
                    tiles[x, y].bUWall = true;
                    tiles[x, y].bRWall = true;
                }
                if (x == 5 && y == 4)
                {
                    tiles[x, y].bLWall = true;
                    tiles[x, y].bRWall = true;
                }
                if (x == 6 && y == 4)
                {
                    tiles[x, y].bLWall = true;
                    tiles[x, y].bUWall = true;
                }
            }
        }
        currPlayer = Instantiate(player, new Vector3(5, 5, 0), Quaternion.identity);
        player.startingPos = new Vector3(5, 5, 0); 
    }
        
        private void CreateLevel()
    {
        int mapSize = 11;
        tiles = new Tile[mapSize, mapSize];
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                tiles[x, y] = Instantiate(tile,
                    new Vector3(x, y, 0),
                    Quaternion.identity,
                    parent.transform);
                tiles[x, y].pos.x = x;
                tiles[x, y].pos.y = y;
                
                // LWall
                if (x == 0)
                    tiles[x, y].bLWall = true;
                
                // RWall
                if (x == mapSize - 1)
                {
                    tiles[x, y].bRWall = true;
                    if (y == 0)
                    {
                        tiles[x, y].isEnd = true;
                        tiles[x, y].rWall.color = Color.red;
                    }

                    
                    
                }
                
                // UWall
                if (y == mapSize - 1)
                    tiles[x, y].bUWall = true;
                
                // DWall
                if (y == 0)
                    tiles[x, y].bDWall = true;

                switch (y)
                {
                    case 0:
                    {
                        if (x is 1 or 3 or > 4 and < 9 or 10)
                        {
                            tiles[x, y].bUWall = true;
                        }
                        if (x is 1 or 8)
                        {
                            tiles[x, y].bRWall = true;
                        }
                        if (x is 2 or 9)
                        {
                            tiles[x, y].bLWall = true;
                        }

                        break;
                    }
                    case 1:
                    {
                        if (x is 0 or 8 or 9)
                        {
                            tiles[x, y].bRWall = true;
                        }
                        if (x is 1 or 4 or 9 or 10)
                        {
                            tiles[x, y].bLWall = true;
                        }
                        if (x == 3)
                        {
                            tiles[x, y].bRWall = true;
                            tiles[x, y].bDWall = true;
                        }
                        if (x is 1 or > 4 and < 9 or 10)
                        {
                            tiles[x, y].bDWall = true;
                        }
                        if (x is > 1 and < 8)
                        {
                            tiles[x, y].bUWall = true;
                        }

                        break;
                    }
                    case 2:
                    {
                        if (x is 0 or 7 or 8)
                        {
                            tiles[x, y].bRWall = true;
                        }
                        if (x is 1 or 8 or 9)
                        {
                            tiles[x, y].bLWall = true;
                        }
                        if (x is > 1 and < 8)
                        {
                            tiles[x, y].bDWall = true;
                        }
                        if (x is > 0 and < 7 or 9)
                        {
                            tiles[x, y].bUWall = true;
                        }

                        break;
                    }
                    case 3:
                    {
                        if (x is 0 or 4 or 7)
                        {
                            tiles[x, y].bRWall = true;
                        }
                        if (x is 1 or 5 or 8)
                        {
                            tiles[x, y].bLWall = true;
                        }
                        if (x is (> 1 and < 4)
                                 or (> 4 and < 7)
                                 or (> 7))
                        {
                            tiles[x, y].bUWall = true;
                        }
                        if (x is > 0 and < 7 or 9)
                        {
                            tiles[x, y].bDWall = true;
                        }

                        break;
                    }
                    case 4:
                    {
                        if (x is 0 or 3 or 6 or 9)
                        {
                            tiles[x, y].bRWall = true;
                        }
                        if (x is 1 or 4 or 7 or 10)
                        {
                            tiles[x, y].bLWall = true;
                        }
                        if (x is (> 1 and < 4)
                                 or (> 4 and < 7)
                                 or (> 7))
                        {
                            tiles[x, y].bDWall = true;
                        }
                        if (x is (> 1 and < 6) or (> 6 and < 9))
                        {
                            tiles[x, y].bUWall = true;
                        }

                        break;
                    }
                    case 5:
                    {
                        if (x is 0 or 1 or 5 or 6 or 9)
                        {
                            tiles[x, y].bRWall = true;
                        }
                        if (x is 1 or 2 or 6 or 7 or 10)
                        {
                            tiles[x, y].bLWall = true;
                        }
                        if (x is 4 or (> 7 and < 10))
                        {
                            tiles[x, y].bUWall = true;
                        }
                        if (x is (> 1 and < 6) or (> 6 and < 9))
                        {
                            tiles[x, y].bDWall = true;
                        }

                        break;
                    }
                    case 6:
                    {
                        if (x is 0 or 1 or 2 or 4 or 5 or 6 or 7 or 9)
                        {
                            tiles[x, y].bRWall = true;
                        }
                        if (x is 1 or 2 or 3 or 5 or 6 or 7 or 8 or 10)
                        {
                            tiles[x, y].bLWall = true;
                        }
                        if (x is 4 or (> 7 and < 10))
                        {
                            tiles[x, y].bDWall = true;
                        }

                        break;
                    }
                    case 7:
                    {
                        if (x is 0 or > 0 and < 10)
                        {
                            tiles[x, y].bRWall = true;
                        }
                        if (x is > 0 and < 11)
                        {
                            tiles[x, y].bLWall = true;
                        }

                        break;
                    }
                    case 8:
                    {
                        if (x is 0 or 2 or 3 or 4 or 5 or 6 or 8)
                        {
                            tiles[x, y].bRWall = true;
                        }
                        if (x is 1 or 3 or 4 or 5 or 6 or 7 or 9)
                        {
                            tiles[x, y].bLWall = true;
                        }
                        if (x is (> 0 and < 4) or (> 6 and < 9))
                        {
                            tiles[x, y].bUWall = true;
                        }

                        break;
                    }
                    case 9:
                    {
                        if (x is 4 or 5 or 9)
                        {
                            tiles[x, y].bRWall = true;
                        }
                        if (x is 5 or 6 or 10)
                        {
                            tiles[x, y].bLWall = true;
                        }
                        if (x is (> 0 and < 4) or (> 6 and < 9))
                        {
                            tiles[x, y].bDWall = true;
                        }
                        if (x is 0 or (> 0 and < 5) or (> 5 and < 10))
                        {
                            tiles[x, y].bUWall = true;
                        }

                        break;
                    }
                    case 10:
                    {
                        if (x is 5)
                        {
                            tiles[x, y].bRWall = true;
                        }
                        if (x is 6)
                        {
                            tiles[x, y].bLWall = true;
                        }
                        if (x is 0 or (> 0 and < 5) or (> 5 and < 10))
                        {
                            tiles[x, y].bDWall = true;
                        }

                        break;
                    }
                }
            }
        }
        
        // Player
        currPlayer = Instantiate(player, new Vector3(0, 10, 0), Quaternion.identity);
        player.startingPos = new Vector3(0, 10, 0);

    }

    
    public Tile GetTile(float x, float y) => tiles[(int)x, (int)y];
}
