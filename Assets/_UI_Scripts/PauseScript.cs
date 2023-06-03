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
            CanvasManager.Instance.PauseCanvas.enabled = true;
        }
        if(PlayerState.Instance.Paused)
        {
            Time.timeScale = 0;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerInteractScript>().enabled = false;
            CanvasManager.Instance.MainCanvas.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameObject.Find("Main Camera").GetComponent<CinemachineBrain>().enabled = false;
        }
    }
    public void Resume()
    {
        PlayerState.Instance.Paused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CanvasManager.Instance.MainCanvas.enabled = true;
        CanvasManager.Instance.PauseCanvas.enabled = false;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerInteractScript>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<CinemachineBrain>().enabled = true;
    }
}
