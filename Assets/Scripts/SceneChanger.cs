using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public Button myButton;
    // Start is called before the first frame update
    void Start()
    {
      Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(startGame);
    }

    public void startGame ()
    {
        SceneManager.LoadScene(1);
    }

    

        
}
  