using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{    
    public Slider slider;

    public void SetEnergy (float energy)
    {
        slider.value = energy;
    }
    
    public void SetColorRed (bool setRed)
    {
        if (setRed)
        {
            gameObject.GetComponentInChildren<Image>().color = new Color32(170, 70, 60, 255);
            Color color = gameObject.GetComponentInChildren<Image>().color;
            print(gameObject.GetComponentInChildren<Image>().color);
        }
        else
        {
            gameObject.GetComponentInChildren<Image>().color = new Color32(40, 190, 200, 255);
        }
    }

  
}
