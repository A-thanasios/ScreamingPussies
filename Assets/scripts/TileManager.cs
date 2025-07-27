using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance { get; private set; }
    [SerializeField] private Tile tile;
    [SerializeField] private Player player;
    [SerializeField] private GameObject parent;

    
    private Tile[,] tiles;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one TileManager in scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        CreateLevel();
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
        
        // Player
        Instantiate(player, new Vector3(5, 5, 0), Quaternion.identity);
        player.startingPos = new Vector3(5, 5, 0);

    }

    
    public Tile GetTile(float x, float y) => tiles[(int)x, (int)y];
}
