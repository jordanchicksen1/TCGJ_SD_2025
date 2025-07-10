using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    [SerializeField] float launchForce = 15f;
    [SerializeField] Vector3 launchDirection = Vector3.up;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player01") || other.CompareTag("Player02"))
        {
            PlayerLauncher launcher = other.GetComponent<PlayerLauncher>();
            if (launcher != null)
            {
                Vector3 launchVelocity = transform.TransformDirection(launchDirection.normalized) * launchForce;
                launcher.Launch(launchVelocity);
                Debug.Log($"{other.tag} launched!");
            }
        }
    }


}
