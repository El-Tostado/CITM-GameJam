using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 direction;
    public Rigidbody2D rb;

    public float moveSpeed;

    public GameObject potion1;
    public GameObject CursorPrefab;

    public float maxRange = 3;

    private GameObject InstancedTarget;

    public Texture[] potionTextures = new Texture[4];
    public Texture empty;
    public RawImage PotionUI;

    public int[] potions = new int[2];
    int currentPotion = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        for (int i = 0; i < 2; ++i)
            potions[i] = -1;
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

        if( velX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if( velX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float posX = mousePos.x;
        float posY = mousePos.y;
        CursorPrefab.transform.position = new Vector2(posX, posY);


        if (Input.GetButtonDown("Fire1") && potions[currentPotion] != -1)
        {
            potions[currentPotion] = -1;
            currentPotion = (currentPotion + 1) % 2;
            InstancedTarget = Instantiate(potion1,transform.position, Quaternion.identity);
            Transform t = InstancedTarget.GetComponent<PotionMovement>().target;
            t = CursorPrefab.transform;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, mousePos - transform.position, (mousePos - transform.position).magnitude);
            Debug.Log((mousePos - transform.position).magnitude);
            for (int i = 0; i < hits.Length; ++i)
            {
                Debug.Log(hits[i].collider.name);
                if (hits[i].collider.transform.gameObject.layer == 8)
                {
                    t.position = new Vector3(hits[i].point.x, hits[i].point.y, t.position.z);
                }
            }
            Debug.DrawRay(transform.position, mousePos - transform.position);
        }

        if (Input.GetKeyDown(KeyCode.Q) && currentPotion == 1)
        {
            currentPotion--;
        }
        if (Input.GetKeyDown(KeyCode.E) && currentPotion == 0)
        {
            currentPotion++;
        }

        if (potions[currentPotion] != -1)
        {
            PotionUI.texture = potionTextures[potions[currentPotion]];
        }
        else
        {
            PotionUI.texture = empty;
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Item") && Input.GetKey(KeyCode.E))
        {
            bool exists = false;
            foreach(int i in potions)
            {
                if (i == (int)collision.GetComponent<PotionItem>().type)
                    exists = true;
            }

            if(!exists)
            {
                for (int i = 0; i < 2; ++i)
                {
                    if(potions[i] == -1)
                    {
                        potions[i] = (int)collision.GetComponent<PotionItem>().type;
                        exists = true;
                        currentPotion = i;
                        break;
                    }
                }
                if(exists)
                    Destroy(collision.gameObject);
            }
        }
    }
}
