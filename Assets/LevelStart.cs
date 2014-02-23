using UnityEngine;
using System.Collections;

public class LevelStart : MonoBehaviour {

	private static bool set = false;

	// Use this for initialization
	void Start () {
		if (!set){
			GameObject.Find("Player").GetComponent<PlayerScript>().Popup("You are a slave on the run.\nYou must get north to freedom.",-1);
			set = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
