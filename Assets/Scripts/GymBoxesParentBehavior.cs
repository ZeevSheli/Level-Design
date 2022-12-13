using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GymBoxesParentBehavior : MonoBehaviour
{
    public Animator anim;
    static Animator anim2;

    private void Start()
    {
        anim2 = anim;
    }
    public static void getCollision()
    {
        anim2.SetTrigger("Hit");
    }

}
