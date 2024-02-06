using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Audio;

public class MainMenuManger : MonoBehaviour
{
    [SerializeField] private Image imageFade;
    [SerializeField] private GameObject menuOptions;
    [SerializeField] private GameObject menuCredits;
    private AudioSource Music;
    public AudioMixer Mixer;
  

    public void OnClickPlay()
    {
        imageFade.gameObject.SetActive(true);
        imageFade.DOFade(1, 2.9f).OnComplete(FadeComplete);
       
    }

    private void FadeComplete()
    {
        SceneManager.LoadScene("Gameplay");
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
        imageFade.gameObject.SetActive(true);
        imageFade.DOFade(1,2.9f).OnComplete(FadeCompleteCredits);
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

        Mixer.SetFloat("SFX_Volume", volume);
    }


}
