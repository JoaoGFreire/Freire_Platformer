/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum FacingDirection
    {
        left, right
    }

    public float accelerationTime;
    public float decelerationTime;
    public float maxSpeed;

    public int health = 10;

    private Rigidbody2D playerRB;
    private float acceleration;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        accleration = maxSpeed/accelerationTime;
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        Vector2 playerInput = new Vector2();
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerInput += Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerInput += Vector2.right;
        }
        MovementUpdate(playerInput);
    }



    private void MovementUpdate(Vector2 playerInput)
    {
        Vector2 velocity = playerRB.velocity;
        Debug.Log(playerInput.ToString());
        if (playerInput.magnitude > 0)
        {
            velocity += playerInput * acceleration * Time.deltaTime;
        }
        else
        {
            velocity = new Vector2(0, velocity.y);
        }

        playerRB.velocity = velocity;
    }

    public bool IsWalking()
    {
        return false;
    }
    public bool IsGrounded()
    {
        return true;
    }

    public bool IsDead()
    {
        //return health <= 0;
    }

    public FacingDirection GetFacingDirection2()
    {
        return FacingDirection.left;
    }
}
*/
