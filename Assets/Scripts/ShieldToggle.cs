using UnityEngine;
using System.Collections;

public class ShieldToggle : MonoBehaviour {
	public GameObject targetOfShield;
	public bool shieldAbilityActive = true;
	public bool pressButton = false;

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1")) {

			shieldAbilityActive = !shieldAbilityActive;

		}
		targetOfShield.SetActive (shieldAbilityActive);



	}
}
