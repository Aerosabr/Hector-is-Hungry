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
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    //Walk speed
    public float walkSpeed;

    //Space region movement
    public bool inSpace;
    [SerializeField] private float slowdownRate;

    //Basic movement
    [SerializeField] private bool isMoving;
    [SerializeField] private bool Movable = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (Movable)
        {
            if (inSpace)
            {
                if (movementInput == Vector2.zero)
                {
                    smoothedMovementInput = Vector2.Lerp(smoothedMovementInput, Vector2.zero, slowdownRate * Time.fixedDeltaTime);
                }
                else
                {
                    smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothVelocity, 0.1f);
                }

                rb.velocity = smoothedMovementInput * player.movementSpeed;

                if (smoothedMovementInput.magnitude > 0.1f)
                {
                    currentDirection = smoothedMovementInput;
                }
            }
            else
            {
                smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothVelocity, 0.1f);
                rb.velocity = smoothedMovementInput * player.movementSpeed * 3;

                currentDirection = movementInput;
            }
        }
    }

    //Player Movement 
    public void OnMovement(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
        animator.Play("Run");

        if (movementInput.x != 0 || movementInput.y != 0)
        { 
            currentDirection = movementInput;
            if(player.movementSpeed == 1.25)
            {
				MusicManager.instance.soundSources[3].Stop();
                if(!MusicManager.instance.soundSources[4].isPlaying)
				    MusicManager.instance.soundSources[4].Play();
			}
            else if(player.movementSpeed == 1.875) 
            {
				MusicManager.instance.soundSources[4].Stop();
				if (!MusicManager.instance.soundSources[3].isPlaying)
					MusicManager.instance.soundSources[3].Play();
			}
            if(movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if(movementInput.x > 0)
            {
				spriteRenderer.flipX = false;
			}
			isMoving = true;
		}
        else if (movementInput.x == 0 && movementInput.y == 0)
        {
            isMoving = false;
			MusicManager.instance.soundSources[4].Stop();
			MusicManager.instance.soundSources[3].Stop();
			animator.Play("Idle");
        }
    }

    //Prevent player from moving
    public void RestrictMovement()
    {
        movementInput = new Vector2(0, 0);
        Movable = false;
        isMoving = false;
        animator.Play("Idle");
    }
}
