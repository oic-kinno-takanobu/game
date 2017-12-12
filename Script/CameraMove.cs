using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

	[SerializeField]
	private Transform target;
	[SerializeField]
	private float frame;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Move ();
	}

	void Move ()
	{
		Vector3 vector = target.position - transform.localPosition;
		vector.z = 0;
		transform.localPosition += vector / frame;

	}
}
