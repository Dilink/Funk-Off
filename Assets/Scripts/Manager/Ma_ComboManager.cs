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
            Multipliers[i] = 1;
            GameManager.Instance.uiManager.UpdateMultiplierIcon(i, Color.clear);
        }
    }

    public void AddMultiplier(int emplacement)
    {
        int mult = Multipliers[emplacement];

        if (mult < 2)
        {
            Multipliers[emplacement] = 2;
            GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.green);
        }
        else if (mult < 3)
        {
            Multipliers[emplacement] = 3;
            GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.yellow);
        }
        else if (mult < 4)
        {
            Multipliers[emplacement] = 4;
            GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.red);
        }
        else if (mult < 5)
        {
            Multipliers[emplacement] = 5;
            GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.magenta);
        }
    }

    public void RemoveMultiplier(int emplacement)
    {
        Multipliers[emplacement] = 1;
        GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.clear);
    }
}
