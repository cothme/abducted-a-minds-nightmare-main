using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    Canvas optionsCanvas;
    // Start is called before the first frame update
    void Start()
    {
        optionsCanvas = GetComponent<Canvas>();
        optionsCanvas.GetComponentInChildren<Slider>();
        optionsCanvas.GetComponentInChildren<Slider>().value = 1;
    }

    void Update()
    {
        PlayerData.Instance.Volume = optionsCanvas.GetComponentInChildren<Slider>().value;
        AudioListener.volume =  PlayerData.Instance.Volume;
    }
}
