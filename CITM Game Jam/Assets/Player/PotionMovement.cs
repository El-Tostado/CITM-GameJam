using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionMovement : MonoBehaviour
{
    public AnimationCurve curve;
    public Transform target;
    public Vector3 targetPos;
    public Vector3 targetForward;
    public PotionItem.Type type;

    private Vector3 start;
    private Coroutine coroutine;
    public GameObject HealthExplosion;

    public float archeight = 3.0f;
    public float speed = 3;

    public float duration;
    float time;
    Vector3 end;

    public Sprite[] potionTextures = new Sprite[4];
    public GameObject puddle;

    AudioSource audio;
    public AudioClip throwB;
    public AudioClip breakB;

    bool breaked = false;

    private void Awake()
    {
        start = transform.position;
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audio.clip = throwB;
        audio.Play();
        duration = (0.40f + (targetPos - transform.position).magnitude * 0.015f);
        time = 0f;

        end = targetPos - (targetForward * 0.55f);

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
            if (breaked )
            {
                if(!audio.isPlaying)
                    Destroy(gameObject);
            }
            else
            {
                //Spawn slime
                if (type == PotionItem.Type.Green)
                {
                    Instantiate(HealthExplosion, new Vector3(transform.position.x, transform.position.y, -10), Quaternion.identity);
                }
                else
                {
                    GameObject go = Instantiate(puddle, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    go.GetComponent<puddle>().SetType((int)type);
                }
                breaked = true;
                GetComponent<SpriteRenderer>().enabled = false;
                audio.clip = breakB;
                audio.Play();
            }
        }

    }
    public void SetType(PotionItem.Type _type)
    {
        type = _type;
    }
}
