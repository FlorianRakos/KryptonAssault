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

    [SerializeField] GameObject[] guns;
    [SerializeField] int fireRate = 8;
    [SerializeField] float energyCharge = 100;
    [SerializeField] float energyPerShot = 20;
    [SerializeField] float energyRestored = 20;
    bool coolOffMode = false;

    AudioSource audioSource;
    [SerializeField] AudioClip laserAudio;
    AudioSource laserCannon;

    public EnergyBar energyBar;


    float originalY;
    float xThrow, yThrow;
    bool isControlEnabled = true;
   


   void Start()
   {
      originalY = transform.localPosition.y;      
   }

   void Update()
   {
        if (isControlEnabled == true)
        {
      ProcessMovement();
      ProcessRotation();
      ProcessFire();
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

     void OnPlayerDeath()
    {
        isControlEnabled = false;
    }

    private void ProcessFire()
    {
        if (CrossPlatformInputManager.GetButton("Fire") && energyCharge >= 0 && coolOffMode == false) 
        {
            energyCharge = energyCharge - (energyPerShot * Time.deltaTime);
            SetGunsActive(true);            
        } else
        {
            if (energyCharge <= 0) {
                coolOffMode = true;
                energyBar.SetColorRed(true);
            }

            if (energyCharge < 100)
            {
                energyCharge = energyCharge + (energyRestored * Time.deltaTime);
            }

            if (energyCharge >= 20)
            {
                coolOffMode = false;
                energyBar.SetColorRed(false);
            }

            SetGunsActive(false);
        }
        energyBar.SetEnergy(energyCharge);
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            gameObject.GetComponent<AudioSource>();
            if (isActive)
            {
                gun.GetComponent<ParticleSystem>().emissionRate = fireRate;
                if (!gameObject.GetComponent<AudioSource>().isPlaying)
                { gameObject.GetComponent<AudioSource>().Play(); }
            }   else
            {
                gun.GetComponent<ParticleSystem>().emissionRate = 0;
                gameObject.GetComponent<AudioSource>().Stop();
            }
        }
    }
}
