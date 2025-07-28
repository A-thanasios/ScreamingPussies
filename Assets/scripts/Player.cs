using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Movement movement;
    public Vector3 startingPos;
    
    

    private void Start()
    {
        movement = GetComponent<Movement>();
        movement.SetCurrTile(startingPos);
    }
}
