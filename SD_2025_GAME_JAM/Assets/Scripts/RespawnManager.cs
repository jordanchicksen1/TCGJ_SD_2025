using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RespawnManager : MonoBehaviour
{
    public List<Transform> SpawnPoints;
    public int HP = 100;
    public Slider hpSlider;
    public int DeathCount;
    public TextMeshProUGUI DeathText;
    public string Scene;
    private void Update()
    {
        if (HP <= 0)
        {
            CharacterController characterController = GetComponent<CharacterController>();
            characterController.enabled = false;
            transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Count)].position;
            characterController.enabled = true;
            DeathCount++;

            HP = 100;
        }

        hpSlider.value = HP;

        DeathText.text = DeathCount.ToString();
        if (DeathCount == 5)
        {
            SceneManager.LoadScene(Scene);
        }
    }

}
