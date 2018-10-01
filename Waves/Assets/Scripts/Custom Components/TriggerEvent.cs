using UnityEngine;
using UnityEngine.Events;
using System;

public class TriggerEvent : MonoBehaviour
{
	Action onTriggerEnterEmpty;
	Action<Collider> onTriggerEnterArg;
	[SerializeField] UnityEvent onTriggerEnterEvent;

	Action onTriggerExitEmpty;
	Action<Collider> onTriggerExitArg;
	[SerializeField] UnityEvent onTriggerExitEvent;

	void OnTriggerEnter ( Collider other )
	{
		if ( onTriggerEnterEmpty != null )
			onTriggerEnterEmpty ();

		if ( onTriggerEnterArg != null )
			onTriggerEnterArg ( other );

		onTriggerEnterEvent.Invoke ();
	}

	void OnTriggerExit ( Collider other )
	{
		if ( onTriggerExitEmpty != null )
			onTriggerExitEmpty ();

		if ( onTriggerExitArg != null )
			onTriggerExitArg ( other );
		
		onTriggerExitEvent.Invoke ();
	}

	#region Getter / Setter

	public Action OnTriggerEnterEmpty
	{
		get
		{
			return this.onTriggerEnterEmpty;
		}
		set
		{
			onTriggerEnterEmpty = value;
		}
	}

	public Action<Collider> OnTriggerEnterArg
	{
		get
		{
			return this.onTriggerEnterArg;
		}
		set
		{
			onTriggerEnterArg = value;
		}
	}

	public Action OnTriggerExitEmpty
	{
		get
		{
			return this.onTriggerExitEmpty;
		}
		set
		{
			onTriggerExitEmpty = value;
		}
	}

	public Action<Collider> OnTriggerExitArg
	{
		get
		{
			return this.onTriggerExitArg;
		}
		set
		{
			onTriggerExitArg = value;
		}
	}

	#endregion
}