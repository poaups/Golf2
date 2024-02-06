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
    public AudioMixer MixerSFX;
    public AudioMixer MixerVolume;
  

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
