using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ma_AIManager : MonoBehaviour
{
    [SerializeField] AllAIPattern[] allPatterns;
    /*
    void PrepAttack()
    {
        throw new NotImplementedException();
    }

    void CancelAttack()
    {
        throw new NotImplementedException();
    }

    void GridModification()
    {
        throw new NotImplementedException();
    }*/
}

[System.Serializable]
public struct AllAIPattern
{
     public Sc_AIPattern[] patternsAi;
}
