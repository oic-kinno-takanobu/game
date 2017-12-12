using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class CharBase : MonoBehaviour
{

	protected int hp;
	protected int sp;
	protected int atk;
	protected int def;
	protected float speed = 10;

	protected Animator charAnimator;
	protected Rigidbody charGravity;

	protected void init ()
	{
		charAnimator = GetComponent <Animator> ();
		charGravity = GetComponent <Rigidbody> ();
	}
}
