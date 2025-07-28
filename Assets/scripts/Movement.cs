using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float up = 250;
    
    [SerializeField] private float speed;
    
    private PlayerInput playerInput;
    private InputAction moveAction;
    public Tile currTile;
    
    private float lerpTime = 0f;
    private bool isMoving = false;
    private Vector2 startPos;
    private Vector2 targetPos;
    private float targetRot;
    private float moveSpeed = 5f;


    void Awake()
    {

        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

    void Start()
    {
        VoicePitchDetector.Instance.OnPitchDetected += VoicePitchDetector_OnPitchDetected;
    }

    void Update()
    {

        if (!isMoving)
        {
            //KeyboardInput();
        }
            
        
        if (isMoving)
        {
            
            lerpTime += Time.deltaTime * moveSpeed;
            rb.MovePosition(Vector2.Lerp(
                startPos, 
                targetPos, 
                lerpTime));
            rb.MoveRotation(Quaternion.Lerp(
                transform.rotation, 
                Quaternion.Euler(0, 0, targetRot), 
                lerpTime));
        
            if (lerpTime >= 1f)
            {
                isMoving = false;
                rb.position = targetPos;
                rb.rotation = targetRot;
            }
        }

    }
    
    private void VoicePitchDetector_OnPitchDetected(object sender, float pitch)
    {
        if (isMoving) return;
        //Debug.Log(pitch);
        if (pitch < 100) ;
        else if (pitch < up)
        {
            if (!currTile.bLWall)
            {
                InitMove(TileManager.Instance.GetTile(currTile.pos.x - 1, currTile.pos.y), 90f);
                Debug.Log("GO LEFT");
            }
        }
        else if (pitch < up + 100)
        {
            if (!currTile.bRWall)
            {
                InitMove(TileManager.Instance.GetTile(currTile.pos.x + 1, currTile.pos.y), -90f);
                Debug.Log("GO RIGHT");
            }
        }
        else if (pitch < up + 200)
        {
            if (!currTile.bUWall)
            {
                InitMove(TileManager.Instance.GetTile(currTile.pos.x, currTile.pos.y + 1), 0f);
                Debug.Log("GO UP");
            }
        }
        else if (pitch > up + 201)
        {
            if (!currTile.bDWall)
            {
                InitMove(TileManager.Instance.GetTile(currTile.pos.x, currTile.pos.y - 1), 180f);
                Debug.Log("GO DOWN");
            }
        }
    }

    private void KeyboardInput()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        
        if (moveInput.y > 0) // W
        {
            if (!currTile.bUWall)
            {
                InitMove(TileManager.Instance.GetTile(currTile.pos.x, currTile.pos.y + 1), 0f);
                Debug.Log("GO UP");
            }
        }
        if (moveInput.x < 0) // A
        {
            if (!currTile.bLWall)
            {
                InitMove(TileManager.Instance.GetTile(currTile.pos.x - 1, currTile.pos.y), 90f);
                Debug.Log("GO LEFT");
            }
        }
        if (moveInput.x > 0) // D
        {
            if (!currTile.bRWall)
            {
                InitMove(TileManager.Instance.GetTile(currTile.pos.x + 1, currTile.pos.y), -90f);
                Debug.Log("GO RIGHT");
            }
        }
        if (moveInput.y < 0) // S
        {
            if (!currTile.bDWall)
            {
                InitMove(TileManager.Instance.GetTile(currTile.pos.x, currTile.pos.y - 1), 180f);
                Debug.Log("GO DOWN");
            }
        }
    }

    private void InitMove(Tile nextTile, float rotation)
    {
        startPos = currTile.pos.GetVector();
        targetPos = nextTile.pos.GetVector();
        isMoving = true;
        lerpTime = 0f;
                
        targetRot = rotation;
        currTile = nextTile;
        
    }

    public void SetCurrTile(Vector3 vec3) => currTile = TileManager.Instance.GetTile(vec3.x, vec3.y);
}
