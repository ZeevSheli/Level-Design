using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapDoorBehaviour : MonoBehaviour
{
    public GameObject slap_game_object;
    public GameObject hand_parent;
    public Rigidbody slap_rigidbody;
    public CapsuleCollider capsule_collider;

    private Vector3 original_position;

    private float push_force = 40.0f;
    // Start is called before the first frame update
    void Start()
    {
        capsule_collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        original_position = hand_parent.transform.position + (hand_parent.transform.forward* 0.5f);
    }

    void slap()
    {
        capsule_collider.enabled = true;
        slap_rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        slap_rigidbody.velocity += slap_game_object.transform.forward * push_force;
    }

    void resetSlap()
    {
        slap_rigidbody.velocity = Vector3.zero;
        slap_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        slap_game_object.transform.position = original_position;
        capsule_collider.enabled = false;
    }

    void goBackToOriginalPosition()
    {
        GetComponentInParent<HandBehaviour>().resetDestination();
        //HandBehaviour.resetDestination();
    }
}
