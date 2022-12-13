using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class MinionAI : MonoBehaviour
{
    NavMeshAgent agent;

    //Health on Minions
    [SerializeField] float startHealth, maxHealth;
    public float healthAmount;
    //public float maxHealth;

    GameObject target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        healthAmount = startHealth;
    }

    private void Update()
    {
        GoToTarget();
    }

    private void GoToTarget()
    {
        agent.SetDestination(target.transform.position);
    }

    public void MinionTakeDamage(float damageAmount)
    {
        Debug.Log("TakeDamageCalled");
        healthAmount -= damageAmount;
        if (healthAmount <= 0)
        {
            Destroy(gameObject);
        }
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Damages the player


        if (collision.gameObject.TryGetComponent<PlayerMovementController>(out PlayerMovementController playerComponent))
        {
            playerComponent.PlayerDamage(1);
            Destroy(gameObject);
        }
    }
}
