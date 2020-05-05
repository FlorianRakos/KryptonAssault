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
    [SerializeField] int hits = 4;
    //[SerializeField] enum TypeOfEnemy {hunter, bomber };

    ScoreBoard scoreBoard;
    


    // Start is called before the first frame update
    void Start()
    {
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddNonTriggerBoxCollider()
    {
     BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
        
        boxCollider.size = new Vector3(7, 3, 6);

    }

    

    private void OnParticleCollision(GameObject other)
    {
        scoreBoard.ScoreHit(scorePerHit);
        hits--;
        
        //todo consider hit FX
        if (hits <= 0)
        {
            KillEnemy();
        }
            

    }

    private void KillEnemy()
    {
        GameObject fxE = Instantiate(explosionFX, transform.position, Quaternion.identity);
        fxE.transform.parent = parent;
        GameObject fxS = Instantiate(smokeFX, transform.position, Quaternion.identity);
        fxS.transform.parent = parent;
        
        Destroy(gameObject);
    }
}

// O<>O //
