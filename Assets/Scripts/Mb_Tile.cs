using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//#if UNITY_EDITOR
using Sirenix;
using Sirenix.OdinInspector;


public class Mb_Tile : MonoBehaviour
{

    [Header("Movement")]
    public int posX=0, posZ=0;
    public bool avaible = true;
    [SerializeField] public Mb_PlayerController playerOnTile;
    public Modifier tileProperties;
    public bool canWalkOn=false;

    [Header("Feedback")]
    [SerializeField] Animator feedBackWallUp;
    [SerializeField] Animator feedBackWallRight;
    [SerializeField] Animator feedBackIce;
    [SerializeField] Animator feedBackSlow;
    [SerializeField] Animator feedBackDestruction;


    public ParticleSystem onCompleteFeedBack;
    [SerializeField] float timeBeforeDeasaparence = 1;
    [SerializeField] GameObject feedBackTilePrecompletion;
    [SerializeField] GameObject tileAvaibleFeedBack;


    private void Awake()
    {
        if (playerOnTile != null)
        {
            avaible = false;
            playerOnTile.currentTile = this;
        }

        ResetNoFeedBack();


    }

    void AddModification(TileModifier newModifier)
    {
        tileProperties.type = newModifier | tileProperties.type;
    }

    void RemoveModification(TileModifier removedModifier)
    {
        tileProperties.type = (removedModifier & tileProperties.type) | tileProperties.type;
    }

    public void UpdateTileType(TileModifier newTileType)
    {
        ResetBaseTile();

        if ((newTileType & TileModifier.Destroyer) == TileModifier.Destroyer)
        {
            tileProperties.type = TileModifier.Destroyer;
            feedBackDestruction.SetBool("Appear", true);
            GameManager.Instance.soundManager.PlaySound(GameSound.S_LDTileAppear);
        }

        else if ((newTileType & TileModifier.Ice) == TileModifier.Ice)
        {
            feedBackIce.SetBool("Appear",true);
            tileProperties.type = TileModifier.Ice;
            GameManager.Instance.soundManager.PlaySound(GameSound.S_LDTileAppear);
        }

        else if ((newTileType & TileModifier.Slow) == TileModifier.Slow)
        {
            feedBackSlow.SetBool("Appear", true);
            tileProperties.cost = 2;
            tileProperties.type = TileModifier.Slow;
            GameManager.Instance.soundManager.PlaySound(GameSound.S_LDTileAppear);
        }

        else if (newTileType == 0)
        {
            tileProperties.type = 0;
        }

        if ((newTileType & TileModifier.WalledRight) == TileModifier.WalledRight)
        {
            GameManager.Instance.soundManager.PlaySound(GameSound.S_WallUp);
            feedBackWallUp.SetBool("Appear", true);
            tileProperties.type = (tileProperties.type| TileModifier.WalledRight);
        }

         if ((newTileType & TileModifier.WalledLeft) == TileModifier.WalledLeft)
        {
            tileProperties.type = (tileProperties.type | TileModifier.WalledLeft);
        }

         if ((newTileType & TileModifier.WalledUp) == TileModifier.WalledUp)
        {
            GameManager.Instance.soundManager.PlaySound(GameSound.S_WallUp);
            feedBackWallRight.SetBool("Appear", true);
            tileProperties.type  = (tileProperties.type | TileModifier.WalledUp);
        }

         if ((newTileType & TileModifier.WalledDown) == TileModifier.WalledDown)
        {
            tileProperties.type = (tileProperties.type | TileModifier.WalledDown);
        }


    }


    public void ResetNoFeedBack()
    {
        feedBackWallRight.SetBool("Appear", false);
        feedBackWallUp.SetBool("Appear", false);
        feedBackIce.SetBool("Appear", false);
        feedBackDestruction.SetBool("Appear", false);
        feedBackSlow.SetBool("Appear", false);
    }

    public void ResetBaseTile()
    {
        tileProperties.type = 0;
        tileProperties.cost = 1;
        ResetNoFeedBack();
    }

    public void RestBaseTileButWalls()
    {
        tileProperties.type = (tileProperties.type & (TileModifier.WalledDown | TileModifier.WalledUp | TileModifier.WalledRight | TileModifier.WalledLeft));
        tileProperties.cost = 1;
    }

    public void OnMove(bool fromTP)
    {
        if ((tileProperties.type & TileModifier.Destroyer) == TileModifier.Destroyer)
        {
            GameManager.Instance.soundManager.PlaySound(GameSound.S_AntiGrooveTileEffect);
            GameManager.Instance.patternManager.CutRandomPattern();
        }

        if ((tileProperties.type & TileModifier.Tp) == TileModifier.Tp && fromTP == false)
        {
            playerOnTile.CheckTp(GameManager.Instance.TpTile(this));
        }

        if ((tileProperties.type & TileModifier.Ice) == TileModifier.Ice)
        {
            GameManager.Instance.soundManager.PlaySound(GameSound.S_IceTileEffect);
            playerOnTile.Drift();
        }


    }

    public void ResetOccupent()
    {
        print("ResetÔccupent"+ name);
        avaible = true;
        playerOnTile = null;
    }

    public void setOccupent(Mb_PlayerController player)
    {
        playerOnTile = player;
    }

    public void OnPatternCompleteFeedback()
    {
        onCompleteFeedBack.Play();
    }

    public void PrecompletionFeedback(bool isActivated)
    {
        feedBackTilePrecompletion.SetActive(isActivated);
    }

    public void ActivateAvaibleFeedback()
    {
        tileAvaibleFeedBack.SetActive(true);
    }

    public void DesactivateAvaibleFeedback()
    {
        tileAvaibleFeedBack.SetActive(false);
    }

    public void ActivateCanWalkFeedBack()
    {
        tileAvaibleFeedBack.SetActive(true);
    }

    public void DesctivateCanWalkFeedBack()
    {
        tileAvaibleFeedBack.SetActive(false);
    }
}


[System.Serializable]
public struct Modifier 
{
    public int cost;
    public TileModifier type;
}

[System.Flags]
[System.Serializable]
public enum TileModifier
{
    Destroyer    = 1 << 0,
    Ice         = 1 << 1,
    Tp          = 1 << 2,
    Slow        = 1 << 3,
    WalledUp    = 1 << 4,
    WalledLeft  = 1 << 5,
    WalledDown  = 1 << 6,
    WalledRight = 1 << 7,
}
