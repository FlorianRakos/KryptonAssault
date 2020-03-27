using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionFX;
    [SerializeField] GameObject smokeFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {

        //explosionFX.SetActive(true);
        //smokeFX.SetActive(true);
        print(gameObject);
        Destroy (gameObject);


    }
}

// O<>O //
