﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Class to shake the camera when the player is hit
/// </summary>
public class CameraShaker : MonoBehaviour {

	public Transform camTransform;
	public static float shake = 0f;
	public float shakeAmount = 0.1f;
	public float decreaseFactor = 1.0f;
	Vector3 originalPos;

    /// <summary>
    /// Get all necessary components
    /// </summary>
	void Awake(){
		if(camTransform == null){
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

    /// <summary>
    /// Shake the camera when hit
    /// </summary>
	void Update(){
		originalPos = camTransform.localPosition;
		if (shake > 0 && !GameObject.Find("player").GetComponent<PlayerController>().death) {
			CameraController.shaking = true;
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			shake -= Time.deltaTime * decreaseFactor;
		} else {
			shake = 0;
			camTransform.localPosition = originalPos;
			CameraController.shaking = false;
		}
	}

	public static void shakeCamera(){
		shake = 0.5f;
	}
}
