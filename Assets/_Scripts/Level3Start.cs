using UnityEngine;
using System.Collections;

public class Level3Start : MonoBehaviour {
	
	private static bool set = false;
	
	// Use this for initialization
	void Start () {
		if (!set){
			GameObject.Find("Player").GetComponent<PlayerScript>().Popup("You're close to freedom now!\nUnfortunately, the slavers suspect you're nearby,\nThey've set up patrols to find you before you cross the Ohio River to safety.\n",-1);
			set = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
