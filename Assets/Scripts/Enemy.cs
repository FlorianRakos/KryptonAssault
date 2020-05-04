using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionFX;
    [SerializeField] GameObject smokeFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 10;

    ScoreBoard scoreBoard;
    


    // Start is called before the first frame update
    void Start()
    {
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddNonTriggerBoxCollider()
    {
     Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    

    private void OnParticleCollision(GameObject other)
    {
        GameObject fxE =  Instantiate(explosionFX, transform.position, Quaternion.identity);
        fxE.transform.parent = parent;
        GameObject fxS = Instantiate(smokeFX, transform.position, Quaternion.identity);
        fxS.transform.parent = parent;

        scoreBoard.ScoreHit(scorePerHit);

        Destroy (gameObject);


    }
}

// O<>O //
