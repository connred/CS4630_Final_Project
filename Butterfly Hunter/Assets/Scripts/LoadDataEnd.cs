using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using System;

public class LoadDataEnd : MonoBehaviour
{
    public GameObject endUI;
    // Start is called before the first frame update
    void Start()
    {
        endUI = GameObject.Find("GameInfoText");
        LoadGame();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
                       + "/Log.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                       File.Open(Application.persistentDataPath
                       + "/Log.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            endUI.GetComponent<Text>().text = "Player: " + data.playerID + "\n Score: " + data.score + "\n Level: " + data.level + "\n Day playing: " + data.lastPlayed;
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
}
