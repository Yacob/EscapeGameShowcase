using UnityEngine;
using System.Collections;

	public class Enemy : MonoBehaviour {
		public Vector3 start_pos;
		public Vector3 dest;
		public float wander_radius = 5;
		public float speed = 1;
		
		protected void Start(){	
			start_pos = this.gameObject.transform.position;
			GenerateDest ();
		}
		
		protected void GenerateDest () {
			Vector2 d = Random.insideUnitCircle*wander_radius;
			dest = start_pos;
			dest.x += d.x;
			dest.y += d.y;
		}
		
		protected void MoveToDest(Vector3 dest, float t) {
			Vector3 p = this.gameObject.transform.position;
			if (Vector3.Distance(dest,p)<speed*t){
				this.gameObject.transform.position = dest;
			} else {
				Vector3 d = Vector3.Normalize(dest - p);
				this.gameObject.transform.position = p + d*speed*t;
			}
			
		}
}
