using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float MaxSpeed = 4;
	public Vector3 movement;
	public Transform breadCrumb;
	public Transform CrumbParent;
	public bool inWater = false;
	public float drop_rate = 0.1f;
	public float till_drop = 0.1f;
	private bool paused = false;
	private string pop_text = "";
	private int t_level = 0;

	public void Popup(string text, int level){
		Time.timeScale = 0;
		paused = true;
		pop_text = text+"\nPress space to continue.";
		t_level = level;
	}

	void OnGUI() {
		if (paused)
			GUI.Box (new Rect (Screen.width/10, Screen.height/10, Screen.width*8/10, Screen.height*8/10), pop_text);
	}

	void Update () {
		if (paused && Input.GetKeyDown(KeyCode.Space)){
			Time.timeScale = 1;
			paused = false;
			Application.LoadLevel(t_level);
		}
		Move();
		if(!inWater && till_drop <= 0){
			DropBreadCrumb();
			till_drop = drop_rate;
		} else {
			till_drop-=Time.deltaTime;
		}
	}
	
	void DropBreadCrumb(){
		Transform crumb;
		crumb = (Transform)Instantiate(breadCrumb, transform.position, Quaternion.identity);
		crumb.name = string.Format("({0}, {1})", transform.position.x, transform.position.y);
		crumb.parent = CrumbParent;
	}
	
	
	void Move(){
		movement = decelerate();
		rigidbody.velocity = new Vector3(0, 0, 0);
	
		if(Input.GetKey(KeyCode.UpArrow)) {
			Vector3 temp = new Vector3(0, .1f, 0);
			movement += temp;
		}
		if(Input.GetKey(KeyCode.DownArrow)) {
			Vector3 temp = new Vector3(0, -0.1f, 0);
			movement += temp;
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			Vector3 temp = new Vector3(.1f, 0, 0);
			movement += temp;
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			Vector3 temp = new Vector3(-0.1f, 0, 0);
			movement += temp;
		}
		if(movement.x > MaxSpeed){
			movement.x = MaxSpeed;
		}
		else if(movement.x < -MaxSpeed){
			movement.x = -MaxSpeed;
		}
		if(movement.y > MaxSpeed){
			movement.y = MaxSpeed;
		}
		else if(movement.y < -MaxSpeed){
			movement.y = -MaxSpeed;	
		}
		rigidbody.velocity = movement;
	}
	
	Vector3 decelerate(){
		Vector3 temp = rigidbody.velocity;
		if(rigidbody.velocity.y > 0){
			temp.y -= .03f;
			rigidbody.velocity = temp;			
		}
		else if(rigidbody.velocity.y < 0){
			temp.y += .03f;
			rigidbody.velocity = temp;	
		}
		
		if(rigidbody.velocity.x > 0){
			temp.x -= .03f;	
		}
		else if(rigidbody.velocity.x < 0){
			temp.x += .03f;
		}
		return temp;
	}
}
