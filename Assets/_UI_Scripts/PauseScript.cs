using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] AudioSource settingsOpen;
    [SerializeField] GameObject player;
    bool paused = false;
    void Update()
    {
        if(ControlsManager.Instance.IsPauseButtonDown)
        {
            settingsOpen.Play();
            paused = !paused;
            gameObject.GetComponent<Canvas>().enabled = true;
        }
        if(paused)
        {
            Time.timeScale = 0;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerInteractScript>().enabled = false;
            player.GetComponent<PlayerShootingScript>().enabled = false;
            player.GetComponent<PlayerAnimation>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            GameObject.Find("Main Camera").GetComponent<CinemachineBrain>().enabled = false;
        }
        else
        {
            // Time.timeScale = 1;
            // Cursor.lockState = CursorLockMode.Locked;
            // gameObject.GetComponent<Canvas>().enabled = false;
            // player.GetComponent<PlayerMovement>().enabled = true;
            // player.GetComponent<PlayerInteractScript>().enabled = true;
            // player.GetComponent<PlayerShootingScript>().enabled = true;
            // player.GetComponent<PlayerAnimation>().enabled = true;
            // GameObject.Find("Main Camera").GetComponent<CinemachineBrain>().enabled = true;
            //Time.timeScale = 1;
            //Cursor.lockState = CursorLockMode.Locked;
            //gameObject.GetComponent<Canvas>().enabled = false;
            //player.GetComponent<PlayerMovement>().enabled = true;
            //player.GetComponent<PlayerInteractScript>().enabled = true;
            //player.GetComponent<PlayerShootingScript>().enabled = true;
            //player.GetComponent<PlayerAnimation>().enabled = true;
            //GameObject.Find("Main Camera").GetComponent<CinemachineBrain>().enabled = true;
        }
    }
    public void Resume()
    {
        paused = false;
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
