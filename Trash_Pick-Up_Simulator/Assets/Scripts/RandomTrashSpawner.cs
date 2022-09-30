/*****************************************************************************
// File Name :         RandomTrashSpawner.cs
// Author :            John Green
// Brief Description : Handles random trash spawn rates
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomTrashSpawner : MonoBehaviour
{
    //public members
    public bool ready = false;
    public float accelerator = .15f;
    public GameObject[] trashPrefabs = new GameObject[2];
    public float waitTime = 7f;
    public Vector3 maxSpawnRange;
    public Vector3 minSpawnRange;
    public float maxSpawnRate = 4f;

    public Slider messTracker;

    //private members
    private int startDelay;

    void Start()
    {
        //chooses delay and sets start bool to true after it passes
        startDelay = Random.Range(0, 10);
        Invoke("InitialWait", startDelay);
    }

    void Update()
    {
        //prevents coroutine being called hundreds of times 
        if(ready)
        {
            StartCoroutine("TrashSpawn");
        }
    }

    IEnumerator TrashSpawn()
    {
        //spawns random trash prefab at random spot within range
        Vector3 offset = new Vector3(Random.Range(minSpawnRange.x, maxSpawnRange.x), Random.Range(minSpawnRange.y, maxSpawnRange.y), Random.Range(minSpawnRange.z, maxSpawnRange.z));
        Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Length)], gameObject.transform.position + offset, gameObject.transform.rotation);

        messTracker.value++;

        //delay for next spawn
        ready = false;
        yield return new WaitForSeconds(waitTime);
        ready = true;

        //makes spawn delay decrease over time
        if(waitTime > maxSpawnRate)
        {
            waitTime -= accelerator;
        }
    }

    //enables coroutine startup
    void InitialWait()
    {
        ready = true;
    }

}
