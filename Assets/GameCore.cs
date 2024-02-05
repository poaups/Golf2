using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCore : MonoBehaviour
{

    public Rigidbody2D rb;
    public float ForceMax;
    public float ForceMin;
    public float m_timerClick;
    public Image ImageRelaod;
    public Level CurrentLevel;
    public static int s_currentLevel;
    public Level[] Levels;
    public Lanceur_Vidéo lanceurVideoScript;
    public static float force;

    [Header("Golf Visuel")]
    public GameObject Golfeur;
    public Image i1;
    public Image i2;
    public Image i3;

    // Start is called before the first frame update
    void Start()
    {
        CurrentLevel = Levels[s_currentLevel];
        CurrentLevel.gameObject.SetActive(true);
        //lanceurVideoScript = GetComponent<Lanceur_Vidéo>();


    }

    // Update is called once per frame
    void Update()
    {

        ImageRelaod.fillAmount = m_timerClick;
        Debug.Log(m_timerClick);

        //code caca sa sert a rien mdrrr 

        //if (Input.GetMouseButtonDown(0))
        //{
        //    rb.simulated = true;
        //    rb.AddForce(new Vector2(ForceMin, ForceMax), ForceMode2D.Impulse);
        //}
        //if (rb.velocity.magnitude > 0.00001f)
        //{
        //    float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        //    rb.transform.rotation = Quaternion.Euler(0, 0, angle);
        //}

        Golfeur_Image();

        if (Input.GetMouseButton(0))
        {
            m_timerClick += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            
            if (m_timerClick > 1.0)
            {
                m_timerClick = 1;
            }

            force = Mathf.Lerp(ForceMin, ForceMax, m_timerClick);

            rb.simulated = true;
            rb.AddForce(new Vector2(force, force), ForceMode2D.Impulse);
            m_timerClick = 0;
        }


        int victoireAlive = 0;
        foreach (var item in CurrentLevel.platformVictoire)
        {
            if (item != null)
            {
                victoireAlive++;
            }
        }
        if ( victoireAlive == 0)
        {
            Debug.Log("Victoire");
            lanceurVideoScript.Allumer();

            s_currentLevel++;
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            return;
        }
    }

    //Fct qui s'occupe de l'affichage du golfeur en fct de m_timerClick
    public void Golfeur_Image()
    {
        if(m_timerClick <= 0.33)
        {
            Golfeur.GetComponent<Image>().sprite = i1.sprite;

        }
        else if (m_timerClick >= 0.33 && m_timerClick  <= 0.66)
        {
            Golfeur.GetComponent<Image>().sprite = i2.sprite;
        }
        else
        {
            Golfeur.GetComponent<Image>().sprite = i3.sprite;
        }

    }

    public static Vector2[] PreviewPhysics(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;
        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }
        return results;
    }
    private void OnDrawGizmos()
    {
        Vector2[] points = PreviewPhysics(rb, rb.transform.position, new Vector2(ForceMin / rb.mass, ForceMin / rb.mass), 200);
        Vector2[] point = PreviewPhysics(rb, rb.transform.position, new Vector2(ForceMax / rb.mass, ForceMax / rb.mass), 200);

        foreach (var item in point)
        {
            Gizmos.DrawSphere(item, 0.05f);
        }

        foreach (var item in points)
        {
            Gizmos.DrawSphere(item, 0.05f);
        }
    }
}

    // a utiliser si est si seulement on souhaite detruire quelque chose (ex enemeie une box,etc)

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (GetComponent < Rigidbody2D>().velocity.magnitude >= 0.5f)
//        {
//            GameObject.Destroy(gameObject);
//        }
//    }
//}
