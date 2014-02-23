using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			other.GetComponent<PlayerScript>().inWater = true;
			other.GetComponent<PlayerScript>().MaxSpeed -=1;
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			other.GetComponent<PlayerScript>().inWater = false;
			other.GetComponent<PlayerScript>().MaxSpeed +=1;
		}
	}
}
