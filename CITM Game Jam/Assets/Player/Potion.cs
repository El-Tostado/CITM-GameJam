using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
	public float speed = 1;

	public Vector3 p0 = new Vector3(0, 0, 0);
	public Vector3 p1 = new Vector3(0, 0, 0);

	public float h = 1;

	[Range(0, 1)]
	public float s = 0;

	public Vector3 UP = new Vector3(0, 0, 1);

	public Vector3 pc = new Vector3(0, 0, 0);
	public Vector3 CameraN = new Vector3(0, 1 / Mathf.Sqrt(2), -1 / Mathf.Sqrt(2));

	// Use this for initialization
	void Start()
	{
		CameraN.Normalize();
		p1 = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		//pc = Camera.main.transform.position;
        Vector3 fp = f();
        Vector3 Vy = projection(UP);
        Vector3 Vx = Vector3.Cross(Vy, CameraN);
        float x = Vector3.Dot(fp, Vx);
        float y = Vector3.Dot(fp, Vy);
        transform.position = new Vector3(-x, y, transform.position.z);

        s = (s + speed * Time.deltaTime);

        if (s > 1)
		{
			s = s % 1;
			//Destroy(gameObject);
			Debug.Log("----------------------------------");
			Debug.Log("----------------------------------");
		}
		Debug.Log(transform.position);
	}

	Vector3 f()
	{
		return s * p0 + (1 - s) * p1 + UP * 4 * h * s * (1 - s);
	}

	//Project 3D Space to 2D
	Vector3 projection(Vector3 p)
	{
		return p - Vector3.Dot(p, CameraN) * CameraN;
	}
}
