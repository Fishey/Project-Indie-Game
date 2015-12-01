	using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HudScript : MonoBehaviour {

	public Image ShurikenImage;
	public Image HealthImage;
	public Image EnergyImage;
	public GameObject PauseMenu;
	public GameObject Hud;
	public Button Resume;
	private FMOD.Studio.EventInstance Tutorial;
	public bool Music;
	private FMOD.Studio.Bus bus;

	void Start () {
	if (Music)
        {
            FMOD.Studio.System system = FMOD_StudioSystem.instance.System;
            System.Guid guid;
            system.lookupID("bus:/", out guid);
            system.getBusByID(guid, out bus);
        }
		Tutorial = FMOD_StudioSystem.instance.GetEvent ("event:/Tutorial");
	}
	void Update (){
		PausingMenu ();
	}

	public void ChangeShurikens(int max, int current)
	{
		ShurikenImage.fillAmount = (float)current/max;
	}

	public void ChangeHealth(float max, float current)
	{
		HealthImage.fillAmount = (float)current/max;
	}

	public void ChangeEnergy(float max, float current)
	{
		EnergyImage.fillAmount = (float)current/max;
	}
	
	public void PausingMenu()
	{
		if (Input.GetButtonDown ("Cancel")) {
			if (PauseMenu.activeInHierarchy == false){
				Time.timeScale = 0;
			if (PauseMenu){
				PauseMenu.SetActive (true);
					Hud.SetActive(false);
				Resume.Select ();
				}
				}
			else {
				PauseMenu.SetActive (false);
				Hud.SetActive(true);
				Time.timeScale = 2f;
			}
		}
	}

	public void MenuPause()
	{
			if (PauseMenu.activeInHierarchy == false){
				Time.timeScale = 0;
				if (PauseMenu){
					PauseMenu.SetActive (true);
					Hud.SetActive(false);
					Resume.Select ();
				}
			}
			else {
				PauseMenu.SetActive (false);
				Hud.SetActive(true);
				Time.timeScale = 2f;
			}
	}
	public void BackToMenu()
	{
		Tutorial.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);	
		Application.LoadLevel(0);
	}

	public void ReadingScroll(bool reading)
	{
		if (reading)
			Tutorial.setParameterValue("Voice-over", 1f);
		else 
			Tutorial.setParameterValue("Voice-over", 0f);
	}
	
	public void VolumeControl (float vol) {
		AudioListener.volume = vol;
        if (Music)
		    bus.setFaderLevel (vol);
	}
}
