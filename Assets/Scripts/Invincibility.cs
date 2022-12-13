using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Invincibility : MonoBehaviour
{
    
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
        Debug.Log("Invincible");

        PlayerMovementController damage = Player.GetComponent<PlayerMovementController>();
        damage.Invincible = true;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        damage.Invincible = false;
        Debug.Log("Damagable");

        
        Destroy(this.gameObject);
    }
}