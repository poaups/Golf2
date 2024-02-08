using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inversement : MonoBehaviour
{
    public Rigidbody2D rb;

    private bool isInverse = false;
    
    void Start()
    {
        //Rigidbody du gameobjecy de tous les niveaux
        rb = GetComponent<Rigidbody2D>();
        isInverse = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) 
        {
            InverserPosition();
        }
    }

    //Inversion du gameobject des niveaux
    public void InverserPosition()
    {
        isInverse = !isInverse;
        if (isInverse)
        {
            transform.localScale = new Vector3(1f, -1f, 1f);
           
            
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
