using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_ComboManager : MonoBehaviour
{
    public List<int> Multipliers;

    public void AddMultiplier(int emplacement, int mult)
    {
        Multipliers[emplacement] = mult;

        if(mult >=2)
            GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.green);
        else if(mult >= 3)
            GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.yellow);
        else if (mult >= 4)
            GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.red);
        else if (mult >= 5)
            GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.magenta);
    }

    public void RemoveMultiplier(int emplacement)
    {
        Multipliers[emplacement] = 0;
        GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.white);
    }
}
