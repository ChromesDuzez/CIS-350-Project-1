/*****************************************************************************
// File Name :         EnemyMove.cs
// Author :            Colin Gamagami
// Creation Date :     September 25, 2022
//
// Brief Description : A C# script that handles movement of the enemy chasing the player
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    public Transform playerTransform;
    private NavMeshAgent enemyAgent;
    private MessTracker messTracker;
    public bool canBeHurt = true;
    private PlayerController pc;

    private void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        messTracker = GameObject.FindGameObjectWithTag("MessTracker").GetComponent<MessTracker>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (!playerTransform)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyAgent.destination = playerTransform.position;

        if(pc.health <= 0)
        {
            messTracker.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(canBeHurt)
            {
                StartCoroutine("Damage");
            }
        }
    }

    public IEnumerator Damage()
    {
        pc.health--;
        canBeHurt = false;
        yield return new WaitForSeconds(0.5f);
        canBeHurt = true;
    }
}
