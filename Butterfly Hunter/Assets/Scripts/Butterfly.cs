using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    float speed = 10f;

    float startPos;
    private BoundsCheck bndCheck;

    void Awake() {                                                           
        bndCheck = GetComponent<BoundsCheck>();
        startPos = transform.position.x;
    }

    public Vector3 pos {                                                     
        get {
            return( this.transform.position );
        }
        set {
            this.transform.position = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Need Butterfly movement//
        if (startPos < 0){
            MoveRight();
        }
        else {
            MoveLeft();
        }

        // if (bndCheck != null && bndCheck.offLeft) {                    
        //     Destroy( gameObject );
        // }

        //Note: Collision with butterfly is handled in player script//
    }

    public void MoveRight() {                                             
        Vector3 tempPos = pos;
        tempPos.x += speed * Time.deltaTime;
        pos = tempPos;
    }

    public void MoveLeft() {                                             
        Vector3 tempPos = pos;
        tempPos.x -= speed * Time.deltaTime;
        pos = tempPos;
    }
}
