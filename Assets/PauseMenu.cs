using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public Canvas pauseMenu;
	public Canvas quitMenu;
	public Canvas mainScreen;
	public Button resumeGame;
	public Button quitGame;
	public Button pauseGame;
	public Button yesQuit;
	public Button noQuit;
	public Image pauseGame1;


	// Use this for initialization
	void Start () {
		pauseMenu = pauseMenu.GetComponent<Canvas> ();
		quitMenu = quitMenu.GetComponent<Canvas> ();
		resumeGame = resumeGame.GetComponent<Button> ();
		quitGame = quitGame.GetComponent<Button> ();
		pauseMenu.enabled = false;
		quitMenu.enabled = false;
		pauseGame.enabled = true;
		mainScreen.enabled = true;
	}
	
	// Update is called once per frame
	public void Update () {

	}

	public void ClickPause(){
		pauseMenu.enabled = true;
		Time.timeScale = 0.0f;
	}

	public void Resume(){
		pauseMenu.enabled = false;
		Time.timeScale = 1.0f;
	}

	public void Restart() {
		Resume ();
		Application.LoadLevel("main");
	}

	public void ClickQuit(){
		quitMenu.enabled = true;
		pauseMenu.enabled = false;
	}

	public void YesQuit(){
		Application.Quit ();
	}

	public void NoQuit(){
		quitMenu.enabled = false;
		pauseMenu.enabled = true;
	}

}
