using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InputManager : MonoBehaviour
{
   private CharacterController char;
   [HideInInspector] public float vertical;
   [HideInInspector] public float horizontal;
   [HideInInspector] public float xValue, yValue;
   private bool pause = false;

   private void Start()
   {
      char = GetComponent<CharacterController>();
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
   }

   private void FixedUpdate()
   {
      if (Input.GetKeyDown(KeyCode.Escape))isPaused();
      if (pause)
      {

         vertical = 0;
         horizontal = 0;
         xValue = 0;
         yValue = 0;
      }
      else
      {
         vertical = Input.GetAxis("Vertical");
         horizontal = Input.GetAxis("Horizontal");
         xValue = CrossPlatformInputManager.GetAxis("Mouse Y");
         yValue = CrossPlatformInputManager.GetAxis("Mouse X");
         if (Input.GetKeyDown(KeyCode.Space)) char.jump();
         if (Input.GetKeyDown(KeyCode.Q)) char.swap(char.weaponIndicator < 2 ? char.weaponIndicator +1 : 0 );
      }
   }

   private void isPaused()
   {
      if (pause)
      {
         Cursor.lockState = CursorLockMode.Locked;
         Cursor.visible = false;
         pause = false;
      }
      else
      {
         Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true;
         pause = true;
      }
   }
}
