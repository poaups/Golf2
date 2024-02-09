using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{

    [SerializeField] private Color ColorInital;
    [SerializeField] private Color ColorSelected;
    [SerializeField] private TextMeshProUGUI Text;
    
    public void OnPointerEnter()
    {
        Text.DOKill();
        Text.DOColor(ColorSelected, 0.2f);
    }

    public void OnPointerExit()
    {
        Text.DOKill();
        Text.DOColor(ColorInital, 0.2f);
    }
}
