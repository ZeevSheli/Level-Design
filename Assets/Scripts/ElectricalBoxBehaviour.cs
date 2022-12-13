using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalBoxBehaviour : MonoBehaviour
{
    private Animator anim;
    private ParticleSystem particle_system;
    private ParticleSystem blue_sparks;
    public ParticleSystem smoke;
    public bool gymBox;
    public bool boolIsSet;
    LineRenderer lineR;
    public Transform[] lines;


    private void Start()
    {
        anim = GetComponent<Animator>();
        particle_system = GetComponent<ParticleSystem>();
        blue_sparks = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        //B = new ElectircalRecieverBehaviour.BOOL(false);
        //Debug.Log(blue_sparks);

        lineR = GetComponent<LineRenderer>();
        lineR.positionCount = lines.Length;

        for(int i = 0; i < lines.Length; i++)
        {
            lineR.SetPosition(i, lines[i].position);
        }

    }

    ElectircalRecieverBehaviour.BOOL B;
    public void SetBool(ElectircalRecieverBehaviour.BOOL _B)
    {
        boolIsSet = true;
        B = _B;
    }

    public ElectircalRecieverBehaviour.BOOL GetBOOL()
    {
        return B;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Spit"))
        {
            if(B != null)
            {
                B._bool = true;
                //Debug.Log(gameObject.name + B._bool);
                anim.SetTrigger("Hit");
                particle_system.Play();
                blue_sparks.Play();
                smoke.Play();
                SoundManager.PlaySound("power outage");
                if(gymBox == true)
                {
                GymBoxesParentBehavior.getCollision();
                }
            }
        }
    }
}
