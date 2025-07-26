using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputAction moveAction;

    void Awake()
    {

        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        VoicePitchDetector.Instance.OnPitchDetected += VoicePitchDetector_OnPitchDetected;
    }

    // Update is called once per frame
    void Update()
    {
        //KeyboardInput();
    }
    
    private void VoicePitchDetector_OnPitchDetected(object sender, float pitch)
    {
        Debug.Log(pitch);
    }

    private void KeyboardInput()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        
        if (moveInput.y > 0) // W
            rb.position += Vector2.up;
        if (moveInput.x < 0) // A
            rb.position += Vector2.left;
        if (moveInput.x > 0) // D
            rb.position += Vector2.right;
        if (moveInput.y < 0) // S
            rb.position += Vector2.down;
    }
}
