using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 direction;
    public Rigidbody2D rb;

    public float moveSpeed;
    public float aimSpeed;

    public GameObject potion1;
    public GameObject CursorPrefab;
    public GameObject TargetPrefab;

    public float maxRange = 3;

    private GameObject InstancedTarget;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        processInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void processInputs()
    {
        float velX = Input.GetAxisRaw("Horizontal");
        float velY = Input.GetAxisRaw("Vertical");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float posX = mousePos.x;
        float posY = mousePos.y;

        CursorPrefab.transform.position = new Vector3(Mathf.Cos(Mathf.Atan2(posY, posX)) + transform.position.x, posY, 0);

        direction = new Vector2(velX, velY).normalized;

        if (Input.GetButtonDown("Fire1"))
        {
            //if (released)
            //{
            //    if (InstancedTarget != null)
            //    {
            //        Destroy(InstancedTarget);
            //    }
            //    else
            //    {
            //        InstancedTarget = Instantiate(TargetPrefab, transform);
            //        released = false;
            //    }
            //}

            //if (InstancedTarget != null)
            //{
            //    Vector3 direction = CursorPrefab.transform.position - transform.position;
            //    direction.Normalize();
            //    InstancedTarget.transform.position = InstancedTarget.transform.position - transform.position + (direction * aimSpeed * Time.deltaTime);
            //}
            InstancedTarget = Instantiate(potion1, transform);
            Transform t = InstancedTarget.GetComponent<PotionMovement>().target;
            t = CursorPrefab.transform;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, mousePos - transform.position, (mousePos - transform.position).magnitude);
            for (int i = 0; i < hits.Length; ++i)
            {
                if (hits[i].collider.transform.gameObject.layer == 8)
                {
                    Debug.Log(hits[i].collider.name);
                    t.position = new Vector3(hits[i].point.x, hits[i].point.y, t.position.z);
                }
            }

        }
        Debug.DrawRay(transform.position, mousePos - transform.position);
        //if (Input.GetButtonUp("Fire1"))
        //{
        //    Destroy(InstancedTarget);
        //    released = true;
        //}
    }

    void Move()
    {
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }
}
