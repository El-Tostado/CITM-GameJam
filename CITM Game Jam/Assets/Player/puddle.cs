using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puddle : MonoBehaviour
{
    public enum Type { Explosion, Sticky, Cure }

    float timer;
    public float duration = 10;
    public Type type;

    // Start is called before the first frame update
    void Start()
    {
        if(type == Type.Explosion)
        {

        }
        else if (type == Type.Sticky)
        {

        }
        else if (type == Type.Cure)
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

        if (type == Type.Explosion)
        {

        }
        else if (type == Type.Sticky)
        {

        }
        else if (type == Type.Cure)
        {

        }
    }
}
