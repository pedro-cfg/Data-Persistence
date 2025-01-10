using System;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] TMP_InputField nameInput;
    public string playerName;
    public BestInfo bestPlayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(!Instance)
        {
            bestPlayer = new BestInfo();
            LoadBestPlayer();
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        playerName = nameInput.text; 
    }

    [Serializable]
    public class BestInfo
    {
        public string playerName = "Player";
        public int score = 0;
    }

    public void SaveBestPlayer(string name, int score)
    {
        bestPlayer.playerName = name;
        bestPlayer.score = score;

        string json = JsonUtility.ToJson(bestPlayer);
        byte[] bytes = Encoding.ASCII.GetBytes(json);

        File.WriteAllBytes(Application.persistentDataPath + "persistence.bin", bytes);
    }

    public void LoadBestPlayer()
    {
        string path = Application.persistentDataPath + "persistence.bin";
        if(File.Exists(path))
        {
            byte[] bytes = File.ReadAllBytes(path);
            string json = Encoding.ASCII.GetString(bytes);

            bestPlayer = JsonUtility.FromJson<BestInfo>(json);
        }
    }
}
