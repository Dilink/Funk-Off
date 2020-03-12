using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class Sc_TeamBuilderPlayerCards : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler
{
    public Sc_CharacterParameters characterParameters;
    public TMP_Text moveText;
    public Vector3 originalPosition;
    public int movement;
    private bool firstDrag;

    private void Start()
    {
        SetOriginalPosition();
        moveText.text = movement.ToString();
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

        if (Ma_MainMenuManager.Instance.checkForCard(this.gameObject))
        {
            Ma_MainMenuManager.Instance.UpdateMovementText(movement, false);
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
        // For emplacement One
        if (RectTransformUtility.RectangleContainsScreenPoint(Ma_MainMenuManager.Instance.characterEmplacementOne, new Vector2(Input.mousePosition.x, Input.mousePosition.y)) && Ma_MainMenuManager.Instance.IsCharacterSelectionFull())
        {
            Debug.Log("Card " + gameObject.name + " Snapping to zone 1");
            Ma_MainMenuManager.Instance.RemoveCardFromSelectedEmplacement(this.gameObject);
            Ma_MainMenuManager.Instance.AddCardToSelectedEmplacements(this.gameObject, 0);
            Ma_MainMenuManager.Instance.UpdateMovementText(movement, true);
            return;
        }
        // For emplacement Two
        else if (RectTransformUtility.RectangleContainsScreenPoint(Ma_MainMenuManager.Instance.characterEmplacementTwo, new Vector2(Input.mousePosition.x, Input.mousePosition.y)) && Ma_MainMenuManager.Instance.IsCharacterSelectionFull())
        {
            Debug.Log("Card " + gameObject.name + " Snapping to zone 2");
            Ma_MainMenuManager.Instance.RemoveCardFromSelectedEmplacement(this.gameObject);
            Ma_MainMenuManager.Instance.AddCardToSelectedEmplacements(this.gameObject, 1);
            Ma_MainMenuManager.Instance.UpdateMovementText(movement, true);
            return;
        }
        // For emplacement Three
        else if (RectTransformUtility.RectangleContainsScreenPoint(Ma_MainMenuManager.Instance.characterEmplacementThree, new Vector2(Input.mousePosition.x, Input.mousePosition.y)) && Ma_MainMenuManager.Instance.IsCharacterSelectionFull())
        {
            Debug.Log("Card " + gameObject.name + " Snapping to zone 3");
            Ma_MainMenuManager.Instance.RemoveCardFromSelectedEmplacement(this.gameObject);
            Ma_MainMenuManager.Instance.AddCardToSelectedEmplacements(this.gameObject, 2);
            Ma_MainMenuManager.Instance.UpdateMovementText(movement, true);
            return;
        }
        else
        {
            Debug.Log("Card " + gameObject.name + " Unsnapping");
            transform.DOMove(originalPosition, 0.6f, false);
            Ma_MainMenuManager.Instance.RemoveCardFromSelectedEmplacement(this.gameObject);
        }
    }   
}
