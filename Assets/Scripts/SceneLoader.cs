﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    void Start()
    {
        Invoke("loadFirstScene", 30f);
    }
    void loadFirstScene()
    {
        SceneManager.LoadScene(1);
    }

}
