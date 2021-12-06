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

    void Awake() {
        // Set bndCheck to reference the BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();

        // Invoke SpawnGO() once (in 2 seconds, based on default values)
        Invoke( "SpawnGO", 1f/enemySpawnPerSecond );
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
        Vector3 pos = Vector3.zero;              
        float yMin = -bndCheck.camHeight + enemyPadding;
        float yMax =  bndCheck.camHeight - enemyPadding;
        pos.y = Random.Range( yMin, yMax );
        //spawning on either side of screen
        int rand = Random.Range(0,2);
        if (rand == 0){
            pos.x = bndCheck.camWidth + enemyPadding;
        }
        else {
            pos.x = -bndCheck.camWidth - enemyPadding;
        }
        
        go.transform.position = pos;

        // Invoke SpawnGO() again
        Invoke( "SpawnGO", 1f/enemySpawnPerSecond );
    }
}
