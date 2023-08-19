using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelFourScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Transform[] enemySpawnPoints;
    [SerializeField] GameObject[] enemyToSpawn;
    [SerializeField] Canvas timerCanvas;
    [SerializeField] GameObject blockDoor;
    [SerializeField] GameObject levelFourBossDoor;
    bool levelInitiate = false;
    bool canSpawnEnemy = false;
    float timeRemaining = 40f;
    bool timerEnd = false;
    private void Start()
    {
        InvokeRepeating("SpawnEnemy",0f,5f);
    }
    private void Update()
    {
        if(PlayerState.Instance.IsDead)
        {
            timerCanvas.enabled = false;
            levelInitiate = false;
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            canSpawnEnemy = false;
            blockDoor.SetActive(false);
            timeRemaining = 40f;
        }
        if(timerEnd == true)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            blockDoor.SetActive(false);
            levelFourBossDoor.tag = "Door";
            timerCanvas.enabled = false;
        }
        if(levelInitiate == true)
        {
            RunTimer();
        }
    }
    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void RunTimer()
    {
        DisplayTime(timeRemaining);
        if (timeRemaining > 0)
        {
            canSpawnEnemy = true;
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            canSpawnEnemy = false;
            timeRemaining = 0;
            timerEnd = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            timerCanvas.enabled = true;
            levelInitiate = true;
            blockDoor.SetActive(true);
        }
    }
    void SpawnEnemy()
    {
        if(canSpawnEnemy == true)
        {
            int randomSpawnEnemy = Random.Range(0,3);
            int randomSpawnLocation = Random.Range(0,3);
            Instantiate(enemyToSpawn[randomSpawnEnemy],enemySpawnPoints[randomSpawnLocation]);
        } 
    }
    IEnumerator SpawnEnemyCoroutine()
    {
        if(canSpawnEnemy)
        {
            int randomSpawnEnemy = Random.Range(0,2);
            int randomSpawnLocation = Random.Range(0,3);
            Instantiate(enemyToSpawn[randomSpawnEnemy],enemySpawnPoints[randomSpawnLocation]);
            canSpawnEnemy = false;
        }
        yield return new WaitForSeconds(3f);
        canSpawnEnemy = true;
    }
}
