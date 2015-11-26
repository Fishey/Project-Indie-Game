using UnityEngine;
using System.Collections;

public class CheckpointScript : MonoBehaviour {

    PlayerClass lastPlayer;
    Vector3 pos;

	// Use this for initialization
	void Start () {
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            lastPlayer = collider.transform.GetComponent<PlayerClass>();
            if (lastPlayer != null)
            {
                lastPlayer.saveCheckpoint(pos);
            }
        }
    }
}
