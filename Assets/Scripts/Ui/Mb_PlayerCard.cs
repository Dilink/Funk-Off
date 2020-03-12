using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Mb_PlayerCard : MonoBehaviour
{
    [HideInInspector] public RectTransform cardTransform; 

    [Header("NAME PART")]
    [SerializeField] float nameMovementDeployment = 200f;
    [SerializeField] RectTransform nameSprite;
    private float nameLocalBasePos;

    private void Start()
    {
        cardTransform = GetComponent<RectTransform>();
        nameLocalBasePos = nameSprite.localPosition.x;
    }

    public void DeployName()
    {
        nameSprite.DOLocalMoveX(nameLocalBasePos + nameMovementDeployment, .2f).SetEase(Ease.OutQuint);
    }

    public void CleanName()
    {
        nameSprite.DOLocalMoveX(nameLocalBasePos - nameMovementDeployment, .2f).SetEase(Ease.OutQuint);
    }
}
