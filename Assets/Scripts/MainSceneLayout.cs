using UnityEngine;
using System.Collections;

// This script is used to transform and scale objects in the mainScene/ the actual game
public class MainSceneLayout : MonoBehaviour {

    public GameObject raccoonGUI, player, score, background, sky1, sky2, platform1, platform2;

    public static float screenHeight;
    public static float screenRatioXtoY;
    public static float actualScreenWidth;

    public static float actualSpeed;

    public AudioClip darkSound;
    public AudioClip lightSound;
    public AudioClip defaultSound;

	// Use this for initialization
	void Start () {
        // Calculates the size of the current screen
        screenHeight = Camera.main.orthographicSize;
        screenRatioXtoY = (float)Screen.width / (float)Screen.height;
        actualScreenWidth = (screenRatioXtoY * 2f * screenHeight);
        Player.loadProgress();
        actualSpeed = -0.05f - ((Mathf.Sqrt(Player.score)) / 100);
        // Determines which lightSource should be played
        if(Player.isDark)
        {
            GetComponent<AudioSource>().PlayOneShot(darkSound);
        } else if (Player.isLight)
        {
            GetComponent<AudioSource>().PlayOneShot(lightSound);
        } else
        {
            GetComponent<AudioSource>().PlayOneShot(defaultSound);
        }
        // Platform scaling and positioning
        platform1.transform.localScale = new Vector3(actualScreenWidth, screenHeight / 4.5f, screenHeight / 3);
        platform1.transform.position = new Vector3(0, -screenHeight + platform1.transform.localScale.y / 2, 0);
        platform2.transform.localScale = new Vector3(actualScreenWidth, screenHeight / 4.5f, screenHeight / 3);
        platform2.transform.position = new Vector3(actualScreenWidth, -screenHeight + platform1.transform.localScale.y / 2, 0);
        // Background scaling
        background.transform.localScale = new Vector3((screenHeight * screenRatioXtoY) / 5, 1, screenHeight / 5);
        // Sky scaling and positioning
        sky1.transform.localScale = new Vector3((screenHeight * screenRatioXtoY) / 5, 1, screenHeight / 5);
        sky1.transform.position = new Vector3(0, 0, 100);
        sky2.transform.localScale = new Vector3((screenHeight * screenRatioXtoY) / 5, 1, screenHeight / 5);
        sky2.transform.position = new Vector3(actualScreenWidth, 0, 100);
        // Raccoon positioning
        player.transform.position = new Vector3(-actualScreenWidth/2 + 2, -7, 0);
        // Score positioning
        score.transform.position = new Vector3(-1, screenHeight - 1, -2);
        // RaccoonGUI positioning
        raccoonGUI.transform.position = new Vector3((actualScreenWidth / 2) - 2, screenHeight - 2, -2);
    }
	
	// Update is called once per frame
	void Update () {
        // Sky movement
        sky1.transform.Translate(-0.05f - (Mathf.Sqrt(Player.score)) / 100, 0, 0, Space.World);
        if (sky1.transform.position.x <= -actualScreenWidth)
        {
            sky1.transform.position = new Vector3(sky1.transform.position.x + (actualScreenWidth * 2), 0, 100);
        }
        sky2.transform.Translate(-0.05f - (Mathf.Sqrt(Player.score)) / 100, 0, 0, Space.World);
        if (sky2.transform.position.x <= -actualScreenWidth)
        {
            sky2.transform.position = new Vector3(sky2.transform.position.x + (actualScreenWidth * 2), 0, 100);
        }
        // Platform movement
        platform1.transform.Translate(-0.05f - (Mathf.Sqrt(Player.score)) / 100, 0, 0, Space.World);
        if (platform1.transform.position.x <= -actualScreenWidth)
        {
            platform1.transform.position = new Vector3(platform1.transform.position.x + (actualScreenWidth * 2), platform1.transform.position.y, 0);
        }
        platform2.transform.Translate(-0.05f - (Mathf.Sqrt(Player.score)) / 100, 0, 0, Space.World);
        if (platform2.transform.position.x <= -actualScreenWidth)
        {
            platform2.transform.position = new Vector3(platform2.transform.position.x + (actualScreenWidth * 2), platform2.transform.position.y, 0);
        }
    }
}
