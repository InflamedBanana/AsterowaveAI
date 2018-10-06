using MLAgents;
using UnityEngine;

public class ShipsAcademy : Academy
{
	public static ShipsAcademy Instance = null;

	public override void InitializeAcademy()
	{
		base.InitializeAcademy();
		Instance = this;
	}

	public override void AcademyReset()
	{
		JunkSpawner.Instance.CleanJunks();
		//CollectiblesSpawners.Instance.ClearCollectibles();
		Debug.Log( "RESET" );
		foreach( Goal g in FindObjectsOfType<Goal>() )
		{
			g.ResetGoal();
		}

		base.AcademyReset();
	}

	public override void AcademyStep()
	{

	}
}
