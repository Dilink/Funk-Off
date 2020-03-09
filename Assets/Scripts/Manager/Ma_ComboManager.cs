using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_ComboManager : MonoBehaviour
{
    public List<float> Multipliers = new List<float>(5);
    private float funkMultiplier = 0;
    private bool firstMult;
    int comboedPatternSpot=100;

    private void Awake()
    {
        funkMultiplier = 0;
        ClearAllMultiplierUi();
    }

    public void ClearAllMultiplierUi()
    {
        for (int i = 0; i < GameManager.Instance.uiManager.PatternsbarMultipliersImg.Length; i++)
        {
            //RemoveMultiplier(i);
            GameManager.Instance.uiManager.UpdateMultiplierIcon(i, Color.clear, "");
        }
    }

    //FUNK MULTIPLIER SET
    public void SetFunkMultiplier(float newModifier)
    {
        funkMultiplier = newModifier;
    }

    public float getFunkMultiplier()
    {
        return funkMultiplier;
    }

    public void ResetMultiplier()
    {
        SetFunkMultiplier(0);
    }


    public void
        OnPatternAccomplished(int indexOfPatern)
    {
        GameManager.Instance.uiManager.RemoveAllMultiplierIcon();
        if (indexOfPatern == comboedPatternSpot)
        {
            if (funkMultiplier < Multipliers[0])
                funkMultiplier = Multipliers[0];
            else if (funkMultiplier < Multipliers[1])
                funkMultiplier = Multipliers[1];
            else if (funkMultiplier < Multipliers[2])
                funkMultiplier = Multipliers[2];
            else if (funkMultiplier < Multipliers[3])
                funkMultiplier = Multipliers[3];
            else
                funkMultiplier = 1;
        }

        comboedPatternSpot = indexOfPatern;

        if (funkMultiplier < Multipliers[0])
        {
            GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, Color.white, "+" + Multipliers[0].ToString());
        }
        else if (funkMultiplier < Multipliers[1])
        {
            GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, Color.white, "+" + Multipliers[1].ToString());
        }
        else if (funkMultiplier < Multipliers[2])
        {
            GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, Color.white, "+" + Multipliers[2].ToString());
        }
        else if (funkMultiplier < Multipliers[3])
        {
            GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, Color.white, "+" + Multipliers[3].ToString());
        }
        else if (funkMultiplier < 100)
        {
            GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, Color.white, "+" + Multipliers[4].ToString());
        }
        else
        {
            GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, Color.white, "error");
        }
    }
}
