using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Mb_ButtonSize : MonoBehaviour
{
    public void SizeUp(float finalSize)
    {
        transform.DOScale(new Vector3(finalSize, finalSize, 1),0.1f).OnComplete(ResetSize);
    }

    void ResetSize()
    {
        transform.DOScale(new Vector3(1, 1, 1), 0.1f);
    }
}
