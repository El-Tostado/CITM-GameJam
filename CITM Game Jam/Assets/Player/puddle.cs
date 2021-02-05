using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puddle : MonoBehaviour
{
    public enum Type { Blue, Red, Yellow, Green, Purple }
    float timer;
    public float duration = 10;
    public Type type;
    public bool combined = false;

    public GameObject Explosion;
    public GameObject Puddle;

    public Sprite[] Textures = new Sprite[5];

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("");
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

    public void SetType(int _type)
    {
        type = (Type)_type; 
        GetComponent<SpriteRenderer>().sprite = Textures[(int)type];
        Debug.Log((int)type + "-" + type);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (combined == false)
        {
            if (collision.tag == "Puddle")
            {
                Type _type = collision.GetComponent<puddle>().type;
                Vector3 cp = (collision.transform.position + transform.position) / 2;

                if (_type == Type.Yellow && type == Type.Red)
                {
                    //EXPLODE
                    Instantiate(Explosion, new Vector3(cp.x,cp.y,-10), Quaternion.identity);
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                }
                else if(_type == Type.Red && type == Type.Blue)
                {
                    GameObject go = Instantiate(Puddle, cp, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    go.GetComponent<puddle>().combined = true;
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                    go.GetComponent<puddle>().SetType((int)Type.Purple);
                }
                else if (_type == Type.Yellow && type == Type.Blue)
                {
                    GameObject go = Instantiate(Puddle, cp, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    go.GetComponent<puddle>().combined = true;
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                    go.GetComponent<puddle>().SetType((int)Type.Green);
                }
            }
        }
    }
}
