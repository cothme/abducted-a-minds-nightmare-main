using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Slider healthSlider;
    [SerializeField] Canvas deathCanvas;
    [SerializeField] Canvas mainCanvas;
    private void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Attack")
        {
            PlayerData.Instance.PlayerHealth -= 3f;
            int hitNumber = Random.Range(1,4);
            if(hitNumber == 1)
            {
               gameObject.GetComponent<Animator>().Play("Hit 1");
            }
            else if(hitNumber == 2)
            {
                gameObject.GetComponent<Animator>().Play("Hit 2");
            }
            else if(hitNumber == 3)
            {
                gameObject.GetComponent<Animator>().Play("Hit 3");
            }
        }
    }
    
    private void Update()
    {
        healthText.text = PlayerData.Instance.PlayerHealth.ToString();
        healthSlider.value = PlayerData.Instance.PlayerHealth;
        if(PlayerData.Instance.PlayerHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<PlayerShootingScript>().enabled = false;
            gameObject.GetComponent<PlayerAnimation>().enabled = false;
            gameObject.GetComponent<PlayerInventory>().enabled = false;
            Camera.main.GetComponent<CinemachineBrain>().enabled = false;
            mainCanvas.enabled = false;
            deathCanvas.enabled = true;   
        }
    }
}
