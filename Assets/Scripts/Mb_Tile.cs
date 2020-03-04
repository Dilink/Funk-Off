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
    [SerializeField] public Mb_PlayerController playerOnTile;
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

    public void OnMove(bool fromTP)
    {
        print("OnMove");
        if ((tileProperties.type & TileModifier.Damaging) == TileModifier.Damaging)
        {
            GameManager.Instance.FunkVariation(GameManager.Instance.funkDamagesToDeal());
        }

        if ((tileProperties.type & TileModifier.Ice) == TileModifier.Ice)
        {
            print("ICE");
            int x = playerOnTile.oldTile.posX;
            int z = playerOnTile.oldTile.posZ;

            if (playerOnTile.oldTile.posX == posX)
            {
                z = - z;
            }
            if (playerOnTile.oldTile.posZ == posZ)
            {
                x = - x;
            }
            print("X" + x + "Z" + z);

            print(GameManager.Instance.GetTile(x, z));
            if (GameManager.Instance.GetTile(x, z)!= null)
            {
                playerOnTile.CheckFreeMovement(GameManager.Instance.GetTile(x, z));
            }

        }

        if ((tileProperties.type & TileModifier.Tp) == TileModifier.Tp && fromTP == false)
        {
            playerOnTile.CheckTp(GameManager.Instance.TpTile(this));
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
    Damaging = 1<<0,
    Ice = 1<<1,
    Tp = 1<<2,
    Slow = 1<<3,
}
