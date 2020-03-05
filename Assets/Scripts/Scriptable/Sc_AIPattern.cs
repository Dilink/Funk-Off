using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New AI Pattern", menuName = "Custom/AI Pattern")]
public class Sc_AIPattern : ScriptableObject
{
    public string Title;
    public Matrix3x3Int Matrix;
    [ReadOnly]
    public string Name;
}
