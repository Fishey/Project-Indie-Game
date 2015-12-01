using UnityEngine;
using System.Collections;

public class LevelSwitchScript : MonoBehaviour {

	Transform PlayerPosition;
	public Transform NextLevelPosition;

	TextAreaScript textScript;

	int _nextLevelNumber;

	// Use this for initialization
	void Start () {
		textScript = GameObject.Find("Hud").GetComponent<TextAreaScript>();

		PlayerPosition = GameObject.Find("Player").transform;
		_nextLevelNumber = int.Parse(transform.parent.name.Substring(transform.parent.name.Length-1)) + 1;


		if (GameObject.Find("Level" + (_nextLevelNumber)))
			if (GameObject.Find("Level" + (_nextLevelNumber)).transform.GetChild(0).Find("PlayerStartPoint").transform)
				NextLevelPosition = GameObject.Find("Level" + (_nextLevelNumber)).transform.GetChild(0).transform.FindChild("PlayerStartPoint").transform;
	}

	void OnTriggerEnter(Collider col)
	{
		PlayerPosition.GetComponent<PlayerClass>().saveCheckpoint(NextLevelPosition.position);
		PlayerPosition.GetComponent<Rigidbody>().isKinematic = true;

		textScript.SetText("Entering level " + _nextLevelNumber);
		StartCoroutine("DisableCurrentLevel");
	}


	IEnumerator DisableCurrentLevel() // Enable the next level and disable the level so that old enemies don't take up unnecessary performance
	{
		GameObject.Find("Level" + (_nextLevelNumber)).transform.GetChild(0).gameObject.SetActive(true); // Enable next level

		yield return new WaitForSeconds(3f);
		PlayerPosition.position = NextLevelPosition.position;
		PlayerPosition.GetComponent<PlayerClass>().ResetStats();
		yield return new WaitForSeconds(10f);
		GameObject.Find("Level" + (_nextLevelNumber-1)).transform.GetChild(0).gameObject.SetActive(false);
	}

}
