using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float nextFire = 0.2f;

    private Gamepad myGamepad;

    private void Start()
    {
        myGamepad = Gamepad.current;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var ball = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            // ball.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;

        }
    }


    // private void FixedUpdate()
    // {
    //     // if the right stick vector is other than 0 we want this to happen
    //     if (myGamepad.rightStick.ReadValue() != new Vector2(0,0))
    //     {
    //         nextFire = Time.time + fireRate;
    //         var ball = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    //         ball.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed; 
    //     }
    //     
    // }

    private void OnLook(InputValue lookValue)
    {
       
        if (lookValue.Get<Vector2>() != Vector2.zero && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            var ball = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            ball.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            // Destroy(ball, 1f);

        }

    }

}
