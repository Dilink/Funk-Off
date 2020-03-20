using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Deploy : MonoBehaviour
{
    [SerializeField] float distanceToDeploy=100;
    [SerializeField] RectTransform posOnScreen;

    bool isDeployed =false;


    public void Deploying()
    {
        if (isDeployed == true)
        {
            isDeployed = false;
            posOnScreen.DOMoveX(posOnScreen.position.x  + distanceToDeploy, 1);
        }
        else 
        {
            isDeployed = true;
            posOnScreen.DOMoveX(posOnScreen.position.x - distanceToDeploy, 1);
        }
    }
}
