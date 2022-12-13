using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class HeadBehaviour : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public Transform camera_transform;
    public GameObject camera_object;

    private const float MAX_ANGULAR_SPEED = 30.0f /*50,0f*/;  //In rad/s
    private const float TIME_TO_REACH_MAX_SPEED = 0.5f/*2.0f*/; //In seconds
    private const float ANGULAR_ACCELERATION = MAX_ANGULAR_SPEED / TIME_TO_REACH_MAX_SPEED; //In rad/s^2

    private const float MAX_VELOCITY_IN_AIR = 9.0f;

    private const float JUMP_FORCE = 7.5f; //In newtons
    private const float BOOST_FORCE = 1.5f; //In newtons
    private const float AERIAL_FORCE = 0.05f; //In newtons

    private bool can_boost = true;
    private float boost_timer = 0.0f;
    private const float BOOST_INTERVAL = 1.5f;

    private float original_angular_drag;

    private SphereCollider head_collider;

    private float head_radius_scale_factor;

    public HingeJoint locker_door_hinge_joint;

    public bool input_enabled = true;
    public GameObject win_screen;

    public Animator music_animator;

    //SHOOTING
    public GameObject spitProjectile;
    public Transform lookDirectionObject;
    public static class Constants
    {
        public const float SPIT_PROJECTILE_SPEED = 20.0f;
    }
    static bool isAiming = false;
    static bool isSpitting = false;

    public GameObject spitTrajectory;


    public GameObject sideView;
    public GameObject headCamera;
    public GameObject aimCamera;

    public Transform camera_look_at_transform;
    public Transform head_rotation_object_transform;

    private float aim_lerp_timer;
    private const float AIM_LERP_INTERVAL = 0.5f;


    public Animator animator;
    public Animator skinAnimator;

    //effects
    private ParticleSystem blackSmoke;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = MAX_ANGULAR_SPEED;

        head_collider = GetComponent<SphereCollider>();

        head_radius_scale_factor = Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        original_angular_drag = rigidbody.angularDrag;

        music_animator.enabled = false;

        blackSmoke = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //checkJump();
        //checkBoostCooldown();

        if (input_enabled == true)
        {
            checkAim();
            checkShoot();
        }
        //sideView.transform.position = transform.position + (camera_transform.right * 0.5f);
    }

    private void FixedUpdate()
    {
        //checkMovement();
    }

    private void checkAim()
    {
        if(Input.GetAxisRaw("Aim") > 0)
        {
            //Debug.Log("aiming");
            if(isAiming == false)
            {
                isAiming = true;
                aimCamera.SetActive(true);
                headCamera.SetActive(false);
                aim_lerp_timer = 0.0f;
                GetComponent<MovementBehaviour2>().movement_enabled = false;
                spitTrajectory.SetActive(true);
                SoundManager.PlaySound("gurgle");
                //to make the head slide less when aiming
                head_collider.material.dynamicFriction = 1.0f;
                head_collider.material.staticFriction = 1.0f;
            }
        }
        if(Input.GetAxisRaw("Aim") <= 0)
        {
            if(isAiming == true)
            {
                isAiming = false;
                headCamera.SetActive(true);
                aimCamera.SetActive(false);
                GetComponent<MovementBehaviour2>().movement_enabled = true;
                spitTrajectory.SetActive(false);
                //to make the head slide less when aiming
                head_collider.material.dynamicFriction = 0.0f;
                head_collider.material.staticFriction = 0.0f;
            }
        }
        if(isAiming)
        {
            //if (aim_lerp_timer >= AIM_LERP_INTERVAL)
            //{
            //    aim_lerp_timer -= AIM_LERP_INTERVAL;
            //    head_rotation_object_transform.rotation = camera_look_at_transform.rotation;
            //}
            //else
            //{
                aim_lerp_timer += Time.deltaTime;
                head_rotation_object_transform.rotation = Quaternion.Lerp(head_rotation_object_transform.rotation, camera_look_at_transform.rotation, (aim_lerp_timer / AIM_LERP_INTERVAL));
            //}
        }
    }

    void checkShoot()
    {
        if (Input.GetAxisRaw("Spit") > 0 && isAiming == true && isSpitting == false)
        {
            isSpitting = true;
            if (GetComponent<SpitAmmoBehaviour>().spit_amount >= GetComponent<SpitAmmoBehaviour>().MAX_SPIT_AMOUNT / 3.0f)
            {
                animator.Play("Spit");
                SoundManager.PlaySound("spit");
                Vector3 lookDirection = lookDirectionObject.position - spitTrajectory.transform.position/*transform.position*/;
                lookDirection.Normalize();

                GameObject newSpit = GameObject.Instantiate(spitProjectile as GameObject, spitTrajectory.transform.position/*transform.position*/, Quaternion.identity);
                newSpit.GetComponent<Rigidbody>().velocity = lookDirection * Constants.SPIT_PROJECTILE_SPEED;

                GetComponent<SpitAmmoBehaviour>().spit_amount -= GetComponent<SpitAmmoBehaviour>().MAX_SPIT_AMOUNT / 3.0f;
            }
        }
        if (Input.GetAxisRaw("Spit") <= 0)
        {
            isSpitting = false;
        }
    }
    //private void checkJump()
    //{
    //    if (input_enabled == true)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space) == true && isGrounded() == true)
    //        {
    //            rigidbody.AddForce(Vector3.up * JUMP_FORCE, ForceMode.VelocityChange);
    //            SoundManager.PlaySound("jump");
    //        }
    //        if (Input.GetKeyUp(KeyCode.Space) == true)
    //        {
    //            if (rigidbody.velocity.y > 0.0f)
    //            {
    //                rigidbody.AddForce(0.0f, -rigidbody.velocity.y / 2, 0.0f, ForceMode.VelocityChange);
    //            }
    //        }
    //    }
    //}

    //private void checkMovement()
    //{
    //    if (input_enabled == true)
    //    {

    //        if (Input.GetKey(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow) == true)
    //        {
    //            Vector3 angular_speed_direction = (Quaternion.Euler(0.0f, camera_transform.eulerAngles.y, 0.0f) * Vector3.right).normalized;
    //            rigidbody.AddTorque(angular_speed_direction * ANGULAR_ACCELERATION, ForceMode.Acceleration);
    //            if(isGrounded() == false && rigidbody.velocity.magnitude < MAX_VELOCITY_IN_AIR)
    //            {
    //                Vector3 speed_direction = (Quaternion.Euler(0.0f, camera_transform.eulerAngles.y, 0.0f) * Vector3.forward).normalized;
    //                rigidbody.AddForce(speed_direction * AERIAL_FORCE, ForceMode.VelocityChange);
    //            }
    //        }
    //        if (Input.GetKey(KeyCode.S) == true || Input.GetKeyDown(KeyCode.DownArrow) == true)
    //        {
    //            Vector3 angular_speed_direction = (Quaternion.Euler(0.0f, camera_transform.eulerAngles.y, 0.0f) * Vector3.left).normalized;
    //            rigidbody.AddTorque(angular_speed_direction * ANGULAR_ACCELERATION, ForceMode.Acceleration);
    //            if (isGrounded() == false && rigidbody.velocity.magnitude < MAX_VELOCITY_IN_AIR)
    //            {
    //                Vector3 speed_direction = (Quaternion.Euler(0.0f, camera_transform.eulerAngles.y, 0.0f) * Vector3.back).normalized;
    //                rigidbody.AddForce(speed_direction * AERIAL_FORCE, ForceMode.VelocityChange);
    //            }
    //        }
    //        if (Input.GetKey(KeyCode.A) == true || Input.GetKeyDown(KeyCode.LeftArrow) == true)
    //        {
    //            Vector3 angular_speed_direction = (Quaternion.Euler(0.0f, camera_transform.eulerAngles.y, 0.0f) * Vector3.forward).normalized;
    //            rigidbody.AddTorque(angular_speed_direction * ANGULAR_ACCELERATION, ForceMode.Acceleration);
    //            if (isGrounded() == false && rigidbody.velocity.magnitude < MAX_VELOCITY_IN_AIR)
    //            {
    //                Vector3 speed_direction = (Quaternion.Euler(0.0f, camera_transform.eulerAngles.y, 0.0f) * Vector3.left).normalized;
    //                rigidbody.AddForce(speed_direction * AERIAL_FORCE, ForceMode.VelocityChange);
    //            }
    //        }
    //        if (Input.GetKey(KeyCode.D) == true || Input.GetKeyDown(KeyCode.RightArrow) == true)
    //        {
    //            Vector3 angular_speed_direction = (Quaternion.Euler(0.0f, camera_transform.eulerAngles.y, 0.0f) * Vector3.back).normalized;
    //            rigidbody.AddTorque(angular_speed_direction * ANGULAR_ACCELERATION, ForceMode.Acceleration);
    //            if (isGrounded() == false && rigidbody.velocity.magnitude < MAX_VELOCITY_IN_AIR)
    //            {
    //                Vector3 speed_direction = (Quaternion.Euler(0.0f, camera_transform.eulerAngles.y, 0.0f) * Vector3.right).normalized;
    //                rigidbody.AddForce(speed_direction * AERIAL_FORCE, ForceMode.VelocityChange);
    //            }
    //        }
    //    }
    //}

    //private void checkBoostCooldown()
    //{
    //    if (can_boost == false)
    //    {
    //        if (boost_timer >= BOOST_INTERVAL)
    //        {
    //            can_boost = true;
    //            boost_timer -= BOOST_INTERVAL;
    //        }
    //        boost_timer += Time.deltaTime;
    //    }
    //}

    //private bool isGrounded()
    //{
    //    float head_radius = head_collider.radius * head_radius_scale_factor;
    //    Collider[] colliders = new Collider[1];
    //    //int head_mask = 1 << gameObject.layer;  // bitshifting the player layer to get the bit mask
    //    //int camera_mask = 1 << camera_object.layer;  // bitshifting the player layer to get the bit mask
    //    int layer_mask = ~((1 << gameObject.layer) | (1 << camera_object.layer));
    //    //int layer_mask = head_mask | camera_mask; // adding both bit masks to one same mask
    //    //layer_mask = ~layer_mask; //inverting the bitmask so it ignores the player layer
    //    Vector3 sphere_position = transform.position + new Vector3(0, -head_radius * 0.1f, 0);
    //    float sphere_radius = head_radius - head_radius * 0.02f;
    //    Physics.OverlapSphereNonAlloc(sphere_position, sphere_radius, colliders, layer_mask);
    //    if( colliders[0] != null)
    //    {
    //        //Debug.Log(colliders[0].gameObject.name);
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Locker Digit"))
        {
            DigitBehaviour digit_behaviour = other.GetComponent<DigitBehaviour>();
            GameBehaviour.setUIDigit(digit_behaviour.getDigitValue(), digit_behaviour.getDigitPosition());
            SoundManager.PlaySound("note_pickup");
            Destroy(other.gameObject);
            Debug.Log("collected " + other.gameObject.name.ToString());
        }
        if (other.gameObject.CompareTag("Cheer Cube"))
        {
            SoundManager.PlaySound("cheer");
            Debug.Log("KOBE o/");
        }
        if (other.gameObject.CompareTag("Win Collider"))
        {
            //JointSpring joint_spring = locker_door_hinge_joint.spring;
            //joint_spring.targetPosition = 0;
            //joint_spring.spring = 50;
            //joint_spring.damper = 10;
            //locker_door_hinge_joint.spring = joint_spring;
            //play sound for closing
            // SoundManager.PlaySound("locked_locker");
            SoundManager.PlaySound("cheer");
            //fade to black in ui
            input_enabled = false;
            GetComponent<MovementBehaviour2>().movement_enabled = false;
            win_screen.SetActive(true);
            music_animator.enabled = true;
            StartCoroutine("wait_before_changing_scene");
        }
        if (other.gameObject.CompareTag("Water Recharge"))
        {
            if (GetComponent<SpitAmmoBehaviour>().spit_amount < GetComponent<SpitAmmoBehaviour>().MAX_SPIT_AMOUNT)
            {
                //Debug.Log("slurp");
                SoundManager.PlaySound("drink");
                GetComponent<SpitAmmoBehaviour>().spit_amount = GetComponent<SpitAmmoBehaviour>().MAX_SPIT_AMOUNT;
            }
        }
        if(other.CompareTag("Homing Projectile") || other.CompareTag("Throw Projectile"))
        {
            if(other.CompareTag("Homing Projectile"))
            {
                other.GetComponent<HomingBehaviour>().CollideAndDestroy();
                HealthRespawnBehaviour.takeDamage(0.3f);
                SoundManager.PlaySound("slap");
            }
            else
            {
                other.GetComponent<ThrowBehaviour>().CollideAndDestroy();
                HealthRespawnBehaviour.takeDamage(0.2f);
                SoundManager.PlaySound("slap");
            }
            Vector3 directionToHeadHorizontal = transform.position - other.transform.position;
            Vector3 directionToHeadVertical = Vector3.zero; directionToHeadVertical.y = 6.0f;
            directionToHeadHorizontal.y = 0.0f;
            directionToHeadHorizontal.Normalize();
            directionToHeadHorizontal *= 4.0f;
            rigidbody.velocity += directionToHeadHorizontal;
            rigidbody.velocity += directionToHeadVertical;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Slap Sphere"))
        {
            SoundManager.PlaySound("slap");
            HealthRespawnBehaviour.takeDamage(1.0f);
            //Debug.Log("Ouch!");
        }
        else if(other.gameObject.CompareTag("Hurtful Ground"))
        {
            //PLAY BURNING SOUND
            SoundManager.PlaySound("lava sizzle");
            blackSmoke.Play();
            //Debug.Log("IT BURNS!");
            rigidbody.velocity += Vector3.up * 10.0f;
            HealthRespawnBehaviour.takeDamage(1.0f);
        }
        else if (other.gameObject.CompareTag("Boing"))
        {
            rigidbody.velocity += other.gameObject.transform.up * 20.0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Water Recharge"))
        {
            if (GetComponent<SpitAmmoBehaviour>().spit_amount < GetComponent<SpitAmmoBehaviour>().MAX_SPIT_AMOUNT / 3)
            {
                //Debug.Log("slurp");
                SoundManager.PlaySound("drink");
                GetComponent<SpitAmmoBehaviour>().spit_amount = GetComponent<SpitAmmoBehaviour>().MAX_SPIT_AMOUNT;
            }
        }
    }

    IEnumerator wait_before_changing_scene()
    {
        yield return new WaitForSeconds(4.0f);
        //SceneManager.LoadScene("Outro Cutscene");
        SceneManager.LoadScene("Credits");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EatBerry()
    {
     animator.Play("Spit");
     skinAnimator.Play("Head Color Purple");
     SoundManager.PlaySound("berry eat");
    }
}
