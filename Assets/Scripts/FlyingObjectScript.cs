using UnityEngine;
using System.Collections;

public class FlyingObjectScript : MonoBehaviour {

    public float movementDelta = 5.5f; //max distance delta from original position
    public bool moveHorizontalAxis = false;
    public bool moveVerticalAxis = false;
    public float moveSpeed = 50;

    private int currentDirectionSign = 1;
    private float movedDistance = 0; // can't be more than delta
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //host collider for player triggering
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.gameObject.transform.parent = this.gameObject.transform;
        }
    }
 
     void OnTriggerExit2D(Collider2D other) {
         if (other.tag == "Player")
         {
             other.gameObject.transform.parent = null;
         }
    }


    void FixedUpdate()
    {
 
       
        {
            float moveStep = movementDelta / 100 * currentDirectionSign * moveSpeed;
            if (moveHorizontalAxis)
            {
               transform.position += new Vector3(moveStep, 0.0f, 0.0f);         
            }
            if (moveVerticalAxis)
            {
                transform.position += new Vector3(0.0f, moveStep, 0.0f);
            }
            movedDistance += moveStep;

            if (Mathf.Abs(movedDistance / movementDelta) > 1.0f)
            {
                currentDirectionSign = -(int)Mathf.Sign(movedDistance / movementDelta);
            }
        }

    }
}
