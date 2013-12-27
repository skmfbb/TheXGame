using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float speed = 2.0f;
    public float moveForce = 5.0f;			// Amount of force added to move the player left and right.
    public float maxSpeed = 5.0f;
    public float jumpForce = 2.0f;
    public float reachRadius = 0.3f;

    private Collider2D activeObject;
    private Color normalColor;
    private int currentActiveIndex = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        bool playerMoved = (horizontalMovement != 0 || verticalMovement != 0);
        rigidbody2D.AddForce(Vector2.right * horizontalMovement * moveForce);
        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

        if (Input.GetButton("Jump"))
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));

        if (Input.GetButton("Fire1"))
        {
            currentActiveIndex++;
            SelectActive();
        }

        if (playerMoved)
        {
            if (!CheckIfActiveStillInRange())
                ReselectActive();
        }

        
	}

    bool CheckIfActiveStillInRange()
    {
        Collider2D[] usableObjects = Physics2D.OverlapCircleAll(transform.position, reachRadius);
        foreach (Collider2D obj in usableObjects)
            if (obj.Equals(activeObject))
                return true;
        return false;
    }
    void ReselectActive()
    {
        currentActiveIndex = 0;
        SelectActive();
    }

    void SelectActive()
    {
        
        if (activeObject != null)
        {
            SpriteRenderer sr = activeObject.GetComponent<SpriteRenderer>();
            sr.material.color = normalColor;
        }

        // Find all the colliders on the Enemies layer within the bombRadius.
        Collider2D[] usableObjects = Physics2D.OverlapCircleAll(transform.position, reachRadius);
        if (currentActiveIndex >= usableObjects.Length)
            currentActiveIndex = 0;

        Debug.Log("currentIndex" + currentActiveIndex);
        Debug.Log("Length" + usableObjects.Length);

        if (usableObjects.Length != 0)
        {
            activeObject = usableObjects[0];
            SpriteRenderer sr = activeObject.GetComponent<SpriteRenderer>();
            if (sr != null && sr.tag == "Usable")
            {
                normalColor = sr.material.color;
                sr.material.color = Color.yellow;
            }
        }

        // For each collider...
        //foreach (Collider2D uObject in usableObjects)
        //{
        //    // Check if it has a rigidbody (since there is only one per enemy, on the parent).
        //    SpriteRenderer sr = uObject.GetComponent<SpriteRenderer>();
        //    if (sr != null && sr.tag == "Usable")
        //        sr.material.color = Color.yellow;
        //    //Rigidbody2D rb = uObject.rigidbody2D;
        //    //if (rb != null && rb.tag == "Usable")
        //    //{
        //    //    rb.gameObject.GetComponent<MoveScript>().RevertIsKinematic();
        //    //}
        //}
    }

}
