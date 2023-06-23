using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroTransition : MonoBehaviour
{
    public float wait_time = 10f;

    void Start()
    {
        StartCoroutine(Wait_for_Intro());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.Escape)) || (Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    IEnumerator Wait_for_Intro()
    {
        yield return new WaitForSeconds(wait_time);

        SceneManager.LoadScene("Main Menu");
    }
}
