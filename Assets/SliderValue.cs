using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{

    public TMPro.TextMeshProUGUI TextValue;
    public Slider Slider;
    public AudioMixer Mixer;
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
