using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puddle : MonoBehaviour
{

    float timer;
    public float duration = 10;
    public PotionItem.Type type;

    // Start is called before the first frame update
    void Start()
    {
        if(type == PotionItem.Type.Explosion)
        {

        }
        else if (type == PotionItem.Type.Sticky)
        {

        }
        else if (type == PotionItem.Type.Cure)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= duration && timer != 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemies")
            return;

        if (type == PotionItem.Type.Explosion)
        {

        }
        else if (type == PotionItem.Type.Sticky)
        {

        }
        else if (type == PotionItem.Type.Cure)
        {

        }
    }
}
