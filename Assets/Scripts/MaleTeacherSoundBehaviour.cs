using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleTeacherSoundBehaviour : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource gruntSource;
    public AudioSource slapSource;

    public void Step()
    {
        float p = Random.Range(0.8f, 1.1f);
        audioSource.pitch = p;
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void grunt()
    {
        float p = Random.Range(0.8f, 1.1f);
        gruntSource.pitch = p;
        gruntSource.PlayOneShot(gruntSource.clip);
    }

    public void slap()
    {
        float p = Random.Range(0.6f, 1.1f);
        slapSource.pitch = p;
        slapSource.PlayOneShot(slapSource.clip);
    }
}
