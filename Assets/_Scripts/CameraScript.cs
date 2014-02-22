using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform 	poi; // Point of Interest
	public Vector3		offset = new Vector3(0,0,-10);
	
	void Update () {
		Vector3 poiV3 = poi.position;
		poiV3 += offset;
		transform.position = poiV3;
	}
}
