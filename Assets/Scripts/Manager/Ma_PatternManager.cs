using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using System;

public class Ma_PatternManager : MonoBehaviour
{
    public OrderedDictionary currentPatternsList = new OrderedDictionary();

    void CheckGridForPattern()
    {
        throw new NotImplementedException();
    }

    bool PatternValidation(Sc_Pattern pattern)
    {
        return currentPatternsList.Contains(pattern.matrix);
    }
    
    void GenerateStartPattern()
    {
        throw new NotImplementedException();
    }
}
