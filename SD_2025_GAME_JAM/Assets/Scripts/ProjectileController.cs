using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Vector3 hitPoint;
    public int MoveSpeed;
    private Vector3 OriginalPosition;
    public bool IsSlimeBall, isIce, isFlashStar, isBubble, isHotFeet, isTeleport;
    public GameObject Player;
    public CharacterController characterController;

    private void Start()
    {
        OriginalPosition = transform.position;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, hitPoint,MoveSpeed *  Time.deltaTime);
        if (Player != null && IsSlimeBall)
        {
            characterController = Player.GetComponent<CharacterController>();
            characterController.enabled = false;
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, OriginalPosition, 10 * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.position, hitPoint);

        if (distance <=2 && isTeleport)
        {
            CharacterController characterController = Player.GetComponent< CharacterController>();
            characterController.enabled = false;
            Player.transform.position = transform.position;
            characterController.enabled = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player01") || other.CompareTag("Player02"))
        {
            if (IsSlimeBall)
            {
                Player = other.gameObject;
                StartCoroutine(Pull());
            }
            else if (isIce)
            {
                Player = other.gameObject;
                characterController = Player.GetComponent<CharacterController>();
                characterController.enabled = false;
                isIce = false;
                EffectManager effectScript = Player.GetComponent<EffectManager>();
                effectScript.Freeze();
                StartCoroutine(Freeze());
            }
            else if (isFlashStar)
            {
                Player = other.gameObject;
                EffectManager effectScript= Player.GetComponent<EffectManager>();
                effectScript.FlashBang();

            }
            else if (isBubble)
            {
                Player = other.gameObject;
                EffectManager effectScript = Player.GetComponent<EffectManager>();
                effectScript.Float();
                Destroy(gameObject);
            }
            else if (isBubble)
            {
                Player = other.gameObject;
                EffectManager effectScript = Player.GetComponent<EffectManager>();
                effectScript.HotFeet();
                Destroy(gameObject);
            }
            


        }
        else  if (other != null)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Pull()
    {
        yield return new WaitForSeconds(0.5f);
        characterController.enabled = true;
        Player = null;
    }

    IEnumerator Freeze()
    {
        yield return new WaitForSeconds(3);
        characterController.enabled = true;
        Player = null;
        Destroy(gameObject);

    }
}
