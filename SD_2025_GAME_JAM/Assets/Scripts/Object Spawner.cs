using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //Collectable Items
    public GameObject Items;
    public List<Transform> SpawnPoints;
    public GameObject[] CurrentItems;
    public void SpawnItems()
    {
        for(int i =0; i < SpawnPoints.Count; i++)
        {
           GameObject Item = Instantiate(Items, SpawnPoints[i].position, Quaternion.identity);
            Item.transform.parent = SpawnPoints[i];
        }
        CurrentItems = GameObject.FindGameObjectsWithTag("Item");
    }
}
