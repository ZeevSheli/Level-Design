using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SizePickup : MonoBehaviour
{
    public float multiplier = 1.5f;
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
        Debug.Log("Power up 2 picked up");
              
        Player.transform.localScale *= multiplier;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        Player.transform.localScale /= multiplier;

        Destroy(this.gameObject);
    }
}
