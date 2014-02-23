using UnityEngine;
using System.Collections;

	public class Enemy : MonoBehaviour {
		public Vector3 start_pos;
		public Vector3 dest;
		public float wander_radius = 5;
		public float speed = 2.5f;
		public float angular_speed = 360;
				
	
	void Start () {
		start_pos = this.gameObject.transform.position;
		GenerateDest ();
	}
		
		protected void GenerateDest () {
			Vector2 d = Random.insideUnitCircle*wander_radius;
			dest = start_pos;
			dest.x += d.x;
			dest.y += d.y;
		}
		
		protected void MoveToDest(Vector3 dest) {
			float t = Time.deltaTime;
			Vector3 p = this.gameObject.transform.position;
			Vector3 dir = this.gameObject.transform.forward;
			Vector3 d = dest - p;
			float angle = Mathf.Sign(Vector3.Dot (d,this.gameObject.transform.right))*Vector3.Angle(dir,d);
			Vector3 a = new Vector3(0,0,0);
			if (Mathf.Abs(angle)<angular_speed*t){
				a.y = angle;
				this.gameObject.transform.Rotate(a);
			} else {
				a.y = Mathf.Sign(angle)*angular_speed*t;
				this.gameObject.transform.Rotate(a);
			}
			if (Vector3.Distance(dest,p)<speed*t){
				this.gameObject.transform.position = dest;
			} else {
				this.gameObject.transform.position = p + this.gameObject.transform.forward*speed*t;
			}
			
		}
}
