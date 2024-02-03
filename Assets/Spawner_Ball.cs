using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner_Ball : MonoBehaviour
{
    public Transform Spawner;
    private Transform __all;

    // Start is called before the first frame update
    void Start()
    {
        __all = GetComponent<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            //Faudra tres probablement cancel la vitesse de la balle avant de la tp
            Reset_Position();
            
        }
    }

    public void Reset_Position()
    {
        __all.transform.position = Spawner.transform.position;
    }
}
