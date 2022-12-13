using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpitAmmoBehaviour : MonoBehaviour
{
    public float spit_amount;
    public float MAX_SPIT_AMOUNT = 100.0f;
    private const float TIME_TO_REACH_MAX_SPIT = 30.0f;
    public Slider water_slider;

    // Start is called before the first frame update
    void Start()
    {
        //spit_amount = MAX_SPIT_AMOUNT;
        spit_amount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        water_slider.value = spit_amount;
        if (spit_amount > MAX_SPIT_AMOUNT)
        {
            spit_amount = MAX_SPIT_AMOUNT;
        }
        if (spit_amount < MAX_SPIT_AMOUNT)
        {
            spit_amount += Time.deltaTime *  MAX_SPIT_AMOUNT / TIME_TO_REACH_MAX_SPIT;
        }
    }
}
