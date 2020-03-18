using System.Collections;
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
        backGroundColor.color = GameManager.Instance.comboManager.colorNone;
    }

    public void ShakePattern()
    {
        rectTransform.DOScale(1.4f, 0.3f).OnComplete(() =>
        {
            rectTransform.DOScale(1f, 0.3f);
        });
    }

    // Update the multipliers visuals
    public void UpdateMultiplierIcon(Color color, Color colorbkg, string text)
    {
        /*PatternItem item = patternItems[emplacement];
        Image img = multiplierImg;
        TMP_Text tex = multiplierText;
        // Animation
        img.transform.localScale = new Vector3(0, 0, 0);
        tex.transform.localScale = new Vector3(0, 0, 0);
        img.transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f).SetEase(Ease.OutElastic);
        tex.transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f).SetEase(Ease.OutElastic);

        // Color and text
        img.color = color;
        item.color.color = colorbkg;
        tex.text = text;
        tex.color = Color.black;*/
    }

    public void RemoveMultiplierIcon()
    {
        UpdateMultiplierIcon(Color.clear, GameManager.Instance.comboManager.colorNone, "x1");
        multiplierText.color = Color.clear;
    }

    // Update the cancel marker visuals
    public void UpdateCancelMarkerIcon(bool active)
    {
        if (!active)
        {
            backGroundColor.GetComponent<Animator>().SetBool("IsActive", false);
        }
        else
        {
            backGroundColor.GetComponent<Animator>().SetBool("IsActive", true);
        }

        // PatternsbarElements[emplacement].GetChild(3).GetComponent<Image>().color = active ? new Color(0.88f, 0.11f, 0.59f, 1.0f) : Color.clear;
    }

    public void ClearAllMutliplierUi()
    {
        UpdateMultiplierIcon(Color.clear, GameManager.Instance.comboManager.colorNone, "");
    }
    
    public void UpdatePatternsBarIcon(Sc_Pattern pattern)
    {
        patternSprite.sprite = pattern.sprite;
    }

    public void RemovePattern(bool isPatternDestroyed = false)
    {
        StartCoroutine(RemovePatternAnimations(isPatternDestroyed));
    }

    private IEnumerator RemovePatternAnimations(bool isPatternDestroyed)
    {
        if (!isPatternDestroyed)
        {
            multiplierImg.transform.DOScale(new Vector3(1.6f, 1.6f, 1.6f), 0.2f).SetEase(Ease.OutCirc);
            multiplierImg.transform.DOLocalMoveY(rectTransform.anchoredPosition.y + 85, 0.2f, false).SetEase(Ease.OutCirc);
            multiplierText.transform.DOScale(new Vector3(1.6f, 1.6f, 1.6f), 0.2f).SetEase(Ease.OutCirc);
            multiplierText.transform.DOLocalMoveY(rectTransform.anchoredPosition.y + 85, 0.2f, false).SetEase(Ease.OutCirc);

            rectTransform.transform.DOScale(new Vector3(1.6f, 1.6f, 1.6f), 0.2f).SetEase(Ease.OutCirc);
            rectTransform.transform.DOLocalMoveY(rectTransform.anchoredPosition.y + 75, 0.2f, false).SetEase(Ease.OutCirc);

            yield return new WaitForSeconds(0.2f);

            GameManager.Instance.soundManager.PlaySound(GameSound.S_PatternImpactValidation);

            //multiplierImg.transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.5f).SetEase(Ease.OutElastic);
            //multiplierText.transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.5f).SetEase(Ease.OutElastic);

            rectTransform.transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.5f).SetEase(Ease.OutElastic);

            yield return new WaitForSeconds(0.3f);

            multiplierImg.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.InCubic);
            multiplierImg.transform.DOLocalMoveY(rectTransform.anchoredPosition.y - 85, 0.18f, false).SetEase(Ease.InCubic);
            multiplierText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.InCubic);
            multiplierText.transform.DOLocalMoveY(rectTransform.anchoredPosition.y - 85, 0.18f, false).SetEase(Ease.InCubic);

            rectTransform.transform.DOLocalMoveY(rectTransform.anchoredPosition.y - 275, 0.25f, false).SetEase(Ease.InCubic);
        }
        else
        {
            Debug.LogError("TODO: animation cancel");
        }
    }

    public void MovePattern()
    {
        // Déplace le pattern concerné d'une case vers la gauche
        if (rectTransform.anchoredPosition.x >= -350)
        {
            // Déplacement du pattern
            rectTransform.transform.DOLocalMoveX(rectTransform.anchoredPosition.x - 200, 0.4f, false).SetEase(Ease.InOutQuart);

            // Couleur du background et scale, pour qu'il se reset après avoir été sur la case grise
            rectTransform.localScale = new Vector3(1, 1, 1);
            backGroundColor.color = Color.white;
        }
        else // Si le pattern concerné est le plus à gauche, le renvoit sur la case grise
        {
            RespawnAnimation();
            GameManager.Instance.uiManager.RespawnPattern(this);
        }
    }

    private void RespawnAnimation()
    {
        // Remet le pattern sur la case grise
        rectTransform.anchoredPosition = new Vector2(600, 0);

        // Change le background et le scale pour qu'il s'adapte à la case grise 
        rectTransform.localScale = new Vector3(0, 0, 0);
        rectTransform.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.4f).SetEase(Ease.OutBack);
        backGroundColor.color = Color.grey;
    }
}
