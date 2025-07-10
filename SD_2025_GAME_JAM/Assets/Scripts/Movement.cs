using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;      // Movement speed
    [SerializeField] float rotationSpeed = 10f; // Sensitivity for mouse rotation

 

    private bool isTopDownActive = false;

    void Start()
    {
 
    }

    void Update()
    {
        MovePlayer();
        RotateWithMouse();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isTopDownActive = !isTopDownActive;

          
           
        }
    }

    void MovePlayer()
    {
        float moveForward = Input.GetAxisRaw("Vertical");   // W and S keys
        float moveSide = Input.GetAxisRaw("Horizontal");    // A and D keys

        Vector3 moveDirection = (transform.forward * moveForward + transform.right * moveSide).normalized;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void RotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X"); // Horizontal mouse movement
        transform.Rotate(0f, mouseX * rotationSpeed, 0f);
    }


}
