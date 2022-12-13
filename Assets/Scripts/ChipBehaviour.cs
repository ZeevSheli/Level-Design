using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBehaviour : MonoBehaviour
{
    public AudioSource audioSource;
    public ParticleSystem part;

    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float vel = 0.0f;
            Vector3 pos = transform.position;
            string name = collision.gameObject.name + " " + gameObject.name;
            SoundCue cue = new SoundCue(name, pos, vel);
            TeacherBehaviour.soundCues.Add(cue);
            
            part.Play();

            float p = Random.Range(0.7f, 1.3f);
            audioSource.pitch = p;
            audioSource.PlayOneShot(audioSource.clip);
            //SoundManager.PlaySound("crunch");

        }
    }
}
