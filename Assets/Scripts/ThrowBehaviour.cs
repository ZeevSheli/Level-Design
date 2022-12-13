using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBehaviour : MonoBehaviour
{
    private float lifeTimeInSeconds = 4.0f;
    public GameObject destroyEffect;
    private Rigidbody ball_rigidbody;
    Vector3 speed;

    void Start()
    {
        ball_rigidbody = GetComponent<Rigidbody>();
        speed = ball_rigidbody.velocity;
    }

    void Update()
    {
        ball_rigidbody.velocity = speed;
        lifeTimeInSeconds -= Time.deltaTime;
        if (lifeTimeInSeconds <= 0.0f)
        {
            CollideAndDestroy();
        }
    }

    void OnTriggerEnter(Collider c)
    {
        //Debug.Log("Hit: " + c.name);
        if (!c.CompareTag("Player") && !c.CompareTag("Homing Projectile") && !c.CompareTag("Throw Projectile"))
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
