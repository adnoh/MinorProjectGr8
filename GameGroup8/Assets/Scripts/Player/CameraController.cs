using UnityEngine;
using System.Collections;

/// <summary>
/// Class to let the camera follow the player
/// </summary>
public class CameraController : MonoBehaviour {
	
	public GameObject player;
	
	private Vector3 offset;
    private Vector3 tempLocation;
    private Vector3 transformCamera;
    private bool BaseEnter;
    private bool moving;
    private bool saveLocation;
    private int whatMove;

    private int movingSpeed = 15;

	public static bool shaking;

	/// <summary>
	/// the offset between the player and the camera.
	/// </summary>
	void Start (){
		offset = transform.position - player.transform.position;
        moving = false;
        saveLocation = false;
        whatMove = 0;
    }

	/// <summary>
	/// Translates the camera when the player moves.
	/// </summary>
	void LateUpdate (){
		BaseEnter = BaseController.pause;
		switch (whatMove) {
		case 1:
			moveCameraToBase ();
			break;
		case 2:
			moveCameraToPlayer ();
			break;
		default:
			if (!BaseEnter && !shaking) {
				transform.position = player.transform.position + offset;
			} else if (BaseEnter) {
				transform.position = GameObject.FindGameObjectWithTag ("BASE").transform.position + new Vector3 (0, 20, -6);
			}
			break;
		}
	}

    /// <summary>
    /// Move camera from the player position to the base;
    /// </summary>
    void moveCameraToBase(){
        if(saveLocation == false){
            saveLocation = true;
            tempLocation = gameObject.transform.position;
            transformCamera = ((GameObject.FindGameObjectWithTag("BASE").transform.position + new Vector3(0, 20, -6)) - tempLocation)/movingSpeed;
        }

		if (moving == true) {
			gameObject.transform.position += transformCamera;
		}
        if (Vector3.Distance(gameObject.transform.position,GameObject.FindGameObjectWithTag("BASE").transform.position + new Vector3(0, 20, -6)) < 0.1){
            moving = false;
            saveLocation = false;
            whatMove = 0;
        }
    }

    /// <summary>
    /// move the camera from the base to the player
    /// </summary>
    void moveCameraToPlayer(){
        if (saveLocation == false){
            saveLocation = true;
            transformCamera = (tempLocation - (GameObject.FindGameObjectWithTag("BASE").transform.position + new Vector3(0, 20, -6)))/movingSpeed;
        }

		if (moving == true) {
			gameObject.transform.position += transformCamera;
		}
        if (Vector3.Distance(gameObject.transform.position, tempLocation) < 0.1){
            moving = false;
            saveLocation = false;
            whatMove = 0;
        }
    }

    /// <summary>
    /// say how the camera should move (1 to base, 2 from base)
    /// </summary>
    /// <param name="i"></param>
    public void MoveCamera(int i){
        if (moving == false){
            moving = true;
            whatMove = i;
        }
    }
}