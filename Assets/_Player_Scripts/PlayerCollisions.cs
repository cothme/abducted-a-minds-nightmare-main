using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] 
    private void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Attack")
        {
            PlayerData.Instance.PlayerHealth -= 3f;
        }
    }
}
