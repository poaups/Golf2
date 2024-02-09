using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateforme_Projection : MonoBehaviour
{
    public float Puisasance_Projection;
    private Rigidbody2D _rb;
    public AudioSource SFX_Projection;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnCollisionEnter2D(Collision2D col) //col est l'information de la collision de qui avec qui et les points d'impact
    {
        if(col.gameObject.tag == "Projtection")
        {
            SFX_Projection.Play();
            _rb.AddForce(new Vector2(Puisasance_Projection, Puisasance_Projection), ForceMode2D.Impulse);
        }
        
    }
}
