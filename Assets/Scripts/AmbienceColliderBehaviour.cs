using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceColliderBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AmbienceManager.getAmbienceCollision(gameObject);
        }
    }
}
