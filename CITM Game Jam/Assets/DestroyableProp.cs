using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableProp : MonoBehaviour
{
    public Sprite broken;
    bool rescan = false;
    public GameObject brokenTable;

    private void Update()
    {
        //if (rescan)
        //{
        //    rescan = false;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Explosion")
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = broken;
            Instantiate(brokenTable, transform.position, Quaternion.identity);
            Destroy(gameObject);
            rescan = true;
        }
    }
}
