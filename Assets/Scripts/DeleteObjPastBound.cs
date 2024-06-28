using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObjPastBound : MonoBehaviour
{
    public GameObject obj;
    private float leftLimit = -10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (obj.transform.position.x < leftLimit) {
            Destroy(obj);
        }
    }
}
