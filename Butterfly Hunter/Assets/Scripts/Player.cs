using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //----------Movement----------//
        //Get current screen position of mouse pointer
        Vector3 mousePos2D = Input.mousePosition;
        //Camera's z pos sets how far to push mouse in 3D
        mousePos2D.z = -Camera.main.transform.position.z;
        //Convert the point from 2D screen space into 3D space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        //Move the x pos of Basket to x pos of mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        pos.y = mousePos3D.y;
        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll){
        GameObject collideWith = coll.gameObject;

        //Catching butterflies
        if (collideWith.tag == "Butterfly"){
            Destroy(collideWith);
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
    }
}
