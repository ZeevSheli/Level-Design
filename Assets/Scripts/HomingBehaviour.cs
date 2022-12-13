using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBehaviour : MonoBehaviour
{
    GameObject player;
    const float speed = 3.0f;
    private Vector3 movementDirection = Vector3.zero;

    private float lifeTimeInSeconds = 9.0f;

    public GameObject destroyEffect;

    void Awake()
    {
        player = GameObject.Find("Head");
    }

    void Update()
    {
        //Move towards player
        movementDirection = player.transform.position - this.transform.position;
        movementDirection.Normalize();
        transform.position += movementDirection * speed * Time.deltaTime;

        lifeTimeInSeconds -= Time.deltaTime;
        if(lifeTimeInSeconds <= 0.0f)
        {
            CollideAndDestroy();
        }
    }

    void OnTriggerEnter(Collider c)
    {
        //Debug.Log("Hit: " + c.name);
        if(!c.CompareTag("Player") && !c.CompareTag("Homing Projectile") && !c.CompareTag("Throw Projectile"))
        {
            CollideAndDestroy();
        }
    }
    public void CollideAndDestroy()
    {
        Destroy(this.gameObject);
        GameObject.Instantiate(destroyEffect as GameObject, transform.position, Quaternion.identity);
    }
}
