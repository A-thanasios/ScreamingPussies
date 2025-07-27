using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Movement movement;
    public Vector3 startingPos;
    
    

    private void Start()
    {
        movement = GetComponent<Movement>();
        movement.SetCurrTile(startingPos);
    }
}
