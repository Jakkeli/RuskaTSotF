using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTest : MonoBehaviour {

    public float speed = 1;

    float y;
    float x;

    public float radius;

	void Update () {
        y = radius / 100 * Mathf.Sin(Time.time * speed);
        x = radius / 100 * Mathf.Cos(Time.time * speed);
        transform.position += new Vector3(x, y, 0);
    }
}
