using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip ("in s")] [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] GameObject explosionFX;
    [SerializeField] GameObject smokeFX;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    void StartDeathSequence ()
    {
        SendMessage("OnPlayerDeath");
        explosionFX.SetActive (true);
        smokeFX.SetActive(true);
        Invoke("loadSplash", levelLoadDelay);
        
    }

    void loadSplash () // string referenced
    {
    SceneManager.LoadScene(1);
    }
}
