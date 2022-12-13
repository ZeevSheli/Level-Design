using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
    public float duration = 2f;
    public float timeSpeed = 1f;
    public float timeSlow = 0.25f;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }
    IEnumerator Pickup(Collider Player)
    {
        //Debug.Log("Power up 3 picked up");

        Time.timeScale = timeSlow;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        Time.timeScale = timeSpeed;

        Destroy(this.gameObject);
    }
}
