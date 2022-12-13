using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCue
{
    public SoundCue(string _name, Vector3 _point, float _loudness)
    {
        name = _name; point = _point; loudness = _loudness;
    }
    public string name;
    public Vector3 point;
    public float loudness;
    public float age;
    public float GetLoudScore(Vector3 listener)
    {
        float score = loudness - Vector3.Distance(point, listener);
        return score;
    }
}

public class TeacherBehaviour : MonoBehaviour
{
    public static List<SoundCue> soundCues;
    public SoundCue mostInterestingSound = null;

    public float soundMemoryTime = 4.0f;

    public Collider roomBounds;

    public AudioSource coughAudio;

    private Animator animator;

    private float turn_timer;
    private const float TURN_INTERVAL = 4.0f;
    private bool can_increase_turn_timer = true;


    private bool has_gasped = false;

    private AudioSource audio_source;

    public Transform head_transform;
    private bool is_writing;

    void Start()
    {
        soundCues = new List<SoundCue>();
        animator = GetComponent<Animator>();
        if(roomBounds == null)
        {
            Debug.LogError("MISSING COLLIDER REFERENCE ON TEACHER");
        }

        audio_source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {

        //Debug.Log(soundCues.Count);
        foreach (SoundCue cue in soundCues.ToArray())
        {
            if(roomBounds.bounds.Contains(cue.point))
            {
                animator.SetBool("Notice", true);
                if(has_gasped == false)
                {
                    SoundManager.PlaySound("lady gasp");
                    has_gasped = true;
                }
            }
            cue.age += Time.deltaTime;
            if(cue.age >= soundMemoryTime)
            {
                soundCues.Remove(cue);
            }
        }

        //turning down the teacher's writing sound
        if (roomBounds.bounds.Contains(head_transform.position))
        {
            if(is_writing)
            {
                if(audio_source.volume != 1)
                {
                    audio_source.volume = 1;
                }
            }
            if(is_writing == false)
            {
                if (audio_source.volume != 0)
                {
                    audio_source.volume = 0;
                }
            }
        }
        else
        {
            if (audio_source.volume != 0)
            {
                audio_source.volume = 0;
            }
        }

        //turning down the teacher's cough sound
        if (roomBounds.bounds.Contains(head_transform.position))
        {
            if (coughAudio.volume != 1)
                {
                    coughAudio.volume = 1;
                }
        }
        else
        {
            if (coughAudio.volume != 0)
                {
                    coughAudio.volume = 0;
                }
        }

        //automatic turning without hearing sound
        if(can_increase_turn_timer == true)
        {
            if (turn_timer >= TURN_INTERVAL)
            {
                animator.SetBool("Notice", true);

                float p = Random.Range(0.8f,1.2f);
                coughAudio.pitch = p;
                coughAudio.PlayOneShot(coughAudio.clip);
                //make sound when is about to turn automatically //////////////////////////

                can_increase_turn_timer = false;
                turn_timer = 0.0f;
            }
            turn_timer += Time.deltaTime;
        }
        //Debug.Log(turn_timer);
    }

    public void clearList()
    {
       
        soundCues.Clear();
        animator.SetBool("Notice", false);
        has_gasped = false;
        turn_timer = 0.0f;
        can_increase_turn_timer = true;
        //Debug.Log("CLEAR " + animator.GetBool("Notice"));
    }   
    
    public void gasp()
    {
        //SoundManager.PlaySound("lady gasp");
    }

    public void writeON()
    {
        is_writing = true;
    }

    public void writeOff()
    {
        is_writing = false;
    }
}



/*
        mostInterestingSound = null;
        float loudScore = -999999.0f;
        foreach(SoundCue cue in soundCues.ToArray())
        {
            //update?
            cue.age += Time.deltaTime;
            //Get the most loud one,
            if(cue.GetLoudScore(transform.position) > loudScore)
            {
                loudScore = cue.GetLoudScore(transform.position);
                mostInterestingSound = cue;
            }

            //Remove old sounds
            if (cue.age >= soundMemoryTime)
            {
                soundCues.Remove(cue);
            }
        }

        if(mostInterestingSound != null)
        {
            viewFrustum.SetActive(true);
        }
        else
        {
            viewFrustum.SetActive(false);
        }
        */
