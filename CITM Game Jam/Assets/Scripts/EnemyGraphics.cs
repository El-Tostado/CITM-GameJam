using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyGraphics : MonoBehaviour
{
    public AIPath aiPath;
    public AIDestinationSetter aiDestinationSetter;
    Animator animator;

    public GameObject attackColliderLeft;
    public GameObject attackColliderRight;

    bool facingRight = false;

    public float attackSpeed = 1.0f;
    public float visionRange = 20;

    public GameObject player;

    bool trapped = false;
    public bool healed = false;

    AudioSource audio;
    public AudioClip zombie;
    public AudioClip heal;

    public GameObject exlposionEffect;

    RoomManager roomManager;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        attackColliderLeft.SetActive(false);
        attackColliderRight.SetActive(false);
        roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();

        player = GameObject.FindGameObjectWithTag("Player");
        audio.clip = zombie;
        audio.loop = true;
        audio.Play();
    }

    void Update()
    {
        if (healed) 
            return;

        aiPath.canMove = roomManager.startedLevel;

        if (aiPath.desiredVelocity.x <= 0.01f && aiPath.desiredVelocity.x >= -0.01f &&
            aiPath.desiredVelocity.y <= 0.01f && aiPath.desiredVelocity.y >= -0.01f)
            animator.SetBool("walking", false);
        else
        {
            if (!trapped)
            {
                animator.SetBool("walking", true);
                attackColliderRight.SetActive(false);
                attackColliderLeft.SetActive(false);
            }
        }

        if (aiPath.desiredVelocity.x >= 0.0001f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            facingRight = true;
        }

        else if (aiPath.desiredVelocity.x <= -0.0001f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            facingRight = false;
        }

        if (aiDestinationSetter.target == null)
            aiDestinationSetter.target = player.transform;

        if (aiDestinationSetter.target != null && Vector2.Distance(transform.position, aiDestinationSetter.target.position) <= aiPath.endReachedDistance)
            Attack();

        else
            animator.SetBool("attack", false);

        if (aiDestinationSetter.target != null && Vector2.Distance(transform.position, aiDestinationSetter.target.position) <= visionRange)
        {
            aiPath.enabled = true;
            GameObject[] potions = GameObject.FindGameObjectsWithTag("Puddle");
            if (potions.Length > 0)
            {
                foreach(GameObject potion in potions)
                {
                    if (potion != null && potion.GetComponent<puddle>().type == puddle.Type.Purple)
                    {
                        aiDestinationSetter.target = potion.transform;
                        break;
                    }
                    else
                        aiDestinationSetter.target = player.transform;
                }
            }
            else
                aiDestinationSetter.target = player.transform;
        }
        else
            aiPath.enabled = false;
    }

    void Attack()
    {
        animator.SetBool("attack", true);

        if (facingRight)
            attackColliderRight.SetActive(true);

        else
            attackColliderLeft.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Puddle" && collision.gameObject.GetComponent<puddle>().type == puddle.Type.Green)
        {
            aiPath.canMove = false;
            animator.SetBool("walking", false);
            trapped = true;
        }
        if (collision.gameObject.tag == "HealExplosion")
        {
            aiPath.canMove = false;
            aiPath.enabled = false;
            GameObject.Instantiate(exlposionEffect, transform.position, Quaternion.identity);
            animator.SetBool("healed", true);
            animator.SetBool("walking", false);
            animator.SetBool("attack", false);

            healed = true;

            audio.clip = heal;
            audio.loop = false;
            audio.Play();
            roomManager.GetComponent<RoomManager>().enemiesHealed++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Puddle" && collision.gameObject.GetComponent<puddle>().type == puddle.Type.Green)
        {
            aiPath.canMove = true;
            trapped = false;
        }
    }
}
