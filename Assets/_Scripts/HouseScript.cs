using UnityEngine;
using System.Collections;

public class HouseScript : MonoBehaviour {

	public bool safe_house = false;
	public string text= "";
	public int next_level= 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision coll){
		if (!coll.gameObject.name.Equals("Player")) return;
		if (safe_house){
			coll.gameObject.GetComponent<PlayerScript>().Popup(text.Replace("\\n", "\n"), next_level);
		} else {
			GameObject.Find("Player").GetComponent<PlayerScript>().Popup("This house was not safe.\nYou were caught and returned to the plantation.", Application.loadedLevel);
		}
	}
}
