using UnityEngine;
using System.Collections;

public class TeleOnWin : MonoBehaviour {
    public int TeleOnVal;
    int currentVal = 0;
   public GameObject Teleporter;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
        if (currentVal == TeleOnVal)
        {
            Instantiate(Teleporter, transform.position, transform.rotation);
            currentVal++;
        }
	
	}
    public void score()
    {
        currentVal += 1;
    }
}
