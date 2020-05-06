using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;




public class PlayerController : MonoBehaviour
{

    [Header("General")]
   [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 50f;
   [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 50f;
   [Tooltip("In m")] [SerializeField] float xRange = 14f;
   [Tooltip("In m")] [SerializeField] float yRange = 11f;

    [Header("Position based")]
   [SerializeField] float yawSensitivity = 1f;
   [SerializeField] float pitchSensitivity = 1f;

    [Header("Throw based")]
   [SerializeField] float pitchMovementSensitivity = 35f;
   [SerializeField] float rollSensitivity = 40f;
    //[SerializeField] float smoothThrowFactor = 0.2f;

    [SerializeField] GameObject[] guns;
    [SerializeField] int fireRate = 8;



   float originalY;
   float xThrow, yThrow;
    //float xThrowLastFrame = 0;
    //float yThrowLastFrame = 0;
    //float xThrowSmooth;
    //float yThrowSmooth;

    bool isControlEnabled = true;
   


   void Start()
   {
      originalY = transform.localPosition.y;


   }


   void Update()
   {
      //SmoothThrow();

        if (isControlEnabled == true)
        {
      ProcessMovement();
      ProcessRotation();
      ProcessHit();
        }

      
      
   }

  

    void ProcessMovement()
   {
      xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
      float xOffset = xThrow * Time.deltaTime * xSpeed;
      float rawXPos = transform.localPosition.x + xOffset;
      float xPosClamp = Mathf.Clamp(rawXPos, -xRange, xRange);

      yThrow = CrossPlatformInputManager.GetAxis("Vertical");
      float yOffset = yThrow * Time.deltaTime * ySpeed;
      float rawYPos = transform.localPosition.y + yOffset;
      float yPosClamp = Mathf.Clamp(rawYPos, -(yRange + Mathf.Abs(originalY)), (yRange + originalY));

      transform.localPosition = new Vector3(xPosClamp, yPosClamp, transform.localPosition.z);

   }

   void ProcessRotation()
   {
      float pitchPosition = (Mathf.Abs(originalY) + transform.localPosition.y) * -pitchSensitivity;
      float pitchMovement = yThrow * -pitchMovementSensitivity;
      float pitch = pitchPosition + pitchMovement;
      float yaw = transform.localPosition.x * yawSensitivity;
      float roll = xThrow * -rollSensitivity;

      transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
   }

     void OnPlayerDeath() //called by CollisionHandler
    {

        isControlEnabled = false;
    }

    private void ProcessHit()
    {
        if (CrossPlatformInputManager.GetButton("Fire")) 
        {

            SetGunsActive(true);

        } else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            if (isActive)
            {
                gun.GetComponent<ParticleSystem>().emissionRate = fireRate;
            }   else
            {
                gun.GetComponent<ParticleSystem>().emissionRate = 0;
            }
        };
    }




    /* void SmoothThrow()
     {


  if (xThrow < xThrowLastFrame - smoothThrowFactor) {
      xThrowSmooth = xThrowLastFrame - smoothThrowFactor;
  } else if (xThrow > xThrowLastFrame + smoothThrowFactor) {
      xThrowSmooth = xThrowLastFrame + smoothThrowFactor;
  } else {
      xThrowSmooth = xThrow;
  }



  xThrowLastFrame = xThrow;
  yThrowLastFrame = yThrow;

     }*/





}
