using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;



public class Mb_PatternBarElement : MonoBehaviour
{
    [Header("Gameplay")]
    public Image patternSprite;
    [HideInInspector] public RectTransform rectTransform;

    [Header("ComboPart")]
    public ParticleSystem[] FX;
    public Image backGroundColor;
    public Image multiplierImg;
    public TMP_Text multiplierText;


    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void PlayFX(int index)
    {
        FX[index].Play();
    }

    public void SetComboColor(Color color)
    {
        backGroundColor.color = color;
    }

    public void SetText(string newText)
    {
        multiplierText.text = newText;
    }

    public void ClearMutliplier()
    {
        multiplierText.text = "";
    }
}
