using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
   private CharacterControls characterController;

    [SerializeField] public GameObject freezePanle;
    [SerializeField] public GameObject flashPanle;
    [SerializeField] public GameObject slimePanle;
    [SerializeField] public GameObject burnPanle;
    [SerializeField] public GameObject burnParticle;
    public GameObject Bubble;
    public bool canFloat;
    private bool isRunning;

    private void Start()
    {
        characterController = GetComponent<CharacterControls>();
    }

    private void Update()
    {
        MoonFloat();
    }
    public void Float()
    {
        StartCoroutine(Graviole());
    }
    IEnumerator Graviole()
    {
        yield return new WaitForSeconds(0);
        Bubble.SetActive(true);
        characterController.gravity = characterController.gravity * -1; 
        yield return new WaitForSeconds(3);
        characterController.gravity = characterController.gravity * -1;
        Bubble.SetActive(false);


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
    public void Heavy()
    {
        StartCoroutine(HeavyMovement());
    }

    IEnumerator HeavyMovement()
    {
        CharacterControls characterScript = GetComponent<CharacterControls>();
        characterScript.moveSpeed -= 4;
        yield return new WaitForSeconds(5);
        characterScript.moveSpeed += 4;

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
        slimePanle.SetActive(true);
        StartCoroutine(slimePanel());
    }
    IEnumerator slimePanel()
    {
        yield return new WaitForSeconds(0.5f);
        slimePanle.SetActive(false);
    }

    public void HotFeet()
    {
        CharacterControls characterControls = GetComponent<CharacterControls>();
        characterControls.moveSpeed += 7;
        StartCoroutine(HotFeets());
    }

    IEnumerator HotFeets()
    {
        yield return new WaitForSeconds(5);
        CharacterControls characterControls = GetComponent<CharacterControls>();
        characterControls.moveSpeed -= 7;
    }

    public void Burnt()
    {

    }

    private void OnTriggerEnter(Collider hit)
    {
        
    }

    public void Burn()
    {
        StartCoroutine(TakeDamage());
        burnPanle.SetActive(true);
        burnParticle.SetActive(true);
    }

    IEnumerator TakeDamage()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(3);
            RespawnManager HealthScript = GetComponent<RespawnManager>();
            HealthScript.HP -= 10;
        }
        burnPanle.SetActive(false);
        burnParticle.SetActive(false);

    }
    public void ShakeLevel()
    {

    }

    public void MoonFloat()
    {
       if(!isRunning)
        {
            StartCoroutine(moonFloat());
        }
    }
    IEnumerator moonFloat()
    {
        if (canFloat)
        {
            Debug.Log("e");
            isRunning = true;
            characterController = GetComponent<CharacterControls>();
            yield return new WaitForSeconds(Random.Range(3, 5));
            characterController.gravity = characterController.gravity * -1;
            yield return new WaitForSeconds(Random.Range(1, 3));
            characterController.gravity = characterController.gravity * -1;
            isRunning = false;
        }


    }
}
