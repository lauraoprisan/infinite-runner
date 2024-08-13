using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObjPastBound : MonoBehaviour
{
    public GameObject obj;  
    private float leftLimit = -10f;
    private float rightLimit = 12f;
   

    void Update()
    {
        if (transform.position.x < leftLimit || transform.position.x >rightLimit) {
            Destroy(obj);
        }
    }
}
