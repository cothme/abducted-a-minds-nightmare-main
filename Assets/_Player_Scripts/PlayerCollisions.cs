using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
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
    }
}
