using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrapBehaviour : MonoBehaviour
{
    private Animator animator;
    private bool functional;

    private float stuck_timer;
    private const float STUCK_INTERVAL = 3.0f;
    public static bool stuck;

    private GameObject head_gameobject;
    private Rigidbody head_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        functional = true;

        head_gameobject = GameObject.Find("Head");
        head_rigidbody = head_gameobject.GetComponent<Rigidbody>();
        stuck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (functional == false)
        {
            startStuckTimer();
        }
        if (stuck)
        {
            head_rigidbody.velocity = Vector3.zero;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (functional)
        {
            //to work with collisions appart from head

            //float vel = 0.0f;
            //Vector3 pos = transform.position;
            //string name = collision.gameObject.name + " " + gameObject.name;
            //SoundCue cue = new SoundCue(name, pos, vel);
            //TeacherBehaviour.soundCues.Add(cue);

            //animator.SetTrigger("Snap");
            //SoundManager.PlaySound("slap");

            if (collision.gameObject.CompareTag("Player"))
            {
                float vel = 0.0f;
                Vector3 pos = transform.position;
                string name = collision.gameObject.name + " " + gameObject.name;
                SoundCue cue = new SoundCue(name, pos, vel);
                TeacherBehaviour.soundCues.Add(cue);

                animator.SetTrigger("Snap");
                SoundManager.PlaySound("slap");
                //
                //
                HealthRespawnBehaviour.takeDamage(0.5f);
                head_rigidbody.velocity = Vector3.zero;
                //head_rigidbody.useGravity = false;
                head_gameobject.GetComponent<MovementBehaviour2>().input_enabled = false;
                head_gameobject.transform.position = transform.position;
                stuck = true;
            }
            functional = false;
        }
    }

    private void startStuckTimer()
    {
        if (stuck_timer > STUCK_INTERVAL)
        {
            stuck_timer -= STUCK_INTERVAL;
            stuck = false;
            //head_rigidbody.useGravity = true;
            head_gameobject.GetComponent<MovementBehaviour2>().input_enabled = true;
            animator.SetTrigger("Reset");
            SoundManager.PlaySound("trap reset");
            functional = true;
        }
        else
        {
            stuck_timer += Time.deltaTime;
        }
    }

}
