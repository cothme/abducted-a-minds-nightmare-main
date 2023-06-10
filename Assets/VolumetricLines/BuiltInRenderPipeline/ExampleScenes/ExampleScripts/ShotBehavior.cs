using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

	[SerializeField] GameObject hitEffect;
	void Start () {
	
	}
	
	void Update () {
		transform.position += transform.forward * Time.deltaTime * 100f;
		Destroy(gameObject,3f);
	}
	void OnCollisionEnter(Collision other)
	{
		GameObject hitObject = Instantiate(hitEffect,gameObject.transform.position,gameObject.transform.rotation);
		Destroy(hitObject,0.8f);
		Destroy(gameObject);
	}
}
