using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicColliderBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            MusicManager.getMusicCollision(gameObject);
        }
    }
}
