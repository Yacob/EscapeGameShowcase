using UnityEngine;
using System.Collections;

public class Level2Start : MonoBehaviour {
	
	private static bool set = false;
	
	// Use this for initialization
	void Start () {
		if (!set){
			GameObject.Find("Player").GetComponent<PlayerScript>().Popup("After a nerve-racking day of hiding in the church, \nyou wake up, ready for the next leg of your journey north.\n\"Head north, find the shop with two lanterns in front of it\".\nYou must get north to freedom.",-1);
			set = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
