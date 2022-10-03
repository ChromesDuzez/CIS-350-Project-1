/*****************************************************************************
// File Name :         EnemySpawner.cs
// Author :            Colin Gamagami
// Brief Description : Handles enemy spawning
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private Vector3 maxSpawnRange;
    [SerializeField]
    private Vector3 minSpawnRange;
    [SerializeField]
    private float spawnInterval = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCouroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnCouroutine()
    {
        while (true)
        {
        yield return new WaitForSeconds(spawnInterval);
        SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemy, new Vector3(Random.Range(minSpawnRange.x, maxSpawnRange.x), Random.Range(minSpawnRange.y, maxSpawnRange.y), Random.Range(minSpawnRange.z,maxSpawnRange.z)), enemy.transform.rotation);
    }
}
