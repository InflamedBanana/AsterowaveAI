using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
	string selectedLevel = "Game";

	void Start ()
	{
		PlayerPrefs.SetInt ( "PlayersCount", 2 );
	}

	public void SelectLevel ( string levelToLoad )
	{
		selectedLevel = levelToLoad;
	}

	public void Load ()
	{
		SceneManager.LoadScene ( selectedLevel );
	}

	public void SelectPlayersCount ( bool four )
	{
		if ( four )
			PlayerPrefs.SetInt ( "PlayersCount", 4 );
		else
			PlayerPrefs.SetInt ( "PlayersCount", 2 );
	}
}