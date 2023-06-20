using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

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
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "AudioCue")
        {
            
        }
    }
    private void Update()
    {
        healthText.text = PlayerData.Instance.PlayerHealth.ToString();
        healthSlider.maxValue = PlayerData.Instance.PlayerMaxHealth;
        healthSlider.value = PlayerData.Instance.PlayerHealth;
        if(PlayerData.Instance.PlayerHealth <= 0 && PlayerState.Instance.IsDead == false)
        {
            Cursor.lockState = CursorLockMode.None;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<PlayerShootingScript>().enabled = false;
            gameObject.GetComponent<PlayerAnimation>().enabled = false;
            gameObject.GetComponent<PlayerInventory>().enabled = false;
            Camera.main.GetComponent<CinemachineBrain>().enabled = false;
            mainCanvas.enabled = false;
            deathCanvas.enabled = true;   
            PlayerState.Instance.IsDead = true;
        }
    }
    public void RespawnClicked()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        gameObject.GetComponent<PlayerShootingScript>().enabled = true;
        gameObject.GetComponent<PlayerAnimation>().enabled = true;
        gameObject.GetComponent<PlayerInventory>().enabled = true;
        Camera.main.GetComponent<CinemachineBrain>().enabled = true;
        PlayerData.Instance.PlayerHealth = PlayerData.Instance.PlayerMaxHealth;
        PlayerState.Instance.IsDead = false;
        XmlSerializer loadData = new XmlSerializer(typeof(DataMembers));
        StreamReader sr = new StreamReader("Abducted Save File");
        DataMembers dm = (DataMembers)loadData.Deserialize(sr);
        this.gameObject.transform.position = dm.position;
        this.gameObject.transform.rotation = dm.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        deathCanvas.enabled = false;
        mainCanvas.enabled = true;
    }
}
