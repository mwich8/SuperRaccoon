using UnityEngine;
using System.Collections;

public class Collectibles : MonoBehaviour {

    private float oneDirection = 1f;

    private float amountMovingY = 1.5f;
    public bool wasCollected = false;

    private Vector3 raccoonGUIPosition;
    
    private Shader shader;

    private bool isMovingUpwards = false;

    private float screenHeight, screenRatioXtoY, actualScreenWidth;

    // Use this for initialization
    IEnumerator Start () {
        yield return new WaitForSeconds(0.001f);
        // CARE!!! Hardcoded values here, copy&pasted from MainSceneLayout
        screenHeight = Camera.main.orthographicSize;
        screenRatioXtoY = (float)Screen.width / (float)Screen.height;
        actualScreenWidth = (screenRatioXtoY * 2f * screenHeight);
        raccoonGUIPosition = new Vector3((actualScreenWidth / 2) - 2, screenHeight - 2, -2);
    }
	
	// Update is called once per frame
	void Update () {
        if (!wasCollected)
        {
            transform.Translate(-0.08f - (Mathf.Sqrt(Player.score)) / 100, 0, 0, Space.World);
        } else
        {
            transform.position = Vector3.Lerp(gameObject.transform.position, raccoonGUIPosition, 0.075f);
        }
        if (isMovingUpwards)
        {
            transform.Translate(Vector3.up/200);
        } else
        {
            transform.Translate(Vector3.down/200);
        }
        if (oneDirection > 0)
        {
            oneDirection -= Time.deltaTime;
        } else
        {
            oneDirection = amountMovingY;
            isMovingUpwards = !isMovingUpwards;
        }
        if (transform.position.x <= -15.5f || transform.position.y >= 11f)
        {
            Destroy(gameObject);
        }
        if ((transform.position.x >= (actualScreenWidth / 2) - 2 || transform.position.y >= screenHeight - 2) && wasCollected)
        {
            Destroy(gameObject);
        }
    }

    public void PickUp()
    {
        wasCollected = true;
    }
}
