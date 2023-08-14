using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

	[SerializeField] GameObject hitEffect;
	Vector3 target;
	void Start ()
	{
		target = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
	}
	
	void Update () {
		transform.position += transform.forward * Time.deltaTime * 100f;
		Destroy(gameObject,0.5f);
	}
	void OnCollisionEnter(Collision other)
	{
		// GameObject hitObject = Instantiate(hitEffect,gameObject.transform.position,gameObject.transform.rotation);
		// Destroy(hitObject,0.8f);
		Destroy(gameObject);
	}
}
