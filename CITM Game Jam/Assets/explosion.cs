using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    float timer = 0f;
    public float duration = 1;

    private Shake shake;

    void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 0f)
            shake.CamShake();

        timer += Time.deltaTime;
        if (timer >= duration)
            Destroy(gameObject);     
    }
}
