using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewItem", menuName = "CreateNewItem")]
public class Sc_Item : ScriptableObject
{
    [Header("UI")]
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;
}
