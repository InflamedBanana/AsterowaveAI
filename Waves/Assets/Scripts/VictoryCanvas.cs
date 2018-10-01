using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public enum TeamSide
{
	Hippies,
	Army
}

public class VictoryCanvas : MonoBehaviourSingleton<VictoryCanvas>
{
	[SerializeField] Text hippiesText;
	[SerializeField] Text armyText;
	[SerializeField] Text victoryText;

	[SerializeField] Animator animator;

	public void Show ( TeamSide winner, int winnerLives, int maxLives )
	{
		if ( winner == TeamSide.Hippies )
		{
			hippiesText.text = winnerLives.ToString ( "00" ) + " / " + maxLives.ToString ( "00" );
			armyText.text = "00 / " + maxLives.ToString ( "00" );
			victoryText.text = "The Hippies Won !";
		}
		else
		{
			armyText.text = winnerLives.ToString ( "00" ) + " / " + maxLives.ToString ( "00" );
			hippiesText.text = "00 / " + maxLives.ToString ( "00" );
			victoryText.text = "The Cops Won !";
		}

		animator.SetTrigger ( "Go" );
//		Time.timeScale = 0f;
	}

	public void LoadMainMenu ()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene ( "Menu" );
	}
}