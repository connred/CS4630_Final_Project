using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 pos;
    public int count;
    public bool checkToStart;

    private BoundsCheck bndCheck;

    // Start is called before the first frame update
    void Start()
    {
        bndCheck = GetComponent<BoundsCheck>();
        pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    //----------Movement----------//
        Move();

    //--------Bounds Check--------//
        if (bndCheck != null && (bndCheck.offLeft || bndCheck.offRight || bndCheck.offDown || bndCheck.offUp)) {
            this.transform.position = pos;
        }
    }

    void Move()
    {
        //Get current screen position of mouse pointer
        Vector3 mousePos2D = Input.mousePosition;
        //Camera's z pos sets how far to push mouse in 3D
        mousePos2D.z = -Camera.main.transform.position.z;
        //Convert the point from 2D screen space into 3D space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        pos.x = mousePos3D.x;
        pos.y = mousePos3D.y;
        pos.z = 0;
        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll){
        GameObject collideWith = coll.gameObject;

        //Catching butterflies
        if (collideWith.tag == "Butterfly"){
            Destroy(collideWith);
            count++;
            print("Got butterfly");
            //----Add scoring system----//
        }

        //Colliding with birds
        if (collideWith.tag == "Bird"){
            Destroy(collideWith);
            print("Got bird");
            //----Add scoring system----//
        }

        //Colliding with drone
        if (collideWith.tag == "Drone"){
            Destroy(collideWith);
            print("Got drone");
            //----Add scoring system----//
        }

        if (collideWith.tag == "Whirlwind"){
            print("Hit Whirlwind");
            checkToStart = true;
        }
    }
}
