using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Mb_PlayerCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moveText;
    public Mb_PlayerController playerAssigned;

    public void UpdateMoveLeftUi(int MoveLeft, int MaxMove)
    {
        moveText.text = MoveLeft + "/" + MaxMove;
    }
}
