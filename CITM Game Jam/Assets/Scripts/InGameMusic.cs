using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMusic : MonoBehaviour
{
    void Awake()
    {
        if(SceneManager.GetActiveScene().name != "TheEnd" || SceneManager.GetActiveScene().name != "Main Menu")
            DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "TheEnd" || SceneManager.GetActiveScene().name == "Main Menu")
            Destroy(gameObject);
    }
}
