using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.collider.name);
        if(col.collider.tag == "Attack")
        {
            
        }
    }
}
