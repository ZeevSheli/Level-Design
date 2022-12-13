using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldTemp : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void TurnOff()
    {
        anim.SetTrigger("TurnOff");
        SoundManager.PlaySound("shield deactivate");
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void ShieldHit()
    {
        SoundManager.PlaySound("shield hit");
    }
}
