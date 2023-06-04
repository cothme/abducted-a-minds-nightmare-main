using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float closeDelay = 2.0f;
    public Vector3 closedPosition;
    public float closeSpeed = 2.0f;

    private Vector3 openPosition;
    private bool isOpen = true; 

    private void Start()
    {
        openPosition = transform.position;
        StartCoroutine(CloseDoor());
    }
    private IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(closeDelay);

        while (transform.position != closedPosition)
        {
            // Move the door towards the closed position
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, closeSpeed * Time.deltaTime);

            yield return null;
        }
        isOpen = false;  // Set the flag to indicate that the door is closed
    }
}
