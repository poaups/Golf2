using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{

    public TMPro.TextMeshProUGUI TextValue;
    public Slider Slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnValueChanged(float newValue)
    {
        int valueInt = (int)Mathf.Round(newValue * 100.0f);
        TextValue.text = valueInt.ToString();
    }


    
}
