using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_CameraManager : MonoBehaviour
{
    [SerializeField] Transform center;

  /*  void Update()
    {
        if (GameManager.Instance.currentPlayerSelectionned == null)
        {
            if(Input.GetMouseButton(1) && Input.GetAxis("Mouse X")>0)
            {
                center.Rotate(new Vector3(0, 1 + center.rotation.y, 0));
            }
            else if (Input.GetMouseButton(1) && Input.GetAxis("Mouse X") < 0)
            {
                center.Rotate(new Vector3(0, -1 + center.rotation.y, 0));
            }
        }
    }*/
}
