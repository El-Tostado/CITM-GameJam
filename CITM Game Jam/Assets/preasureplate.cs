using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preasureplate : MonoBehaviour
{
    public List<GameObject> Doors;
    bool active;

    AudioSource audio;
    public AudioClip ON;
    public AudioClip OFF;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (active)
        {
            foreach(var door in Doors)
            {
                if(door != null)
                {
                    if (!door.GetComponent<Door>().opened)
                    {
                        door.GetComponent<Door>().Open();
                    }
                    //Bounds bounds = door.GetComponent<BoxCollider2D>().bounds;
                    //gameObject.layer = 0;
                    //AstarPath.active.UpdateGraphs(bounds);
                }
            }
        }
        else
        {
            foreach (var door in Doors)
            {
                if (door != null)
                {
                    if (door.GetComponent<Door>().opened)
                    {
                        door.GetComponent<Door>().Close();
                    }
                    //Bounds bounds = door.GetComponent<BoxCollider2D>().bounds;
                    //gameObject.layer = 12;
                    //AstarPath.active.UpdateGraphs(bounds);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" || other.tag == "Player")
        {
            GetComponent<Animator>().SetBool("active", true); 
            active = true;
            audio.clip = ON;
            audio.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Player")
        {
            GetComponent<Animator>().SetBool("active", false);
            active = false;
            audio.clip = OFF;
            audio.Play();
        }
    }
}
