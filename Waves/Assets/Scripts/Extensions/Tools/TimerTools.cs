using UnityEngine;
using System;

public struct Timer
{
	#region Properties

	int hours;
	int minutes;
	int seconds;
	int milliSeconds;

	public Action onObjectiveCompleted;
	bool haveAnObjective;
	float objective;
	bool timerLoop;

	#endregion

	#region Getter / Setter

	public float Hours
	{
		get
		{
			return this.hours;
		}
		set
		{
			hours = (int) value;
			Minutes = ( value - Hours ) * 60;
		}
	}

	public float Minutes
	{
		get
		{
			return this.minutes;
		}
		set
		{

			int newValue = (int) value;

			if ( value >= 60 )
			{
				newValue = (int) ( value % 60 );
				Hours += (int) ( value / 60 );
			}
			else if ( value < 1 )
				Seconds += ( value - newValue ) * 60;

			minutes = newValue;
		}
	}

	public float Seconds
	{
		get
		{
			return this.seconds;
		}
		set
		{
			int newValue = (int) value;

			if ( value >= 60 )
			{
				newValue = (int) ( value % 60 );
				Minutes += (int) ( value / 60 );
			}
			else if ( value < 1 )
				MilliSeconds += ( value - newValue ) * 100;

			seconds = newValue;
		}
	}

	public float MilliSeconds
	{
		get
		{
			return this.milliSeconds;
		}
		set
		{
			int newValue = (int) value;

			if ( value >= 1000 )
			{
				newValue = (int) ( value % 1000 );
				Seconds += (int) ( value / 1000 );
			}

			milliSeconds = newValue;
		}
	}

	#endregion

	#region Function

	/// <summary>
	/// Updates the timer (call this in the Update).
	/// step is in seconds.
	/// </summary>
	public void Update ( float step )
	{ 
		MilliSeconds += (int) ( step * 1000 );

		if ( haveAnObjective )
		if ( this.GetTotalSeconds () >= objective )
		{
			if ( onObjectiveCompleted != null )
				onObjectiveCompleted ();
			
			if ( timerLoop )
				Reset ();
			else
				ResetObjective ();
		}
	}

	/// <summary>
	/// Sets the objective in seconds (you can set a delegate with Timer.onObjectiveCompleted).
	/// </summary>
	public void SetObjectiveInSeconds ( float objectiveInSeconds,
	                                    Action act = null,
	                                    bool resetAfterCompletingObjective = false )
	{
		objective = objectiveInSeconds;
		haveAnObjective = true;
		if ( act != null )
			onObjectiveCompleted += act;

		timerLoop = resetAfterCompletingObjective;
	}

	/// <summary>
	/// Reset this timer.
	/// </summary>
	public void Reset ()
	{
		hours = 0;
		minutes = 0;
		seconds = 0;
		milliSeconds = 0;
	}

	/// <summary>
	/// Reset the objective of this timer.
	/// </summary>
	public void ResetObjective ()
	{
		onObjectiveCompleted = null;
		haveAnObjective = false;
		objective = 0;
		timerLoop = false;
	}

	/// <summary>
	/// Gets the string timer in a basic format.
	/// (MM:SS:mmm)
	/// </summary>
	public string GetStringTimer ()
	{
		return Minutes.ToString ( "00" ) + ":" + Seconds.ToString ( "00" ) + ":" + MilliSeconds.ToString ( "000" );
	}

	/// <summary>
	/// Converts timer to seconds.
	/// </summary>
	public float GetTotalSeconds ()
	{
		float s = 0;
		
		s += Hours * 3600;
		s += Minutes * 60;
		s += Seconds;
		s += MilliSeconds / 1000f;
		
		return s;
	}

	/// <summary>
	/// Converts timer to minutes.
	/// </summary>
	public float GetTotalMinutes ()
	{
		float m = 0;
		
		m += Hours * 60;
		m += Minutes;
		m += Seconds / 60f;
		m += MilliSeconds / 60000f;
		
		return m;
	}

	/// <summary>
	/// Converts timer to hours.
	/// </summary>
	public float GetTotalHours ()
	{
		float h = 0;
		
		h += Hours;
		h += Minutes / 60f;
		h += Seconds / 3600f;
		h += MilliSeconds / 3600000f;
		
		return h;
	}

	#endregion
}



public static class TimerTools
{
	/// <summary>
	/// Converts seconds to timer.
	/// </summary>
	public static Timer FromSecondsToTimer ( this float seconds )
	{
		Timer t = new Timer ();
		
		int intS = (int) seconds;

		t.Hours = (int) ( intS / 3600 );
		t.Minutes = (int) ( intS / 60 % 60 );
		t.Seconds = intS % 60;

		t.MilliSeconds = (int) ( ( seconds - intS ) * 1000 );

		return t;
	}
}