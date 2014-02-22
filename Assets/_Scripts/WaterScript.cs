using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			other.GetComponent<PlayerScript>().inWater = true;
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			other.GetComponent<PlayerScript>().inWater = false;
		}
	}
}
