using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MouseSensitivity : MonoBehaviour
{
    public Slider MouseSlider;
    private CinemachineFreeLook freeLook;

    // Start is called before the first frame update
    void Start()
    {
        MouseSlider.value = PlayerPrefs.GetFloat("SensitivityValue", 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSensitivity(float slidervalue)
    {
        PlayerPrefs.SetFloat("SensitivityValue", slidervalue);
    }

}
