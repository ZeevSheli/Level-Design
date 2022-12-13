using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunScript : MonoBehaviour
{
    float speed;
    public float x = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Rotate(0, x, 0 * Time.deltaTime);
    }
}
