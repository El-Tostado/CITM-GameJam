using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : MonoBehaviour
{
    public enum Type { Blue, Red, Yellow, Green}
    public Type type;

    bool destroy = false;

    AudioSource audio;
    public AudioClip pick;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (destroy)
        {
            if (!audio.isPlaying)
            {
                if (audio.time > 0 && !audio.isPlaying)
                    Destroy(gameObject);
                else
                {
                    audio.Play();
                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                }
            }

        }
    }

    public void Destroy()
    {
        destroy = true;
    }
}
