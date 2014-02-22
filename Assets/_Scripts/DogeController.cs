using UnityEngine;
using System.Collections;

public class DogeController : MonoBehaviour {
	
	public bool barking = false;
	public float scent_range = 5;
	public float speed = 1;
	public bool has_crumb = false;
	public GameObject crumb_to_follow;
	
	public Vector3 start_pos;
	public Vector3 dest;
	public float wander_radius = 5;

	// Use this for initialization
	void Start () {
		start_pos = this.gameObject.transform.position;
		GenerateDest ();
	}
	
	void GenerateDest () {
		Vector2 d = Random.insideUnitCircle*wander_radius;
		dest = start_pos;
		dest.x += d.x;
		dest.y += d.y;
	}

	void CheckForCrumbs() {
		GameObject [] crumbs = GameObject.FindGameObjectsWithTag ("breadcrumb");
		float max_crumb_time = 0;
		has_crumb = false;
		Debug.Log(crumbs.Length);
		foreach (GameObject crumb in crumbs) {
			if (Vector3.Distance(crumb.transform.position, this.gameObject.transform.position)<scent_range){
				float lt = crumb.GetComponent<BreadcrumbScript>().lifetime;
				if ( lt > max_crumb_time || !has_crumb){
					crumb_to_follow = crumb;
					max_crumb_time = lt;
					has_crumb = true;
				}
			}
		}
	}
	
	void MoveToDest(Vector3 dest, float t) {
		Vector3 p = this.gameObject.transform.position;
		if (Vector3.Distance(dest,p)<speed*t){
			this.gameObject.transform.position = dest;
		} else {
			Vector3 d = Vector3.Normalize(dest - p);
			this.gameObject.transform.position = p + d*speed*t;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckForCrumbs();
		MoveToDest(has_crumb ? crumb_to_follow.transform.position:dest, Time.deltaTime);
		if (Vector3.Distance(this.transform.position,dest)==0)
			GenerateDest();
	}
}
