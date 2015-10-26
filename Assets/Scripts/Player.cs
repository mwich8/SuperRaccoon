using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public enum State
    {
        normal,
        jumping
    }

    private Animator animator;

    public State state;

    private float jumpHeight = 13.5f;

    public static int score = 0;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -7.4f)
        {
            transform.position = new Vector3(transform.position.x, -7.4f, transform.position.z);
        }
        if (Input.GetKey("space") && state == State.normal)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, jumpHeight, 0);
        }
        if (state == State.jumping)
        {
            animator.SetBool("isJumping", true);
        } else
        {
            animator.SetBool("isJumping", false);
        }
        // Tap once for jump(, tap twice for double-jump)
        /*
        if (Input.touchCount > 0 && Input.GetTouch(0).position.x < Screen.width/4 && state == State.normal && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            transform.position = transform.position + Vector3.up * jumpHeight;
        }
        */
        // Slide downwards to dive or get grounded again
        /*
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && state == State.jumping)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // Move player down
            if (touchDeltaPosition.y < 0)
            {
                transform.Translate(0, touchDeltaPosition.y * 0.1f, 0);
            }
        }
        */
        /*
        if (Input.GetTouch(1).position.x < Screen.width/4)
        {
            print("zweiter finger: " + Input.GetTouch(1).position);
        }
        */
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Platform")
        {
            state = State.normal;
        }
        if (col.gameObject.tag == "Obstacle")
        {
            state = State.normal;
            //Destroy(col.gameObject);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Platform" || col.gameObject.tag == "Obstacle")
        {
            state = State.jumping;
        }
    }
}
