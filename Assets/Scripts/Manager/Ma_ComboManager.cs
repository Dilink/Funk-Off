using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_ComboManager : MonoBehaviour
{
    public List<int> Multipliers = new List<int>(5);

    private void Awake()
    {
        for(int i = 0; i< GameManager.Instance.uiManager.PatternsbarMultipliersImg.Length; i++)
        {
            RemoveMultiplier(i);
        }
    }

    public void RotateMultipliers(int index)
    {
        // Add the multiplier to the removed pattern
        GameManager.Instance.comboManager.AddMultiplier(index);

        // Remove all multipliers from patterns after
        if (index <= GameManager.Instance.comboManager.Multipliers.Count)
        {
            for (int j = index + 1; j < GameManager.Instance.comboManager.Multipliers.Count; j++)
            {
                GameManager.Instance.comboManager.RemoveMultiplier(j);
            }
        }

        // Remove all multipliers from patterns before
        if (index > 0)
        {
            for(int k = index -1; k >= 0; k--)
            {
                GameManager.Instance.comboManager.RemoveMultiplier(k);
            }
        }
    }

    public void AddMultiplier(int emplacement)
    {
        int mult = Multipliers[emplacement];

        if (mult < 2)
        {
            Multipliers[emplacement] = 2;
            GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.green, "x2");
        }
        else if (mult < 3)
        {
            Multipliers[emplacement] = 3;
            GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.yellow, "x3");
        }
        else if (mult < 4)
        {
            Multipliers[emplacement] = 4;
            GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.red, "x4");
        }
        else if (mult < 5)
        {
            Multipliers[emplacement] = 5;
            GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.magenta, "x5");
        }
    }

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
}
