using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float playerSpeed;
    private float xRange = 8.8f;
    private float yRange = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Create the movement on x and y axis 
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector2.right * playerSpeed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector2.up * playerSpeed * verticalInput * Time.deltaTime);

        //Limit the movement on x and y axis
        float clampedX = Mathf.Clamp(transform.position.x, -xRange, xRange);
        float clampedY = Mathf.Clamp(transform.position.y, -yRange, yRange);
        transform.position = new Vector2(clampedX, clampedY);
    }
}
