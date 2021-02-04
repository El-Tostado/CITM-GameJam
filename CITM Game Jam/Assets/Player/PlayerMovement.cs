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

    GameObject[] potions = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
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
        direction = new Vector2(velX, velY).normalized;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float posX = mousePos.x;
        float posY = mousePos.y;
        CursorPrefab.transform.position = new Vector2(posX, posY);


        if (Input.GetButtonDown("Fire1"))
        {
            InstancedTarget = Instantiate(potion1);
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
    }

    void Move()
    {
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            switch (collision.gameObject.GetComponent<PotionItem>().type)
            {
                case (PotionItem.Type.Cure):
                    break;
                case (PotionItem.Type.Explosion):
                    break;
                case (PotionItem.Type.Sticky):
                    break;

            };



        }
    }
}
