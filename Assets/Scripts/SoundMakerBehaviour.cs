using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMakerBehaviour : MonoBehaviour
{

    static float VelocityThreshold = 5f;

    [Header("Changes sounds position")]
    [Tooltip("If set, sounds will spawn at the position of the object that hits this object, if unset sounds will spawn at the position of this object")]
    public bool isStructure;

    [SerializeField]
    GameObject[] ignoredObjects;

    void OnCollisionEnter(Collision collision)
    {
        foreach(GameObject g in ignoredObjects)
        {
            if (g == collision.gameObject)
                return;
        }

        float vel = collision.relativeVelocity.magnitude;
        Vector3 pos = Vector3.zero;
        //isStructure ? pos = collision.transform.position : pos = transform.position;
        if(isStructure)
        {
            pos = collision.transform.position;
        }
        else
        {
            pos = transform.position;
        }

        if(vel >= VelocityThreshold)
        {
            string name = collision.gameObject.name + " " + gameObject.name;
            SoundCue cue = new SoundCue(name, pos, vel);
            TeacherBehaviour.soundCues.Add(cue);
        }
    }
}
