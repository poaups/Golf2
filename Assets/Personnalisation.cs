using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnalisation : MonoBehaviour
{
    public Sprite SpriteR_1;
    public Sprite SpriteR_2;
    public Sprite SpriteR_3;
    public Sprite SpriteR_4;

    private int _currentLevel;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _currentLevel = GameCore.s_currentLevel; //Variable int static du Gamecore qui determine l'index du niv
        //_currentLevel = Gamecore.GetComponent<GameCore>().s_currentLevel;
    }

    void Update()
    {
        if (_currentLevel == 0 || _currentLevel == 1 || _currentLevel == 2)
        {
            spriteRenderer.sprite = SpriteR_1;
        }
        else if (_currentLevel == 3 || _currentLevel == 4)
        {
            spriteRenderer.sprite = SpriteR_2;
        }
        else if (_currentLevel == 5 || _currentLevel == 6)
        {
            spriteRenderer.sprite = SpriteR_3;
        }
        else if (_currentLevel == 7 || _currentLevel == 8)
        {
            spriteRenderer.sprite = SpriteR_4;
        }
    }
}
