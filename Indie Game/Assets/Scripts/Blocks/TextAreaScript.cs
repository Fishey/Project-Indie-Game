using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextAreaScript : MonoBehaviour {

    
	Text CurrentTextDisplayed;
	HudScript _hud;
	public Image BackgroundImage;

	public float Timer = 0f;

	// Use this for initialization
	void Start () {
		if (!CurrentTextDisplayed)
			CurrentTextDisplayed = GameObject.Find("Text").GetComponent<Text>();
		_hud = GameObject.Find("Hud").GetComponent<HudScript>();
	}

	void Update()
	{
		if (Timer > 0f)
			Timer -= Time.deltaTime;
		else {CurrentTextDisplayed.text = "";
			BackgroundImage.enabled = false;
			_hud.ReadingScroll(false);
		}
	}

	public void SetText(string text)
	{
		FMOD_StudioSystem.instance.PlayOneShot("event:/TheySeeMeScrolling", transform.position);
		_hud.ReadingScroll(true);
		BackgroundImage.enabled = true;
		CurrentTextDisplayed.text = text;
		Timer = 5f;
	}

	public void SetText(string text, float time)
	{
		FMOD_StudioSystem.instance.PlayOneShot("event:/TheySeeMeScrolling", transform.position);
		_hud.ReadingScroll(true);
		BackgroundImage.enabled = true;
		CurrentTextDisplayed.text = text;
		Timer = time;
	}
	
    
}
