using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternModifier
{

}


[CreateAssetMenu(fileName = "New Pattern", menuName = "Custom/New Pattern")]
public class Sc_Pattern : ScriptableObject
{
    [PreviewField(70, ObjectFieldAlignment.Left)]
    public Sprite sprite;
    public Matrix3x3Bool Matrix;
    public List<PatternModifier> modifiers;
    public string Name;
}
