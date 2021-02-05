using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public Animator anim;

    public GameObject NextLevelDoor;
    bool enemiesKilled = false;
    public int enemiesHealed = 0;
    public int currentlevel;
    public int nextlevel;
    public PlayerMovement Player;
    public EnemyGraphics[] Enemies;

    Text counter; 

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Enemies = GameObject.FindObjectsOfType<EnemyGraphics>();
        counter = GameObject.Find("Counter").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesKilled = true;

        counter.text = enemiesHealed.ToString() + "/" + Enemies.Length.ToString();

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
