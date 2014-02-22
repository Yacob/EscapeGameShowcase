using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float MaxSpeed = 4;
	public Vector3 movement;
	public Transform breadCrumb;

	void Update () {
		Move();
		DropBreadCrumb();
	}
	
	void DropBreadCrumb(){
		Transform crumb;
		crumb = (Transform)Instantiate(breadCrumb, transform.position, Quaternion.identity);
		crumb.name = string.Format("({0}, {1})", transform.position.x, transform.position.y);
		crumb.parent = transform;
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
			temp.y -= .02f;
			rigidbody.velocity = temp;			
		}
		else if(rigidbody.velocity.y < 0){
			temp.y += .02f;
			rigidbody.velocity = temp;	
		}
		
		if(rigidbody.velocity.x > 0){
			temp.x -= .02f;	
		}
		else if(rigidbody.velocity.x < 0){
			temp.x += .02f;
		}
		return temp;
	}
}
