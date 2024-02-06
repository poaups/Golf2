using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoirePlateform : MonoBehaviour
{
    public GameObject LanceurVideo;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Victoire"))
        {
            LanceurVideo.GetComponent<Lanceur_Vidéo>().Allumer();
            GameObject.Destroy(gameObject);
            //if (LanceurVideo.GetComponent<Lanceur_Vidéo>().On_Off == false)
            //{
            //    GameObject.Destroy(gameObject);
            //}
        }
    }
}
