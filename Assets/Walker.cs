using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Walker : MonoBehaviour
{
    Rigidbody walker_rigidbody;

    public Animator animator;

    [Header("The parent of all the positions that will be walked")]
    public Transform pathParent;

    [Header("Enabled turning before walking back")]
    [Tooltip("Makes agent stop and turn instead of just changing direction")]
    public bool stopAndTurn = false;
    
    [Header("Press once to recalculate path!")]
    [Tooltip("one useful for when the path is edited during runtime")]
    public bool reCalculatePath;

    public float smoothingAmmount = 1.0f;
    public float turningSpeed = 1.0f;

    public float moveSpeed = 10.0f;
    public float MAX_SPEED = 3.0f;

    float rageTime = 1.45f;
    float rageTimer = 1.45f;    
    
    float hitTime = 3.06f;
    float hitTimer = 3.06f;

    List<Transform> walkPath = new List<Transform>();

    List<Transform> gizmoPath = new List<Transform>();

    int goalNodeIndex;
    bool walkingTowardsRed = true;

    public float startDelay;


    public enum State
    {
        WALKING, TURNING, RAGE, WAITING, HIT
    }
    public State currentState = State.WAITING;

    void Awake()
    {
        goalNodeIndex = 0;
        walker_rigidbody = GetComponent<Rigidbody>();
        for (int i = 0; i < pathParent.childCount; i++)
        {
            walkPath.Add(pathParent.GetChild(i));
        }
    }
    void Start()
    {
        currentState = State.WAITING;
        startDelay = Random.Range(0.0f, 2.5f);
    }
    void FixedUpdate()
    {
        if (walker_rigidbody.velocity.magnitude > MAX_SPEED)
        {
            walker_rigidbody.velocity.Normalize();
            walker_rigidbody.velocity *= MAX_SPEED;
        }
        if (walker_rigidbody.velocity.magnitude > MAX_SPEED)
        {

        }
    }

    void Update()
    {
        if (reCalculatePath)
        {
            reCalculatePath = false;
            for (int i = 0; i < pathParent.childCount; i++)
            {
                walkPath.Add(pathParent.GetChild(i));
            }
        }

        if (goalNodeIndex >= walkPath.Count)
        {
            return;
        }

        Transform currentNode = walkPath[goalNodeIndex];
        Transform lastNode = walkingTowardsRed ? walkPath[walkPath.Count - 1] : walkPath[0];

        if(currentState == State.WAITING)
        {
            startDelay -= Time.deltaTime;
            if (startDelay <= 0)
            {
                currentState = State.WALKING;
                animator.SetTrigger("Walk");
            }
        }
        else if (currentState == State.WALKING)
        {
            //Direction to next node
            Vector3 targetDirection = (currentNode.position - transform.position);
            targetDirection.Normalize();
            Vector3 direction = Vector3.Lerp(transform.forward, targetDirection, Time.deltaTime * turningSpeed / smoothingAmmount);
            direction.y = 0; direction.Normalize(); direction.y = 0;
            transform.forward = direction;
            walker_rigidbody.velocity += direction * Time.deltaTime * moveSpeed;

            //UPDATE TO WALK TOWARDS THE NEXT NODE
            if (Vector3.Distance(currentNode.position, transform.position) <= 0.5f)
            {

            }
            else if (Vector3.Distance(currentNode.position, transform.position) <= smoothingAmmount * 0.5f + 0.5f)
            {
                if (currentNode == lastNode)
                {
                    if(stopAndTurn)
                    {
                        currentState = State.TURNING;
                        animator.SetTrigger("Idle");
                    }
                    walkingTowardsRed = walkingTowardsRed ? false : true;
                }
                if (walkingTowardsRed) { goalNodeIndex++; }
                else { goalNodeIndex--; }
            }
        }
        else if (currentState == State.TURNING)
        {
            //Direction to next node
            Vector3 targetDirection = (currentNode.position - transform.position);
            targetDirection.Normalize();
            Vector3 direction = Vector3.Lerp(transform.forward, targetDirection, Time.deltaTime * turningSpeed / smoothingAmmount);
            direction.y = 0; direction.Normalize(); direction.y = 0;
            transform.forward = direction;
            if (Vector3.Dot(transform.forward, targetDirection) >= 0.8f)
            {
                currentState = State.WALKING;
                animator.SetTrigger("Walk");
            }
        }
        else if(currentState == State.RAGE)
        {
            rageTimer -= Time.deltaTime;
            if(rageTimer <= 0)
            {
                rageTimer = rageTime;
                currentState = State.WALKING;
            }
        }        
        else if(currentState == State.HIT)
        {
            hitTimer -= Time.deltaTime;
            if(hitTimer <= 0)
            {
                hitTimer = hitTime;
                currentState = State.WALKING;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (currentState != State.HIT)
        {
            if (other.CompareTag("Player"))
            {
                currentState = State.RAGE;
                animator.SetTrigger("Rage");
                HealthRespawnBehaviour.takeDamage(3.0f);
                SoundManager.PlaySound("teacher laugh");
                //SoundManager.PlaySound("gym groan");
                //SoundManager.PlaySound("slap");
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Spit"))
        {
            //Debug.Log("Stun!");
            if(currentState != State.HIT && currentState != State.RAGE)
            {
                currentState = State.HIT;
                animator.SetTrigger("Hit");
            }
        }
    }

    

    //void OnDrawGizmos()
    //{
    //    if (pathParent == null) { return; }
    //    bool isSelected = false;
    //    if (Selection.Contains(gameObject) || Selection.Contains(pathParent.gameObject)) { isSelected = true; }
    //    else
    //    {
    //        foreach (Transform t in gizmoPath)
    //        {
    //            if (Selection.Contains(t.gameObject)) { isSelected = true; }
    //        }
    //    }
    //    if (!isSelected) { return; }

    //    gizmoPath.Clear();
    //    for (int i = 0; i < pathParent.childCount; i++)
    //    {
    //        gizmoPath.Add(pathParent.GetChild(i));
    //    }
    //    for (int i = 0; i < gizmoPath.Count - 1; i++)
    //    {
    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawLine(gizmoPath[i].position, gizmoPath[i + 1].position);
    //        Gizmos.color = Color.white;
    //        Gizmos.DrawSphere(gizmoPath[i].position, 0.1f);
    //    }
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawCube(gizmoPath[0].position, new Vector3(0.5f, 3.0f, 0.5f));

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(gizmoPath[gizmoPath.Count - 1].position, new Vector3(0.5f, 3.0f, 0.5f));
    //}
}
