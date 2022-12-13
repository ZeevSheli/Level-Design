using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeGrowthBehaviour : MonoBehaviour
{
    [Header("Grow this much each time")]
    [Tooltip("How much the object should grow per each spit")]
    public float growthPerSpit = 100;

    [Header("Grow This fast")]
    [Tooltip("multiplies the speed for which to grow by this number")]
    public float growthSpeed = 50;

    [Header("only scale to max original size * this")]
    [Tooltip("Only allow this object to scale up to this many times the original size")]
    public float MaximumSize = 5;

    [Header("Mass per Size")]
    [Tooltip("specify mass per any given size")]
    public float massPerSize = 0.1f;

    Vector3 desiredSize;
    Vector3 maxSize;
    Rigidbody rb;


    SpriteRenderer speechBubble;
    bool speechBubbleToggle;

    bool hasGrown;

    private AudioSource audioGrow;

    void Start()
    {
        desiredSize = transform.localScale;
        maxSize = transform.localScale * MaximumSize;

        rb = GetComponent<Rigidbody>();

        speechBubble = transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        speechBubbleToggle = false;

        audioGrow = GetComponentInChildren<AudioSource>();
    }
    
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Spit"))
        {
            transform.GetChild(0).GetComponent<BillboardBehaviour>().Hide();
            AddGrowth(growthPerSpit);         
            hasGrown = true;
        }
    }

    void Update()
    {
        if(transform.localScale.x < desiredSize.x)
        {
            transform.localScale += new Vector3(1f, 1f, 1.0f) * (desiredSize.x / transform.localScale.x)* Time.deltaTime * growthSpeed;
        }
        rb.mass = transform.localScale.x * transform.localScale.x * massPerSize;
    }

    void FixedUpdate()
    {
        if(hasGrown)
        {
            return;
        }
        Transform camera = Camera.main.transform;
        Vector3 pos = camera.position + camera.forward * 12.0f;
        Debug.DrawLine(camera.position, pos);
        if (Vector3.Distance(pos, transform.position) <= 3.0f)
        {
            if(!speechBubbleToggle)
            {
                //SoundManager.PlaySound("speech_bubble_show");
                SoundManager.PlaySound("droplet");
                transform.GetChild(0).GetComponent<BillboardBehaviour>().Show();
                speechBubbleToggle = true;
            }
        }
        else
        {
            if (speechBubbleToggle)
            {
                speechBubbleToggle = false;
                transform.GetChild(0).GetComponent<BillboardBehaviour>().Hide();
                //SoundManager.PlaySound("speech_bubble_hide");
                SoundManager.PlaySound("droplet");
            }
        }
    }

    void AddGrowth(float ammount)
    {
        if((desiredSize.x + ammount) <= maxSize.x)
        {
            desiredSize.x += ammount;
            desiredSize.y += ammount;
            desiredSize.z += ammount;

            if(!hasGrown)
            {
                float p = Random.Range(0.8f, 1.2f);
                audioGrow.pitch = p;
                audioGrow.PlayOneShot(audioGrow.clip);
            }
        }
    }
}
