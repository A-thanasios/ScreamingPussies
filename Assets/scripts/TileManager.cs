using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance { get; private set; }
    [SerializeField] private Tile tile;
    [SerializeField] private Player player;

    
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
        tiles = new Tile[10, 10];
        for (int i = 0; i < 10; i++)
        {
            tiles[i, 0] = Instantiate(tile, new Vector3(i, 0, 0), Quaternion.identity);
            tiles[i, 0].pos.x = i;
            tiles[i, 0].pos.y = 0;

            switch (i)
            {
                case 0:
                    tiles[0, 0].bLWall = true;
                    tiles[0, 0].bUWall = true;
                    tiles[0, 0].bDWall = true;
                    Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                case 9:
                    tiles[9, 0].bRWall = true;
                    tiles[9, 0].bUWall = true;
                    tiles[9, 0].bDWall = true;
                    break;
                default:
                    tiles[i, 0].bUWall = true;
                    tiles[i, 0].bDWall = true;
                    break;
            }
        }
    }

    private void Start()
    {
        
    }
    
    public Tile GetTile(int x, int y) => tiles[x, y];
}
