using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;
        gameObject.layer = 0;
        AstarPath.active.UpdateGraphs(bounds);
    }

}
