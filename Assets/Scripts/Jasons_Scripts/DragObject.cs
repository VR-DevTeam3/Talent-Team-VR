using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider))]

public class DragObject : MonoBehaviour 
{
	private Rigidbody rb;
	private Vector3 screenPoint;
	private Vector3 offset;

	public void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	public void OnMouseDrag()
	{
		rb = gameObject.GetComponent<Rigidbody>();

		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)+offset;
		transform.position = curPosition;

		//rb.constraints = RigidbodyConstraints.FreezeRotation;
	}

	public void OnMouseRelease()
	{
		rb = gameObject.GetComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.None;
	}

}