using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] GameObject bulletDecal;
    float speed = 200f;
    public Vector3 target { get; set; }
    public bool hit { get; set; }
    // void Start()
    // {
    //     Destroy(gameObject,timeToDestroy);
    // }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,target,speed * Time.deltaTime);
        if(hit && Vector3.Distance(transform.position,target) < .01f)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        // ContactPoint contactPoint = col.GetContact(0);
        // GameObject.Instantiate(bulletDecal,contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
        Destroy(this.gameObject);
    }
}
