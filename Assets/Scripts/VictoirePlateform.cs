using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoirePlateform : MonoBehaviour
{
    public GameObject LanceurVideo;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Victoire")) //Victoire etant le tag de la balle
        {
            LanceurVideo.GetComponent<Lanceur_Glitch>().Activation();
        }
    }
}
