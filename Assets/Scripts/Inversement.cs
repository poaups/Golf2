using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inversement : MonoBehaviour
{
    private bool isInverse = false;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();     
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) 
        {
            isInverse = !isInverse;

            InverserPosition();
            //InvertGravity();
        }
    }

    public void InverserPosition()
    {
        if (isInverse)
        {
            transform.localScale = new Vector3(1f, -1f, 1f);
           
            
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    //public void InvertGravity()
    //{
    //    Vector2 currentGravity = Physics2D.gravity;
    //    Physics2D.gravity = new Vector2(currentGravity .x, -currentGravity.y);
    //    rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
    //}
}
