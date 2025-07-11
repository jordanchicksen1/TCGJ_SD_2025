using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public List<Transform> SpawnPoints;
    public int HP = 100;

    private void Update()
    {
        if (HP <= 0)
        {
            transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Count)].position;
        }
    }

}
