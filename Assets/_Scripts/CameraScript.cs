//GIU Bar Code found from duck ♦♦ on Unity Answers

using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform 	poi; // Point of Interest
	public Vector3		offset = new Vector3(0,0,-10);
	
	float sprintBarDisplay = 0;
	Vector2 sprintPos = new Vector2(20,40);
	Vector2 sprintSize = new Vector2(60,20);
	public Texture2D sprintProgressBarEmpty;
	public Texture2D sprintProgressBarFull;
	
	float caughtBarDisplay = 0;
	Vector2 caughtPos = new Vector2(20,10);
	Vector2 caughtSize = new Vector2(60,20);
	public Texture2D caughtProgressBarEmpty;
	public Texture2D caughtProgressBarFull;
	
	void Update () {
		Vector3 poiV3 = poi.position;
		poiV3 += offset;
		transform.position = poiV3;
		
		// for this example, the bar display is linked to the current time,
		// however you would set this value based on your desired display
		// eg, the loading progress, the player's health, or whatever.
		sprintBarDisplay = PlayerScript.sprintRemaining;
		sprintBarDisplay = sprintBarDisplay / 5;
		caughtBarDisplay = HunterScript.seen;
		caughtBarDisplay = caughtBarDisplay / 3;
	}
	
	
	void OnGUI()
	{
		
		// draw the background:
		GUI.BeginGroup (new Rect (sprintPos.x, sprintPos.y, sprintSize.x, sprintSize.y));
		GUI.Box (new Rect (0,0, sprintSize.x, sprintSize.y),sprintProgressBarEmpty);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, sprintSize.x * sprintBarDisplay, sprintSize.y));
		GUI.Box (new Rect (0,0, sprintSize.x, sprintSize.y), sprintProgressBarFull);
		GUI.EndGroup ();
		
		GUI.EndGroup ();
		
		// draw the background:
		GUI.BeginGroup (new Rect (caughtPos.x, caughtPos.y, caughtSize.x, caughtSize.y));
		GUI.Box (new Rect (0,0, caughtSize.x, caughtSize.y),caughtProgressBarEmpty);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, caughtSize.x * caughtBarDisplay, caughtSize.y));
		GUI.Box (new Rect (0,0, caughtSize.x, caughtSize.y), caughtProgressBarFull);
		GUI.EndGroup ();
		
		GUI.EndGroup ();
		
	} 
}
