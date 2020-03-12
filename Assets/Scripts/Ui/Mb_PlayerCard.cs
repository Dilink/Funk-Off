using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Mb_PlayerCard : MonoBehaviour
{
    [HideInInspector] public RectTransform cardTransform; 

    [SerializeField] TextMeshProUGUI moveText;
    [SerializeField] Image CardIcon;
    [SerializeField] Image CardItem;
    [SerializeField] Image CardPassive;
    [SerializeField] RectTransform toolTip;
    [SerializeField] Sc_Item item;

    [Header("NAME PART")]
    [SerializeField] Vector3 nameMovementDeployment = new Vector3(200,0,0);
    [SerializeField] RectTransform nameSprite;
    private Vector3 nameLocalBasePos;

    public string powerName;
    [TextArea()]
    public string powerDesc;

    private void Start()
    {
        cardTransform = GetComponent<RectTransform>();
        nameLocalBasePos = nameSprite.localPosition;
    }
    /*
    public void UpdateMoveLeftUi(int MoveLeft, int MaxMove)
    {
        moveText.text = MoveLeft + "/" + MaxMove;
    }
    */
    public void UpdateCardIcon(Sprite img)
    {
        CardIcon.sprite = img;
    }

    public void UpdateCardItem(Sprite img)
    {
        CardItem.sprite = img;
    }

    public void DeployName()
    {
        nameSprite.DOLocalMove(nameLocalBasePos + nameMovementDeployment, .2f);
    }

    public void CleanName()
    {
        nameSprite.DOLocalMove(nameLocalBasePos - nameMovementDeployment, .2f);
    }

    private void DisplayInfoBubble(string title, string desc)
    {
        // Move the bubble & scale
        toolTip.DOLocalMoveX(400, 0.2f, false);
        toolTip.DOScale(new Vector3(1, 1, 1), 0.2f);

        // Update the texts
        toolTip.GetChild(2).GetComponent<TMP_Text>().text = title;
        toolTip.GetChild(3).GetComponent<TMP_Text>().text = desc;
    }

    public void HideInfoBubble()
    {
        toolTip.DOLocalMoveX(0, 0.2f, false);
        toolTip.DOScale(new Vector3(0.6f, 0.6f, 1), 0.2f);
    }
}
