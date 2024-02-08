using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformDeath : MonoBehaviour
{
    public Transform Spawner;
    public GameObject MainCamera;
    public GameObject Gamecore;
    public SpriteRenderer SpriteRenderer2;
    public GameObject GameDesing;

    private Transform __all;
    private Transform ballTransform;
    
    void Start()
    {
        
        __all = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Balle")
        {
            SpriteRenderer2.color = new Color(1f, 1f, 1f, 0f); // Opacité a 0
            MainCamera.GetComponent<Shake>().start = true;
            Reset_Position();
            Gamecore.GetComponent<GameCore>().Reset_Velocity();   //Fct Reset_Velocity du script GameCore qui annule la vitesse
            //Reverse le niveau
            
            GameDesing.GetComponent<Inversement>().InverserPosition();
            print("reverselvl");

        }
    }

    //Baisse de l'opacité, teleportation au Spawner posé dans la scene
    public void Reset_Position()
    {
        SpriteRenderer2.color = new Color(1f, 1f, 1f, 1f); // Opacité a 100
        __all.transform.position = Spawner.transform.position;
    }
}
