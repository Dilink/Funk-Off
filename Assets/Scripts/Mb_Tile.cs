using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mb_Tile : MonoBehaviour
{
    public int cost=1;

    void AddModification(TileModifier newModifier)
    {

    }

    void RemoveModification(TileModifier removedModifier)
    {

    }

    void OnMove()
    {

    }

}

class TileModifier : ScriptableObject
{
    public float cost;
    public TileCostModifier type;
}

[System.Flags]
public enum TileCostModifier
{
    Base = 1<<0,
    Fire = 1<<1,
    Ice = 1<<2,

    IceFire = Fire|Ice

}
