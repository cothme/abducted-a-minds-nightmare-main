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
        PlayerData.Instance.IsSessionSaved = false;
        mainMenu.SetActive(false);
        loadingImage.sprite = images.loadingScreenImages[Random.Range(1,images.loadingScreenImages.Length)];
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync(levelToLoad)); 
    }
    public void ContinueGame(string levelToLoad)
    {
        XmlSerializer loadData = new XmlSerializer(typeof(DataMembers));
        StreamReader sr = new StreamReader("Abducted Save File");
        DataMembers dm = (DataMembers)loadData.Deserialize(sr);  
        PlayerData.Instance.IsSessionSaved = dm.isSessionSaved;
        ItemList.Instance.Itemlist.Clear();
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync(levelToLoad));
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
        loadingImage.sprite = images.loadingScreenImages[Random.Range(1,images.loadingScreenImages.Length)];
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync(levelToLoad));
    }
    void QuitClicked()
    {
        Application.Quit();
    }
}
