using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBase : MonoBehaviour
{

	protected float moveSpeed = 20f;

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
		Vector3 vector = transform.forward * moveSpeed * Time.deltaTime;

		transform.localPosition += vector;
	}
		
}
