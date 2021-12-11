using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    Transform player;

    private BoundsCheck      bndCheck;

    // Start is called before the first frame update
    void Start()
    {
        bndCheck = GetComponent<BoundsCheck>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //Get current screen position of mouse pointer
        Vector3 mousePos2D = Input.mousePosition;
        //Camera's z pos sets how far to push mouse in 3D
        mousePos2D.z = -Camera.main.transform.position.z;
        //Convert the point from 2D screen space into 3D space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        if (player.position.y < 0 && !bndCheck.offDown){
            pos.y = player.position.y + 13;
        }
        else {
            pos.y = 20;
        }
        this.transform.position = pos;
    }
}
