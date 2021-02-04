using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyGraphics : MonoBehaviour
{
    public AIPath aiPath;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (aiPath.desiredVelocity.x <= 0.01f && aiPath.desiredVelocity.x >= -0.01f)
            animator.SetBool("walking", false);
        else
            animator.SetBool("walking", true);

        if (aiPath.desiredVelocity.x >= 0.0001f)
            GetComponent<SpriteRenderer>().flipX = true;

        else if (aiPath.desiredVelocity.x <= -0.0001f)
            GetComponent<SpriteRenderer>().flipX = false;

        if (Vector2.Distance(transform.position, GetComponentInParent<AIDestinationSetter>().target.position) <= aiPath.endReachedDistance)
        {
            Debug.Log("HitPlayer");
        }
    }
}
