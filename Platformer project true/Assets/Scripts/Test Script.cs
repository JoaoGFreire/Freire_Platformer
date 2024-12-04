using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    float acceleration = 10;
    float accelerationSpeed = 3;
    float maxSpeed = 50; 
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        acceleration = maxSpeed/accelerationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        if(Input.GetKey(KeyCode.RightArrow)) 
        {
            velocity = Vector2.right * acceleration * Time.deltaTime;
        }
        rb.velocity = velocity;
    }
}
