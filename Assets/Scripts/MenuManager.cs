using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager menuManager;

    public int highScore;
    public string highScoreName;
    public string currentPlayerName;
    public TextMeshProUGUI BestScoreText;
    public Text placeholderText;
    public Text NameText;

    private void Awake()
    {
        if (menuManager != null)
        {
            Destroy(gameObject);
            return;
        }
        menuManager = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();

    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string highScoreName;
        public string currentPlayerName;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.highScoreName = highScoreName;
        data.currentPlayerName = currentPlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Saving " + Application.persistentDataPath + "/savefile.json");
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            //Debug.Log("Loading " + Application.persistentDataPath + "/savefile.json");

            highScore = data.highScore;
            highScoreName = data.highScoreName;
            currentPlayerName = data.currentPlayerName;
            BestScoreText.text = "Best Score : " + highScoreName + " : " + highScore;
            placeholderText.text = currentPlayerName;
            placeholderText.color = Color.black;
            NameText.text = currentPlayerName;
            //Debug.Log("Current player name = " + currentPlayerName + " NameText.text = " + NameText.text);
            Debug.Log("loading " + Application.persistentDataPath + "/savefile.json");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        MenuManager.menuManager.SaveHighScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    //public TextMeshProUGUI playerName;

    public void GetName()
    {
        currentPlayerName = NameText.text;
    }

}
