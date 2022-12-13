using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    static AudioSource music_audio_source;

    // Start is called before the first frame update
    void Start()
    {
        music_audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    static public void getMusicCollision(GameObject collider_object)
    {
        setMusic(collider_object.name);
        if(collider_object.name == "Battle Theme")
        {
            collider_object.SetActive(false);
        }
    }

    static public void setMusic(string place_name)
    {
        switch (place_name)
        {
            case "Barbershop":
                if(music_audio_source.clip != Resources.Load<AudioClip>("Kinda Sus"))
                {
                    music_audio_source.clip = Resources.Load<AudioClip>("Kinda Sus");
                    music_audio_source.Play();
                }
                break;
            case "Battle Theme":
                if (music_audio_source.clip != Resources.Load<AudioClip>("Out of Hand"))
                {
                    music_audio_source.clip = Resources.Load<AudioClip>("Out of Hand");
                    music_audio_source.Play();
                }
                break;
            case "Main Theme":
                if (music_audio_source.clip != Resources.Load<AudioClip>("Heading Out"))
                {
                    music_audio_source.clip = Resources.Load<AudioClip>("Heading Out");
                    music_audio_source.Play();
                }
                break;           
            case "Sneak Theme":
                if (music_audio_source.clip != Resources.Load<AudioClip>("Do Not Disturb"))
                {
                    music_audio_source.clip = Resources.Load<AudioClip>("Do Not Disturb");
                    music_audio_source.Play();
                }
                break;
        }
    }
}
