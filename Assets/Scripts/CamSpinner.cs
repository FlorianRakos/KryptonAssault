using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CamSpinner : MonoBehaviour
{
    [SerializeField] float spinningSpeed = 1000;
    float yStart = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spinCam();
    }

    private void spinCam()
    {
        
        

        float yRotation = transform.rotation.eulerAngles.y;
        print(yRotation);
        float yOffset = yRotation + (spinningSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(0, yOffset, 0);
    }
}
