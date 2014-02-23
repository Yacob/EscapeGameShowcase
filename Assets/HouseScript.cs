using UnityEngine;
using System.Collections;

public class HouseScript : MonoBehaviour {

	public bool safe_house = false;
	public string text= "";
	public int next_level= 0;
	
	public static int message = 0;
	public static string [] messages = {"Dogs lose track of your scent trail if you move through water.", 
		"Hunting dogs will alert slave catchers if they scent your trail.", 
		"Slave catchers may lose sight of you if you move fast enough.",
		"Less than 2% of slaves ever made it north."};

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
			GameObject.Find("Player").GetComponent<PlayerScript>().Popup("This house was not safe.\nYou were caught and returned to the plantation.\n"+messages[message], Application.loadedLevel);
			message = (message+1)%messages.Length;
		}
	}
}
