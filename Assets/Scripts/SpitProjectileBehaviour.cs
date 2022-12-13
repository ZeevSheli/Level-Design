using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitProjectileBehaviour : MonoBehaviour
{

    public GameObject SplashEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject.Instantiate(SplashEffect as GameObject, transform.position, Quaternion.identity);
        SoundManager.PlaySound("droplet");
        Destroy(gameObject);
    }
}
