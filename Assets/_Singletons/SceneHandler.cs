using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void GoToLevelOneIntro()
    {
        SceneManager.LoadScene("Level 1 Intro");
    }
    public void GoToLevelOne()
    {
        SceneManager.LoadScene("Level 1");
    }
}
