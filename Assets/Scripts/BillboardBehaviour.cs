using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardBehaviour : MonoBehaviour
{
    //GameObject player;

    Animator animator;

    SpriteRenderer sprite;


    Vector3 center;
    void Start()
    {
        //player = GameObject.Find("Head");
        animator = transform.GetChild(0).GetComponent<Animator>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sprite.enabled = false;

        center = transform.parent.position;
    }

    void Update()
    {
        transform.forward = -Camera.main.transform.forward;
        transform.position = center + (Camera.main.transform.right * 1.0f) + (Vector3.up * 0.6f);
        if(transform.GetChild(0).localScale.x > 0.001f)
        {
            sprite.enabled = true;
        }
        else
        {
            sprite.enabled = false;
        }
    }

    public void Show()
    {
        sprite.enabled = true;
        animator.Play("WaterSpeechBubbleActivate");
    }
    public void Hide()
    {
        animator.Play("WaterSpeechBubbleHide");
    }
    public void DeActivate()
    {
        sprite.enabled = false;
    }
}
