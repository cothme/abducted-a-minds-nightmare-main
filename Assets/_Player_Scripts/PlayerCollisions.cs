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
    [SerializeField] Slider oxygenSlider;
    [SerializeField] GameObject oxygenIcon;
    [SerializeField] Canvas deathCanvas;
    [SerializeField] public Canvas mainCanvas;
    Coroutine damageCoroutine;
    private bool isDamaging = false;
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
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Fog")
        {
            if (!isDamaging)
            {
                damageCoroutine = StartCoroutine(StartDamageCoroutine());
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Fog")
        {
            if(isDamaging)
            {
                StopCoroutine(damageCoroutine);
                isDamaging = false;
            }
        }
    }
    private void Update()
    {
        oxygenSlider.value = PlayerData.Instance.PlayerOxygen;
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
        if(gameObject.GetComponent<PlayerShootingScript>().mask.activeInHierarchy)
        {
            oxygenSlider.gameObject.SetActive(true);
            oxygenIcon.SetActive(true);
            oxygenSlider.value = PlayerData.Instance.PlayerOxygen;
        }
        else
        {
            oxygenSlider.enabled = false;
            oxygenIcon.SetActive(false);
            oxygenSlider.value = PlayerData.Instance.PlayerOxygen;
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
        if(PlayerData.Instance.Stage == 1)
        {
            this.gameObject.transform.position = new Vector3(-286f,13.2200003f,-16.3999996f);
            this.gameObject.transform.rotation = new Quaternion(0,-0.74098742f,0,0.671518922f);
        }
        else if(PlayerData.Instance.Stage == 2)
        {
            this.gameObject.transform.position = new Vector3(74.0699997f,61.7700005f,-1026.83997f);
            this.gameObject.transform.rotation = new Quaternion(0,0.707106829f,0f,0.707106829f);
        }
        Cursor.lockState = CursorLockMode.Locked;
        deathCanvas.enabled = false;
        mainCanvas.enabled = true;
    }
    private void ApplyDamageToPlayer()
    {
        if(gameObject.GetComponent<PlayerShootingScript>().mask.activeInHierarchy)
        {
            if(PlayerData.Instance.PlayerOxygen >= 0)
            {
                ReduceOxygen();
            }
            else
            {
                ReduceHealth();
            }
        }
        else
        {
            ReduceHealth();
        }
    }
    private IEnumerator StartDamageCoroutine()
    {
        isDamaging = true;
        while (true)
        {
            ApplyDamageToPlayer();
            yield return new WaitForSeconds(2);
        }
    }
    void ReduceHealth()
    {
        PlayerData.Instance.PlayerHealth -= 5f;
        healthSlider.value = PlayerData.Instance.PlayerHealth;
    }
    void ReduceOxygen()
    {
        PlayerData.Instance.PlayerOxygen -= 5f;
        oxygenSlider.value = PlayerData.Instance.PlayerOxygen;
    }
}
