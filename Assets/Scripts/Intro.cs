using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

    public static float screenHeight;
    public static float screenRatioXtoY;
    public static float actualScreenWidth;

    public Light pointLight;
    public TextMesh introText;
    public GameObject SRLogo;

    private bool alphaRises;
    private bool isGoingRight;

    private float slowTextShowingTime = 1.25f;
    private float textShowingTime = 0.3f;
    private float logoShowingTime = 2f;
    private string[] introStory = new string[7];
    private Color sRLogoColor;

    private int currentText = 0;

    // Use this for initialization
    void Start () {
        // Scales the background
        screenHeight = Camera.main.orthographicSize;
        screenRatioXtoY = (float)Screen.width / (float)Screen.height;
        actualScreenWidth = (screenRatioXtoY * 2f * screenHeight);
        transform.localScale = new Vector3((screenHeight * screenRatioXtoY) / 5, 1, screenHeight / 5);
        // Sets the start values
        introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, 0);
        introText.transform.position = new Vector3(0,2,-5);
        alphaRises = true;
        pointLight.transform.position = new Vector3(-actualScreenWidth / 3, -1, -3);
        isGoingRight = true;
        // Sets up the story
        introStory[0] = "A Dumwi2 Production...";
        introStory[1] = "A long time ago...";
        introStory[2] = "There";
        introStory[3] = "was";
        introStory[4] = "an";
        introStory[5] = "awesome";
        introStory[6] = "Raccoon";
        // Makes the logo transparent at the beginning
        sRLogoColor = SRLogo.GetComponent<Renderer>().material.color;
        SRLogo.GetComponent<Renderer>().material.color = new Color(sRLogoColor.r, sRLogoColor.g, sRLogoColor.b, 0);
    }
	
	// Update is called once per frame
	void Update () {
        // Skip Intro
        if ((Input.touchCount > 0  && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetKeyDown("space"))
        {
            Application.LoadLevel("MainMenu");
        }
        // Intro
        // Moves light
        if (pointLight.transform.position.x < actualScreenWidth / 3 && isGoingRight)
        {
            pointLight.transform.position = new Vector3(pointLight.transform.position.x + slowTextShowingTime / 6, pointLight.transform.position.y, pointLight.transform.position.z);
        } else if (pointLight.transform.position.x >= actualScreenWidth / 3)
        {
            isGoingRight = false;
            pointLight.transform.position = new Vector3(pointLight.transform.position.x - slowTextShowingTime/6, pointLight.transform.position.y, pointLight.transform.position.z);
        } else if (pointLight.transform.position.x > -actualScreenWidth / 3 && !isGoingRight)
        {
            pointLight.transform.position = new Vector3(pointLight.transform.position.x - slowTextShowingTime/6, pointLight.transform.position.y, pointLight.transform.position.z);
        } else if (pointLight.transform.position.x < actualScreenWidth / 3 && !isGoingRight)
        {
            pointLight.transform.position = pointLight.transform.position;
        } 
            // Display slow text
            if (currentText < 2)
        {
            if (introText.color.a < 1 && alphaRises)
            {
                introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, introText.color.a + slowTextShowingTime/100);
            }
            else if (introText.color.a == 0)
            {
                alphaRises = true;
                currentText++;
                introText.text = introStory[currentText];
            }
            else
            {
                alphaRises = false;
                introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, introText.color.a - slowTextShowingTime/100);
            }
        // Display fast texts
        } else
        {
            introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, 1);
            if (textShowingTime > 0)
            {
                textShowingTime -= Time.deltaTime;
            } else
            {
                // Display texts
                if (currentText < introStory.Length - 1)
                {
                    currentText++;
                    introText.text = introStory[currentText];
                    textShowingTime = 0.3f;
                // Displays logo after texts
                } else
                {
                    introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, 0);
                    if (SRLogo.GetComponent<Renderer>().material.color.a < 1 && alphaRises)
                    {
                        SRLogo.GetComponent<Renderer>().material.color = new Color(sRLogoColor.r, sRLogoColor.g, sRLogoColor.b, SRLogo.GetComponent<Renderer>().material.color.a + slowTextShowingTime / 100);
                    }
                    else
                    {
                        if (logoShowingTime > 0)
                        {
                            logoShowingTime -= Time.deltaTime;
                        } else
                        {
                            Application.LoadLevel("MainMenu");
                        }
                    }
                }
            }
        }
	}
}
