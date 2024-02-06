using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCore : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam; //Camera du player;
    private bool _canShoot;

    [Header("Power Parameters")]
    public float ForceMax;
    public float ForceMin;
    public float m_timerClick;
    public Image ImageRelaod;

    [Header("Levels Parameters")]
    public Level CurrentLevel;
    public static int s_currentLevel;
    public Level[] Levels;

    [Header("Vidéo Parameters")]
    public Lanceur_Vidéo lanceurVideoScript;
    public static float force;

    [Header("Golf Visuel")]
    public GameObject Golfeur;
    public SpriteRenderer Default_Image;
    public SpriteRenderer Second_Image;
    public SpriteRenderer Third_Image;

    RaycastHit2D hit;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        CurrentLevel = Levels[s_currentLevel];
        CurrentLevel.gameObject.SetActive(true);
        //lanceurVideoScript = GetComponent<Lanceur_Vidéo>();


    }
    private void FixedUpdate()
    {
        //Player.transform.position le rayon commence de la, puis la direciotn, puis la taille du rayon et facultatif(le Layer sur les quelle ca fonctionne)
        hit = Physics2D.Raycast(Player.transform.position, Vector3.down, 0.35f, LayerMask.GetMask("Default"));
        if (hit.collider != null)
        {
            //Affiche que dans l'editeur(Player.transform.position commencement, hit.point = impact du rayon, - un new vector2(player.x,player.y) car le player a 3 vecteur alors)
            //On en cree un vecteur2() avec que le x et le y et on le soustrait pour avoir la distance entre l'imapct et la balle puis la couleur
            _canShoot = true;
            Debug.DrawRay(Player.transform.position, hit.point - new Vector2(Player.transform.position.x, Player.transform.position.y), Color.red);
            Debug.Log("Did hit : " + hit.collider.name);

        }
        else
        {
            _canShoot = false;
            Debug.DrawRay(Player.transform.position, -Vector3.up, Color.yellow);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_canShoot + "can shoot");

        ImageRelaod.fillAmount = m_timerClick;
        Debug.Log(m_timerClick);

        Golfeur_Image();
        Puissance();
        Camera();

        int victoireAlive = 0;
        foreach (var item in CurrentLevel.platformVictoire)
        {
            if (item != null)
            {
                victoireAlive++;
            }
        }
        if (victoireAlive == 0)
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
        //m_timerClick etant bloqué a 1 max
        if (m_timerClick <= 0.33)
        {
            Golfeur.GetComponent<SpriteRenderer>().sprite = Default_Image.sprite;

        }
        else if (m_timerClick >= 0.33 && m_timerClick <= 0.66)
        {
            Golfeur.GetComponent<SpriteRenderer>().sprite = Second_Image.sprite;
        }
        else
        {
            Golfeur.GetComponent<SpriteRenderer>().sprite = Third_Image.sprite;
        }

    }

    //Fct qui gere la puissance (force), et m_timerClick pour les jauges (Golfeur + Feedback tire)
    public void Puissance()
    {
        //Incrementation m_timerClick 
        if (Input.GetMouseButton(0) && m_timerClick <= 1)
        {
            m_timerClick += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0) && _canShoot == true)
        {

            force = Mathf.Lerp(ForceMin, ForceMax, m_timerClick);

            rb.simulated = true;
            rb.AddForce(new Vector2(force, force), ForceMode2D.Impulse);
            m_timerClick = 0;
        }
    }

    //Fct qui gere le dezoom de la camera
    public void Camera()
    {

        if (_canShoot == true)
        {
            //cam.orthographicSize = orthographic = type de la camera, Size = le Zoom
            //Par defaut le size de la camera est a 10 alors je l'additione a m_timerClick
            cam.orthographicSize = 10 + m_timerClick;
        }
        else
        {
            //Si je peux pas tirer je reset la position de la camera + m_timerClick
            cam.orthographicSize = 10;
            m_timerClick = 0;
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