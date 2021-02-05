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

    public Sprite[] potionTextures = new Sprite[4];
    public Sprite empty;
    public Image PotionUI;
    public Image PotionUI2;

    public int[] potions = new int[2];
    public int currentPotion = 0;

    public bool isDead = false;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        for (int i = 0; i < 2; ++i)
            potions[i] = -1;

        PotionUI = GameObject.Find("PotionUI").GetComponent<Image>();
        PotionUI2 = GameObject.Find("PotionUI2").GetComponent<Image>();

        animator = GetComponent<Animator>();
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
        if (velX == 0 && velY == 0)
        {
            animator.SetBool("walking", false);
        }
        else
        {
            animator.SetBool("walking", true);
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float posX = mousePos.x;
        float posY = mousePos.y;
        CursorPrefab.transform.position = new Vector2(posX, posY);


        if (Input.GetButtonDown("Fire1") && potions[currentPotion] != -1)
        {
            InstancedTarget = Instantiate(potion1,transform.position, Quaternion.identity);
            InstancedTarget.GetComponent<PotionMovement>().SetType((PotionItem.Type)potions[currentPotion]);
            Transform t = InstancedTarget.GetComponent<PotionMovement>().target;
            t = CursorPrefab.transform;

            potions[currentPotion] = -1;
            currentPotion = (currentPotion + 1) % 2;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, CursorPrefab.transform.position - transform.position, (CursorPrefab.transform.position - transform.position).magnitude);
            
            for (int i = 0; i < hits.Length; ++i)
            {
                if (hits[i].collider.transform.gameObject.layer == 8)
                {
                    t.position = new Vector3(hits[i].point.x, hits[i].point.y, t.position.z);
                }
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            currentPotion = (currentPotion + 1) % 2;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 )
        {
            currentPotion = (currentPotion + 1) % 2;
        }


        if (potions[currentPotion] != -1)
        {
            PotionUI.overrideSprite = potionTextures[potions[currentPotion]];
            PotionUI.color = Color.white;
        }
        else
        {
            PotionUI.color = Color.clear;
        }

        if (potions[(currentPotion + 1) % 2] != -1)
        {
            PotionUI2.overrideSprite = potionTextures[potions[(currentPotion + 1) % 2]];
            PotionUI2.color = Color.white;
        }
        else
        {
            PotionUI2.color = Color.clear;
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
                        currentPotion = i;
                        exists = true;
                        break;
                    }
                }
                if(exists)
                    Destroy(collision.gameObject);
            }
        }
    }
}
