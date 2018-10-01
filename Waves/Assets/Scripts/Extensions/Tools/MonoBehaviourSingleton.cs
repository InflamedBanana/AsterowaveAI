using UnityEngine;

public class MonoBehaviourSingleton <T> : MonoBehaviour where T : Component
{
	private static T instance;

	public static T Instance 
	{
		get
		{
			if ( instance == null )
			{
				var objs = FindObjectsOfType <T> ();
				if ( objs.Length == 0 ) 
				{
					GameObject obj = new GameObject ();
					instance = obj.AddComponent <T> ();
					instance.name =  typeof ( T ).Name;
				}
				else if ( objs.Length == 1 )
					instance = objs [ 0 ];
				else				
					Debug.LogError ( "You must have at most one " + typeof ( T ).Name + " in the scene." );
			}
			return instance;
		}
	}

	public virtual void Awake ()
	{
		if ( instance == null )
			instance = this as T;
		else if ( instance != this )
			Destroy ( gameObject );	
	}
}