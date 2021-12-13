using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private BoundsCheck bndCheck;
    Vector3 startPos;
    float birthTime;

    void Start() {                                                           
        bndCheck = GetComponent<BoundsCheck>();
        startPos = GameObject.Find("Main Camera").GetComponent<Main>().pos;
        birthTime = Time.time;
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

    public void Move()
    {
        // Bézier curves work based on a u value between 0 & 1
        float u = (Time.time - birthTime) / 10;

        // If u>1, then it has been longer than lifeTime since birthTime
        if (u > 1) {
            // This Enemy_2 has finished its life
            Destroy( this.gameObject );                                      // d
            return;
        }

        // Adjust u by adding a U Curve based on a Sine wave
        u = u + 0.6f * (Mathf.Sin(u*Mathf.PI*2));

        // Interpolate the two linear interpolation points
        pos = (1-u) * startPos + u * -startPos;
    }
}
