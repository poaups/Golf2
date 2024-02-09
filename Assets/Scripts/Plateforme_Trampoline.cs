using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Plateforme_Trampoline : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float intensiteRebond = 2f; // Ajustez cette valeur pour modifier l'intensité du rebond
    public AudioSource SFXTranpoline;

    void Start()
    {
        //_rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Trampoline")
        {
            SFXTranpoline.Play();
            //// Appliquer la force de rebond en fonction de la vélocité de la balle
            //Vector2 rebondForce = -_rb.velocity * intensiteRebond;
            //_rb.AddForce(rebondForce, ForceMode2D.Impulse);
        }
    }
}
