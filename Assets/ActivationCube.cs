using UnityEngine;
using System.Collections;

public class ActivationCube : MonoBehaviour {

	public int numberOfActivations = 0;

	public int numberRequired = 0;

	public float wallSpeed;

	
	// Update is called once per frame
	void Update () {

		if (numberOfActivations > numberRequired) {

			float step = wallSpeed * Time.deltaTime;
			GameObject wallToRemove = GameObject.Find ("Wall_to_remove");
			Vector3 targetLocation = new Vector3 (wallToRemove.transform.position.x, wallToRemove.transform.position.y - 50, wallToRemove.transform.position.z);
			wallToRemove.transform.position = Vector3.MoveTowards (wallToRemove.transform.position, targetLocation, step);
		
		}
	
	}

	void OnCollisionEnter(Collision col){

		if (col.gameObject.name == "Basic_Projectile(Clone)") {

			numberOfActivations++;

			if(numberOfActivations == numberRequired/2  || numberOfActivations == numberRequired - 1){

				gameObject.GetComponent<AudioSource>().Play ();

			}

		}


	}
}
