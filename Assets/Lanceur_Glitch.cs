using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanceur_Glitch : MonoBehaviour
{
    public GameObject Glitch;
    public float Waiting;

    void Start()
    {
        Glitch.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Activation()
    {
        if(!Glitch.activeSelf)// .activeSelf s'auto detecte son activation (true / false)
        {
        
            StartCoroutine(Tempsactivation());
            
        }
        else
        {
            Glitch.SetActive(false);
        }
    }

    //Activation + temps
    IEnumerator Tempsactivation()
    {
        Glitch.SetActive(true);
        yield return new WaitForSeconds(Waiting);
        Glitch.SetActive(false);
    }
}
