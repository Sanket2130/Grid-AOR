using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{
   private InputManager inputManager;
   private CapsuleCollider capsuleCollider;
   private CharacterController characterController;
   public GameObject camera;
   public int weaponIndicator;
   public GameObject[] weapons = new GameObject[3];
   public UImanager ui;

   public float jumpForce = 200f, movementSpeed = 12, gravityForce = -9.81f;
   [Range(0.1f, 5f)] public float mouseSens = 0.7;
   private Vector3 movementVector;
   private Vector3 gravity;

   private void Start()
   {
      ui = GameObject.FindGameObjectsWithTag("UI").GetComponent<UImanager>();
      characterController = GetComponent<CharacterController>();
      inputManager = GetComponent<InputManager>();
      capsuleCollider = GetComponent<CapsuleCollider>();
      swap(0);
      ui.setHealth("150");
      ui.setCredit("9000");
      ui.setDisplay(0);
   }

   private void FixedUpdate()
   {
      movementVector = transform.right * inputManager.horizontal + transform.forward * inputManager.vertical;
      characterController.Move(movementVector * movementSpeed * Time.deltaTime);
      if (isGrounded() && gravity.y < 0)
      {
         gravity.y = -2;
      }

      gravity.y += gravityForce * Time.deltaTime;
      characterController.Move(gravity * Time.deltaTime);
      
      transform.localRotation *= Quaternion.Euler(0f,inputManager.yValue * mouseSens, 0f);
      if(inputManager.xValue > 0 && camera.transform.localRotation.x > -0.7f)
         camera.transform.localRotation *= Quaternion.Euler(-inputManager.xValue * mouseSens ,0f,0f);
      if(inputManager.xValue < 0 && camera.transform.localRotation.x < 0.7f)
         camera.transform.localRotation *= Quaternion.Euler(-inputManager.xValue * mouseSens,0f,0f);
   }

   public void jump()
   {
      if(isGrounded())
         gravity.y = Mathf.Sqrt(jumpforce * -2 * gravityForce)
   }

   private bool isGrounded()
   {
      RaycastHit raycastHit;
      if(Physics.SphereCast(transform.position,characterController.radius * (1.0f - 0),Vector3.down,out.raycastHit,
         ((characterController.height/2f) -characterController.radius) + .1f,Physics.AllLayers,QueryTriggerInteraction.Ignore))
         {
            return;
         }
         else return false;
   }

   private void OnGUI()
   {
      GUI.contentColor = Color.cyan;
      GUILayout.Label(("grounded : " + isGrounded()));
   }

   public void swap(int index)
   {
      for (int i = 0; i < weapons.Length; i++)
      {
         weapons[i].SetActive(false);
      }
      weapons[index].SetActive(true);
      ui.setDisplay(index);
      weaponIndicator = index;
   }
}
