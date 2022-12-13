using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthPickUp : MonoBehaviour
{
    //public GameObject pickupEffect;
    public float multiplier = 2f;
    public float duration = 4f;

    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider Player)
    {
        Debug.Log("Power up picked up");

        //Particle Effect
        //Instantiate(pickupEffect, transform.position, transform.rotation);

        PlayerMovementController stats = Player.GetComponent<PlayerMovementController>();
        stats.maxHealth *= multiplier;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        stats.maxHealth /= multiplier;

        Destroy(this.gameObject);
    }
}
