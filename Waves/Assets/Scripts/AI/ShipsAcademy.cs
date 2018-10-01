using UnityEngine;
using MLAgents;

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

		foreach( Goal g in FindObjectsOfType<Goal>() )
		{
			g.ResetGoal();
		}
	}

	public override void AcademyStep()
	{

	}
}
