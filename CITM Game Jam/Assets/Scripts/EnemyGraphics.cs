using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyGraphics : MonoBehaviour
{
    public AIPath aiPath;

    private void Start()
    {
    }

    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1f);
        }
    }
}
