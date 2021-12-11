using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject[]      prefabEnemies;              // Array of Enemy prefabs
    public float             enemySpawnPerSecond = 0.5f; // # Enemies/second
    public float             enemyDefaultPadding = 1.5f; // Padding for position

    private BoundsCheck      bndCheck;
    public GameObject whirlwind;
    public Player player;

    public Vector3 pos;

    //int numButterflies = 0;

    void Start() {
        // Set bndCheck to reference the BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();
        player = GameObject.Find("Player").GetComponent<Player>();

        // Invoke SpawnGO() once (in 2 seconds, based on default values)
        Invoke( "SpawnGO", 1f/enemySpawnPerSecond );

        SpawnWhirlwind();
    }

    void Update()
    {
        //count between 0-3
        // numButterflies = player.count % 4;
        // print(numButterflies);
        // if (numButterflies == Random.Range(1,4)){
        //     SpawnWhirlwind();
        //     numButterflies = 0;
        // }
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
        whirlwindPos.z = -1f;

        go.transform.position = whirlwindPos;

        //Invoke("SpawnWhirlwind", 5f);
    }
}
