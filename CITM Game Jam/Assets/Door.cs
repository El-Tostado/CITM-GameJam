using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool opened = false;
    Animator anim;
    Collider2D collider;

    AudioSource audio;
    public AudioClip open;
    public AudioClip close;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        audio = GetComponent<AudioSource>();
        Close();
    }

    // Update is called once per frame
    void Update()
    {
        if (opened)
        {

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("OpenDoor") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                collider.enabled = false;
            }
        }
        else { 
            collider.enabled = true;
        }
    }

    public void Open()
    {
        opened = true;
        anim.SetBool("opened", true);
        audio.clip = open;
        audio.Play();
    }

    public void Close()
    {
        opened = false;
        anim.SetBool("opened", false);
        audio.clip = close;
        audio.Play();
    }
}
