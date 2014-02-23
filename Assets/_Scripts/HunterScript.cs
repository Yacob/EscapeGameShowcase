using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HunterScript : Enemy {

	public float viewAngle;
	public float viewRadius;
	
	public List<Vector3> patrolLocations;
	
	public Material lineMaterial;
	private int curPatrolLoc = 0;

	private LineRenderer line;

	private Vector3 homeLoc;

	// Use this for initialization
	void Start () {
		line = this.gameObject.AddComponent<LineRenderer>();
		StartPatrol();
	}
	
	// Update is called once per frame
	void Update () {
		DrawCone();
		GameObject player = GameObject.Find ("Player");
		Vector3 PlayerPos = player.transform.position;
		Vector3 curPos = transform.position;
		float playerDist = Vector3.Distance (PlayerPos, curPos);
		GameObject barkingDoge = null;
		GameObject[] doges = GameObject.FindGameObjectsWithTag ("doge");
		foreach(GameObject doge in doges){
			DogeController dogescript = doge.GetComponent<DogeController>();
			Vector3 dogePos = doge.transform.position;
			if(dogescript.barking){
				if(barkingDoge == null)
					barkingDoge = doge;
				else if(Vector3.Distance(barkingDoge.transform.position, curPos) > Vector3.Distance(dogePos, curPos))
					barkingDoge = doge;
			}
		}
		if (playerDist <= viewRadius) {
			float playerAngle = Vector3.Angle (PlayerPos, curPos);
			Debug.Log(playerAngle);
			if(Mathf.Abs(playerAngle) <= viewAngle){
				Chase(player);
			}
		}
		else if(barkingDoge != null){
			Chase(barkingDoge);
		}
		else{
			Patrol();
		}
	}

	void Patrol(){
		if (patrolLocations.Count>0){
			if(transform.position == patrolLocations[curPatrolLoc]){
				curPatrolLoc++;
				if(curPatrolLoc >= patrolLocations.Count)
					curPatrolLoc = 0;
			}
			MoveToDest(patrolLocations[curPatrolLoc]);
		}
	}
	
	void StartPatrol(){
		if (patrolLocations.Count>0){
			MoveToDest (patrolLocations[curPatrolLoc]);
		}
	}

	void Chase(GameObject target){
		MoveToDest(target.transform.position, Time.deltaTime);
	}

	void DrawCone(){
		float width = Mathf.Tan((0.5f) * viewAngle*(Mathf.PI/180)) * viewRadius;
		line.SetWidth(0, width);
		line.SetVertexCount(2);
		line.material = lineMaterial;
		line.renderer.enabled = true;
		line.SetPosition(0, transform.position);
		line.SetPosition(1, transform.position + transform.forward * viewRadius);
	}

}
