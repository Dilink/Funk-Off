﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_ComboManager : MonoBehaviour
{
    public List<float> Multipliers = new List<float>(5);
    private float funkBonus = 0;
    private bool firstMult;
    int comboedPatternSpot=100;


    [Header("Colors pattern background")]
    public Color colorNone = Color.white;
    [Space]
    public Color colorX2 = Color.white;
    public Color colorX3 = Color.white;
    public Color colorX4 = Color.white;
    public Color colorX5 = Color.white;
    public Color colorX6 = Color.white;

    [Header("Colors Multiplier icon background")]
    public Color colorMX2 = Color.white;
    public Color colorMX3 = Color.white;
    public Color colorMX4 = Color.white;
    public Color colorMX5 = Color.white;
    public Color colorMX6 = Color.white;

    private void Awake()
    {
        funkBonus = 0;
        GameManager.Instance.uiManager.ClearAllMultiplierUi();
    }



    //FUNK MULTIPLIER SET
    public void SetFunkMultiplier(float newModifier)
    {
        funkBonus = newModifier;
    }

    public float getFunkMultiplier()
    {
        return funkBonus;
    }

    public void ResetMultiplier()
    {
        SetFunkMultiplier(0);
    }


    public void OnNewTurn(int indexOfPatern, bool isPatternDestroyed = false)
    {
        GameManager.Instance.uiManager.RemoveAllMultiplierIcon();

        if (isPatternDestroyed)
            return;

        int multiplierIndex = -1;

        if(indexOfPatern == comboedPatternSpot)
        {
            if (funkBonus < Multipliers[0])
            {
                multiplierIndex = 0;
            }
            else if (funkBonus < Multipliers[1])
            {
                multiplierIndex = 1;
            }
            else if (funkBonus < Multipliers[2])
            {
                multiplierIndex = 2;
            }
            else if (funkBonus < Multipliers[3])
            {
                multiplierIndex = 3;
            }
            else if (funkBonus < 100)
            {
                multiplierIndex = 4;
            }
        }
        else
        {
            multiplierIndex = 0;
        }

        comboedPatternSpot = indexOfPatern;

        if (multiplierIndex > -1)
        {
            if (indexOfPatern == comboedPatternSpot)
            {
                funkBonus = Multipliers[multiplierIndex];
            }

            GameManager.Instance.musicManager.PlayLayer(multiplierIndex + 1);

            GameManager.Instance.uiManager.DisplayFX(indexOfPatern, multiplierIndex);

            GameManager.Instance.soundManager.PlaySound(GameSound.S_MultiplierAppear);

            switch(multiplierIndex)
            {
                case 0:
                    GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, colorMX2, colorX2 , "Good");
                    break;

                case 1:
                    GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, colorMX3, colorX3, "Nice");
                    break;

                case 2:
                    GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, colorMX4, colorX4, "Great");
                    break;

                case 3:
                    GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, colorMX5, colorX5, "Perfect");
                    break;

                case 4:
                    GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, colorMX6, colorX6, "Funkulouss");
                    break;
            }

        }
        else
        {
            GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, Color.white, Color.red, "error");
        }
        
        GameManager.Instance.UpdateFeedBackAutourGrid(multiplierIndex+1);

    }
}
