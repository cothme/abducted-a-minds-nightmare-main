using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using System.IO;
using TMPro;

public class AsyncManager : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject mainMenu;
    [SerializeField] Slider loadingSlider;
    [SerializeField] TextMeshProUGUI loadingText;
    [SerializeField] LoadingScreenImages images;
    [SerializeField] Image loadingImage;

    public void NewGame(string levelToLoad)
    {
        Time.timeScale = 1;
        PlayerData.Instance.IsSessionSaved = false;
        mainMenu.SetActive(false);
        loadingImage.sprite = images.loadingScreenImages[Random.Range(1,images.loadingScreenImages.Length)];
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync(levelToLoad)); 
    }
    public void ContinueGame(string levelToLoad)
    {
        Time.timeScale = 1;
        XmlSerializer loadData = new XmlSerializer(typeof(DataMembers));
        StreamReader sr = new StreamReader("Abducted Save File");
        DataMembers dm = (DataMembers)loadData.Deserialize(sr);  
        PlayerData.Instance.IsSessionSaved = dm.isSessionSaved;
        PlayerData.Instance.PlayerPosition = dm.position;
        PlayerData.Instance.PlayerRotation = dm.rotation;
        ItemList.Instance.Itemlist.Clear();
        loadingImage.sprite = images.loadingScreenImages[Random.Range(1,images.loadingScreenImages.Length)];
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("Level 1");
    }
    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        loadOperation.allowSceneActivation = false;
        loadingText.text = "Loading...";
        while(!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            if (progressValue >= 0.9f)
            {
                loadingText.text = "Press space to continue";
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    loadOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
    public void goToMainMenu(string levelToLoad)
    {
            Time.timeScale = 1;
            PlayerData.Instance.Stage = 0;
            PlayerData.Instance.IsSessionSaved = false;
            PlayerData.Instance.PlayerHealth = 50;
            PlayerData.Instance.PlayerOxygen= 100;
            GunManager.Instance.WeaponEquipped = null;
            GunManager.Instance.BulletsLoaded = 0;
            GunManager.Instance.TotalBullets = 0;
            GunManager.Instance.CanEquipRifle = false;
            GunManager.Instance.CanEquipShotgun = false;
            GunManager.Instance.CanEquipPistol = false;
            GunManager.Instance.CanEquipKnife = false;
            PlayerState.Instance.Aiming = false;
            PlayerState.Instance.Reloading = false;
            PlayerState.Instance.Running = false;
            PlayerState.Instance.IsPuzzleOneSolved = false;
            PlayerState.Instance.LevelOneDoorUnlocked = false;
            PlayerState.Instance.LevelOneCageUnlocked = false;
            loadingImage.sprite = images.loadingScreenImages[Random.Range(1,images.loadingScreenImages.Length)];
            loadingScreen.SetActive(true);
            StartCoroutine(LoadLevelAsync(levelToLoad));
    }
    public void LoadLevelTwo(string levelToLoad)
    {
        PlayerData.Instance.IsSessionSaved = true;
        PlayerData.Instance.PlayerPosition = this.gameObject.transform.position;
        PlayerData.Instance.PlayerRotation = this.gameObject.transform.rotation;
        DataMembers dm = new DataMembers();
        dm.level = PlayerData.Instance.Stage;
        dm.isSessionSaved = PlayerData.Instance.IsSessionSaved;
        dm.health = PlayerData.Instance.PlayerHealth;
        dm.oxygen = PlayerData.Instance.PlayerOxygen;
        dm.weaponEquipped = GunManager.Instance.WeaponEquipped;
        dm.itemList = ItemList.Instance.Itemlist;
        dm.isPuzzleOneSolved = PlayerState.Instance.IsPuzzleOneSolved;
        dm.levelOneDoorUnlocked = PlayerState.Instance.LevelOneDoorUnlocked;
        dm.levelOneCageUnlocked = PlayerState.Instance.LevelOneCageUnlocked;
        XmlSerializer saveData = new XmlSerializer(typeof(DataMembers));
        StreamWriter sw = new StreamWriter("Abducted Save File");
        saveData.Serialize(sw,dm);
        sw.Close();
        loadingImage.sprite = images.loadingScreenImages[Random.Range(1,images.loadingScreenImages.Length)];
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync(levelToLoad));
    }
    public void QuitClicked()
    {
        Application.Quit();
    }
    public void Level1Intro()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level 1 Intro");
    }
}
