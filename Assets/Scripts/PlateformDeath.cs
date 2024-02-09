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
    public AudioSource DeadSFX;

    private Transform __all;
    //private Transform ballTransform;
    
    void Start()
    {
        
        __all = GetComponent<Transform>();
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Balle")
        {
            DeadSFX.Play();
            SpriteRenderer2.color = new Color(1f, 1f, 1f, 0f); // Opacit� a 0
            MainCamera.GetComponent<Shake>().start = true;
            Reset_Position();
            Gamecore.GetComponent<GameCore>().Reset_Velocity();         //Fct Reset_Velocity du script GameCore qui annule la vitesse
            GameDesing.GetComponent<Inversement>().isInverse = false;   //Variable qui change la gravit� des niv dans le script Inversement
            GameDesing.GetComponent<Inversement>().InverserPosition();  //Fct appel� qui active l'inversion de gravit� dans le script Inversement

        }
    }

    //Baisse de l'opacit�, teleportation au Spawner pos� dans la scene
    public void Reset_Position()
    {
        SpriteRenderer2.color = new Color(1f, 1f, 1f, 1f); // Opacit� a 100
        __all.transform.position = Spawner.transform.position;
    }
}
