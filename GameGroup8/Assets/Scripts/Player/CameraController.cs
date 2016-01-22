using UnityEngine;
using System.Collections;

/// <summary>
/// Class to let the camera follow the player
/// </summary>
public class CameraController : MonoBehaviour {
	
	public GameObject player;
	
	private Vector3 offset;
    private bool BaseEnter;

	public static bool shaking;

	/// <summary>
	/// the offset between the player and the camera.
	/// </summary>
	void Start (){
		offset = transform.position - player.transform.position;
	}

	/// <summary>
	/// Translates the camera when the player moves.
	/// </summary>
	void LateUpdate (){
		BaseEnter = BaseController.pause;
        if (!BaseEnter && !shaking){
            transform.position = player.transform.position + offset;
        } else if (BaseEnter){
            transform.position = GameObject.FindGameObjectWithTag("BASE").transform.position + new Vector3(0, 20, -7);
        }
	}
}