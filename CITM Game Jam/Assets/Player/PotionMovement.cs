using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionMovement : MonoBehaviour
{
    public AnimationCurve curve;
    public Transform target;
    public PotionItem.Type type;

    private Vector3 start;
    private Coroutine coroutine;

    public float archeight = 3.0f;
    public float speed = 3;

    public float duration;
    float time;
    Vector3 end;

    public Sprite[] potionTextures = new Sprite[4];
    public GameObject puddle;

    private void Awake()
    {
        start = transform.position;
    }

    private void Start()
    {
        if (target == null)
            target = GameObject.Find("AimTarget").transform;

        duration = (0.40f + (target.position - transform.position).magnitude * 0.015f);
        time = 0f;

        end = target.position - (target.forward * 0.55f);

        GetComponent<SpriteRenderer>().sprite = potionTextures[(int)type];
    }

    private void Update()
    {
        if(time < duration)
        {
            time += Time.deltaTime;

            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, archeight, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);
            transform.localRotation = Quaternion.Euler(0,0,transform.rotation.z + Time.fixedTime * 1500);
        }
        else
        {
            //Spawn slime
            GameObject go = Instantiate(puddle, transform.position, Quaternion.Euler(0,0, Random.Range(0, 360)));
            go.GetComponent<puddle>().SetType((int)type);
            Destroy(gameObject);
        }
    }
    public void SetType(PotionItem.Type _type)
    {
        type = _type;
    }
}
