using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JadBerryBehaviour : MonoBehaviour
{
    public GameObject SplashParticle;

    public static int berries_collected = 0;
    private bool collected;

    public static int berries_total = 10;

    private void Start()
    {
        berries_collected = 0;
        collected = false;

        //Debug.Log(total_available_berries);
    }

    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(collected == false)
            {
                collected = true;
                berries_collected++;
                //Debug.Log(berries_collected);
            }

            other.gameObject.GetComponent<HeadBehaviour>().EatBerry();
            Eaten();
            //Debug.Log("eaten");
        }
    }
    void Eaten()
    {
     //Instantiate(SplashParticle, transform.position, transform.rotation);
      GameObject Splash = Instantiate(SplashParticle, transform.position, transform.rotation);
      Destroy(gameObject);
      Destroy(Splash, 1);
    }
}
