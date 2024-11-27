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


    public enum FacingDirection
    {
        left, right
    }

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
        else if(Input.GetKey(KeyCode.A)) //if player input key A, sets the input vector to be -1,0 signifying that it is moving left
        {
            moving = true; //sets the moving bollean to true to show that its moving
            input += new Vector2(-1, 0);
        }
        else
        {
            moving = false;
        }

        
        //gravity force applied on the player
        rb.AddForce(-transform.up * (-Gravity));


        if (Input.GetKeyDown(KeyCode.Space))
        {
            input += new Vector2(0, 1);
        }

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

        //Vector2 currentVelocity = Rigidbod.velocity;
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    currentVelocity += acceleration * Vector2.left * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    currentVelocity += acceleration * Vector2.right * Time.deltaTime;
        //}
        //Rigidbod.velocity = currentVelocity;
        
        
        
        
        if(playerInput.x> 0)
        {
            rb.AddForce(transform.right * velocity);
            Debug.Log("test");
        }
        else if(playerInput.x< 0) { rb.AddForce(-transform.right * velocity); }

    }
    
    public void MovementUpdateVertical(Vector2 playerInput)
    {
        if (CoyoteTimer  > 0 && playerInput.y > 0) //if the 0.2 seconds granted by the coyote time have yet to pass and player input is right                               
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            CoyoteTimer = 0f;
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
    //acceleration
    //velocity
    //is velocity greater than 0

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
        if (Physics2D.Raycast(transform.position, -transform.up, 1,groundLayerMask))
        {
            //Debug.Log("its touching");
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
}
