using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class UpdatePathfindingGrid : MonoBehaviour
{
    public GameObject[] destroyableObjects;
    private void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Bounds bounds = destroyableObjects[0].gameObject.GetComponent<BoxCollider2D>().bounds;
            destroyableObjects[0].layer = 0;
            //destroyableObjects[0].SetActive(false);
            AstarPath.active.UpdateGraphs(bounds);
        }
    }
}
