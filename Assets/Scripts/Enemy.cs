using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    //Recieving Spawner info
    public Spawner spawner;
    

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    
    //Healthbar
    public Image healthBar;
    //public float healthAmount = 10;
    //Makes health editable in the Inspector - Simon
    public float healthAmount;

    [SerializeField] private float health, maxHealth = 10f;
    
    //Patrolling
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    
    //attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject projectile;
    public Transform gunBarrel;
    
    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;
    }

    private void Update()
    {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        float distance = Vector3.Distance(player.position, transform.position);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
        if (distance <= sightRange)
        {
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }

    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        

        //Check if ground is there to walk on
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        FaceTarget();
    }

    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void AttackPlayer()
    {
        //make sure enemy doesnt move
        agent.SetDestination(transform.position);
        
        FaceTarget();
        //                                       transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //AttackCode
            // Rigidbody rb = Instantiate(projectile, transform.position, transform.rotation).GetComponent<Rigidbody>();
            GameObject bullet = Instantiate(projectile, gunBarrel.position, gunBarrel.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 32f, ForceMode.Impulse);
            // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            // rb.AddForce(transform.up * 4f, ForceMode.Impulse);
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damageAmount)
    {
        Debug.Log("TakeDamageCalled");
        health -= damageAmount;
        healthBar.fillAmount = health / healthAmount;

        var addNew = true;

        //Spawning enemies on certain health treshholds * Buggy: Enemy doesn't spawn. And bullet does not get destroyed, but still deals damage 
        if (health == 98f && addNew)
        {
            Debug.Log("spawn!!!!!!!");
            spawner.SpawnEnemy();
            spawner.SpawnEnemy();
            spawner.SpawnEnemy();
            addNew = false;
            
            
        }
        else if (health == 80f && addNew)
        {
            Debug.Log("spawn!!!!!!!");
            spawner.SpawnEnemy();
            spawner.SpawnEnemy();
            spawner.SpawnEnemy();
            addNew = false;

        }

        else if(health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
