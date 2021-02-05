﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public GameObject NextLevelDoor;
    bool enemiesKilled = false;

    public string currentlevel;
    public string nextlevel;
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

        if (Player.isDead)
        {
            ResetScene();
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName(currentlevel).buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName(nextlevel).buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            NextLevel();
        }
    }

}