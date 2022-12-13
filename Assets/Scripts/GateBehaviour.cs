using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehaviour : MonoBehaviour
{
    private HingeJoint gate_hinge;
    private Rigidbody gate_rigidbody;

    private JointSpring hinge_spring;
    private float zero_spring = 0.0f;
    private float spring_amount = 50.0f;

    public static bool have_opened_gate;

    private JointLimits limits;
    private float min_limit_target, max_limit_target;

    // Start is called before the first frame update
    void Start()
    {
        gate_hinge = GetComponent<HingeJoint>();
        gate_rigidbody = GetComponent<Rigidbody>();


        hinge_spring = gate_hinge.spring;
        hinge_spring.spring = zero_spring;
        gate_hinge.spring = hinge_spring;

        min_limit_target = gate_hinge.limits.min;
        max_limit_target = gate_hinge.limits.max;

        limits.min = 0.0f;
        limits.max = 0.0f;
        gate_hinge.limits = limits;

        //gate_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        gate_hinge.useSpring = false;
        have_opened_gate = false;
    }

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Q))
    //    {
    //        openSesame();
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(GameBehaviour.getWinState() == false)
            {
                SoundManager.PlaySound("locked_locker");
            }
        }
    }

    public void openSesame()
    {
        have_opened_gate = true;
        gate_rigidbody.constraints = RigidbodyConstraints.None;
        if (gate_hinge.useSpring == false)
        {
            SoundManager.PlaySound("locker_open");
        }

        hinge_spring.spring = spring_amount;
        gate_hinge.spring = hinge_spring;

        limits.min = min_limit_target;
        limits.max = max_limit_target;
        gate_hinge.limits = limits;

        gate_hinge.useSpring = true;
    }
}
