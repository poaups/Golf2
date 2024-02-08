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
        if (collision.gameObject.tag == ("Victoire")) 
        {
            LanceurVideo.GetComponent<Lanceur_Glitch>().Activation();//Lancement glitch
            GameObject.Destroy(gameObject);//detruire la plateforme de victoire
        }
    }
}
