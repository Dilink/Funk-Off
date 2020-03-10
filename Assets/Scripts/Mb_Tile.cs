using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Feedback")]
    [SerializeField] GameObject feedBackWallUp;
    [SerializeField] GameObject feedBackWallRight;

    private MeshRenderer meshRenderer;
    private Material tileMaterial;
    private Material baseMaterial;

    private void Awake()
    {
        if (playerOnTile != null)
        {
            avaible = false;
            playerOnTile.currentTile = this;
        }

        //creer une instance du materiau pour pouvoir le set comme on veut pendant la game
        meshRenderer = GetComponent<MeshRenderer>();
        baseMaterial = meshRenderer.material;
        tileMaterial = new Material(meshRenderer.material);


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

        if ((newTileType & TileModifier.Damaging) == TileModifier.Damaging)
        {
            SetTileMaterial(tileMaterial);
            tileProperties.type = TileModifier.Damaging;
            SetTileMaterial(GameManager.Instance.gridFeedbackRules.damagingMaterial);
        }

        else if ((newTileType & TileModifier.Ice) == TileModifier.Ice)
        {
            tileProperties.type = TileModifier.Ice;
            tileMaterial = GameManager.Instance.gridFeedbackRules.iceMaterial;
            SetTileMaterial(tileMaterial);
        }

        else if ((newTileType & TileModifier.Slow) == TileModifier.Slow)
        {
            tileProperties.cost = 2;
            tileProperties.type = TileModifier.Slow;
            SetTileMaterial(GameManager.Instance.gridFeedbackRules.slowMaterial);
        }

        else if (newTileType == 0)
        {
            tileProperties.type = 0;
            SetTileMaterial(baseMaterial);
        }

        if ((newTileType & TileModifier.WalledRight) == TileModifier.WalledRight)
        {
            tileProperties.type = (tileProperties.type| TileModifier.WalledRight);
        }

         if ((newTileType & TileModifier.WalledLeft) == TileModifier.WalledLeft)
        {
            tileProperties.type = (tileProperties.type | TileModifier.WalledLeft);
        }

         if ((newTileType & TileModifier.WalledUp) == TileModifier.WalledUp)
        {
            print("WalledUp");
            tileProperties.type  = (tileProperties.type | TileModifier.WalledUp);
        }

         if ((newTileType & TileModifier.WalledDown) == TileModifier.WalledDown)
        {
            tileProperties.type = (tileProperties.type | TileModifier.WalledDown);
        }


        UpdateWallFeedBack();
    }

    void UpdateWallFeedBack()
    {
        
        feedBackWallUp.SetActive(false);
        feedBackWallRight.SetActive(false);

        if ((tileProperties.type & TileModifier.WalledUp) == TileModifier.WalledUp)
            {
                feedBackWallUp.SetActive(true);
            }
        
        if ((tileProperties.type & TileModifier.WalledRight) == TileModifier.WalledRight ) 
            {
                feedBackWallRight.SetActive(true);
            }
        
    }

    void SetTileMaterial(Material newMaterial)
    {
        meshRenderer.material = newMaterial;
    }

    void ResetBaseTile()
    {
        tileProperties.type = 0;
        tileProperties.cost = 1;
        SetTileMaterial(baseMaterial);
    }

    public void OnMove(bool fromTP)
    {
        if ((tileProperties.type & TileModifier.Damaging) == TileModifier.Damaging)
        {
            GameManager.Instance.FunkVariation(GameManager.Instance.funkDamagesToDeal()/3);
        }

        if ((tileProperties.type & TileModifier.Tp) == TileModifier.Tp && fromTP == false)
        {
            playerOnTile.CheckTp(GameManager.Instance.TpTile(this));
        }

        if ((tileProperties.type & TileModifier.Ice) == TileModifier.Ice)
        {
            playerOnTile.Drift();

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
public struct Modifier 
{
    public int cost;
    public TileModifier type;
}

[System.Flags]
[System.Serializable]
public enum TileModifier
{
    Damaging    = 1 << 0,
    Ice         = 1 << 1,
    Tp          = 1 << 2,
    Slow        = 1 << 3,
    WalledUp    = 1 << 4,
    WalledLeft  = 1 << 5,
    WalledDown  = 1 << 6,
    WalledRight = 1 << 7,
}
