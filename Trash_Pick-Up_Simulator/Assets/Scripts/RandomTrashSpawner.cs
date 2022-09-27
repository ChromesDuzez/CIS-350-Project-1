using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomTrashSpawner : MonoBehaviour
{
    public bool ready = false;
    public float accelerator = .15f;
    public GameObject[] trashPrefabs = new GameObject[2];
    private int startDelay;
    public float waitTime = 7f;
    public Vector3 maxSpawnRange;
    public Vector3 minSpawnRange;
    public float maxSpawnRate = 4f;

    public Slider messTracker;

    void Start()
    {
        startDelay = Random.Range(0, 10);
        Invoke("InitialWait", startDelay);
    }

    void Update()
    {
        if(ready)
        {
            StartCoroutine("TrashSpawn");
        }
    }

    IEnumerator TrashSpawn()
    {
        Vector3 offset = new Vector3(Random.Range(minSpawnRange.x, maxSpawnRange.x), Random.Range(minSpawnRange.y, maxSpawnRange.y), Random.Range(minSpawnRange.z, maxSpawnRange.z));
        Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Length)], gameObject.transform.position + offset, gameObject.transform.rotation);
        messTracker.value++;
        ready = false;
        yield return new WaitForSeconds(waitTime);
        ready = true;
        if(waitTime > maxSpawnRate)
        {
            waitTime -= accelerator;
        }
    }

    void InitialWait()
    {
        ready = true;
    }

}
