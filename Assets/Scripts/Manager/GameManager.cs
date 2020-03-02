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
            CastRayPlayer();
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
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit);
            currentPlayerSelectionned = hit.collider.GetComponent<Mb_PlayerController>();
        }
    }

    void CastRayTile()
    {
        RaycastHit hit;
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit);
            currentPlayerSelectionned.CheckMovement(hit.collider.GetComponent<Mb_Tile>());
        }
    }

}
