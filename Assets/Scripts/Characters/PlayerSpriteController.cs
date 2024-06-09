using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerSpriteController : MonoBehaviour
{
    [SerializeField] private Player player;

    //Sprite Movement
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 movementInput; //The input from keyboard
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputSmoothVelocity;
    [SerializeField] private Vector2 currentDirection; //Direction player is facing for sprite purposes

    //Walk speed
    public float walkSpeed;

    //Basic movement
    [SerializeField] private bool isMoving;
    [SerializeField] private bool Movable = true;

    //Sprinting
    [SerializeField] private bool isSprinting;
    [SerializeField] private bool Sprintable = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (Movable)
        {
            smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothVelocity, 0.1f);
            if (Sprintable && isSprinting)
                rb.velocity = smoothedMovementInput * player.movementSpeed * 6;
            else
                rb.velocity = smoothedMovementInput * player.movementSpeed * 3;

            currentDirection = movementInput;
        }
    }

    //Detecting when player is sprinting
    public void OnSprintStart()
    {
        isSprinting = true;
    }

    public void OnSprintFinish()
    {
        isSprinting = false;
    }

    //Player Movement 
    public void OnMovement(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();

        if (movementInput.x != 0 || movementInput.y != 0)
            currentDirection = movementInput;
        
        if (movementInput.x == 0 && movementInput.y == 0)
            isMoving = false;
        else
            isMoving = true;
    }

    //Prevent player from moving
    public void RestrictMovement()
    {
        movementInput = new Vector2(0, 0);
        Movable = false;
        isMoving = false;
    }

    //Prevent player from sprinting
    public void RestrictSprint()
    {
        isSprinting = false;
        Sprintable = false;
    }

}
