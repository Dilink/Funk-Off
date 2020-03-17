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
    public RectTransform multiplierImg;
    public TMP_Text multiplierText;
    Vector2 basePosTorwardPatternBar;

    [Header("AnimTweaking")]
    [SerializeField] Vector2 posToAddOnCompletion;

    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        basePosTorwardPatternBar = multiplierImg.localPosition;
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
        backGroundColor.color = GameManager.Instance.comboManager.colorNone;
    }

    void DelockBonusIcon()
    {
        multiplierImg.SetParent(null);
    }

    void LockBonusIcon()
    {
        multiplierImg.SetParent(transform);
    }

    public void AnimBonusCompleted()
    {
        float y = posToAddOnCompletion.y - 10;
        multiplierImg.DOLocalMoveY((basePosTorwardPatternBar.y +  y), .8f).OnComplete(()=>{
            multiplierImg.DOLocalMoveY((10), .3f);
            multiplierImg.DOScale(1.2f, .3f).OnComplete(() => {
                multiplierImg.DOScale(1, 1.1f);
                multiplierImg.DOLocalMoveY(-posToAddOnCompletion.y, 1.1f);
            });
        });
    }
}
