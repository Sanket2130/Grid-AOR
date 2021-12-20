using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponController : MonoBehaviour
{
   public float fireRate = 20f;
   public int magzine = 2;
   public int ammo;
   public int mag = 50;
   public GameObject cameraGameObject;
   public ParticleSystem flash;
   public GameObject bullet;
   private float fireStart;
   private Animator anim;
   private InputManager input;
   private float reload = 0;
   public float reloadAnimation = 2f;
   private int magRef;
   private bool isReloading = false;
   public UImanager ui;

   private void Start()
   {
       anim = gameObject.GetComponent<Animator>();
       input = gameObject.GetComponent<InputManager>();
       anim.SetInteger("Movement", 0);
       ammo = mag * magzine;
       magRef = mag;
       ui = GameObject.FindGameObjectsWithTag("UI").GetComponent<UImanager>();
       ui.setAmmo(mag + "/" + ammo);
       
   }

   private void FixedUpdate() {
       if (Time.time >= fireStart)
       {
           anim.SetInteger("Fire", -1);
           anim.SetInteger("Movement", (input.vertical == 0 && input.horizontal == 0)? 0 : 1);
       }
       
       
       
       
       if (Input.GetButton("Fire1") && Time.time >= fireStart && !isReloading && mag > 0)
       {
           fireStart = Time.time + 1f / fireRate;
           fire();
           anim.SetInteger("Fire" , 1);
           anim.SetInteger("Fire", -1);
       }

       if (input.GetKeyDown(KeyCode.R) && !isReloading && ammo > 0)
       {
           reload = reloadAnimation;
           anim.SetInteger("Reload", 1);
           isReloading = true;
       }

       if (isReloading && reload <= 1)
       {
           reload = 0;
           anim.SetInteger("Reload", -1);
           isReloading = false;
           ammo = ammo - 50 + mag;
           mag = magRef;
           if (ammo < 0)
           {
               mag += ammo;
               ammo = 0;
               ui.setAmmo(mag + "/" + ammo);
           }
           ui.setAmmo(mag + "/" + ammo);
       }
       else
       {
           reload -= Time.deltaTime;
       }
   }

   

   private void fire()
   {
       ui.setAmmo(mag + "/" + ammo);
       flash.Play();
       RaycastHit hit;
       mag -- ;
       if (Physics.Raycast(cameraGameObject.transform.position, cameraGameObject.transform.forward, out hit))
       {
           Instantiate(bullet, hit.point, Quaternion.LookRotation(hit.normal));
       }
   }
   
   
   
   
   
   
}
