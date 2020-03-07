using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_ComboManager : MonoBehaviour
{
    //public List<float> Multipliers = new List<float>(5);
    private float funkMultiplier = 1;
    int comboedPatternSpot=100;

    private void Awake()
    {
        funkMultiplier = 1;
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

    /*
    public void RotateMultipliers(int index)
    {
        // Add the multiplier to the removed pattern
        GameManager.Instance.comboManager.AddMultiplier(index);

        // Remove all multipliers from patterns after
        if (index <= GameManager.Instance.comboManager.Multipliers.Count)
        {
            for (int j = index + 1; j < GameManager.Instance.comboManager.Multipliers.Count; j++)
            {
               RemoveMultiplier(j);
            }
        }

        // Remove all multipliers from patterns before
        if (index > 0)
        {
            for(int k = index -1; k >= 0; k--)
            {
               RemoveMultiplier(k);
            }
        }
    }*/
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
        SetFunkMultiplier(1);
    }


    public void
        OnPatternAccomplished(int indexOfPatern)
    {
        GameManager.Instance.uiManager.RemoveAllMultiplierIcon();
        if (indexOfPatern == comboedPatternSpot)
        {
         //   float newMultiplier = getFunkMultiplier() + 1;
            
            funkMultiplier += 1;
        }
        comboedPatternSpot = indexOfPatern;

        switch (getFunkMultiplier())
        {
            case 0:
                GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, Color.clear, "");
                break;
            case 1:
                GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, Color.green, "x2");
                break;
            case 2:
                GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, Color.yellow, "x3");
                break;
            case 3:
                GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, Color.red, "x4");
                break;
            case 4:
                GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, Color.magenta, "x5");
                break;
            case 5:
                GameManager.Instance.uiManager.UpdateMultiplierIcon(indexOfPatern, Color.magenta, "x5");
                break;
        }
    }

    /*
    public void RemoveMultiplier(int emplacement)
    {
        Multipliers[emplacement] = 1;
        GameManager.Instance.uiManager.RemoveMultiplierIcon(emplacement);
    }

    public void RemoveAllMultipliers()
    {
        for(int l = 0; l < Multipliers.Count; l++)
        {
            Multipliers[l] = 1;
            GameManager.Instance.uiManager.RemoveMultiplierIcon(l);
        }
    }
    */


    
}
