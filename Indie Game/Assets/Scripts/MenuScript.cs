using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

private FMOD.Studio.EventInstance Menu;
private FMOD.Studio.ParameterInstance OptionsMuch;
private FMOD.Studio.ParameterInstance StartGame;
public AudioSource SelectSound;
private bool _start = false;
private float _startTimer = 0f;

	// Use this for initialization
	void Start () {
	Menu = FMOD_StudioSystem.instance.GetEvent ("event:/Menu");
		Menu.start ();
	Menu.getParameter ("OptionsMuch", out OptionsMuch);
	Menu.getParameter ("StartGame", out StartGame);
		Time.timeScale = 1.0f;
	}

public void StartTheGame() {
		StartGame.setValue (1);
		_startTimer = 5f;
		_start = true;
	
	} 
public void ToOptionsAndBeyond() {
		OptionsMuch.setValue (1);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

public void MainCena(){
		OptionsMuch.setValue (0);
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Vertical"))
			SelectSound.Play ();
	if (_start) {
			_startTimer -= Time.deltaTime;
			if (_startTimer<0f)
				Application.LoadLevel(1);
		}
	
	}
}