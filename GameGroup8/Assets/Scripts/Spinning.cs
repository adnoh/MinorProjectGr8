using UnityEngine;
using System.Collections;

public class Spinning : MonoBehaviour {

	public float speed = 10f;
    public Vector3 turn = new Vector3(0,1,0);

	/// <summary>
	/// Spins the object is attached to in a certain speed.
	/// </summary>
	void Update () {
        transform.Rotate(turn, speed * Time.deltaTime);
	}
}
