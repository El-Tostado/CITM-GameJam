using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class UpdatePathfindingGrid : MonoBehaviour
{
    void Update()
    {
        /*
        if (Input.GetKeyDown("space"))
        {
            Bounds bounds = destroyableObjects[0].gameObject.GetComponent<BoxCollider2D>().bounds;
            destroyableObjects[0].layer = 0;
            //destroyableObjects[0].SetActive(false);
            AstarPath.active.UpdateGraphs(bounds);
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            Bounds bounds = gameObject.GetComponent<BoxCollider2D>().bounds;
            gameObject.layer = 0;
            AstarPath.active.UpdateGraphs(bounds);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
