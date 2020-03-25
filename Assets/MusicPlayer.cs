using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("loadFirstScene", 3f);
        
    }


void loadFirstScene() {
     SceneManager.LoadScene(1);

}

void Awake () {
   DontDestroyOnLoad(this.gameObject);
}

}
