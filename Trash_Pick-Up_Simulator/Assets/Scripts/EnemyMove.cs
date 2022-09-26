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

public class EnemyMove : MonoBehaviour
{
    public Transform playerTransform;
    private NavMeshAgent enemyAgent;

    private void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyAgent.destination = playerTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Game Over");
        }
    }
}
