using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mb_Tile : MonoBehaviour
{
    public int cost;

    void AddModification(Modifier newModifier)
    {

    }

    void RemoveModification(Modifier removedModifier)
    {

    }

    void OnMove()
    {

    }

}

class Modifier : ScriptableObject
{
    public float cost;
    public TileModifier type;
}

[System.Flags]
public enum TileModifier
{
    Base = 1<<0,
    Fire = 1<<1,
    Ice = 1<<2,

    IceFire = Fire|Ice

}
