using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyGraphics : MonoBehaviour
{
    public AIPath aiPath;
    Animator animator;

    public GameObject attackColliderLeft;
    public GameObject attackColliderRight;

    bool facingRight = false;

    public float attackSpeed = 1.0f;
    public float visionRange = 40f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        attackColliderLeft.SetActive(false);
        attackColliderRight.SetActive(false);
    }

    void Update()
    {
        if (aiPath.desiredVelocity.x <= 0.01f && aiPath.desiredVelocity.x >= -0.01f)
            animator.SetBool("walking", false);
        else
        {
            animator.SetBool("walking", true);
            attackColliderRight.SetActive(false);
            attackColliderLeft.SetActive(false);
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

        if (Vector2.Distance(transform.position, GetComponentInParent<AIDestinationSetter>().target.position) <= aiPath.endReachedDistance)
            Attack();
       
        else
            animator.SetBool("attack", false);

        if (Vector2.Distance(transform.position, GetComponentInParent<AIDestinationSetter>().target.position) <= visionRange)
            aiPath.enabled = true;
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
}
