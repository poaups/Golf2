using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float TimeLeft;
    public bool TimerOn = false;

    public TMP_Text TimerTXT;
    // Start is called before the first frame update
    void Start()
    {
        TimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                UpdateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Time is Up");
                TimeLeft = 0;
                TimerOn = false;
            }
        }
    }
    void UpdateTimer(float currentTime)
    {
        currentTime += 1; 
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float secondes = Mathf.FloorToInt(currentTime % 60);

        TimerTXT.text = string.Format("(0:00) : (1:00)", minutes, secondes);
    }
}
