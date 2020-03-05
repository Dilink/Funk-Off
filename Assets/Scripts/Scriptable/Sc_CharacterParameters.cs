using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="NewCharacter", menuName ="CreateNewCharacterParameters")]
public class Sc_CharacterParameters : ScriptableObject
{
    public CharacterSkills characterSkills;
    public string characterName;
    public Texture2D characterIcon;
}

[System.Serializable]
[System.Flags]
public enum CharacterSkills
{
    Swift = 1 << 0,
    JumpOff = 1 << 1,
    Finisher = 1 << 2
}
