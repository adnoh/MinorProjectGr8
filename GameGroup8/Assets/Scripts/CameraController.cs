using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public GameObject player;
	
	private Vector3 offset;
    private bool BaseEnter;
	
	void Start (){
		offset = transform.position - player.transform.position;
	}
	
	void LateUpdate (){
		BaseEnter = BaseController.pause;

        if (!BaseEnter){
            transform.position = player.transform.position + offset;
        } else if (BaseEnter){
            transform.position = new Vector3(0, 20, -9);
        }
	}
}