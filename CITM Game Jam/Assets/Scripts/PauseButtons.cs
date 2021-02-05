using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{

    private bool IsPaused;

    public Animator anim;

    public GameObject bg;
    public GameObject resume;
    public GameObject menu;
    public GameObject exit;

    // Start is called before the first frame update
    void Start()
    {
        IsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsPaused)
            {
                UnPauseGame();
                CloseESCMenu();
                IsPaused = false;
                Cursor.visible = false;
            }
            else if (!IsPaused)
            {
                PauseGame();
                OpenESCMenu();
                IsPaused = true;
                Cursor.visible = true;
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1;
    }

    private void OpenESCMenu()
    {
        bg.SetActive(true);
        resume.SetActive(true);
        menu.SetActive(true);
        exit.SetActive(true);
    }

    private void CloseESCMenu()
    {
        bg.SetActive(false);
        resume.SetActive(false);
        menu.SetActive(false);
        exit.SetActive(false);
    }

    public void OnResume()
    {
        UnPauseGame();
        CloseESCMenu();
        IsPaused = false;
    }

    public void OnMenu()
    {
        UnPauseGame();
        StartCoroutine(LoadlevelMenu());
    }

    public void OnExit()
    {
        Application.Quit();
    }

    IEnumerator LoadlevelMenu()
    {
        anim.SetBool("transitionEND", true);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Main Menu");
    }
}
