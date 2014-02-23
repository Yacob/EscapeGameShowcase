using UnityEngine;
using System.Collections;

public class HunterScript : MonoBehaviour {

	public float viewAngle;
	public float viewRadius;

	public float moveSpd;
	public float turnSpd;
	
	public float patrolTime;
	public float patrolDir;

	public Material lineMaterial;

	private float curTime;
	private float curDir;
	private float turnTime;
	private bool isPatrol = true;

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
			if(Mathf.Abs(playerAngle) <= viewAngle){
				Chase(player);
			}
		}
		else if(barkingDoge != null){
			Chase (barkingDoge);
		}
		else{
			Patrol();
		}
	}

	void Patrol(){
		if(!isPatrol){
			StartPatrol();
		}		
		//this.transform.position += this.transform.forward * moveSpd*Time.deltaTime;
		curTime -= Time.deltaTime;
		if(curTime <= 0){
			curDir *= -1;
			curTime = patrolTime;
		}
	}
	
	void StartPatrol(){
			
	}
	public IEnumerator PatrolTurn(){
		Transform target = this.transform.GetChild(0);
		Vector3 dir = target.position - transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(dir);
	
		while(Quaternion.Angle(transform.rotation, targetRotation) >.01){
			Quaternion old = transform.rotation;
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnTime);
			turnTime += .01f;
			yield return(new WaitForSeconds(Time.deltaTime));
		}
		turnTime = 0;
		
	}

	void Chase(GameObject target){
		Vector3 dir = target.transform.position - transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(dir);
		Quaternion old = transform.rotation;
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnTime);
		turnTime += .01f;
		this.transform.position += this.transform.forward * moveSpd*Time.deltaTime;
	}

	void DrawCone(){
		float width = Mathf.Tan(1/2 * viewAngle*Mathf.PI/180) * viewRadius;
		line.SetWidth(0, width);
		line.SetVertexCount(2);
		line.material = lineMaterial;
		line.renderer.enabled = true;
		line.SetPosition(0, transform.position);
		line.SetPosition(1, transform.position + transform.forward * viewRadius);
		Debug.Log("done drawing");
	}

}
