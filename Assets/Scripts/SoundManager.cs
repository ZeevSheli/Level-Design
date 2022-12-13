using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip locked_locker, locker_open, note_pickup, school_bell, thud,
        jump_groan_1, jump_groan_2, jump_groan_3, crowd_cheer, slap, power_outage, drink,
        lava_sizzle, shield_deactivate, spit_1,spit_2 , spit_gurgle, droplet1, droplet2,
        droplet3, gym_teahcer_death, gym_groan1, gym_groan2, throw_wind, shield_hit,
        gym_call, alert_stab, lady_gasp1, lady_gasp2, lady_gasp3, speech_bubble_show,
        speech_bubble_hide, crunch1, crunch2, crunch3, teacherLaugh, berryEat, trap_reset;
    static AudioSource audio_source;

    private void Awake()
    {
        locked_locker = Resources.Load<AudioClip>("Locked 01");
        locker_open = Resources.Load<AudioClip>("Locker Open 01");
        note_pickup = Resources.Load<AudioClip>("Paper Pick Up Jingle 01");
        school_bell = Resources.Load<AudioClip>("School Bell");
        thud = Resources.Load<AudioClip>("Thud 01");
        jump_groan_1 = Resources.Load<AudioClip>("Jump Groan 01");
        jump_groan_2 = Resources.Load<AudioClip>("Jump Groan 02");
        jump_groan_3 = Resources.Load<AudioClip>("Jump Groan 03");
        crowd_cheer = Resources.Load<AudioClip>("Crowd Cheer 01");
        slap = Resources.Load<AudioClip>("slaphit 01");
        drink = Resources.Load<AudioClip>("Drink");
        power_outage = Resources.Load<AudioClip>("Power Outage");
        lava_sizzle = Resources.Load<AudioClip>("Lava Sizzle");
        shield_deactivate = Resources.Load<AudioClip>("Shield Deactivate");
        spit_1 = Resources.Load<AudioClip>("Spit 01");
        spit_2 = Resources.Load<AudioClip>("Spit 02");
        spit_gurgle = Resources.Load<AudioClip>("Spit Gurgle");
        droplet1 = Resources.Load<AudioClip>("Droplet 01");
        droplet2 = Resources.Load<AudioClip>("Droplet 02");
        droplet3 = Resources.Load<AudioClip>("Droplet 03");
        gym_teahcer_death = Resources.Load<AudioClip>("Gym Death 01");
        gym_groan1 = Resources.Load<AudioClip>("Gym Moan 01");
        gym_groan2 = Resources.Load<AudioClip>("Gym Moan 02");
        throw_wind = Resources.Load<AudioClip>("Throw Wind");
        shield_hit = Resources.Load<AudioClip>("Shield Hit");
        gym_call = Resources.Load<AudioClip>("Gym Call");
        alert_stab = Resources.Load<AudioClip>("Alert Stab");
        lady_gasp1 = Resources.Load<AudioClip>("Lady Gasp 01");
        lady_gasp2 = Resources.Load<AudioClip>("Lady Gasp 02");
        lady_gasp3 = Resources.Load<AudioClip>("Lady Gasp 03");
        speech_bubble_show = Resources.Load<AudioClip>("Speech Bubble");
        speech_bubble_hide = Resources.Load<AudioClip>("Speech Bubble Close");
        crunch1 = Resources.Load<AudioClip>("Crunch1");
        crunch2 = Resources.Load<AudioClip>("Crunch2");
        crunch3 = Resources.Load<AudioClip>("Crunch3");
        teacherLaugh = Resources.Load<AudioClip>("Teacher Caught Laugh");
        berryEat = Resources.Load<AudioClip>("Berry Eating");
        trap_reset = Resources.Load<AudioClip>("Trap Winding");

        audio_source = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "locked_locker":
                audio_source.PlayOneShot(locked_locker);
                break;
            case "locker_open":
                audio_source.PlayOneShot(locker_open);
                break;
            case "note_pickup":
                audio_source.PlayOneShot(note_pickup);
                break;
            case "school_bell":
                audio_source.PlayOneShot(school_bell);
                break;
            case "thud":
                audio_source.PlayOneShot(thud);
                break;
            case "jump":
                int i = Random.Range(0, 3);
                randomJumpSound();
                break;
            case "cheer":
                audio_source.PlayOneShot(crowd_cheer);
                break;          
            case "slap":
                audio_source.PlayOneShot(slap);
                break;
            case "drink":
                audio_source.PlayOneShot(drink);
                break;
            case "power outage":
                audio_source.PlayOneShot(power_outage);
                break;
            case "lava sizzle":
                audio_source.PlayOneShot(lava_sizzle);
                break;
            case "shield deactivate":
                audio_source.PlayOneShot(shield_deactivate);
                break;
            case "spit":
                int s = Random.Range(0, 2);
                randomSpitSound();
                break;
            case "gurgle":
                audio_source.PlayOneShot(spit_gurgle);
                break;
            case "droplet":
                int d = Random.Range(0, 3);
                randomDropletSound();
                break;
            case "gym death":
                audio_source.PlayOneShot(gym_teahcer_death);
                break;
            case "throw":
                audio_source.PlayOneShot(throw_wind);
                break;
            case "gym groan":
                int g = Random.Range(0, 2);
                randomGymMoanSound();
                break;
            case "shield hit":
                audio_source.PlayOneShot(shield_hit);
                break;
            case "gym call":
                audio_source.PlayOneShot(gym_call);
                break;          
            case "alert stab":
                audio_source.PlayOneShot(alert_stab);
                break;
            case "lady gasp":
                int o = Random.Range(0, 3);
                randomGaspSound();
                break;
            case "speech_bubble_show":
                audio_source.PlayOneShot(speech_bubble_show);
                break;
            case "speech_bubble_hide":
                audio_source.PlayOneShot(speech_bubble_hide);
                break;
            case "crunch":
                int c = Random.Range(0, 3);
                randomCrunchSound();
                break;
            case "teacher laugh":
                audio_source.PlayOneShot(teacherLaugh);
                break;
            case "berry eat":
                audio_source.PlayOneShot(berryEat);
                break;           
            case "trap reset":
                audio_source.PlayOneShot(trap_reset);
                break;
        }
    }

    static void randomJumpSound()
    {
        int i = Random.Range(1, 4);

        switch (i)
        {
            case 1:
                audio_source.PlayOneShot(jump_groan_1);
                break;
            case 2:
                audio_source.PlayOneShot(jump_groan_2);
                break;
            case 3:
                audio_source.PlayOneShot(jump_groan_3);
                break;
        }   
    }
    static void randomSpitSound()
    {
        int s = Random.Range(1, 3);

        switch (s)
        {
            case 1:
                audio_source.PlayOneShot(spit_1);
                break;
            case 2:
                audio_source.PlayOneShot(spit_2);
                break;
        }
    }
    static void randomDropletSound()
    {
        int d = Random.Range(1, 4);

        switch (d)
        {
            case 1:
                audio_source.PlayOneShot(droplet1);
                break;
            case 2:
                audio_source.PlayOneShot(droplet2);
                break;
            case 3:
                audio_source.PlayOneShot(droplet3);
                break;
        }
    }
    static void randomGymMoanSound()
    {
        int g = Random.Range(1, 3);

        switch (g)
        {
            case 1:
                audio_source.PlayOneShot(gym_groan1);
                break;
            case 2:
                audio_source.PlayOneShot(gym_groan2);
                break;
        }
    }
    static void randomGaspSound()
    {
        int o = Random.Range(1, 4);

        switch (o)
        {
            case 1:
                audio_source.PlayOneShot(lady_gasp1);
                break;
            case 2:
                audio_source.PlayOneShot(lady_gasp2);
                break;
            case 3:
                audio_source.PlayOneShot(lady_gasp3);
                break;
        }
    }
    static void randomCrunchSound()
    {
        int c = Random.Range(1, 4);

        switch (c)
        {
            case 1:
                audio_source.PlayOneShot(crunch1);
                break;
            case 2:
                audio_source.PlayOneShot(crunch2);
                break;
            case 3:
                audio_source.PlayOneShot(crunch3);
                break;
        }
    }
}
