using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Boolean isToRight = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        float direction = isToRight ? 1 : -1;
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);
    }
}
