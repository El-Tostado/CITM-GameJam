using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    float timer = 0.0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && timer < transform.parent.GetChild(0).GetComponent<EnemyGraphics>().attackSpeed)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerMovement>().isDead = true;
            }
        }
    }
}
