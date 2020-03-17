using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternModifier
{

}

[CreateAssetMenu(fileName = "New Pattern", menuName = "Custom/Pattern")]
public class Sc_Pattern : ScriptableObject
{
    public string Title;
    [PreviewField(70, ObjectFieldAlignment.Left)]
    public Sprite sprite;
    public Matrix5x5Bool Matrix;
    public List<PatternModifier> modifiers;
    public int danceToPlay=1;
    [ReadOnly]
    public string Name;
    [ReadOnly]
    public string Category;
}

