using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float speed = 2.0f;
    public float moveForce = 5.0f;			// Amount of force added to move the player left and right.
    public float maxSpeed = 5.0f;
    
    //jumping staff
    public float jumpForce = 2.0f;
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    
    public float reachRadius = 0.3f;

    private Collider2D activeObject;
    private Color normalColor;
    private int currentActiveIndex = 0;
	// Use this for initialization
	void Start () {
	
	}

    //physics staff here
    void FixedUpdate() { 
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        bool playerMoved = (horizontalMovement != 0 || verticalMovement != 0);
       
       // rigidbody2D.AddForce(Vector2.right * horizontalMovement * moveForce);
       
     //   if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
    //        rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

        rigidbody2D.velocity = new Vector2(horizontalMovement * maxSpeed, rigidbody2D.velocity.y);// isn't it enough to use simple horizontal speed and add force only in jumping cases?

        //if (playerMoved)
        //{
        //    if (!CheckIfActiveStillInRange())
        //        ReselectActive();
        //}
    }
    
	// button events here fir better accuracy
	void Update () {

        if (Input.GetButton("Jump") && grounded)
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));

        if (Input.GetButton("Fire1"))
        {
            currentActiveIndex++;
            SelectActive();
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
