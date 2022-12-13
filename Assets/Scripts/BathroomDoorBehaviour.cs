using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomDoorBehaviour : MonoBehaviour
{
    //private HingeJoint door_hinge;
    private Rigidbody door_rigidbody;

    private bool opened_door;
    // Start is called before the first frame update
    void Start()
    {
        //door_hinge = GetComponent<HingeJoint>();
        door_rigidbody = GetComponent<Rigidbody>();
        door_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        opened_door = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (opened_door == false)
            {
                SoundManager.PlaySound("locked_locker");
            }
        }
    }


    public void openDoor()
    {
        door_rigidbody.constraints = RigidbodyConstraints.None;
        opened_door = true;
        //door_hinge.useSpring = true;
    }
}

