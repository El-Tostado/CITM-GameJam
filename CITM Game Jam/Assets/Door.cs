using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool opened = false;
    Animator anim;
    Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
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
        else
        {
            collider.enabled = true;
        }
    }

    public void Open()
    {
        opened = true;
        anim.SetBool("opened", true);
    }

    public void Close()
    {
        opened = false;
        anim.SetBool("opened", false);
    }
}
