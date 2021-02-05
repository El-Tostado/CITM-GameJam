using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public Animator anim;
    public GameObject Play;
    public GameObject Title;
    public GameObject Credits;
    public GameObject Exit;
    public GameObject Back;
    public GameObject CreditsImage;

    public AudioSource audio;

    public void Start()
    {
        GameObject.Find("Back").SetActive(false);
        GameObject.Find("CreditsImage").SetActive(false);
    }

    public void OnPlay()
    {
        audio.Play();
        StartCoroutine(LoadlevelStart());
    }

    public void OnCredits()
    {
        audio.Play();
        Title.SetActive(false);
        Play.SetActive(false);
       Credits.SetActive(false);
        Exit.SetActive(false);
        Back.SetActive(true);
        CreditsImage.SetActive(true);
    }

    public void OnBack()
    {
        audio.Play();
        Title.SetActive(true);
        Play.SetActive(true);
        Credits.SetActive(true);
        Exit.SetActive(true);
        Back.SetActive(false);
        CreditsImage.SetActive(false);
    }

    public void OnExit()
    {
        audio.Play();
        Application.Quit();
    }

    IEnumerator LoadlevelStart()
    {
        anim.SetBool("transitionEND", true);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("level1");
    }

}
