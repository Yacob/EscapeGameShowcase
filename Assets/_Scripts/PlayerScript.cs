using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float MaxSpeed = 4;
	public Vector3 movement;

	void Update () {
		move();
	}
	
	void move(){
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
			temp.y -= .01f;
			rigidbody.velocity = temp;			
		}
		else if(rigidbody.velocity.y < 0){
			temp.y += .01f;
			rigidbody.velocity = temp;	
		}
		
		if(rigidbody.velocity.x > 0){
			temp.x -= .01f;	
		}
		else if(rigidbody.velocity.x < 0){
			temp.x += .01f;
		}
		return temp;
	}
}
