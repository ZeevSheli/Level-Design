using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
    private static TextMeshProUGUI[] ui_digits = new TextMeshProUGUI[4];
    private static GameObject digits_parent;
    public GameObject particle_effect;
    public Material green_light_on_locker;

    private bool opened_door = false;

    // Start is called before the first frame update
    void Start()
    {
        digits_parent = GameObject.FindWithTag("UI Digits Parent");
        ui_digits = digits_parent.GetComponentsInChildren<TextMeshProUGUI>();
        green_light_on_locker.SetColor("_EmissionColor", new Color(190.0f, 30.0f, 30.0f, 1.0f) * 0.022f);
        foreach(TextMeshProUGUI digit in ui_digits)
        {
            digit.text = "?";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene("ProBuilder_Scene");
        //}

        //if (getWinState() == true && opened_door == false)
        //{
        //    green_light_on_locker.SetColor("_EmissionColor", new Color(50.0f, 190.0f, 130.0f, 1.0f) * 0.022f);
        //    particle_effect.SetActive(true);
        //    SoundManager.PlaySound("school_bell");
        //    opened_door = true;
        //}       
        if (getWinState() == true && opened_door == false)
        {
            SoundManager.PlaySound("school_bell");
            opened_door = true;
        }
    }

    public static void setUIDigit(int value, DigitBehaviour.DigitPosition position)
    {
        ui_digits[(int)position].text = value.ToString();
    }

    public static bool getWinState()
    {
        //foreach (TextMeshProUGUI digit in ui_digits)
        //{
        //    if (digit.text == "?")
        //    {
        //        return false;
        //    }
        //}
        if(GateBehaviour.have_opened_gate == false)
        {
            return false;
        }
        return true;
    }
}
