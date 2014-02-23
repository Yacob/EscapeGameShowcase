using UnityEngine;
using System.Collections;

public class DogeController : Enemy {
	
	public bool barking = false;
	public float scent_range_2 = 25;
	public bool has_crumb = false;
	public GameObject crumb_to_follow;
	public AudioSource barkingSounds;
	public float timer = 5;
	// Use this for initialization
	void Start () {
		start_pos = this.gameObject.transform.position;
		GenerateDest ();
		barkingSounds = gameObject.GetComponent<AudioSource>();
		barkingSounds.loop = true;
		barkingSounds.volume = 10;
		barkingSounds.Stop();
		
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
		barking = crumb_to_follow;
		Debug.Log(barking);
		if(barking && !barkingSounds.isPlaying){
			barkingSounds.Play();
			timer = 5;
		}
		else if(barkingSounds.isPlaying){
			timer -= Time.deltaTime;
			if(timer <= 0){
				barkingSounds.Stop();
			}						
		}
		CheckForCrumbs();
		MoveToDest(has_crumb ? crumb_to_follow.transform.position:dest);
		if (Vector3.Distance(this.transform.position,dest)==0){
			GenerateDest();
		}	
	}
}
