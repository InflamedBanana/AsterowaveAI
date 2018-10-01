using UnityEngine;
using System.Collections.Generic;

public static class AngleTools
{
	/// <summary>
	/// Return the average angle of the given list (-180 to 180)
	/// </summary>
	public static float AverageAngleByVector ( List <float> angles )
	{
		Vector3 forward = new Vector3 ( 0f, 0f, 1f );

		List <Vector3> vectors = new List <Vector3> ();

		for ( int i = 0; i < angles.Count; i++ )
		{
			Quaternion angle = Quaternion.Euler ( new Vector3 ( 0f, angles [i], 0f ) );

			vectors.Add ( angle * forward );
		}

		Vector3 vAverage = Vector3.zero;

		for ( int i = 0; i < vectors.Count; i++ )
			vAverage += vectors [i];

		int sign = 1;

		if ( vAverage.x < 0 )
			sign = -1;

		return Vector3.Angle ( forward, ( vAverage / vectors.Count ) ) * sign;
	}
}