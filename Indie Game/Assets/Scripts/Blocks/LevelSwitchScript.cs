using UnityEngine;
using System.Collections;

public class LevelSwitchScript : MonoBehaviour {

	Transform PlayerPosition;
	Transform NextLevelPosition;

	TextAreaScript textScript;

	int _nextLevelNumber;

	// Use this for initialization
	void Start () {
		textScript = GameObject.Find("Hud").GetComponent<TextAreaScript>();

		PlayerPosition = GameObject.Find("Player").transform;
		_nextLevelNumber = int.Parse(transform.parent.name.Substring(transform.parent.name.Length-1)) + 1;

		if (GameObject.Find("Level" + (_nextLevelNumber)).transform.FindChild("PlayerStartPoint").transform)
			NextLevelPosition = GameObject.Find("Level" + (_nextLevelNumber)).transform.FindChild("PlayerStartPoint").transform;
	}

	void OnTriggerEnter(Collider col)
	{
		PlayerPosition.GetComponent<PlayerClass>().ResetStats();
		PlayerPosition.position = NextLevelPosition.position;
		textScript.SetText("Entering level " + _nextLevelNumber);
		StartCoroutine("DisableCurrentLevel");
	}


	IEnumerator DisableCurrentLevel() // Disable the level so that old enemies don't take up unnecessary performance
	{
		yield return new WaitForSeconds(10f);
		GameObject.Find("Level" + (_nextLevelNumber-1)).SetActive(false);
	}

}
