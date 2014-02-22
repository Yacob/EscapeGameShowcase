using UnityEngine;
using System.Collections;

public class SlaveHunter : MonoBehaviour {

	public float viewAngle;
	public float viewRadius;

	public float moveSpd;
	public float turnSpd;
	
	public float patrolTime;
	public float patrolDir;

	private float curTime;
	private float curDir;
	private float turnTime;
	private bool isPatrol = true;
	
	private Vector3 homeLoc;

	// Use this for initialization
	void Start () {
		StartPatrol();
	}
	
	// Update is called once per frame
	void Update () {
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
		this.transform.position += this.transform.forward * moveSpd*Time.deltaTime;
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

}
