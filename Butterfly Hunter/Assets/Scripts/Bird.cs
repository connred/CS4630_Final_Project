using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    float speed = 5f;

    private BoundsCheck bndCheck;
    float startPos;

    void Start() {                                                           
        bndCheck = GetComponent<BoundsCheck>();
        startPos = GameObject.Find("Main Camera").GetComponent<Main>().pos.x;
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
        if (startPos < 0){
            MoveRight();
        }
        else {
            MoveLeft();
        }

        if (bndCheck != null && (bndCheck.offLeft || bndCheck.offRight)) {                    
            Destroy( gameObject );
        }
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
