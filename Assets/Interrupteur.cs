using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupteur : MonoBehaviour
{
    public GameObject platformeADetruire;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("BalleGolf"))
        {
            Debug.Log("Toucher");
   
  
            Destroy(platformeADetruire);
        }
    }
}
