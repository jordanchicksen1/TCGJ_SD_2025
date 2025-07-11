using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
   private CharacterControls characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterControls>();
    }

    IEnumerator Graviole()
    {
        yield return new WaitForSeconds(0);
        characterController.gravity = characterController.gravity * -1; 
        yield return new WaitForSeconds(1);
        characterController.gravity = characterController.gravity * -1;

    }

    public void Freeze()
    {

    }

    public void Teleport()
    {

    }

    public void FlashBang()
    {

    }

    public void SlimeSling()
    {

    }

    public void Burnt()
    {

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Flame"))
        {
            Destroy(hit.collider.gameObject);
            StartCoroutine(Graviole());
        }
    }


    //Side Effects
    public void SmokeScreen()
    {

    }

    public void BlackCloud()
    {

    }

    public void ScreenShake()
    {

    }

    public void Burn()
    {

    }

    public void ShakeLevel()
    {

    }
}
