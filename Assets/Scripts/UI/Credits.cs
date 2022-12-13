using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    private float normal_speed = 1.5f;
    private float nyoom = 6.0f;

    private bool is_fast, down_is_pressed;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody2D>().velocity = transform.up * normal_speed;
        is_fast = false;
        down_is_pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_fast)
        {
            this.GetComponent<Rigidbody2D>().velocity = transform.up * nyoom;
        }

        if (is_fast == false)
        {
            this.GetComponent<Rigidbody2D>().velocity = transform.up * normal_speed;
        }
        checkIfFast();
    }

    private void checkIfFast()
    {
        if (Input.GetButton("Jump") == true || Input.GetAxisRaw("Vertical") < 0.0f)
        {
            is_fast = true;
        }
        if (Input.GetButton("Jump") == false && Input.GetAxisRaw("Vertical") >= 0.0f)
        {
            is_fast = false;
        }
    }
}