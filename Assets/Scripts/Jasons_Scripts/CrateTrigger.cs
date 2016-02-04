using UnityEngine;
using System.Collections;

public class CrateTrigger : MonoBehaviour {

	private GameObject audio;
	private bool alreadyTriggered = false;
    public GameObject teleporter;

	void Start() {
		audio = GameObject.Find ("Crate Placed Voice Over");
	}

	void OnTriggerEnter(Collider other) {
		GameObject go = other.gameObject;

		if (!alreadyTriggered && go.name.Equals("Crate")) {
			audio.GetComponent<AudioSource> ().Play ();
			alreadyTriggered = true;
            Instantiate(teleporter,new Vector3(0,0,0),transform.rotation);
		}
	}
}