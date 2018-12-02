using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waver : MonoBehaviour {

    public float waveSpeed = 3f;
    public float widthMultiplier = 0.3f;

    private float alpha = 0;
    private float startY;

    void Start() {
        startY = transform.position.y;
    }

    void Update() {
        transform.position = new Vector3(transform.position.x, startY + Mathf.Sin(alpha) * widthMultiplier, transform.position.z);
        alpha += Time.deltaTime * waveSpeed;
    }
}
