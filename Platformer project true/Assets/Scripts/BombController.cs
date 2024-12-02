using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class BombController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private bool inRange = false;
    public GameObject player;
    public float KnockBackForce;
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
        PlayerPosition = player.transform.position;
        //rb.AddForce(new Vector2(20, 20));
        Explosion();
        Debug.DrawLine(transform.position, PlayerPosition);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //rb.constraints =  RigidbodyConstraints2D.FreezePosition;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
        if (inRange)
        {
            player.SendMessage("KnockBack", PlayerPosition - transform.position);
        }
    }

  
}
