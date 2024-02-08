using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject MenuPause;
    [SerializeField] private Image ImageFade;
    public GameObject OptionsPauseMenu;
    private AudioSource Music;
    public AudioMixer MixerSFX;
    public AudioMixer MixerVolume;
    public Rigidbody2D rigiplayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MenuPause.SetActive(true);
            Time.timeScale = 0f;
        }
    }


    public void OnClickResume()
    {
        MenuPause.SetActive(false);
        Time.timeScale = 1f;
        GetComponent<GameCore>().m_timerClick = 0;
    }

    public void OnClickLeave()
    {
        ImageFade.DOFade(1, 2.9f).OnComplete(FadeComplete);
        Time.timeScale = 1f;
    }

    public void FadeComplete()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickExit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void OnClickOptions()
    {
        OptionsPauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnMusicValueChanged(float newValue)
    {
        Music.volume = newValue;

    }

    public void OnClickBackOptions()
    {
        OptionsPauseMenu.SetActive(false);
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
