using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillySoundBehaviour : MonoBehaviour
{

    public AudioSource gruntSource;
    public ParticleSystem stepPoof;
    
    public void grunt()
    {
        float p = Random.Range(1.0f, 1.9f);
        gruntSource.pitch = p;
        gruntSource.PlayOneShot(gruntSource.clip);
    }

    public void step()
    {
     stepPoof.Play();
    }
}
