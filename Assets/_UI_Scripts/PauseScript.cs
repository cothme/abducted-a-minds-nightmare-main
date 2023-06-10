using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    void Update()
    {
        if(ControlsManager.Instance.IsPauseButtonDown)
        {
            PlayerState.Instance.Paused = !PlayerState.Instance.Paused;
            gameObject.GetComponent<Canvas>().enabled = true;
        }
        if(PlayerState.Instance.Paused)
        {
            Time.timeScale = 0;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerInteractScript>().enabled = false;
            player.GetComponent<PlayerShootingScript>().enabled = false;
            player.GetComponent<PlayerAnimation>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            GameObject.Find("Main Camera").GetComponent<CinemachineBrain>().enabled = false;
        }
    }
    public void Resume()
    {
        PlayerState.Instance.Paused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.GetComponent<Canvas>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerInteractScript>().enabled = true;
        player.GetComponent<PlayerShootingScript>().enabled = true;
        player.GetComponent<PlayerAnimation>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<CinemachineBrain>().enabled = true;
    }
}
