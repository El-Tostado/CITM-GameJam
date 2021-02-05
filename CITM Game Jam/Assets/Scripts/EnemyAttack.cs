using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    float timer = 0.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().animator.SetBool("dead", true);
            collision.gameObject.GetComponent<PlayerMovement>().isDead = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().animator.SetBool("dead", true);
            collision.gameObject.GetComponent<PlayerMovement>().isDead = true;
        }
    }
}
