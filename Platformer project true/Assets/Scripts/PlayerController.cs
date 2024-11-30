using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float velocity;
    Vector2 input;
    bool moving;
    public Transform raycastStart;
    private Rigidbody2D Rigidbod;


    



    public float maxSpeed;
    public float TimeToReachMaxSpeed;
    public float timetoDecelerate;

    public LayerMask groundLayerMask;

    private float acceleration;
    private float deceleration;

    //Week 11 Journal
    float Gravity;
    float jumpVelocity;
    public float terminalVelocity;
    public float coyoteTime;
    private float CoyoteTimer;
    
    
    public float apexHeight;
    public float apexTime;

    public int health = 10;

    public bool jumping;

    //Final Assignment 
    private float DoubleJumpCounter = 0;
 
    public enum FacingDirection
    {
        left, right
    }
    public enum CharacterState
    {
        idle,walk,jump,die
    }
    public CharacterState currentCharacterState = CharacterState.idle;
    public CharacterState previousCharacterState = CharacterState.idle;

    // Start is called before the first frame update
    void Start()
    {
        acceleration = maxSpeed / TimeToReachMaxSpeed;
        deceleration = maxSpeed / timetoDecelerate;


        //equation for gravity. negative 2 times the apex height divided by the apex time to the power of two
        Gravity = -2 * apexHeight / (Mathf.Pow(apexTime, 2));
        //equation for the jump velocity. two times the apex height divided by the apex time
        jumpVelocity = 2 * apexHeight / apexTime;

       

        //Rigidbod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        input = Vector2.zero;
        if (Input.GetKey(KeyCode.D)) //if player inputed key D, sets the input vector to be 1,0 signifying that it is moving right
        {
            moving = true; //sets the moving boolean to true to show that its moving
            input += new Vector2(1, 0);
        }
        else if (Input.GetKey(KeyCode.A)) //if player input key A, sets the input vector to be -1,0 signifying that it is moving left
        {
            moving = true; //sets the moving bollean to true to show that its moving
            input += new Vector2(-1, 0);
        }
        else
        {
            moving = false;
        }

        previousCharacterState = currentCharacterState;


        if (Input.GetKeyDown(KeyCode.Space) /*&& IsGrounded()*/)
        {
            jumping = true;
        }
        if (IsGrounded())
        {
            DoubleJumpCounter = 0;
        }

        Debug.Log(DoubleJumpCounter);


        switch (currentCharacterState)
        {
            case CharacterState.die:
                //do nothing
                break;
            case CharacterState.jump:
                if (IsGrounded())
                {
                    if (IsWalking())
                    {
                        currentCharacterState = CharacterState.walk;

                    }
                    else
                    {
                        currentCharacterState = CharacterState.idle;
                    }
                }

                break;
            case CharacterState.walk:
                if (!IsWalking())
                {
                    currentCharacterState = CharacterState.idle;
                }
                if (!IsGrounded())
                {
                    currentCharacterState = CharacterState.jump;
                }

                break;
            case CharacterState.idle:
                //walking
                if (IsWalking())
                {
                    currentCharacterState = CharacterState.walk;
                }
                //jumping
                if (!IsGrounded())
                {
                    currentCharacterState = CharacterState.jump;
                }

                break;
        }
        
        //gravity force applied on the player
        rb.AddForce(-transform.up * (-Gravity));

        MovementUpdateVertical(input);
        CoyoteTimeController();

    }
    private void FixedUpdate()
    {
        Vector2 playerInput = input;
        MovementUpdate(input);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        if(playerInput.x> 0)
        {
            rb.AddForce(transform.right * velocity);
            Debug.Log("test");
        }
        else if(playerInput.x< 0) { rb.AddForce(-transform.right * velocity); }

    }
    
    public void MovementUpdateVertical(Vector2 playerInput)
    {
        if (/*oyoteTimer  > 0 &&*/ jumping && DoubleJumpCounter < 1) //if the 0.2 seconds granted by the coyote time have yet to pass and player input is right                               
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            CoyoteTimer = 0f;
            jumping = false;
            DoubleJumpCounter++;
        }


        //checks whether the current vertical velocity is greater than the terminal velocity
        if (rb.velocity.y < terminalVelocity)
        {
            //if it is then set the vertical velocity to be the terminal velocity
            rb.velocity = new Vector2(rb.velocity.x, terminalVelocity);
        }
    }

    private void CoyoteTimeController()
    {
        //if it is grounded it will reset the counter
        if(IsGrounded())
        {
            CoyoteTimer = coyoteTime;
        }
        else //if it isnt it will start the countdown
        {
            CoyoteTimer -= Time.deltaTime;
        }
    }
   

    public bool IsWalking()
    {
        if(moving)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    public bool IsGrounded()
    {
        
        if (Physics2D.Raycast(transform.position, -transform.up, 0.8f,groundLayerMask))
        {
            
            return true;
        }
        return false;
    }

    public FacingDirection GetFacingDirection()
    {
       if(input.x < 0)
        {
            return FacingDirection.left;
        } 
       else { return FacingDirection.right; }
     
    }
    public bool IsDead()
    {
        return health <= 0;
    }
    public void OnDeathAnimationComplete()
    {
        gameObject.SetActive(false);
    }
}
