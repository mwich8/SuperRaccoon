using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Player : MonoBehaviour {

    public enum State
    {
        normal,
        jumping
    }

    private Animator animator;

    public static State state;

    private TextMesh textObject;

    private float jumpHeight = 16f;

    public static int score = 0;

    // Use this for initialization
    void Start () {
        textObject = GameObject.Find("Score").GetComponent<TextMesh>();
        animator = gameObject.GetComponentInChildren<Animator>();
        laodProgress();
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
        if (col.gameObject.tag == "Obstacle")
        {
            // print(Vector3.Dot(col.contacts[0].normal, -Vector3.up));
            if (Vector3.Dot(col.contacts[0].normal, -Vector3.up) > -0.5f && Vector3.Dot(col.contacts[0].normal, -Vector3.up) <= 0)
            {
                saveProgress();
                Application.LoadLevel("mainScene");
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Collectible")
        {
            score++;
            textObject.text = "Score: " + Player.score;
            Destroy(coll.gameObject);
        }
    }

    void saveProgress()
    {
        print("saving");
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/progress.sg"); //you can call it anything you want
        bf.Serialize(file, score);
        file.Close();
    }

    void laodProgress()
    {
        if (File.Exists(Application.persistentDataPath + "/progress.sg"))
        {
            print("loading");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/progress.sg", FileMode.Open);
            score = (int)bf.Deserialize(file);
            textObject.text = "Score: " + Player.score;
            file.Close();
        }
    }
}
