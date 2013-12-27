using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {



	// Use this for initialization
	void Start () {
        rigidbody2D.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
            rigidbody2D.isKinematic = false; 
    }

    public void RevertIsKinematic()
    {
        rigidbody2D.isKinematic = !rigidbody2D.isKinematic;
    }

}
