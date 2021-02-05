using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public bool opened = true;
    Animator anim;
    Collider2D collider;
    Animator alarmAnim;

    AudioSource audio;
    public AudioClip open;
    public AudioClip close;

    bool levelStart = false;

    RoomManager roomManager;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("opened", true);
        collider = GetComponent<Collider2D>();
        audio = GetComponent<AudioSource>();
        alarmAnim = GameObject.FindGameObjectWithTag("Alarm").GetComponentInChildren<Animator>();
        roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();
        alarmAnim.SetBool("alarmOn", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (roomManager.startedLevel && !levelStart)
        {
            levelStart = true;
            Close();
        }     

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
        alarmAnim.SetBool("alarmOn", false);
    }

    public void Close()
    {
        opened = false;
        anim.SetBool("opened", false);
        audio.clip = close;
        audio.Play();
    }
}
