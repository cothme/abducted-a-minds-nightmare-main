using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;
    private float movingX;
    private float movingZ;

    private bool aiming;
    private bool reloading;
    private bool running;

    private bool paused = false;
    private bool isPuzzleOneSolved = false;
    public bool Aiming { get => aiming; set => aiming = value; }
    public bool Reloading { get => reloading; set => reloading = value; }
    public bool Running { get => running; set => running = value; }
    public bool Paused { get => paused; set => paused = value; }
    public float MovingX { get => movingX; set => movingX = value; }
    public float MovingZ { get => movingZ; set => movingZ = value; }
    public bool IsPuzzleOneSolved { get => isPuzzleOneSolved; set => isPuzzleOneSolved = value; }

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
