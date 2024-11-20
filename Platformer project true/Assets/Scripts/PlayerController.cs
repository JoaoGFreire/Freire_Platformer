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
    public LayerMask groundLayerMask;

    private float acceleration;

    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        acceleration = maxSpeed / TimeToReachMaxSpeed;
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
        if (IsGrounded())
        {
            Debug.Log("grounded");
        }
        //Debug.Log(rb.velocity.y);
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
        //LayerMask layer = LayerMask.GetMask("Ground");
        //Vector3 downward = raycastStart.TransformDirection(Vector3.down) * 0.8f;
        //Debug.DrawRay(raycastStart.position,downward , Color.red);
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
