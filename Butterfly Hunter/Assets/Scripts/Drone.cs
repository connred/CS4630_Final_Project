using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
   [Header("Set in Inspector: Enemy")]
    float speed = 10f;

    private BoundsCheck bndCheck;

    void Start() {                                                           
        bndCheck = GetComponent<BoundsCheck>();
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
        Move();

        if (bndCheck != null && (bndCheck.offLeft || bndCheck.offRight || bndCheck.offUp)) {                    
            Destroy( gameObject );
        }
    }

    public void Move() {                                             
        Vector3 tempPos = pos;
        tempPos.y += speed * Time.deltaTime;
        pos = tempPos;
    }
}
