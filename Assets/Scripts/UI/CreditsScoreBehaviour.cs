using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScoreBehaviour : MonoBehaviour
{
    private Text berry_score_UItext;
    private int berries_collected, berries_total;

    // Start is called before the first frame update
    void Start()
    {
        berry_score_UItext = GetComponent<Text>();
        berries_collected = JadBerryBehaviour.berries_collected;
        berries_total = JadBerryBehaviour.berries_total;
        berry_score_UItext.text = "Berries  collected\n"+ berries_collected + "/" + berries_total + "      ";
    }
}
