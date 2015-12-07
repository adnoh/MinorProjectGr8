using UnityEngine;
using System.Collections;

public class SimplePlayerController : MonoBehaviour {


    private static Vector3 currentPosition;
    private Vector3 playerPos;
    public GameObject player;
    public int speed;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Vertical") * Time.deltaTime * -1;
        float moveVertical = Input.GetAxis("Horizontal") * Time.deltaTime;

        transform.Translate(speed * moveHorizontal, 0.0f, speed * moveVertical, Space.World);

        Vector3 playerPos = player.transform.position;
        setPosition(playerPos);
    }

    void setPosition(Vector3 here)
    {
        currentPosition = here;
    }

    public static Vector3 getPosition()
    {
        return currentPosition;
    }
}
