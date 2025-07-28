using DefaultNamespace;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool hasPlayer;
    [SerializeField] private bool isStart;
    [SerializeField] public bool isEnd;
    [SerializeField] public bool bUWall;
    [SerializeField] public bool bDWall;
    [SerializeField] public bool bLWall;
    [SerializeField] public bool bRWall;

    [SerializeField] private SpriteRenderer uWall;
    [SerializeField] private SpriteRenderer dWall;
    [SerializeField] private SpriteRenderer lWall;
    [SerializeField] public SpriteRenderer rWall;

    public Position pos;
    
    private void Start()
    {
        SetWalls();
    }

    private void SetWalls()
    {
        if (bUWall)
            uWall.enabled = true;
        if (bDWall)
            dWall.enabled = true;
        if (bLWall)
            lWall.enabled = true;
        if (bRWall)
            rWall.enabled = true;
    }
}
