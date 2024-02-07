using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Lanceur_Vidéo : MonoBehaviour
{
    public GameObject videoPlayerObject;
    public RawImage   Raw;
    public KeyCode    key;
    public bool       On_Off = false;

    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = videoPlayerObject.GetComponent<VideoPlayer>();
        videoPlayerObject.SetActive(true);
        Raw.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (!videoPlayerObject.activeSelf)
            {
                Allumer();
            }
            else
            {
                Eteindre();
            }
        }
    }

    public void Allumer()
    {
        On_Off = true;
        Raw.gameObject.SetActive(true);
        videoPlayerObject.SetActive(true);

        videoPlayer.loopPointReached += OnVideoFinished; //cet evenement est delanché quand la video est a sa fin
    }

    void Eteindre()
    {
        On_Off = false;
        Raw.gameObject.SetActive(false);
        videoPlayerObject.SetActive(false);
    }

    // Méthode appelée lorsque la vidéo est terminée
    void OnVideoFinished(VideoPlayer vp)
    {
        //print("Vidéo terminée");
        Eteindre();
        
    }
}
