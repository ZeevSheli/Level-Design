using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthRespawnBehaviour : MonoBehaviour
{
    public static float health_amount;
    private const float MAX_HEALTH = 2.0f; //you can take 3 hits of 1 damage because you regenerate health

    private const float TIME_TO_REACH_MAX_HEALTH = 20.0f;    
    
    //detection from teacher
    public static float caught_amount;
    private const float UNSEEN = 2.0f;
    private const float TIME_TO_REACH_UNSEEN = 3.0f;
    public AudioSource suspense_audio;

    // invincibility cooldown
    static bool can_take_damage;
    static float invincibility_timer;
    const float INVINCIBILITY_INTERVAL = 1.0f;
    static bool invincibility_cooldown_active;

    //Respawning Animation
    public GameObject blackImage;
    bool isRespawning;
    float respawn_timer;
    const float RESPAWN_TIME = 2.0f;

    public static bool can_res;

    string location_name;
    enum Locations { Bathroom, Showers, Gym, Playground, Hallway, Classroom, Vent, Teachers, Principle }
    private Locations location;
    public Transform[] checkpoint_transforms = new Transform[9];
    private Transform checkpoint_transform;


    private Rigidbody head_rigidbody;

    private int progress;
    private Vector3 start_position;

    public Image health_indicator;
    public Image caught_indicator;

    // Start is called before the first frame update
    void Start()
    {
        health_amount = MAX_HEALTH;
        caught_amount = UNSEEN;
        head_rigidbody = gameObject.GetComponent<Rigidbody>();
        progress = 0;

        location_name = "Start";
        start_position = gameObject.transform.position;
        can_take_damage = true;
        invincibility_cooldown_active = false;

        can_res = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health_amount <= 0.0f)
        {
            if(can_res == false)
            {
                startRespawn();
            }
        }
        if(can_res == true)
        {
            respawn();
        }

        if (health_amount > MAX_HEALTH)
        {                   
            health_amount = MAX_HEALTH;
        }                    
        if (health_amount < MAX_HEALTH)
        {   
            health_amount += Time.deltaTime * MAX_HEALTH/ TIME_TO_REACH_MAX_HEALTH;
        }

        Color temp = health_indicator.color;
        temp.a = ((MAX_HEALTH - health_amount) / MAX_HEALTH * 1);
        health_indicator.color = temp;

        //for caught indicator
        if (caught_amount > UNSEEN)
        {
            caught_amount = UNSEEN;
        }
        if (caught_amount < UNSEEN)
        {
            caught_amount += Time.deltaTime * UNSEEN / TIME_TO_REACH_UNSEEN;
        }
        if(caught_amount < 0.0f)
        {
            //caught_amount = 0.0f;
            startRespawn();
        }

        Color temp_caught = caught_indicator.color;
        temp_caught.a = ((UNSEEN - caught_amount) / UNSEEN * 1);
        caught_indicator.color = temp_caught;
        suspense_audio.volume = ((UNSEEN - caught_amount) / UNSEEN * 1)/2.0f;
        ///


        if (invincibility_cooldown_active)
        {
            invincibility_timer += Time.deltaTime;
            if (invincibility_timer >= INVINCIBILITY_INTERVAL)
            {
                can_take_damage = true;
                invincibility_cooldown_active = false;
                invincibility_timer -= INVINCIBILITY_INTERVAL;
            }
        }

        checkInput();
    }

    public static void startRespawn()
    {
        can_res = true;
    }

    void playFadeOut()
    {
        blackImage.GetComponent<Animator>().Play("fadeOutToBlack");
    }

    void respawn()
    {
        if(isRespawning == false)
        {
            playFadeOut();
        }

        isRespawning = true;

        if (isRespawning)
        {
            respawn_timer += Time.deltaTime;
            if (respawn_timer >= 1.0f)
            {
                teleport(location_name);
                respawn_timer = 0.0f; isRespawning = false;
                if(can_res != false)
                {
                    can_res = false;
                    restoreHealth();
                    restoreSeen();
                    restoreStuck();
                    gameObject.GetComponent<MovementBehaviour2>().input_enabled = true;
                }
            }
        }
    }

    void teleport(string location_name)
    {
        head_rigidbody.velocity = Vector3.zero;
        switch (this.location_name)
        {
            case "Start":
                gameObject.transform.position = start_position;
                break;
            case "Bathroom":
                break;
            case "Showers":
                gameObject.transform.position = checkpoint_transform.position;
                break;
            case "Gym":
                gameObject.transform.position = checkpoint_transform.position;
                break;
            case "Playground":
                break;
            case "Hallway":
                gameObject.transform.position = checkpoint_transform.position;
                break;
            case "Classroom":
                gameObject.transform.position = checkpoint_transform.position;
                break;
            case "Vent":
                gameObject.transform.position = checkpoint_transform.position;
                break;            
            case "Teachers":
                gameObject.transform.position = checkpoint_transform.position;
                break;
            case "Principle":
                break;
        }
    }

    void restoreHealth()
    {
        health_amount = MAX_HEALTH;
    }    
    void restoreSeen()
    {
        caught_amount = UNSEEN;
    }
    void restoreStuck()
    {
        HandTrapBehaviour.stuck = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            switch (other.name)
            {
                case "Showers_Checkpoint":
                    if (progress < 1)
                    {
                        location_name = Locations.Showers.ToString();
                        progress = 1;
                        checkpoint_transform = other.gameObject.transform.GetChild(0).transform;
                    }
                    break;

                case "Gym_Checkpoint":
                    if(progress<2)
                    {
                        location_name = Locations.Gym.ToString();
                        progress = 2;
                        checkpoint_transform = other.gameObject.transform.GetChild(0).transform;
                    }
                    break;

                case "Hallway_Checkpoint":
                    if (progress < 3)
                    {
                        location_name = Locations.Hallway.ToString();
                        progress = 3;
                        checkpoint_transform = other.gameObject.transform.GetChild(0).transform;
                    }
                    break;                
                case "Classroom_Checkpoint":
                    if (progress < 4)
                    {
                        location_name = Locations.Classroom.ToString();
                        progress = 4;
                        checkpoint_transform = other.gameObject.transform.GetChild(0).transform;
                    }
                    break;                
                case "Vent_Checkpoint":
                    if (progress < 5)
                    {
                        location_name = Locations.Vent.ToString();
                        progress = 5;
                        checkpoint_transform = other.gameObject.transform.GetChild(0).transform;
                    }
                    break;               
                case "Lounge_Checkpoint":
                    if (progress < 6)
                    {
                        location_name = Locations.Teachers.ToString();
                        progress = 6;
                        checkpoint_transform = other.gameObject.transform.GetChild(0).transform;
                    }
                    break;
            }
        }
    }

    public static void takeDamage(float amount)
    {
        if(can_take_damage)
        {
            SoundManager.PlaySound("jump");
            health_amount -= amount;
            can_take_damage = false;
            startInvincibilityCooldown();
        }
    }

    public static void increaseSeen(float amount)
    {
        caught_amount -= amount;
    }

    static void startInvincibilityCooldown()
    {
        invincibility_cooldown_active = true;
    }


    private void checkInput()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                takeDamage(1.0f);
            }
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                location = Locations.Bathroom;
                teleportCommand(location);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                location = Locations.Gym;
                teleportCommand(location);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                location = Locations.Hallway;
                teleportCommand(location);
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                location = Locations.Classroom;
                teleportCommand(location);
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                location = Locations.Vent;
                teleportCommand(location);
            }
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                location = Locations.Teachers;
                teleportCommand(location);
            }
        }
    }

    void teleportCommand(Locations location)
    {
        switch (location)
        {
            case Locations.Bathroom:
                location_name = Locations.Showers.ToString();
                checkpoint_transform = checkpoint_transforms[0];
                startRespawn();
                break;
            case Locations.Showers:
                //checkpoint_transform = checkpoint_transforms[1];
                break;
            case Locations.Gym:
                location_name = Locations.Gym.ToString();
                checkpoint_transform = checkpoint_transforms[2];
                startRespawn();
                break;
            case Locations.Playground:
                //checkpoint_transform = checkpoint_transforms[3];
                break;
            case Locations.Hallway:
                location_name = Locations.Hallway.ToString();
                checkpoint_transform = checkpoint_transforms[4];
                startRespawn();
                break;
            case Locations.Classroom:
                location_name = Locations.Classroom.ToString();
                checkpoint_transform = checkpoint_transforms[5];
                startRespawn();
                break;
            case Locations.Vent:
                location_name = Locations.Vent.ToString();
                checkpoint_transform = checkpoint_transforms[6];
                startRespawn();
                break;
            case Locations.Teachers:
                location_name = Locations.Teachers.ToString();
                checkpoint_transform = checkpoint_transforms[7];
                startRespawn();
                break;
            case Locations.Principle:
                //checkpoint_transform = checkpoint_transforms[8];
                break;
        }
    }
}
