using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    static int spawnTime = 0;

    void Start()
    {
        spawnTime = Random.Range(180, 360);
    }

    void Update()
    {
        spawnTime--;
        if (spawnTime <= 0)
        {
            Instantiate(enemyPrefab, new Vector3(transform.position.x, 1.05f, transform.position.z), Quaternion.identity);
            spawnTime = Random.Range(120, 320);
        }
    }
}
