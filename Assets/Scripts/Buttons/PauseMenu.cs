using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject MenuPause;
    [SerializeField] private Image ImageFade;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            MenuPause.SetActive(true);
            Time.timeScale = 0f;
        }
    }


    public void OnClickResume()
    {
        Time.timeScale = 1f;
        MenuPause.SetActive(false);

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
}
