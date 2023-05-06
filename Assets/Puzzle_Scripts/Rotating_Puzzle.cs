using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Rotating_Puzzle : MonoBehaviour
{
    public Button move3,move4;
    int pointedNumber;
    int nodeID,limit;
    public Transform arrow;
    [SerializeField] List<Transform> images;
    public Color defaultColor;
    public Color pointedColor;
    public float rotationSpeed; 
    public float rotationDelay;
    public List <int> winningImages;
    List <int> coloredImages;
    public GameObject gameWonPanel;
    private void Start()
    {
        coloredImages = new List<int>{};
        pointedNumber = 1;
        limit = images.Count;
        foreach(Transform image in images)
        {
            image.GetComponent<Image>().color = defaultColor;
        }
    }
    IEnumerator RotateArrow(float targetAngle)
    {
        // wait for the delay before starting to rotate the arrow
        yield return new WaitForSeconds(rotationDelay);

        // calculate the difference between the current angle and the target angle
        float currentAngle = arrow.rotation.eulerAngles.z;
        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

        // rotate the arrow gradually over time
        while (Mathf.Abs(angleDifference) > 0.01f)
        {
            float rotationAmount = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
            arrow.rotation = Quaternion.Euler(0f, 0f, rotationAmount);
            currentAngle = arrow.rotation.eulerAngles.z;
            angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);
            yield return null;
        }
        Transform pointedImage = images[pointedNumber - 1];
        Image pointedImageColor = pointedImage.GetComponent<Image>();
        if(pointedImageColor.color == defaultColor)
        {
            pointedImageColor.color = pointedColor;
            coloredImages.Add(pointedNumber);
            coloredImages.Sort();
        }
        else
        {
            pointedImageColor.color = defaultColor;
            coloredImages.Remove(pointedNumber);
        }
        if(CheckWinCondition())
        {
            gameWonPanel.SetActive(true);
            Debug.Log("Win");
        }
        move3.interactable = true;
        move4.interactable = true;
    }

    public void Move_Three()
    {
        move3.interactable = false;
        move4.interactable = false;
        pointedNumber += 3;
        if(pointedNumber > limit)
        {
            pointedNumber = pointedNumber % limit;
            if (pointedNumber == 0) {
                pointedNumber = limit;
            }
        }
        Vector3 direction = images[pointedNumber - 1].position - arrow.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        StartCoroutine(RotateArrow(targetAngle));
    }

    public void Move_Four()
    {
        move3.interactable = false;
        move4.interactable = false;
        pointedNumber += 4;
        if(pointedNumber > limit)
        {
            pointedNumber = pointedNumber % limit;
            if (pointedNumber == 0) {
                pointedNumber = limit;
            }
        }
        Vector3 direction = images[pointedNumber - 1].position - arrow.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        StartCoroutine(RotateArrow(targetAngle));
    }
    private bool CheckWinCondition()
    {
        return Enumerable.SequenceEqual(winningImages,coloredImages);
    }
}