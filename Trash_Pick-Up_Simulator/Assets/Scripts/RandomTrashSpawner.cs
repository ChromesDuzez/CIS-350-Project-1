using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrashSpawner : MonoBehaviour
{
    public bool ready = true;
    public float accelerator = .15f;
    public GameObject[] trashPrefabs = new GameObject[2];
    public float startDelay;
    public float waitTime = 7f;

    private void Start()
    {
        startDelay = Random.Range(0, 10);
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
        Vector3 offset = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Length)], gameObject.transform.position + offset, gameObject.transform.rotation);
        ready = false;
        yield return new WaitForSeconds(waitTime);
        ready = true;
        if(waitTime > 3)
        {
            waitTime -= accelerator;
        }
    }
}
