using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
   private CharacterControls characterController;

    [SerializeField] public GameObject freezePanle;
    [SerializeField] public GameObject flashPanle;

    private void Start()
    {
        characterController = GetComponent<CharacterControls>();
    }

    IEnumerator Graviole()
    {
        yield return new WaitForSeconds(0);
        characterController.gravity = characterController.gravity * -1; 
        yield return new WaitForSeconds(3);
        characterController.gravity = characterController.gravity * -1;

    }

    public void Freeze()
    {
        freezePanle.SetActive(true);
        StartCoroutine(FreezePanel());
    }

    IEnumerator FreezePanel()
    {
        yield return new WaitForSeconds(3);
        freezePanle.SetActive(false);

    }
    public void Teleport()
    {

    }

    public void FlashBang()
    {
        flashPanle.SetActive(true);

        StartCoroutine(flashPanel());
    }
    IEnumerator flashPanel()
    {
        yield return new WaitForSeconds(5);
        flashPanle.SetActive(false);
    }

    public void SlimeSling()
    {

    }

    public void Burnt()
    {

    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Flame"))
        {
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
