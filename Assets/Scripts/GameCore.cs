using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCore : MonoBehaviour
{
    public Rigidbody2D rb; //Rigibody du plaer
    public Camera cam; //Camera du player
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
    //public Lanceur_Vidéo lanceurVideoScript;
    public static float force;

    [Header("Golf Visuel")]
    public GameObject Golfeur;
    public SpriteRenderer Default_Image;
    public SpriteRenderer Second_Image;
    public SpriteRenderer Third_Image;

    RaycastHit2D hit;
    public GameObject Player;
    public GameObject Maprefabs;
    // Start is called before the first frame update
    void Start()
    {
        // Obtenir le nom de la scène actuelle
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

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
        //if(Input.GetKeyDown(KeyCode.N))
        //{
        //    OnDrawGizmos();
        //}

        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    DestroyGizmos();
        //}


 

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
       
            //lanceurVideoScript.Allumer();

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

    ////Affichage Gizmo 
    //private void OnDrawGizmos()
    //{
    //    Vector2[] points = PreviewPhysics(rb, rb.transform.position, new Vector2(m_timerClick / rb.mass * 12, m_timerClick / rb.mass * 12), 200);

    //    int maxPrefabs = 0; // Déplacer la déclaration et l'initialisation en dehors de la boucle

    //    foreach (var point in points)
    //    {
    //        if (maxPrefabs < 30) // Vérifier si le nombre d'instances créées est inférieur à 30
    //        {
    //            Instantiate(Maprefabs, point, Quaternion.identity);
    //            maxPrefabs++; // Incrémenter le compteur après avoir instancié un prefab
    //        }
    //    }
    //}

    //private void DestroyGizmos()
    //{
    //    // Récupérer tous les objets de la scène
    //    GameObject[] allObjects = SceneManager.GetActiveScene().GetRootGameObjects();

    //    // Parcourir tous les objets de la scène
    //    foreach (GameObject obj in allObjects)
    //    {
    //        // Faire quelque chose avec chaque objet, par exemple, afficher leur nom
    //        Debug.Log("Nom de l'objet : " + obj.name);
    //    }
    //    //if (obj.name == "r(Clone)")
    //    //{
    //    //    Debug.Log("Objet à détruire trouvé !");
    //    //    Destroy(obj);
    //    //}
    //    //GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Prefabs");
    //    //foreach (var obj in objectsWithTag)
    //    //{
    //    //    Debug.Log("Objet trouvé avec le nom : " + obj.name);
    //    //    if (obj.name == "r(Clone)")
    //    //    {
    //    //        Debug.Log("Objet à détruire trouvé !");
    //    //        Destroy(obj);
    //    //    }
    //    //}
    //}


    //Renitialisation de la Velocité
    public void Reset_Velocity()
    {
        rb.velocity = new Vector2(0, 0);
    }



//public void CircleOfPoint()
//    {
//        if (pointsNumbers > 30)
//        {
//            pointsNumbers = 30;
//        }
//        if (pointsAreActive == false)
//        {
//            float valueAngleEachPoint = 360f / pointsNumbers;

//            for (int i = 0; i < pointsNumbers; i++)
//            {
//                float angle = i * valueAngleEachPoint * Mathf.Deg2Rad; //Mathf.Deg2Rad = (PI * 2)/360 ° = ((2 * PI) / 360) * (180 / PI) = (2/360) * 180 = 1°

//                Vector2 pointPosition = new Vector2(transform.position.x + Mathf.Cos(angle) * 3, transform.position.y + Mathf.Sin(angle) * 3);
//                GameObject point = Instantiate(PointArround, pointPosition, Quaternion.identity);

//                //Gizmos.DrawSphere(pointPosition, 0.05f);
//            }
//            pointsAreActive = true;
//        }
//    }
//.

//SUPPRIMER LE CERCLE

//GameObject[] PointsToDestroy = GameObject.FindGameObjectsWithTag("PointGizmo");
//foreach (GameObject obj in PointsToDestroy)
//{
//        if (obj.name == "PointGizmo(Clone)")
//        {
//                 Destroy(obj);
//}
//}

}