using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;




public class Player : MonoBehaviour
{

   [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 75f;
   [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 75f;

   [Tooltip("In m")] [SerializeField] float xRange = 10f;
   [Tooltip("In m")] [SerializeField] float yRange = 10f;

   [SerializeField] float yawSensitivity = 1f;
   [SerializeField] float pitchSensitivity = 1f;
   [SerializeField] float pitchMovementSensitivity = 20f;
   [SerializeField] float rollSensitivity = 15f;
   [SerializeField] float smoothThrowFactor = 0.2f;



   float originalY;
   float xThrow, yThrow;
   float xThrowLastFrame = 0;
   float yThrowLastFrame = 0;
   float xThrowSmooth;
   float yThrowSmooth;
   


   void Start()
   {
      originalY = transform.localPosition.y;

   }


   void Update()
   {
      SmoothThrow(); 
      ProcessMovement();
      ProcessRotation();
      
      print(xThrowLastFrame);
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

   void SmoothThrow()
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

   }
   
}
