using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public Image Fade;

    public void OnClickWinGame()
    {
        Fade.DOFade(1, 2.9f).OnComplete(FadeComplete);
    }

    public void FadeComplete()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
