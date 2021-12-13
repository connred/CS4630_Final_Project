using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Vector3      p0;
    public Vector3      p1;
    public float        timeDuration = 1000f;
    // Click the checkToStart checkbox to start moving

    [Header("Set Dynamically")]
    public Vector3      p01;
    public bool         moving = false;
    public float        timeStart;

    private BoundsCheck bndCheck;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        bndCheck = GetComponent<BoundsCheck>();
        player = GameObject.Find("Player");
        p0 = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        p1 = Input.mousePosition;
        if (player.GetComponent<Player>().checkToStart) {
            player.GetComponent<Player>().checkToStart = false;

            moving = true;
            timeStart = Time.time;
        }

        if (moving) {
            float u = (Time.time-timeStart)/timeDuration;
            if (u >= 1) {
                u = 1;
                moving = false;
            }

            // This is the standard linear interpolation function
            p01 = (1 - u) * p0 + u * new Vector3(p0.x, p0.y, 4f);

            player.transform.position = p01;
            print(p01);
        }
    }
}
