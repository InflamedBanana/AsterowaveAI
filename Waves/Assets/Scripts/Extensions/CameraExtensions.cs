using UnityEngine;

public static class CameraExtensions
{
	/// <summary>
	/// Gets the focused point of the Camera.
	/// </summary>
	public static Vector3 GetFocusedPoint ( this Camera c, float maxFocusedDistance = 1000f, int LayerMask = -1, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore )
	{
		RaycastHit hit;
		Ray ray = new Ray ( c.transform.position, c.transform.forward );
		Physics.Raycast ( ray, out hit, maxFocusedDistance, LayerMask, triggerInteraction );

		if ( hit.transform != null )
			return hit.point;
		else
			return Camera.main.ScreenToWorldPoint ( new Vector3 ( Screen.width / 2, Screen.height / 2, maxFocusedDistance ) );
	}

	/// <summary>
	/// Check if the camera has a focused point
	/// </summary>
	public static bool HasFocusedPoint ( this Camera c, out Vector3 pointPos, float maxFocusedDistance = 1000f, int LayerMask = -1, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore )
	{
		RaycastHit hit;
		Ray ray = new Ray ( c.transform.position, c.transform.forward );
		Physics.Raycast ( ray, out hit, maxFocusedDistance, LayerMask, triggerInteraction );

		if ( hit.transform != null )
		{
			pointPos = hit.point;
			return true;
		}
		else
		{
			pointPos = Camera.main.ScreenToWorldPoint ( new Vector3 ( Screen.width / 2, Screen.height / 2, maxFocusedDistance ) );			
			return false;
		}
	}
}