using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RespawnManager : MonoBehaviour
{
    public List<Transform> SpawnPoints;
    public int HP = 100;
    public Slider hpSlider;
    private void Update()
    {
        if (HP <= 0)
        {
            CharacterController characterController = GetComponent<CharacterController>();
            characterController.enabled = false;
            transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Count)].position;
            characterController.enabled = true;

            HP = 100;
        }

        hpSlider.value = HP;
    }

}
