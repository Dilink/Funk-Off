using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New AI Pattern", menuName = "Custom/AI Pattern")]
public class Sc_AIPattern : ScriptableObject
{
    public string Title;
    public Matrix5x5Int Matrix;
    [ReadOnly]
    public string Name;
    public int difficultyLevel;


    [Button(50), GUIColor(0.89f, 0.14f, 0.14f)] 
    void UpdateDifficulty()
    {
        difficultyLevel = Matrix.difficultyLevel;
    }
}
