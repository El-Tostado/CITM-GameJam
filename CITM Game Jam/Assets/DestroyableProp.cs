using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableProp : MonoBehaviour
{
    public Sprite broken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Explosion")
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = broken;
        }
    }
}
