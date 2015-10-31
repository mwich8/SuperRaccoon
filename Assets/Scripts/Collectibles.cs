using UnityEngine;
using System.Collections;

public class Collectibles : MonoBehaviour {

    private float oneDirection = 1f;

    private float amountMovingY = 1.5f;
    public bool wasCollected = false;

    private Vector3 raccoonGUIPosition;
    
    private Shader shader;

    private bool isMovingUpwards = false;

    // Use this for initialization
    IEnumerator Start () {
        yield return new WaitForSeconds(0.001f);
        raccoonGUIPosition = new Vector3((MainSceneLayout.actualScreenWidth / 2) - 2, MainSceneLayout.screenHeight - 2, -2);
    }
	
	// Update is called once per frame
	void Update () {
        if (!wasCollected)
        {
            transform.Translate(MainSceneLayout.actualSpeed, 0, 0, Space.World);
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
        if (transform.position.x <= -(MainSceneLayout.actualScreenWidth / 2) - gameObject.transform.localScale.y || transform.position.y >= 11f)
        {
            Destroy(gameObject);
        }
        if ((transform.position.x >= (MainSceneLayout.actualScreenWidth / 2) - 2 || transform.position.y >= MainSceneLayout.screenHeight - 2) && wasCollected)
        {
            Destroy(gameObject);
        }
    }

    public void PickUp()
    {
        wasCollected = true;
    }
}
