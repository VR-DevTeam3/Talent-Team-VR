using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour
{

    // Use this for initialization
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "SplitMetalBall")
        {
            Destroy(col.gameObject);
        }

    }
}
	
