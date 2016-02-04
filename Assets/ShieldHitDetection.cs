using UnityEngine;
using System.Collections;

public class ShieldHitDetection : MonoBehaviour {

	public int numberOfHits = 0;
	public bool wallEvent = false;
	public float wallSpeed; 


	
	// Update is called once per frame
	void Update () {

		if (wallEvent) {
				
				float step = wallSpeed * Time.deltaTime;
				GameObject wallToRemove = GameObject.Find ("Wall_to_remove");
				Vector3 targetLocation = new Vector3 (wallToRemove.transform.position.x, wallToRemove.transform.position.y - 50, wallToRemove.transform.position.z);
				wallToRemove.transform.position = Vector3.MoveTowards (wallToRemove.transform.position, targetLocation, step);
			}
	
	}

	void OnCollisionEnter(Collision col){

		numberOfHits++;
		if (numberOfHits > 15) {
			wallEvent = true;
		}
	}

}
