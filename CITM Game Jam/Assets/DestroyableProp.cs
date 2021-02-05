using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableProp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Explosion")
        {
            Bounds bounds = gameObject.GetComponent<BoxCollider2D>().bounds;
            gameObject.layer = 0;
            AstarPath.active.UpdateGraphs(bounds);
            Destroy(gameObject);
        }
    }
}
