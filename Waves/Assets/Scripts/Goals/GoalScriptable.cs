using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoalTweaks", menuName = "GoalTweaks", order = 1 ) ]
public class GoalScriptable : ScriptableObject 
{
	[SerializeField] float shieldTime;
	[Space]
	[SerializeField] float shrinkSizeFactor;
	[SerializeField] float shrinkTime;
	[SerializeField] float growSizeFactor;
	[SerializeField] float growTime;
	[SerializeField] float growShrinkBaseSpeed;
	[Space]
	[SerializeField] float inverseInputsTime;
	[Space]
	[SerializeField] float attractAsteroidTime;
	[Space]
	[SerializeField] float shieldAppearSpeed;


	public float ShieldTime { get { return shieldTime;}}
	public float ShrinkSizeFactor { get { return shrinkSizeFactor;}}
	public float ShrinkTime { get { return shrinkTime;}}
	public float GrowSizeFactor { get { return growSizeFactor;}}
	public float GrowTime { get { return growTime;}}
	public float InverseInputsTime { get { return inverseInputsTime;}}
	public float AttractAsteroidTime { get { return attractAsteroidTime;}}
	public float ShieldAppearSpeed { get { return shieldAppearSpeed;}}
	public float GrowShrinkBaseSpeed { get { return growShrinkBaseSpeed;}}
}
