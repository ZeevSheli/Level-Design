using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour2 : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform mesh;

    Vector3 movement_velocity = Vector3.zero;
    private Rigidbody rigidBody;
    public Transform camera_transform;

    public bool input_enabled;
    public bool movement_enabled;
    public bool jump_enabled;
    public bool in_air_because_of_jump;
    public bool can_stomp;
    private bool W_isPressed;
    private bool A_isPressed;
    private bool S_isPressed;
    private bool D_isPressed;

    private bool Left_isPressed;
    private bool Right_isPressed;
    private bool Forwards_isPressed;
    private bool Backwards_isPressed;

    const float maxMovementSpeed = 7.0f;
    const float timeToReach = 0.6f;
    float accelerationSpeed = maxMovementSpeed / timeToReach;

    private Vector3 cashed_velocity;

    private SphereCollider head_collider;
    private float head_radius_scale_factor;
    float JUMP_FORCE = 7.5f;

    const float ROTATION_SPEED = 200.0f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        head_collider = GetComponent<SphereCollider>();

        head_radius_scale_factor = Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        input_enabled = movement_enabled = jump_enabled = true;
    }
    
    // Update is called once per frame
    void Update()
    {

        if (input_enabled)
        {
            checkMovementInput();
            updateMovement();
            if(jump_enabled)
            {
                checkJump();
            }
        }
    }

    void checkMovementInput()
    {
        //if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    W_isPressed = true;
        //}
        //if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    A_isPressed = true;
        //}
        //if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    S_isPressed = true;
        //}
        //if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    D_isPressed = true;
        //}

        //if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        //{
        //    W_isPressed = false;
        //}
        //if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        //{
        //    A_isPressed = false;
        //}
        //if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        //{
        //    S_isPressed = false;
        //}
        //if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    D_isPressed = false;
        //}

        if(movement_enabled)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                Right_isPressed = true;
                Left_isPressed = false;
            }
            if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                Left_isPressed = true;
                Right_isPressed = false;
            }
            if(Input.GetAxisRaw("Horizontal") == 0.0f)
            {
                Right_isPressed = false;
                Left_isPressed = false;
            }


            if (Input.GetAxisRaw("Vertical") > 0.0f)
            {
                Forwards_isPressed = true;
                Backwards_isPressed = false;
            }
            if (Input.GetAxisRaw("Vertical") < 0.0f)
            {
                Backwards_isPressed = true;
                Forwards_isPressed = false;
            }      
            if(Input.GetAxisRaw("Vertical") == 0.0f)
            {
                Forwards_isPressed = false;
                Backwards_isPressed = false;
            }
        }
        if(movement_enabled == false)
        {
            Right_isPressed = false;
            Left_isPressed = false;
            Forwards_isPressed = false;
            Backwards_isPressed = false;
        }
    }


    private void updateMovement()
    {
        //Calculate forward vector

        //forward = cross up and camera right
        Vector3 forward = Vector3.Cross(camera_transform.right, Vector3.up);

        if (Forwards_isPressed)
        {
            if (isGrounded())
            {
                rigidBody.velocity += forward * Time.deltaTime * accelerationSpeed;
            }
            else
            {
                rigidBody.velocity += forward * Time.deltaTime * accelerationSpeed * 0.5f;
            }
        }
        if (Left_isPressed)
        {
            if (isGrounded())
            {
                rigidBody.velocity -= camera_transform.right * Time.deltaTime * accelerationSpeed;
            }
            else
            {
                rigidBody.velocity -= camera_transform.right * Time.deltaTime * accelerationSpeed * 0.5f;
            }
        }
        if (Backwards_isPressed)
        {
            if (isGrounded())
            {
                rigidBody.velocity -= forward * Time.deltaTime * accelerationSpeed;
            }
            else
            {
                rigidBody.velocity -= forward * Time.deltaTime * accelerationSpeed * 0.5f;
            }
        }
        if (Right_isPressed)
        {
            if (isGrounded())
            {
                rigidBody.velocity += camera_transform.right * Time.deltaTime * accelerationSpeed;
            }
            else
            {
                rigidBody.velocity += camera_transform.right * Time.deltaTime * accelerationSpeed * 0.5f;
            }
        }

        if(isGrounded())
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x * 0.999f, rigidBody.velocity.y, rigidBody.velocity.z * 0.999f);
            in_air_because_of_jump = false;
            can_stomp = false;
        }
        else
        {
            checkStomp();
        }

        //Threshold
        Vector3 horizontalVelocity = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z);
        if (horizontalVelocity.magnitude >= maxMovementSpeed)
        {
            horizontalVelocity.Normalize();
            horizontalVelocity *= maxMovementSpeed;
            rigidBody.velocity = new Vector3(horizontalVelocity.x, rigidBody.velocity.y, horizontalVelocity.z);
        }

        //Rotation of Mesh
        Vector3 hori = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z);
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, hori.normalized);
        Vector3 rigidbodyVelocity = rigidBody.velocity;
        rigidbodyVelocity = new Vector3(rigidbodyVelocity.x, 0, rigidbodyVelocity.z);
        mesh.Rotate(rotationAxis, rigidbodyVelocity.magnitude * ROTATION_SPEED * Time.deltaTime, Space.World); //change rigidbodyVelocity.magnitude to rigidBody.velocity.magnitude if you want vertical velocity to affect rotation of the head.
    }

    private void checkJump()
    {
        if (Input.GetButtonDown("Jump") == true && isGrounded() == true)
        {
            can_stomp = false;
            rigidBody.velocity += Vector3.up * JUMP_FORCE;
            SoundManager.PlaySound("jump");
            in_air_because_of_jump = true;
        }
        if (Input.GetButtonUp("Jump") == true && in_air_because_of_jump)
        {
            can_stomp = true;
            if (rigidBody.velocity.y > 0.0f)
            {
                rigidBody.velocity += Vector3.up * (-rigidBody.velocity.y / 2);
            }
        }
    }

    private void checkStomp()
    {
        if (Input.GetButtonDown("Jump") == true && can_stomp == true)
        {
            SoundManager.PlaySound("thud");
        }
        if (Input.GetButton("Jump") == true && can_stomp == true)
        {
            /*glide*/   rigidBody.velocity += Vector3.down * (rigidBody.velocity.y / 2);
            //rigidBody.velocity += Vector3.down;
        }
    }

    private bool isGrounded()
    {
        float head_radius = head_collider.radius * head_radius_scale_factor;
        Collider[] colliders = new Collider[2];

        int layer_mask = ~((1 << gameObject.layer));

        Vector3 sphere_position = transform.position + new Vector3(0, -head_radius * 0.2f, 0);
        float sphere_radius = head_radius - head_radius * 0.02f;
        Physics.OverlapSphereNonAlloc(sphere_position, sphere_radius, colliders, layer_mask, QueryTriggerInteraction.Ignore);
        //Debug.Log(colliders.Length);
        if (colliders[0] != null)
        {
            return true;
            
        }
        else
        {
            return false;
        }
    }
}

