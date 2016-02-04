using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashRedDamage : MonoBehaviour {

	bool damaged = false;
	public Color flashColor = new Color (1f, 0f, 0f, 1f);
	public Image damageImage;
	public float flashSpeed = 5f;


	
	// Update is called once per frame
	void Update ()
	{
		// If the player has just been damaged...
		if(damaged)
		{
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColor;
		}
		// Otherwise...
		else
		{
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// Reset the damaged flag.
		damaged = false;
	}

	void OnCollisionEnter(Collision col){

		damaged = true;
	
	}
}
