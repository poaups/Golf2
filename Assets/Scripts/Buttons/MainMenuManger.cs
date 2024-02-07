using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Audio;

public class MainMenuManger : MonoBehaviour
{
    public AudioMixer MixerSFX;
    public AudioMixer MixerVolume;


    [SerializeField] private Image imageFade;
    [SerializeField] private GameObject menuOptions;
    [SerializeField] private GameObject menuCredits;
    private AudioSource Music;
    
    [Header("Animation Credits")]
    public GameObject Background;
    public GameObject Grid;
    public GameObject Back_Button;


    public void OnClickPlay()
    {
        imageFade.gameObject.SetActive(true);
        imageFade.DOFade(1, 2.9f).OnComplete(FadeComplete);
       
    }

    private void FadeComplete()
    {
        SceneManager.LoadScene("SceneTestCameraz");
    }

    public void OnClickOptions()
    {
        imageFade.gameObject.SetActive(true);
        imageFade.DOFade(1, 2.9f).OnComplete(FadeCompleteOptions);
        

    }
    public void FadeCompleteOptions()
    {
        menuOptions.SetActive(true);
        imageFade.gameObject.SetActive(false);
    }

    public void OnClickExit()
    {

        menuOptions.SetActive(false);
        menuCredits.SetActive(false);
    }
    

    public void OnClickCredits()
    {
        //Animator Credits
        Animator animator_Credits = Background.GetComponent<Animator>();
        if(animator_Credits != null)
        {
            bool Isopen = animator_Credits.GetBool("Open");

            animator_Credits.SetBool("Open", !Isopen);
        }
        

        //Animator Grid
        Animator animator_Grid = Grid.GetComponent<Animator>();
        if (animator_Grid != null)
        {
            bool Isopen_Grid = animator_Grid.GetBool("Open_Grid");

            animator_Grid.SetBool("Open_Grid", !Isopen_Grid);
        }

        // Animator BackButton
        Animator animator_BackButton = Back_Button.GetComponent<Animator>();
        if (animator_BackButton != null)
        {
            bool Isopen_BackButton = animator_BackButton.GetBool("Open_BackButton");
            animator_BackButton.SetBool("Open_BackButton", !Isopen_BackButton);
        }

        //print("terminé2");
        //imageFade.gameObject.SetActive(true);
        //imageFade.DOFade(1, 2.9f).OnComplete(FadeCompleteCredits);

    }
    public void FadeCompleteCredits()
    {
        menuCredits.SetActive(true);
        imageFade.gameObject.SetActive(false);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
    
    public void OnMusicValueChanged(float newValue)
    {
        Music.volume = newValue;

    }


    public void OnSFXValueChanged(float newValue)
    {
        if (newValue < 0.01f)
        {
            newValue = 0.01f;
        }

        float volume = Mathf.Log10(newValue) * 20;
        PlayerPrefs.SetFloat("SFX_Volume", newValue);// permet de recuper apres les preference du player en change Set par Get. 
        MixerSFX.SetFloat("SFX_Volume", volume);
    }

    public void OnVolumeValueChanges(float newValue)
    {
        if (newValue < 0.01f)
        {
            newValue = 0.01f;
        }
        float volume = Mathf.Log10(newValue) * 20;

        MixerVolume.SetFloat("Volume_Volume", volume);
    }



}
