using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HandBehaviour : MonoBehaviour
{
    public Transform head_transform;
    private Transform camera_transform;
    private Vector3 original_position;

    public NavMeshAgent agent;

    private const float DESTINATION_INTERVAL = 0.0f;
    private float destination_timer;

    private float camera_fov;

    private float detection_radius = 15.0f;
    //private float camera_fov_vision_expansion = 10.0f;

    public Animator animator;

    private float slap_timer;
    private const float SLAP_INTERVAL = 0.5f;
    private bool waited_after_slap = false, can_start_timer = false, has_slaped = false;

    //stun
    private float stun_timer;
    private const float STUN_INTERVAL = 4.0f;
    private bool stunned = false;

    //for reseting position
    public bool has_moved;
    public bool going_back;

    //for Roaming
    //private bool chosen_random_direction;
    //private Vector3 randomPosition;

    //for staying stationary
    public bool is_stationary = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        camera_transform = Camera.main.transform;
        camera_fov = Camera.main.fieldOfView;

        destination_timer = 0.0f;

        has_moved = false;
        going_back = false;
        //chosen_random_direction = false;
        original_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("has_moved = " + has_moved);
        Vector3 camera_to_head = head_transform.position - camera_transform.position;
        Vector3 camera_to_head_flat = Vector3.ProjectOnPlane(camera_to_head, Vector3.up);

        Vector3 head_to_hand = transform.position - head_transform.position;
        Vector3 head_to_hand_flat = Vector3.ProjectOnPlane(head_to_hand, Vector3.up);

        if(is_stationary)
        {
            agent.SetDestination(transform.position);
        }
        //if (Vector3.Angle(camera_to_head_flat, head_to_hand_flat) >= camera_fov /*+ camera_fov_vision_expansion / 2.0f*/)  //romoved for VS
        //{
        if (stunned == false && has_slaped == false) // added for VS
        {
            if (destination_timer >= DESTINATION_INTERVAL)
            {
                if(IsReachable() == true)
                {
                    agent.SetDestination(head_transform.position);
                    has_moved = true;
                    going_back = false;
                    animator.ResetTrigger("Seen");
                    animator.SetTrigger("Walk");
                }
                destination_timer -= DESTINATION_INTERVAL;
            }

            destination_timer += Time.deltaTime;
        }
        //}
        //else
        //{
        //    agent.SetDestination(transform.position);
        //    animator.ResetTrigger("Walk");
        //    animator.SetTrigger("Seen");
        //}

        if (IsReachable() == false && !going_back)
        {
            //Debug.Log("not reachable");
            agent.SetDestination(transform.position);
            animator.SetBool("Out of range", true);
        }
        else
        {
            animator.SetBool("Out of range", false);
        }
        
        if (head_to_hand.magnitude - agent.stoppingDistance <= 0.0f && has_slaped == false && !going_back)
        {
            agent.SetDestination(transform.position);
            animator.SetTrigger("Slap");
            animator.ResetTrigger("Walk");
            has_slaped = true;
            can_start_timer = true;
        }

        else if (head_to_hand.magnitude - agent.stoppingDistance > 0.0f && waited_after_slap == true && has_slaped == true)
        {
            animator.ResetTrigger("Slap");
            waited_after_slap = false;
            has_slaped = false;
        }

        if (can_start_timer == true)
        {
            check_for_slap();
        }

        checkStunTimer();


        // for going to original position
        if(going_back == true)
        {
            //Debug.Log("actually going back");
            agent.SetDestination(original_position);
            animator.ResetTrigger("Seen");
            animator.SetTrigger("Walk");
            if(agent.remainingDistance <= 1.0f)
            {
                has_moved = false;
                going_back = false;
                //Debug.Log("not going back");
            }
        }


        //for Roaming randomly

        //if (has_moved == false)
        //{
        //    if(chosen_random_direction == false)
        //    {
        //        Vector3 randomDirection = Random.insideUnitSphere * 5.0f;
        //        randomDirection += transform.position;
        //        NavMeshHit hit;
        //        NavMesh.SamplePosition(randomDirection, out hit, 5.0f, 1);
        //        randomPosition = hit.position;
        //        chosen_random_direction = true;
        //        animator.SetTrigger("Walk");
        //    }
        //    if(chosen_random_direction == true)
        //    {
        //        if (agent.remainingDistance <= 1.0f)
        //        {

        //        }
        //        agent.SetDestination(randomPosition);
        //    }
        //}
    }

    private void check_for_slap()
    {
        if (slap_timer >= SLAP_INTERVAL)
        {
            can_start_timer = false;
            waited_after_slap = true;
            slap_timer -= SLAP_INTERVAL;
        }
        slap_timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Spit") && has_slaped == false)
        {
            //Debug.Log("Stun!");

            agent.SetDestination(transform.position);
            animator.ResetTrigger("Walk");   

            animator.SetTrigger("Seen");
            stunned = true;
        }
    }

    void checkStunTimer()
    {
        if(stunned == true)
        {
            if (stun_timer >= STUN_INTERVAL)
            {
                stun_timer -= STUN_INTERVAL;
                stunned = false;
            }

            stun_timer += Time.deltaTime;
        }
    }

    private bool IsReachable()
    {
        NavMeshPath navMeshPath = new NavMeshPath();
        Vector3 head_to_hand = transform.position - head_transform.position;
        //create path and check if it can be done
        // and check if navMeshAgent can reach its target
        if (agent.CalculatePath(head_transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            if (head_to_hand.magnitude > detection_radius)
            {
                //Debug.Log("Far Away");
                return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }



    public void resetDestination()
    {
        if(has_moved)
        {
            going_back = true;
            agent.SetDestination(original_position);
            //Debug.Log("going back = true");
            //has_moved = false;
        }
    }
}
