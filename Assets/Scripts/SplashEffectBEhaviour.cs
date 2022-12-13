using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashEffectBEhaviour : MonoBehaviour
{
    private ParticleSystem particle_system;
    // Start is called before the first frame update
    void Start()
    {
        particle_system = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(particle_system)
        {
            if (!particle_system.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }

    //ParticleSystem splash_particle = SplashEffect.GetComponent<ParticleSystem>();
    //Destroy(SplashEffect, splash_particle.main.duration);
}
