using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    private float moving;

    private bool aiming;
    private bool reloading;
    private bool paused = false;
    private float moving;
    public bool Aiming { get => aiming; set => aiming = value; }
    public bool Reloading { get => reloading; set => reloading = value; }
    public bool Paused { get => paused; set => paused = value; }
    public float Moving { get => moving; set => moving = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
