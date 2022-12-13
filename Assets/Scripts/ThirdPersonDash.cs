using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

public class ThirdPersonDash : MonoBehaviour
{
    private PlayerMovementController moveScript;

    public float dashSpeed;
    public float dashTime;

    public Transform playerBody;
    

    public bool isDashing;

    private void Start()
    {
        moveScript = GetComponent<PlayerMovementController>();
    }

    private void OnJump(InputValue Jump)
    {
        if (Jump.isPressed)
        {
            if (isDashing == true)
            {
                return;
            }

            
            StartCoroutine(FuckingMove());
            
            // moveScript.controller.Move(Vector3.Lerp(Vector3.zero,(new Vector3(moveScript.leftStickPosition.x, 0, moveScript.leftStickPosition.y) * dashSpeed), dashTime));
            // isDashing = true;
        }
        
        
    }

    IEnumerator FuckingMove()
    {
        isDashing = true;
        float timer = 0;
        
        while (timer < dashTime)
        {
            timer += Time.deltaTime;
    
            var t = timer / dashTime;
            //                                                 //A                              //B                                                                                                                      //T
            this.transform.position = Vector3.Lerp(this.transform.position,  playerBody.transform.position + new Vector3(moveScript.leftStickPosition.x, 0, moveScript.leftStickPosition.y) * dashSpeed, t);
            yield return new WaitForEndOfFrame();
            
        }
        isDashing = false;
    
    
    }
}
