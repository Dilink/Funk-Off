using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_ComboManager : MonoBehaviour
{

    public void AddMultiplier(int emplacement, int mult)
    {
        
    }

    public void RemovePattern(int emplacement)
    {
        for(int i = emplacement; i< GameManager.Instance.uiManager.PatternsbarIconsImg.Length; i++)
        {
            //GameManager.Instance.uiManager.UpdatePatternsBarIcon(i, GameManager.Instance.patternManager.currentPatternsList[i]);
        }
    }

}
