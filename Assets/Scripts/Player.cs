using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Player : MonoBehaviour {

    public enum State
    {
        normal,
        jumping,
        flying,
        stamping
    }

    public GameObject raccoonGUI;

    private Animator animator;

    public static State state;

    private TextMesh textObject;

    private float jumpHeight = 16f;

    public static int score = 0;

    public static int darkEnergy = 0;
    public static int lightEnergy = 0;

    public static bool isDark = false;
    public static bool isLight = false;

    // Use this for initialization
    void Start () {
        loadProgress();
        textObject = GameObject.Find("Score").GetComponent<TextMesh>();
        textObject.text = "Score: " + Player.score;
        animator = gameObject.GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -7.4f)
        {
            transform.position = new Vector3(transform.position.x, -7.4f, transform.position.z);
        }
        if ((Input.touchCount > 0 && state == State.normal && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetKeyDown("space"))
        {
            print("Single");
            GetComponent<Rigidbody>().velocity = new Vector3(0, jumpHeight, 0);
        }
        if ((Input.touchCount > 0 && state == State.jumping && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetKeyDown("space"))
        {
            print("Double");
            if (isDark)
            {
                print("Dark");
                GetComponent<Rigidbody>().velocity = new Vector3(0, -jumpHeight, 0);
                state = State.stamping;
            }
            if (isLight)
            {
                print("Light");
                GetComponent<Rigidbody>().velocity = new Vector3(0, jumpHeight, 0);
                state = State.flying;
            }
        }

        if (state != State.normal)
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
            if (Vector3.Dot(col.contacts[0].normal, -Vector3.up) > -0.5f && Vector3.Dot(col.contacts[0].normal, -Vector3.up) <= 0)
            {
                saveProgress();
                Application.LoadLevel("MainScene");
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Collectible")
        {
            // Checks whether dark or light energy was collected
            if (coll.GetComponent<Light>().color == Color.red)
            {
                darkEnergy++;
                // print("Dark Energy counter: " + darkEnergy);
            } else
            {
                lightEnergy++;
                // print("Light Energy counter: " + lightEnergy);
            }
            score++;
            textObject.text = "Score: " + Player.score;
            coll.gameObject.GetComponent<Collectibles>().PickUp();
        }
    }

    public void updateRaccoonGUI()
    {
        // Renderer rend = Player.raccoonGUI.GetComponent<Renderer>();
        // rend.material.SetColor("_Color", Color.red);
        print("RACCOON!");
    }

    public static void saveProgress()
    {
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/progress.sg");
        int[] scores = new int[4];
        scores[0] = score;
        scores[1] = darkEnergy;
        scores[2] = lightEnergy;
        if(isDark)
        {
            scores[3] = -1;
        } else if (isLight)
        {
            scores[3] = 1;
        } else
        {
            scores[3] = 0;
        }
        bf.Serialize(file, scores);
        file.Close();
    }

    public static void loadProgress()
    {
        if (File.Exists(Application.persistentDataPath + "/progress.sg"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/progress.sg", FileMode.Open);
            int[] scores = (int[])bf.Deserialize(file);
            score = scores[0];
            darkEnergy = scores[1];
            lightEnergy = scores[2];
            if (scores[3] == -1)
            {
                isDark = true;
                isLight = false;
            }
            else if (scores[3] == 1)
            {
                isDark = false;
                isLight = true;
            }
            else
            {
                isDark = false;
                isLight = false;
            }
            file.Close();
        }
    }
}
