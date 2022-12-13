using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    static AudioSource ambience_audio_source;

    // Start is called before the first frame update
    void Start()
    {
        ambience_audio_source = GetComponent<AudioSource>();
    }

    static public void getAmbienceCollision(GameObject collider_object)
    {
        setAmbience(collider_object.name);
    }

    static public void setAmbience(string place_name)
    {
        switch (place_name)
        {
            case "Barbershop Ambience":
                if (ambience_audio_source.clip != Resources.Load<AudioClip>("Barbershop_Ambience_01"))
                {
                    ambience_audio_source.clip = Resources.Load<AudioClip>("Barbershop_Ambience_01");
                    ambience_audio_source.Play();
                }
                break;
            case "School Ambience":
                if (ambience_audio_source.clip != Resources.Load<AudioClip>("School_Ambience_01"))
                {
                    ambience_audio_source.clip = Resources.Load<AudioClip>("School_Ambience_01");
                    ambience_audio_source.Play();
                }
                break;
            case "Outside Ambience":
                if (ambience_audio_source.clip != Resources.Load<AudioClip>("Outside_Ambience_02"))
                {
                    ambience_audio_source.clip = Resources.Load<AudioClip>("Outside_Ambience_02");
                    ambience_audio_source.Play();
                }
                break;
        }
    }
}
