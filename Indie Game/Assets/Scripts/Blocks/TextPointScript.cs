using UnityEngine;
using System.Collections;

public class TextPointScript : MonoBehaviour {

	TextAreaScript area;
	public string TextToDisplay;
	public float TimeToDisplayText;

	// Use this for initialization
	void Start () {
		area = GameObject.Find("Hud").GetComponent<TextAreaScript>();
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
			if (TimeToDisplayText > 0f)
				area.SetText(TextToDisplay, TimeToDisplayText);
			else area.SetText(TextToDisplay);
        }
    }
}
