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

    public GameObject lightParticle;
    public GameObject darkParticle;

    // Use this for initialization
    void Start () {
        loadProgress();
        textObject = GameObject.Find("Score").GetComponent<TextMesh>();
        textObject.text = "Score: " + Player.score;
        animator = gameObject.GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        // Tap once for jump
        if ((Input.touchCount > 0 && state == State.normal && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetKeyDown("space") && state == State.normal)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, jumpHeight, 0);
            state = State.jumping;
        }
        // (, tap twice for double-jump or stamping)
        else if ((Input.touchCount > 0 && state == State.jumping && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetKeyDown("space") && state == State.jumping)
        {
            if (isDark)
            {
                GameObject particle = (GameObject)Instantiate(darkParticle);
                particle.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                GetComponent<Rigidbody>().velocity = new Vector3(0, -jumpHeight, 0);
                state = State.stamping;
                Destroy(particle, 0.5f);
            }
            if (isLight)
            {
                GameObject particle = (GameObject)Instantiate(lightParticle);
                particle.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                GetComponent<Rigidbody>().velocity = new Vector3(0, jumpHeight, 0);
                state = State.flying;
                Destroy(particle, 0.5f);
            }
        }
        // Set animations w.r.t. current state
        if (state != State.normal)
        {
            animator.SetBool("isJumping", true);
        } else
        {
            animator.SetBool("isJumping", false);
        }
    }
    // Load Main Menu if a obstacle is hit
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            if (Vector3.Dot(col.contacts[0].normal, -Vector3.up) > -0.5f && Vector3.Dot(col.contacts[0].normal, -Vector3.up) <= 0)
            {
                saveProgress();
                Application.LoadLevel("MainMenu");
            }
        }
    }
    // Picking up collectibles
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Collectible")
        {
            // Checks whether dark or light energy was collected
            if (coll.GetComponent<Light>().color == Color.red)
            {
                darkEnergy++;
            } else
            {
                lightEnergy++;
            }
            score++;
            textObject.text = "Score: " + Player.score;
            coll.gameObject.GetComponent<Collectibles>().PickUp();
        }
    }
    // Saves the current progress
    public static void saveProgress()
    {
        BinaryFormatter bf = new BinaryFormatter();
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
    // Loads the progress the player made so far
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
