using UnityEngine;
using System.Collections;

public class Rotato : MonoBehaviour {

	// Use this for initialization
	void Start () {
        iTween.RotateBy(gameObject, iTween.Hash("x", 1, "easeType", "easeInOutBack", "loopType", "loop", "delay", .4));
    }

}
	
