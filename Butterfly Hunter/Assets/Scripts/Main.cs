using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using Random = UnityEngine.Random;

[Serializable]
class SaveData
{
    public int playerID;
    public int score;
    public string feedback;
    public DateTime lastPlayed;
}

public class Main : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject[]      prefabEnemies;              // Array of Enemy prefabs
    public float             enemySpawnPerSecond = 0.5f; // # Enemies/second
    public float             enemyDefaultPadding = 1.5f; // Padding for position
    public float winCondiiton = 150f;
    public GameObject timeUI;
    public GameObject playerUI;
    public GameObject lvlHighschool;
    public int playerID;
    public int level = 1;
    //public GameObject scoreUI;

    private BoundsCheck      bndCheck;
    public GameObject whirlwind;
    public Player player;

    public Vector3 pos;
    public bool spawned = false;

    public float time = 0;
    int numButterflies = 0;
    public float        timeStart;
    public float        timeDuration = 10f;

    void addPlayer(int score, int playerNum, string feedback)
    {
        //path
        string path = Application.dataPath + "/Log.txt";

        if (File.Exists(path))
        {
            File.WriteAllText(path, "Player Log \n\n");
        }
        //create

        string content =
            "Player ID: " + playerNum + "\n" +
            "Score " + score + "\n" +
            "Date Played: " + System.DateTime.Now + "\n" +
            "Player Feedback: " + feedback + "\n\n";
        File.AppendAllText(path, content);


        //text
    }
    void Start() {
        // Set bndCheck to reference the BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();
        player = GameObject.Find("Player").GetComponent<Player>();
        

        // Load player prefs and create player ID if not in prefs
        LoadGame();
        if (playerID <= 1000)
        {
            playerID = Random.Range(1000, 9999);
        }
        // Invoke SpawnGO() once (in 2 seconds, based on default values)
        Invoke( "SpawnGO", 1f/enemySpawnPerSecond );
    }

    void Update()
    {

        //Time update
        if ( player.score >= 0)
        {
            time += Time.deltaTime;
            timeUI.GetComponent<Text>().text = "Time: " + time;
        }
          
        if (player.score < 0)
        {
            string feedback = showFeedback();
            addPlayer(player.score, playerID, feedback);
            SaveGame(player.score, playerID, feedback);
            //end game scene load
            SceneManager.LoadScene("GameOver");
        }
        if (player.score >= winCondiiton)
        {
            if (level == 1)
            {
                string feedback = showFeedback();
                addPlayer(player.score, playerID, feedback);
                SaveGame(player.score, playerID, feedback);
                SceneManager.LoadScene("Level2");
            }
            if (level == 2)
            {
                string feedback = showFeedback();
                addPlayer(player.score, playerID, feedback);
                SaveGame(player.score, playerID, feedback);
                SceneManager.LoadScene("Level3");
            }
            if (level == 3)
            {
                string feedback = showFeedback();
                addPlayer(player.score, playerID, feedback);
                SaveGame(player.score, playerID, feedback);
                SceneManager.LoadScene("EndGame");
            }
            
        }
        //count between 0-3
        numButterflies = player.count % 4;
        if (numButterflies == Random.Range(1,5) && !spawned){
            SpawnWhirlwind();
            spawned = true;
            timeStart = Time.time;
        }
        if (spawned){
            float u = (Time.time-timeStart)/timeDuration;
            if (u >= 0.01f) {
                u = 0.01f;
                spawned = false;
                Destroy(GameObject.Find("Whirlwind(Clone)"));
            }
        }
    }

    public void SpawnGO() {
        // Pick a random gameobject prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>( prefabEnemies[ ndx ] );

        //Set padding for object bounds check
        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>() != null) {
            enemyPadding = Mathf.Abs( go.GetComponent<BoundsCheck>().radius );
        }

        // Set the initial position for the go
        if (go.tag == "Butterfly" || go.tag == "Bird"){
            pos = Vector3.zero;              
            float yMin = -bndCheck.camHeight + enemyPadding;
            float yMax =  bndCheck.camHeight - enemyPadding;
            pos.y = Random.Range( yMin, yMax );
            //spawning on either side of screen
            int rand = Random.Range(0,2);
            if (rand == 0){
                pos.x = bndCheck.camWidth - enemyPadding;
            }
            else {
                pos.x = -bndCheck.camWidth + enemyPadding;
            }
            
            go.transform.position = pos;
        }

        if (go.tag == "Drone"){
            pos = Vector3.zero;              
            float xMin = -bndCheck.camWidth + enemyPadding;
            float xMax =  bndCheck.camWidth - enemyPadding;
            pos.x = Random.Range( xMin, xMax );
            pos.y = -bndCheck.camHeight - enemyPadding;
            
            go.transform.position = pos;
        }

        // Invoke SpawnGO() again
        Invoke( "SpawnGO", 1f/enemySpawnPerSecond );
    }

    public void SpawnWhirlwind()
    {
        GameObject go = Instantiate<GameObject>(whirlwind);
        Vector3 whirlwindPos = Vector3.zero;
        float yMin = -bndCheck.camHeight;
        float yMax =  bndCheck.camHeight;
        float xMin = -bndCheck.camWidth;
        float xMax =  bndCheck.camWidth;
        whirlwindPos.y = Random.Range( yMin, yMax );
        whirlwindPos.x = Random.Range(xMin, xMax);
        //whirlwindPos.z = 1f;

        go.transform.position = whirlwindPos;

        //Invoke("SpawnWhirlwind", 5f);
    }
    string showFeedback()
    {
        return null;
    }
    void SaveGame(int score, int playerNum, string feedback)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Log.dat");
        SaveData data = new SaveData();
        data.playerID = playerNum;
        data.score = score;
        data.feedback = feedback;
        data.lastPlayed = DateTime.Now;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved");
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
            lvlHighschool.GetComponent<Text>().text = "Highscore: " + data.score;
            playerUI.GetComponent<Text>().text = "Player: " + (data.playerID);
            playerID = data.playerID;
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
}
