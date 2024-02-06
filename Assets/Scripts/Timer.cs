using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Timer : MonoBehaviour
{
    public Animator animator;
    private float hasPlayed = 0;
    public Image imageFade;


    [Header("Component")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerAnimation;

    [Header("TimerSettings")]
    public float currentTime;
    public bool countDown;

    [Header("LimitSettings")]
    public bool hasLimit;
    public float timerLimit;
    public float timerAlarm;
    public float save_currenttime;

    [Header("FormatSettings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    void Start()
    {
        save_currenttime = currentTime;


        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethsDecimal, "0.00");
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText ();
            enabled = false;
            ResetTimer();
            //imageFade.DOFade(1, 2.9f).OnComplete(() => ResetTimer());
        }
        
        SetTimerText();
        if (currentTime < timerAlarm && hasPlayed == 0)
        {
            timerText.color = Color.red;
            Debug.Log("ehfsieufhseuf");
            animator.SetTrigger("Move");
            hasPlayed = 1;
            
        }
    }

    private void SetTimerText()
    {
        timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
    }
    
   
    public void ResetTimer()
    {
        currentTime = save_currenttime;

    }

    

    //public void MoveText()
    //{
    //    if (currentTime <= timerAlarm)
    //    {
    //        Animator animator = timerText.GetComponent<Animator>();
    //        if (animator != null)
    //        {
    //            bool isMove = animator.GetBool("Move");

    //            animator.SetBool("Move", !isMove);
    //        } 
            








    //    }
    //}


}

public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundrethsDecimal,
}