using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    void StartDeathSequence ()
    {
        print("Player Dying");
        SendMessage("OnPlayerDeath");
    }
}
