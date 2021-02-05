using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public Animator anim;

    public GameObject NextLevelDoor;
    bool enemiesKilled = false;

    public int currentlevel;
    public int nextlevel;
    public PlayerMovement Player;
    public EnemyGraphics[] Enemies;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Enemies = GameObject.FindObjectsOfType<EnemyGraphics>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesKilled = true;

        foreach (var enemy in Enemies)
        {
            if (!enemy.healed)
            {
                enemiesKilled = false;
            }
        }

        if (enemiesKilled)
        {
            if (NextLevelDoor != null)
                NextLevelDoor.GetComponent<Door>().Open();
        }

        if (Player.isDead && Player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            ResetScene();
        }
    }

    public void ResetScene()
    {
        StartCoroutine(Loadlevel(currentlevel));
    }

    public void NextLevel()
    {
        StartCoroutine(Loadlevel(nextlevel));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            NextLevel();
        }
    }

    IEnumerator Loadlevel(int level)
    {
        anim.SetBool("transitionEND", true);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(level);
    }

}
