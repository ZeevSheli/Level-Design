using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbershopBehavior : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            HealthRespawnBehaviour.startRespawn();
        }
    }
}
