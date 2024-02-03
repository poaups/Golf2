using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChut : MonoBehaviour
{
    private Rigidbody2D rb;
   [SerializeField] private float Time = 5;
 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        DesactiverSimulation();
    }

    public void ActiverSimulation()
    {
        rb.WakeUp();
    }

    public void DesactiverSimulation()
    {
        rb.Sleep();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Chute"))
        {
            Debug.Log("Collision");
            StartCoroutine(TimerActive());

        
        }
    }
    IEnumerator TimerActive()
    {
        Debug.Log("Startdela couroutine");
        yield return new WaitForSeconds(Time);
        //Debug.Log("Ca marche un peu ");
        //yield return null;

        ActiverSimulation();

    }
}
