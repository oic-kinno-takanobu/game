using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class PlayerBase : CharBase
{
	[SerializeField]
	protected GameObject magicObj;

	protected float inputTime = 0.4f;
	protected float maxMagicIntervalTime = 0.2f;
	protected float magicIntervalTime;


	protected int maxCombo = 3;
	protected int combo = 0;

	protected bool attackFlg = false;
	protected bool inputFlg = false;
	protected bool jumpFlg = false;

	// Use this for initialization
	void Start ()
	{
		init ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		Move ();
		Jump ();
		if (attackFlg) {
			ComboCheck ();
		} else {
			Attack ();
		}
		MagicAttack ();
	}

	/// <summary>
	/// 移動処理
	/// Move this instance.
	/// </summary>
	protected void Move ()
	{
		float hori = Input.GetAxis ("Horizontal");
		if (hori == 0) {
			charAnimator.SetBool ("Run", false);
			return;
		}

		float moveSpeed = hori * speed * Time.deltaTime;
		charAnimator.SetBool ("Run", true);

		if (hori > 0) {
			transform.eulerAngles = new Vector3 (0, 90, 0);
			transform.localPosition += new Vector3 (moveSpeed, 0, 0);
		} else if (hori < 0) {
			transform.eulerAngles = new Vector3 (0, 270, 0);
			transform.localPosition += new Vector3 (moveSpeed, 0, 0);
		}

	}

	/// <summary>
	/// ジャンプ処理
	/// Jump this instance.
	/// </summary>
	protected void Jump ()
	{
		jumpFlg = Physics.Raycast (transform.position,
			transform.up * -1, 2.1f);

		Debug.DrawRay (transform.position, transform.up);
		Debug.Log (jumpFlg);

		if (Input.GetKeyDown (KeyCode.Space) && jumpFlg) {
			charGravity.AddForce (0, 750, 0);
		} else if (jumpFlg == false) {
			charGravity.AddForce (0, -20, 0);
		}

	}

	protected void MagicAttack ()
	{
		if (magicIntervalTime > 0) {
			magicIntervalTime -= Time.deltaTime;
		} else if (Input.GetButtonDown ("Fire2")) {
			magicIntervalTime = maxMagicIntervalTime;
			Destroy (Instantiate (magicObj, transform.position, transform.rotation), 5f);
		}

	}

	/// <summary>
	/// 攻撃開始
	/// Attack this instance.
	/// </summary>
	protected void Attack ()
	{
		if (Input.GetButtonDown ("Fire1")) {
			attackFlg = true;
			combo++;
			charAnimator.SetInteger ("combo", combo);
		}
	}

	/// <summary>
	/// コンボ処理
	/// Combos the check.
	/// </summary>
	protected void ComboCheck ()
	{
		AnimatorStateInfo info = charAnimator.GetCurrentAnimatorStateInfo (0);
		float totalTime = info.length * info.speed;
		int nowCommbo = charAnimator.GetInteger ("combo");
		if (Input.GetButtonDown ("Fire1") && !inputFlg) {
			inputFlg = true;
			combo++;
			charAnimator.SetInteger ("combo", combo);
		}
			
		if (info.normalizedTime >= totalTime) {
			if (!inputFlg) {
				combo = 0;
				charAnimator.SetInteger ("combo", combo);
				attackFlg = false;
			}
			inputFlg = false;
		}
	}
}
