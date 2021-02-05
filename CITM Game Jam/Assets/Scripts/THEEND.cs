using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class THEEND : MonoBehaviour
{

    public Animator anim;

    public AudioSource audio;

    public void OnMenu()
    {
        audio.Play();
        StartCoroutine(LoadlevelMenu());
    }

    IEnumerator LoadlevelMenu()
    {
        anim.SetBool("transitionEND", true);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Main Menu");
    }
}
