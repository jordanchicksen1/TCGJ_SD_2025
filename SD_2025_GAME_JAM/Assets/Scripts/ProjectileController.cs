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

    public bool isLightning, isFlame, isGlove, isMolotove, isMoon;
    public GameObject Player;
    public CharacterController characterController;

    public AudioSource sfx2;
    public AudioClip playerHit;
    GameObject AudioSource;

    private void Start()
    {
        OriginalPosition = transform.position;
        AudioSource = GameObject.FindGameObjectWithTag("Sfx2");

        Destroy(gameObject, 10);
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
        //Support Effects
        if (other.CompareTag("Player01") || other.CompareTag("Player02"))
        {
            sfx2 = AudioSource.GetComponent<AudioSource>();
            sfx2.Play();
            if (IsSlimeBall)
            {
                Player = other.gameObject;
                EffectManager effectscript = Player.GetComponent<EffectManager>();
                effectscript.SlimeSling();
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
                EffectManager effectScript = Player.GetComponent<EffectManager>();
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

            //Attack Effects and Damage
            else if (isFlame)
            {
                Player = other.gameObject;
                RespawnManager PlayerHealth = Player.GetComponent<RespawnManager>();
                PlayerHealth.HP -= 25;
                Destroy(gameObject);
            }
            else if (isGlove)
            {
                Player = other.gameObject;
                RespawnManager PlayerHealth = Player.GetComponent<RespawnManager>();
                PlayerHealth.HP -= 50;
                Destroy(gameObject);
            }
            else if (isLightning)
            {
                Player = other.gameObject;
                RespawnManager PlayerHealth = Player.GetComponent<RespawnManager>();
                PlayerHealth.HP -= 40;
                Destroy(gameObject);
            }
            else if (isMolotove)
            {
                Player = other.gameObject;
                EffectManager effectManager = Player.GetComponent<EffectManager>();
                effectManager.Burn();
                Destroy(gameObject);
            }
            else if (isMoon)
            {
                Player = other.gameObject;
                RespawnManager PlayerHealth = Player.GetComponent<RespawnManager>();
                PlayerHealth.HP -= 25;
                EffectManager effectsScript = Player.GetComponent<EffectManager>();
                effectsScript.Heavy();
                Destroy(gameObject);
            }
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
