using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextAreaScript : MonoBehaviour {

    
	Text CurrentTextDisplayed;
	public Image BackgroundImage;

	public float Timer = 0f;

	// Use this for initialization
	void Start () {
		if (!CurrentTextDisplayed)
			CurrentTextDisplayed = GameObject.Find("Text").GetComponent<Text>();
	}

	void Update()
	{
		if (Timer > 0f)
			Timer -= Time.deltaTime;
		else {CurrentTextDisplayed.text = "";
			BackgroundImage.enabled = false;
		}
	}

	public void SetText(string text)
	{
		BackgroundImage.enabled = true;
		CurrentTextDisplayed.text = text;
		Timer = 5f;
	}

	public void SetText(string text, float time)
	{
		BackgroundImage.enabled = true;
		CurrentTextDisplayed.text = text;
		Timer = time;
	}
	
    
}
