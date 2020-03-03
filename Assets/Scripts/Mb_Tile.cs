﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//#if UNITY_EDITOR
using Sirenix;
using Sirenix.OdinInspector;


public class Mb_Tile : MonoBehaviour
{
    public int posX=0, posZ=0;
    public bool avaible = true;
    [SerializeField] Mb_PlayerController playerOnTile;
    [InlineEditor] public Modifier tileProperties;
    

    private void Start()
    {
        if (playerOnTile != null)
        {
            avaible = false;
            playerOnTile.currentTile = this;
        }

        Modifier newParamaters = new Modifier();
        tileProperties = newParamaters;

    }

    void AddModification(TileModifier newModifier)
    {
        tileProperties.type = newModifier | tileProperties.type;
    }

    void RemoveModification(TileModifier removedModifier)
    {
        tileProperties.type = (removedModifier & tileProperties.type) | tileProperties.type;
    }

    public void OnMove()
    {
        if ((tileProperties.type & TileModifier.Damaging) == TileModifier.Damaging)
        {

        }

        if ((tileProperties.type & TileModifier.Ice) == TileModifier.Ice)
        {
            print("ICE");
            int x = posX;
            int z = posZ;

            if (playerOnTile.oldTile.posX != posX)
            {
                print("XDifferent");
                x = -x;
            }
            if (playerOnTile.oldTile.posX != posX)
            {
                print("ZDifferent");
                z = -z;
            }
            print(GameManager.Instance.GetTile(x, z));
            playerOnTile.Move(GameManager.Instance.GetTile(x, z));
        }

        if ((tileProperties.type & TileModifier.Tp) == TileModifier.Tp)
        {

        }
    }

    public void ResetOccupent()
    {
        playerOnTile = null;
    }

    public void setOccupent(Mb_PlayerController player)
    {
        playerOnTile = player;
    }
}
[System.Serializable]
public class Modifier : ScriptableObject
{
    public int cost=1;
    public TileModifier type;
}

[System.Flags]
[System.Serializable]
public enum TileModifier
{
    Base = 1<<0,
    Damaging = 1<<1,
    Ice = 1<<2,
    Tp = 1<<3,
}
