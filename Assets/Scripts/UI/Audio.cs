using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class Audio : MonoBehaviour
{
    public AudioMixer Mixer;
    public Slider Soundtrack;
    public Slider SFX;

    // Start is called before the first frame update
    void Start()
    {
        //Soundtrack.value = PlayerPrefs.GetFloat("ParamSound", 0.1f);
        //SFX.value = PlayerPrefs.GetFloat("ParamSFX", 1f);

        //Mixer.SetFloat("ParamSound", (Mathf.Log10(0.1f) * 20));


        //set values each time the scene is loaded
        //Soundtrack.value = 0.1f;   
        //SFX.value = 1;

        //if you want it to keep volume and slider position between play sessions use this vvv

        Soundtrack.value = PlayerPrefs.GetFloat("ParamSound", 0.1f);
        SFX.value = PlayerPrefs.GetFloat("ParamSFX", 1f);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetMusic(float slidervalue)
    {
        float sliderM = Soundtrack.value;
        //temporary fix for sound maxing out when it's left at under 0.0001
        if (sliderM <= 0.0005f)
        {
            sliderM = 0.0005f;
            Soundtrack.value = 0.0005f;
        }
        //
        Mixer.SetFloat("ParamSound", (Mathf.Log10(sliderM) * 20));
        PlayerPrefs.SetFloat("ParamSound", sliderM);
    }

    public void setSFX(float Slidervalue)
    {
        float sliderS = SFX.value;
        //temporary fix for sound maxing out when it's left at under 0.0001
        if (sliderS <= 0.001f)
        {
            sliderS = 0.001f;
            SFX.value = 0.001f;
        }
        //temporary fix for sound maxing out when it's left at under 0.0001
        Mixer.SetFloat("ParamSFX", (Mathf.Log10(sliderS) * 20));
        PlayerPrefs.SetFloat("ParamSFX", sliderS);
    }
}
