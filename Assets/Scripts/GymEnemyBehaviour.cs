using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GymEnemyBehaviour : MonoBehaviour
{
    public Transform throw_transform;
    //public float rangeRadius = 40.0f;

    GameObject player;

    private bool in_range;

    //homing
    public GameObject thrownObject_homing;
    float homing_throw_timer;
    const float HOMING_THROW_INTERVAL = 3.5f;

    //with physics
    public GameObject thrownObject_physics;               //const float predictionAmmount = 1.0f;
    public const float InitialThrowSpeed = 1.5f;          //const float noiseScale = 0.2f;

    float throw_timer;
    const float THROW_INTERVAL = 1.0f;

    //switch
    private bool homing = true;
    private int amount_of_shots;

    //wait after switching
    float switch_timer;
    const float SWITCH_TO_THROW_INTERVAL = 15.0f;
    const float SWITCH_TO_HOMING_INTERVAL = 10.0f;

    private bool can_shoot = false;

    //animation
    public Animator animator;


    [SerializeField]
    GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Head");
        in_range = false;
        homing_throw_timer = HOMING_THROW_INTERVAL * Random.Range(0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (in_range == true)              //Vector3.Distance(player.transform.position, throw_transform.position) <= rangeRadius
        {
            animator.SetTrigger("Awake");
        }

            if (can_shoot == true)
        {
            if (homing == true)
            {
                homing_throw_timer += Time.deltaTime;
                if (homing_throw_timer >= HOMING_THROW_INTERVAL)
                {
                    if (in_range == true)
                    {
                        animator.SetTrigger("Throw");
                        homing_throw_timer = 0.0f;
                    }
                }
            }
            else if (homing == false)
            {
                throw_timer += Time.deltaTime;
                if (throw_timer >= THROW_INTERVAL)
                {
                    if (in_range == true)
                    {
                        animator.SetTrigger("Throw");
                        throw_timer = 0.0f;
                    }
                }
            }
        }
        else if(can_shoot == false)
        {
            if (homing == false)
            {
                startSwitchTimer(SWITCH_TO_THROW_INTERVAL);
            }
            else if(homing == true)
            {
                startSwitchTimer(SWITCH_TO_HOMING_INTERVAL);
            }
        }

        if (homing == true && amount_of_shots == 5)
        {
            amount_of_shots = 0;
            homing = false;
            can_shoot = false;
        }
        else if(homing == false && amount_of_shots == 10)
        {
            amount_of_shots = 0;
            homing = true;
            can_shoot = false;
        }
    }

    void ThrowBall()
    {
        if (homing)
        {
            ThrowHoming();
        }
        else
        {
            ThrowWithPhysics(player.transform.position, player.GetComponent<Rigidbody>().velocity);
        }
    }

    void ThrowHoming()
    {
        //Instantiate object, based on position of player and animation?
        GameObject newlyThrown = GameObject.Instantiate(thrownObject_homing as GameObject, throw_transform.position, Quaternion.identity);

        amount_of_shots += 1;
    }


    void ThrowWithPhysics(Vector3 targetPosition, Vector3 targetVelocity)
    {
        //Debug.Log("THREW OBJECT");
        GameObject newThrownObject = GameObject.Instantiate(thrownObject_physics as GameObject, throw_transform.position, Quaternion.identity);

        float timeToReachTargetAtStartingPosition = Vector3.Distance(targetPosition, throw_transform.position) / InitialThrowSpeed;

        Vector3 extraDistanceToAdd = targetVelocity * timeToReachTargetAtStartingPosition;

        //float timeToReachPrediction = Vector3.Distance(targetPosition + extraDistanceToAdd, throw_transform.position) / InitialThrowSpeed;

        //extraDistanceToAdd = targetVelocity * timeToReachPrediction;

        Vector3 predictionNoise = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        //Calculate the predicted position of the target
        Vector3 predictedPosition = targetPosition /*+ (extraDistanceToAdd * predictionAmmount * 0.08f)*/;// + (predictionNoise * noiseScale);

        Vector3 directionToTarget = predictedPosition - throw_transform.position + new Vector3(0.0f, 0.4f, 0.0f);
        newThrownObject.GetComponent<Rigidbody>().velocity = directionToTarget * InitialThrowSpeed;

        amount_of_shots += 1;
    }

    void randomTaunt()
    {
        int i =  Random.Range(0, 100);
        if(i<= 50)
        {
            animator.SetTrigger("Taunt");
        }
    }

    void startSwitchTimer(float timer_interval)
    {
        switch_timer += Time.deltaTime;
        {
            if (switch_timer >= timer_interval)
            {
                can_shoot = true;
                switch_timer -= timer_interval;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Spit"))
        {
            animator.Play("Death");
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            //Open Doors
            foreach (GameObject g in doors)
            {
                g.SetActive(false);
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           in_range = true;
            //Close Doors
            foreach (GameObject g in doors)
            {
                g.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            in_range = false;
        }
    }

    void deathsound()
    {
        SoundManager.PlaySound("gym death");
        MusicManager.setMusic("Main Theme");
    }
    void throwsound()
    {
        SoundManager.PlaySound("gym groan");
        SoundManager.PlaySound("throw wind");
    }
    void enterSound()
    {
        SoundManager.PlaySound("gym call");
    }
}
