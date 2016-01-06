using UnityEngine;
using System.Collections;

public class ObjectFollower : MonoBehaviour {

    public GameObject trackObject;
    public Vector3 Offset;

    void Update(){
        gameObject.transform.position = Camera.main.WorldToScreenPoint(trackObject.transform.position) + Offset;
    }
}
