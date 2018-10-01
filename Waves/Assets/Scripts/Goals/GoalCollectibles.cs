using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoalCollectibles : MonoBehaviour
{
	[SerializeField] GoalScriptable tweaks;
	[SerializeField] GameObject shield;

	public bool HasShield
	{
		get
		{
			return shieldCo == null ? false : true;
		}
	}

	public bool IsShrinked
	{
		get
		{
			return shrinkCo == null ? false : true;
		}
	}

	public bool IsGrowthed
	{
		get
		{
			return growCo == null ? false : true;
		}
	}

	public bool IsInverse
	{
		get
		{
			return inverseCo == null ? false : true;
		}
	}

	Coroutine shieldCo, shrinkCo, growCo, inverseCo, attractCo, scaleCo;

	public void GetShield ()
	{
		if ( HasShield )
			return;
		print ( "Shield ON" );
		if (shield != null)
		{
			shield.SetActive(true);

			if (scaleCo != null)
				StopCoroutine(scaleCo);

			scaleCo = StartCoroutine( ScaleUp( shield.transform, new Vector3 ( 1.0f,0.95f,1.0f ), tweaks.ShieldAppearSpeed , true ) );
		}
		
		shieldCo = StartCoroutine ( Timer ( tweaks.ShieldTime, LooseShield ) );
	}

	public void LooseShield ()
	{
		StopCoroutine ( shieldCo );
		shieldCo = null;
		if (shield != null)
		{
			if (scaleCo != null)
				StopCoroutine(scaleCo);

			scaleCo = StartCoroutine(  ScaleUp( shield.transform, Vector3.one / 100f, tweaks.ShieldAppearSpeed * 2, false ) );
		}
		print ( "Shield OFF" );
	}

	public IEnumerator ScaleUp(Transform toScale, Vector3 scaleTarget,float speed, bool enable)
	{
		while(toScale.localScale != scaleTarget)
		{
			toScale.localScale = Vector3.Lerp(toScale.localScale,scaleTarget,speed * Time.deltaTime);
//			Vector3 vect = Vector3.zero;
//			toScale.localScale = Vector3.SmoothDamp(toScale.localScale,scaleTarget,ref vect, speed * Time.deltaTime);
			yield return null;
		}

		toScale.gameObject.SetActive(enable);
	}

	IEnumerator Timer ( float time, Action callBack )
	{
		yield return new WaitForSeconds ( time );
		callBack ();
	}

	public void ShrinkBase ()
	{
		if ( IsGrowthed )
			InverseGrow ();
		
		if ( IsShrinked )
			return;

//		transform.localScale /= tweaks.ShrinkSizeFactor;
		if (shield != null)
		{
			if (scaleCo != null)
				StopCoroutine(scaleCo);
			
			scaleCo = StartCoroutine( ScaleUp( transform, Vector3.one * tweaks.ShrinkSizeFactor, tweaks.GrowShrinkBaseSpeed, true ) );
		}
		print ( "Shrink" );
		shrinkCo = StartCoroutine ( Timer ( tweaks.ShrinkTime, RegrowBase ) );
	}

	public void RegrowBase ()
	{
		StopCoroutine ( shrinkCo );
		shrinkCo = null;

		print ( "Regrow" );
		if (scaleCo != null)
			StopCoroutine(scaleCo);
		
		scaleCo = StartCoroutine( ScaleUp( transform, Vector3.one, tweaks.GrowShrinkBaseSpeed * 2, true ) );
	}

	public void GrowBase ()
	{
		if ( IsShrinked )
			RegrowBase ();
		if ( IsGrowthed )
			return;

		if (scaleCo != null)
			StopCoroutine(scaleCo);
		
		scaleCo = StartCoroutine( ScaleUp( transform, Vector3.one * tweaks.GrowSizeFactor, tweaks.GrowShrinkBaseSpeed, true ) );
//		transform.localScale *= tweaks.GrowSizeFactor;
		print ( "Grow" );
		growCo = StartCoroutine ( Timer ( tweaks.GrowTime, InverseGrow ) );
	}

	public void InverseGrow ()
	{
		StopCoroutine ( growCo );
		growCo = null;

		print ( "InverseGrow" );
		if (scaleCo != null)
			StopCoroutine(scaleCo);

		scaleCo = StartCoroutine( ScaleUp( transform, Vector3.one , tweaks.GrowShrinkBaseSpeed, true ) );
//		transform.localScale /= tweaks.GrowSizeFactor;
	}

	public void InverseInputs ()
	{
		if ( IsInverse )
			return;
		print ( "Inverse Inputs" );

		foreach ( GameObject g in GetComponent<Goal> ().MyPlayers )
			g.GetComponent<PlayerMoves> ().InverseDir ( true );
//		GetComponent<Goal> ().MyPlayer.GetComponent<PlayerMoves> ().InverseDir ( true );

		inverseCo = StartCoroutine ( Timer ( tweaks.InverseInputsTime, ResetInputs ) );
	}

	void ResetInputs ()
	{
		StopCoroutine ( inverseCo );
		inverseCo = null;

		print ( "Reclaim Inputs" );

		foreach ( GameObject g in GetComponent<Goal> ().MyPlayers )
			g.GetComponent<PlayerMoves> ().InverseDir ( false );
//		GetComponent<Goal> ().MyPlayer.GetComponent<PlayerMoves> ().InverseDir ( false );
	}

	//enableattract
	public void EnableAttract ()
	{
		foreach ( GameObject g in GetComponent<Goal> ().MyPlayers )
			g.GetComponent<PlayerAttack> ().EnableAttractObjects ( true );
//		GetComponent<Goal> ().MyPlayer.GetComponent<PlayerAttack> ().EnableAttractObjects ( true );

		attractCo = StartCoroutine ( Timer ( tweaks.AttractAsteroidTime, DisableAttract ) );
	}

	void DisableAttract ()
	{
		StopCoroutine ( attractCo );
		attractCo = null;

		print ( "Reclaim Inputs" );

		foreach ( GameObject g in GetComponent<Goal> ().MyPlayers )
			g.GetComponent<PlayerAttack> ().EnableAttractObjects ( false );
		
//		GetComponent<Goal> ().MyPlayer.GetComponent<PlayerAttack> ().EnableAttractObjects ( false );
	}
}
