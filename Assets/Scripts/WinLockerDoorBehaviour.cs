using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLockerDoorBehaviour : MonoBehaviour
{
    HingeJoint hinge_joint;
    new Rigidbody rigidbody;

    public GameObject highlight_particle;
    // Start is called before the first frame update
    void Start()
    {
        hinge_joint = GetComponent<HingeJoint>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameBehaviour.getWinState() == true)
            {
                rigidbody.constraints = RigidbodyConstraints.None;
                if (hinge_joint.useSpring == false)
                {
                    SoundManager.PlaySound("locker_open");
                }
                hinge_joint.useSpring = true;
                highlight_particle.SetActive(false);
            }
            else
            {
                SoundManager.PlaySound("locked_locker");
                //flash the UI numbers
            }
        }
    }
}
