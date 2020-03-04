using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_ComboManager : MonoBehaviour
{

    public void AddMultiplier(int emplacement, int mult)
    {
        
    }

    public void RemoveMultiplier(int emplacement)
    {

    }

    public void RemovePattern(int emplacement)
    {
        GameManager.Instance.uiManager.PatternsbarIconsImg[emplacement].sprite = null;

        for (int i = emplacement + 1; i< GameManager.Instance.uiManager.PatternsbarIconsImg.Length; i++)
        {

            GameManager.Instance.uiManager.UpdatePatternsBarIcon(i, GameManager.Instance.patternManager.currentPatternsList[i]);

        }
    }

}
