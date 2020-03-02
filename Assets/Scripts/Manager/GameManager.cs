using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Mb_PlayerController currentPlayerSelectionned;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
        else if (currentPlayerSelectionned != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CastRayTile();
            }
        }
    }

    void CastRayPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, 9))
        {
            currentPlayerSelectionned = hit.collider.GetComponent<Mb_PlayerController>();
        }
    }

    void CastRayTile()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, 8))
        {
            currentPlayerSelectionned.CheckMovement(hit.collider.GetComponent<Mb_Tile>());
        }
    }

}
