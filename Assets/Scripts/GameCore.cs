using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCore : MonoBehaviour
{
    public Rigidbody2D rb; //Rigibody du plaer
    public Camera cam; //Camera du player
    public AudioSource GolfSound;

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
    //public Lanceur_Vidéo lanceurVideoScript;
    public static float force;

    [Header("Golf Visuel")]
    public GameObject Golfeur;
    public Image Default_Image;
    public Image Second_Image;
    public Image Third_Image;

    RaycastHit2D hit;
    public GameObject Player;
    public GameObject Maprefabs; //Prefabs feedback tire

    //Liste ou il y'a aura tout les prefabs pour le feedback du tire
    private List<GameObject> instantiatedPrefabs = new List<GameObject>();
    private bool _canShoot;


    void Start()
    {

        CurrentLevel = Levels[s_currentLevel];
        CurrentLevel.gameObject.SetActive(true);
    }

    //Pas de saut en l'air grace a un raycast
    private void FixedUpdate()
    {
        //Player.transform.position le rayon commence de la, puis la direciotn, puis la taille du rayon et facultatif(le Layer sur les quelle ca fonctionne)
        hit = Physics2D.Raycast(Player.transform.position, Vector3.down, 0.50f, LayerMask.GetMask("Default"));
        if (hit.collider != null)
        {
            //Affiche que dans l'editeur(Player.transform.position commencement, hit.point = impact du rayon, - un new vector2(player.x,player.y) car le player a 3 vecteur alors)
            //On en cree un vecteur2() avec que le x et le y et on le soustrait pour avoir la distance entre l'imapct et la balle puis la couleur
            _canShoot = true;
            Debug.DrawRay(Player.transform.position, hit.point - new Vector2(Player.transform.position.x, Player.transform.position.y), Color.red);
            

        }
        else
        {
            _canShoot = false;
            Debug.DrawRay(Player.transform.position, -Vector3.up, Color.yellow);
        }
    }

    

    void Update()
    {
        
        //Feedback Reload du tire
        ImageRelaod.fillAmount = m_timerClick;

        Golfeur_Image();
        Puissance();
        Camera();
        DestroyGuizmos();
        DisableGolfeur();

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
       
            //lanceurVideoScript.Allumer();

            s_currentLevel++;
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            return;
        }
        else if (s_currentLevel == 8)
        {
            SceneManager.LoadScene("SceneWin");
        }
    }

    public void DisableGolfeur()
    {
        if(s_currentLevel >= 1)
        {
            Golfeur.SetActive(false);
        }
    }

    //Fct qui s'occupe de l'affichage du golfeur en fct de m_timerClick 
    public void Golfeur_Image()
    {
        //m_timerClick etant bloqué a 1 max
        if (m_timerClick <= 0.33)
        {
            Golfeur.GetComponent<Image>().sprite = Default_Image.sprite;

        }
        else if (m_timerClick >= 0.33 && m_timerClick <= 0.66)
        {
            Golfeur.GetComponent<Image>().sprite = Second_Image.sprite;
        }
        else
        {
            Golfeur.GetComponent<Image>().sprite = Third_Image.sprite;
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
            GolfSound.Play();
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
            cam.orthographicSize = 12 + m_timerClick;
        }
        else
        {
            //Si je peux pas tirer je reset la position de la camera + m_timerClick
            cam.orthographicSize = 12;
            m_timerClick = 0;
        }


    }

    //Calcule de la trajection de la preview
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

    public void DestroyGuizmos()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // Supprimer les 20 derniers prefabs
            int numToDestroy = Mathf.Min(20, instantiatedPrefabs.Count); // Nombre de prefabs ? supprimer
            for (int i = 0; i < numToDestroy; i++)
            {
                int lastIndex = instantiatedPrefabs.Count - 1;
                Destroy(instantiatedPrefabs[lastIndex]);
                instantiatedPrefabs.RemoveAt(lastIndex);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (m_timerClick > 0.10f)
        {
            Vector2[] points = PreviewPhysics(rb, rb.transform.position, new Vector2(m_timerClick / rb.mass * 12, m_timerClick / rb.mass * 12), 200);

            int pointsToShow = 20;

            if (m_timerClick <= 0.30f)
            {
                foreach (var prefab in instantiatedPrefabs)
                {
                    Destroy(prefab);
                }
                instantiatedPrefabs.Clear();
                return;
            }

            for (int i = instantiatedPrefabs.Count - 1; i >= Mathf.Max(0, instantiatedPrefabs.Count - pointsToShow); i--)
            {
                Destroy(instantiatedPrefabs[i]);
                instantiatedPrefabs.RemoveAt(i);
            }

            for (int i = 0; i < pointsToShow && i < points.Length; i++)
            {
                GameObject instantiatedPrefab = Instantiate(Maprefabs, points[i], Quaternion.identity);
                instantiatedPrefabs.Add(instantiatedPrefab);
            }

        }
    }

    //Renitialisation de la Velocité
    public void Reset_Velocity()
    {
        rb.velocity = new Vector2(0, 0);
    }


}