using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupteur : MonoBehaviour
{
    public GameObject plateformeADetruire;
    public AudioSource InterrupteurSFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Victoire"))
        {
            //InterrupteurSFX.Play();
            Debug.Log("Toucher");

            Destroy(plateformeADetruire);

        }
    }
}
