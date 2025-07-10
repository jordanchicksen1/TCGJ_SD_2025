using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLauncher : MonoBehaviour
{
    [SerializeField] float gravity = -20f;

    private Vector3 velocity;
    private CharacterController controller;
    private bool isLaunched = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Launch(Vector3 launchVelocity)
    {
        StartCoroutine(DelayedLaunch(launchVelocity));
    }

    private System.Collections.IEnumerator DelayedLaunch(Vector3 launchVelocity)
    {
        controller.enabled = false;
        velocity = launchVelocity;

        
        transform.position += launchVelocity * Time.deltaTime;

        yield return new WaitForSeconds(0.01f); 
        controller.enabled = true;
        isLaunched = true;
    }

    void Update()
    {
        if (controller.enabled && isLaunched)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            if (controller.isGrounded && velocity.y < 0)
            {
                velocity = Vector3.zero;
                isLaunched = false;
            }
        }
    }
}
