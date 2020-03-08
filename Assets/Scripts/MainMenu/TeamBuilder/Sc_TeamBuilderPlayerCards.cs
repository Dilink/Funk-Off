using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Sc_TeamBuilderPlayerCards : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler
{
    public Sc_CharacterParameters characterParameters;
    public Vector3 originalPosition;

    private bool firstDrag;

    private void Start()
    {
        SetOriginalPosition();
        //Debug.Log("og position set");
    }

    public void SetOriginalPosition()
    {
        originalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!firstDrag)
        {
            SetOriginalPosition();
            firstDrag = true;
        }

        //Ma_MainMenuManager.Instance.cardsGrid.enabled = false;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.DOMove(Input.mousePosition, 0.1f, false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(RectTransformUtility.RectangleContainsScreenPoint(Ma_MainMenuManager.Instance.selectedCharactersEmplacementsRect, new Vector2(Input.mousePosition.x, Input.mousePosition.y)))
        {
            //Debug.Log("TBPC rect contains");
            Ma_MainMenuManager.Instance.RemoveCardFromSelectedEmplacement(this.gameObject);
            Ma_MainMenuManager.Instance.AddCardToSelectedEmplacements(this.gameObject);
        }
        else
        {
            transform.DOMove(originalPosition, 0.6f, false);
            Ma_MainMenuManager.Instance.RemoveCardFromSelectedEmplacement(this.gameObject);
        }
    }
}
