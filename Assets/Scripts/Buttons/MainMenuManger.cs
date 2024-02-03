using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuManger : MonoBehaviour
{
    [SerializeField] private Image imageFade;
    [SerializeField] private GameObject menuOptions;
    [SerializeField] private GameObject menuCredits;

    public void OnClickPlay()
    {
        imageFade.DOFade(1, 2.9f).OnComplete(FadeComplete);
    }

    private void FadeComplete()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnClickOptions()
    {
        menuOptions.SetActive(true);
    }

    public void OnClickExit()
    {
        menuOptions.SetActive(false);
        menuCredits.SetActive(false);
    }

    public void OnClickCredits()
    {
        menuCredits.SetActive(true);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

}
