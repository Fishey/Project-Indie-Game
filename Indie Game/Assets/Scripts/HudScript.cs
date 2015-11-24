	using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HudScript : MonoBehaviour {

	public Image ShurikenImage;
	public Image HealthImage;
	public Image EnergyImage;

	// Use this for initialization
	void Start () {
	
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
}
