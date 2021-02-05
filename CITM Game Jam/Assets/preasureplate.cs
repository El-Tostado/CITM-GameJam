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
                    door.GetComponent<Door>().Open();
            }
        }
        else
        {
            foreach (var door in Doors)
            {
                if (door != null)
                    door.GetComponent<Door>().Close();
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
