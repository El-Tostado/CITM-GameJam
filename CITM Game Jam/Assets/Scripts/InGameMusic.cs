using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMusic : MonoBehaviour
{

    public static InGameMusic Instance = null;

    void Awake()
    {

        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (SceneManager.GetActiveScene().name != "TheEnd" || SceneManager.GetActiveScene().name != "Main Menu")
            DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "TheEnd" || SceneManager.GetActiveScene().name == "Main Menu")
            Destroy(gameObject);
    }
}
