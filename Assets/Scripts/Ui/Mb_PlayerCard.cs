using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Mb_PlayerCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moveText;
    [SerializeField] Image CardIcon;
    [SerializeField] Image CardItem;
    [SerializeField] Image CardPassive;
    [SerializeField] RectTransform toolTip;
    [SerializeField] Sc_Item item;

    public string powerName;
    [TextArea()]
    public string powerDesc;


    public void UpdateMoveLeftUi(int MoveLeft, int MaxMove)
    {
        moveText.text = MoveLeft + "/" + MaxMove;
    }

    public void UpdateCardIcon(Sprite img)
    {
        CardIcon.sprite = img;
    }

    public void UpdateCardItem(Sprite img)
    {
        CardItem.sprite = img;
    }

    public void UpdateCardPassive(Sprite img)
    {
        CardPassive.sprite = img;
    }

    public void DisplayItemBubble()
    {
        DisplayInfoBubble(item.itemName, item.itemDesc);
    }

    public void DisplayPowerBubble()
    {
        DisplayInfoBubble(powerName, powerDesc);
    }

    private void DisplayInfoBubble(string title, string desc)
    {
        // Move the bubble & scale
        toolTip.DOMoveX(400, 0.2f, false);
        toolTip.DOScale(new Vector3(1, 1, 1), 0.2f);

        // Update the texts
        toolTip.GetChild(2).GetComponent<TMP_Text>().text = title;
        toolTip.GetChild(3).GetComponent<TMP_Text>().text = desc;
    }

    public void HideInfoBubble()
    {
        toolTip.DOMoveX(0, 0.2f, false);
        toolTip.DOScale(new Vector3(0.6f, 0.6f, 1), 0.2f);
    }
}
