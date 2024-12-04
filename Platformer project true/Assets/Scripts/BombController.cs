using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BombController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public SpriteRenderer sprite;
    private bool inRange = false;
    private bool collided = false;
    private GameObject player;
    
    private float timer;
    Vector3 PlayerPosition;
    Vector3 DistanceVector;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        rb.AddForce(new Vector2(250, 250));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Explosion();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.constraints =  RigidbodyConstraints2D.FreezePosition;
        collided = true;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            Debug.DrawLine(transform.position, player.transform.position);
            inRange = true;
            Debug.Log("in Range");
        }
        
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }
    public void Explosion()
    {
        
        if(collided)
        {
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                //Debug.Log("test");
                sprite.color = Color.red;
                if (inRange)
                {
                    player.SendMessage("KnockBack", player.transform.position  - transform.position);
                }
            }
            if(timer >= 2.1)
            {
                Object.Destroy(gameObject);
            }
        }
    }

  
}
