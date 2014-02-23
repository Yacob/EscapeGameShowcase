using UnityEngine;
using System.Collections;

public class DogeController : Enemy {
	
	public bool barking = false;
	public float scent_range_2 = 25;
	public bool has_crumb = false;
	public GameObject crumb_to_follow;

	// Use this for initialization
	void Start () {
		start_pos = this.gameObject.transform.position;
		GenerateDest ();
	}

	void CheckForCrumbs() {
		GameObject [] crumbs = GameObject.FindGameObjectsWithTag ("breadcrumb");
		float max_crumb_time = 0;
		has_crumb = false;
		foreach (GameObject crumb in crumbs) {
			if (Mathf.Pow(crumb.transform.position.x - this.gameObject.transform.position.x,2)+Mathf.Pow(crumb.transform.position.y - this.gameObject.transform.position.y,2)<scent_range_2){
				float lt = crumb.GetComponent<BreadcrumbScript>().lifetime;
				if ( lt > max_crumb_time || !has_crumb){
					crumb_to_follow = crumb;
					max_crumb_time = lt;
					has_crumb = true;
				}
			}
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
