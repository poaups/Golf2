using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inversement : MonoBehaviour
{
    public Rigidbody2D rb;

    public bool isInverse = false;
    public KeyCode _touche;
    public AudioSource InversmentSFX;


    void Start()
    {
        //Rigidbody du gameobject de tous les niveaux
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        InputInversement();
    }

    //True = niveau retourné / False = normal
    public void InputInversement()
    {
        if (Input.GetKeyDown(_touche))
        {
            InversmentSFX.Play();
            isInverse = !isInverse;
            InverserPosition();
        }
    }

    //Inversion du gameobject des niveaux
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
}
